using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerUI playerUI;

    public float distance = 3f;
    public LayerMask mask;
    public Collider[] colliders;

    public Slider progressBar;

    private InputAction holdAction;
    private Interactable curTarget;
    private bool isInteracting;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerUI = GetComponent<PlayerUI>();
        holdAction = playerInput.actions.FindAction("Hold");
    }

    private void OnEnable()
    {
        holdAction.performed += OnHoldPerformed;
        holdAction.canceled += OnHoldCanceled;
    }

    private void OnDisable()
    {
        holdAction.performed -= OnHoldPerformed;
        holdAction.canceled -= OnHoldCanceled;
    }

    private void Update()
    {
        Interactable newTarget = null;

        //Ray ray = new Ray(transform.position, transform.forward * distance);
        //Debug.DrawRay(transform.position, transform.forward * distance, Color.red);
        //if (Physics.Raycast(ray, out RaycastHit hitInfo, distance, mask))
        //{
        //    newTarget = hitInfo.collider.GetComponent<Interactable>();
        //}

        colliders = Physics.OverlapSphere(transform.position, distance, mask);
        if (colliders.Length > 0)
        {
            float shortest = float.MaxValue;
            foreach (Collider target in colliders)
            {
                float curDis = Vector3.Distance(transform.position, target.transform.position);
                if (shortest > curDis)
                {
                    shortest = curDis;
                    newTarget = target.GetComponent<Interactable>();
                }
            }

        }

        if (newTarget != curTarget)
        {
            //상호작용 중 타겟이 바뀐 경우 상호작용 중지
            if (curTarget != null && isInteracting)
            {
                curTarget.StopInteraction();
                isInteracting = false;
            }

            curTarget = newTarget;
            if (curTarget != null)
            {
                curTarget.SetProgressBar(progressBar);
                playerUI.UpdateText(curTarget.promptMessage);
            }
            else
                playerUI.UpdateText(string.Empty);                
        }
    }

    private void OnHoldPerformed(InputAction.CallbackContext context)
    {
        if (curTarget != null && !isInteracting)
        {
            print("홀드");
            isInteracting = true;
            curTarget.StartInteraction();
        }
    }

    private void OnHoldCanceled(InputAction.CallbackContext context)
    {
        if (curTarget != null)
        {
            isInteracting = false;
            curTarget.StopInteraction();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}

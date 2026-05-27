using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    //미사용
    //hold가 아닌 누르기만 하면 되는 상호작용키지만, 지금은 필요없음

    public float distance = 5f;
    public LayerMask mask;
    private PlayerUI playerUI;
    private PlayerInput playerInput; //나눠쓰기로 그록이랑 협의봄

    private InputAction interactAction;
    private Interactable curInteractable;

    // Start is called before the first frame update
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        playerInput = GetComponent<PlayerInput>();

        interactAction = playerInput.actions.FindAction("Interact");
        if(playerUI == null)
            print("야!!!!!!!!");
    }

    private void OnEnable()
    {
        if (interactAction != null)
        {
            //Interact는 OnInteracPerfomed()를 실행한다는 뜻
            interactAction.performed += OnInteractPerformed;
        }
    }

    private void OnDisable()
    {
        if (interactAction != null)
        {
            //해제하는 이유: 걍 (최적화, 오류방지)
            interactAction.performed -= OnInteractPerformed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //이전거 초기화
        if (curInteractable != null)
        {
            playerUI.UpdateText(string.Empty);
            curInteractable = null;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * -distance, Color.red);

        if(Physics.Raycast(ray, out RaycastHit hit, distance, mask))
        {
            if(hit.collider.GetComponent<Interactable>() != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                playerUI.UpdateText(interactable.promptMessage);
                curInteractable = interactable;
            }
        }
    }

    //인수는 형식상
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (curInteractable != null)
        {
            //curInteractable.BaseInteract();
        }
    }
}

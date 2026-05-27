using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class Interactable : MonoBehaviour
{
    public bool useEvents;
    public string promptMessage;
    public float maxProgress = 100f;
    private float curProgress;
    private float progressSpeed;
    private bool isInteracting;
    private bool isFinish = false;

    private Slider progressBar; //런타임 중 할당
    private Coroutine progressCoroutine;

    public UnityEvent interactionEvent;
    public UnityEvent onProgressComplete;

    public void SetProgressBar(Slider slider)
    {
        progressBar = slider;

        progressBar.maxValue = maxProgress;
        progressBar.value = 0;
        progressBar.gameObject.SetActive(false);

        curProgress = 0;
    }

    private void Start()
    {
        progressSpeed = PlayerStats.Instance.ProgressSpeed;
    }

    public void StartInteraction()
    {
        if (!isInteracting && !isFinish)
        {
            if (useEvents)
                interactionEvent.Invoke();
            isInteracting = true;
            progressBar.gameObject.SetActive(true);
            curProgress = 0;

            if (progressCoroutine != null)
                StopCoroutine(progressCoroutine);
            progressCoroutine = StartCoroutine(ProgressCorouine());
        }
    }

    public void StopInteraction()
    {
        if (isFinish) return;

        print("으아아아악!!!");
        isInteracting = false;

        if (progressCoroutine!= null)
            StopCoroutine(progressCoroutine);
        progressCoroutine = null;

        progressBar.gameObject.SetActive(false);
    }


    private IEnumerator ProgressCorouine()
    {
        float elaps = 0f;

        while (isInteracting && elaps < progressSpeed)
        {
            elaps += Time.deltaTime;
            //clamp01 = 값을 0~1로 고정
            float progressRatio = Mathf.Clamp01(elaps / progressSpeed);
            curProgress = progressRatio * maxProgress;
            progressBar.value = curProgress;

            yield return null;
        }

        progressBar.value = maxProgress;
        isInteracting = false;
        OnProgressComplete();
    }

    private void OnProgressComplete()
    {
        isFinish = true;
        progressBar.gameObject.SetActive(false);
        promptMessage = "정화 완료!";
        onProgressComplete.Invoke();
    }

}

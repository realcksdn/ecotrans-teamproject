using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public float progressSpeed = 20f;
    public Slider clearSlider;
    public ClearPanel clearPanel;

    private float targetRate;
    private string curStage;

    private void Awake()
    {
        curStage = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        SetupEventListeners();

        clearSlider.maxValue = 100f;
        clearSlider.value = 0f;
        UpdateProgress(curStage, GameManager.Instance.GetClearProgress(curStage));
        GameManager.Instance.LoadMapState(SceneManager.GetActiveScene().name);

        if (GameManager.Instance.IsStageCleared(curStage))
        {
            clearPanel = null;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            var stage = SceneManager.GetActiveScene().name;
            GameManager.Instance.UpdateClearProgress(stage, 100f);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.onProgressUpdated.RemoveListener(UpdateProgress);
    }

    private void SetupEventListeners()
    {
        // 중복 등록 방지
        GameManager.Instance?.onProgressUpdated.RemoveListener(UpdateProgress);
        GameManager.Instance?.onProgressUpdated.AddListener(UpdateProgress);
        print("등록 완료");
    }


    public void UpdateProgress(string stage, float curRate)
    {
        print("프로그레스 업데이트");
        print(curRate + stage);
        //if (stage != curStage) return;

        targetRate = Mathf.Clamp(curRate, 0f, 100f);
        StopAllCoroutines();
        StartCoroutine(UpdateUI());
    }

    private IEnumerator UpdateUI()
    {
        float curRate = clearSlider.value;
        while (curRate < targetRate)
        {
            curRate += progressSpeed*Time.deltaTime;
            clearSlider.value = Mathf.Min(curRate, targetRate);
            yield return null;            
        }

        clearSlider.value = targetRate;

        if (targetRate >= 100 && GameManager.Instance.IsStageCleared(curStage))
        {
            if (clearPanel != null)
            {

            clearPanel.ShowResult(false);
            }
        }
    }
}

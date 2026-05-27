using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutManager : SingleTone<FadeOutManager>
{
    public Image FadeImage;
    bool isFading = false;

    public void FadeStart(float time, Color color, Action startAction, Action endAction)
    {
        print("페이드 스타트");
        if (isFading) { print("이미 페이드 중입니다");  return; }
        StartCoroutine(FadeOut(time, color, startAction, endAction));
    }

    IEnumerator FadeOut(float time, Color color, Action startAction, Action endAction)
    {
        print("시작");
        isFading = true;
        
        float process = 0;
        FadeImage.gameObject.SetActive(true);

        Color startColor = color;
        Color endColor = color;
        startColor.a = 0;

        while(process <= 1)
        {
            process += (Time.deltaTime/time);
            FadeImage.color = Color.Lerp(startColor, endColor, process);
            yield return null;
        }

        startAction?.Invoke();

        while (process >= 0)
        {
            process -= (Time.deltaTime / time);
            FadeImage.color = Color.Lerp(startColor, endColor, process);
            yield return null;
        }

        isFading = false;
        FadeImage.gameObject.SetActive(false);
    }
}

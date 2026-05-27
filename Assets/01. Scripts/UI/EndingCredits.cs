using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.UI;

public class EndingCredits : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    public float scrollSpeed = 50f;
    public float endPosY = 1000f; //도달할 위치

    public Button skipBtn;

    private RectTransform textTransform;
    private bool isScrolling = false;

    private void Start()
    {
        if (creditsText != null)
        {
            textTransform = creditsText.GetComponent<RectTransform>();
            isScrolling = true;
        }

        skipBtn.onClick.AddListener(FinishCredit);
            
        
    }

    private void Update()
    {
        if (isScrolling)
        {
            textTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

            if (textTransform.anchoredPosition.y >= endPosY)
            {
                FinishCredit();
            }
        }
    }

    private void FinishCredit()
    {
        isScrolling = false;
        FadeOutManager.Instance.FadeStart(2f, Color.black, () => SceneManager.LoadScene("Lobby"), null);
    }
}

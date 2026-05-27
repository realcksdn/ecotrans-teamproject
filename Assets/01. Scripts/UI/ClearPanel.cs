using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ClearPanel : MonoBehaviour
{
    public GameObject panel;

    public Image resultImage;
    public Sprite clearSprite;
    public Sprite failSprite;
    public TextMeshProUGUI resultText;
    public Button continueBtn;
    public Button lobbyBtn;
    public Button endBtn;

    //언젠가 쓰겠지
    public UnityEvent onContinueClicked;
    public UnityEvent onLobbyClicked;

    private bool isMap3;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Map3")
            isMap3 = true;

        SetupButtons();
        panel.SetActive(false);
    }

    private void SetupButtons()
    {
        if (continueBtn != null)
        {
            continueBtn.onClick.AddListener(() =>
            {
                onContinueClicked?.Invoke();
                ContinueGame();
            });
        }
        if (lobbyBtn != null)
        {
            lobbyBtn.onClick.AddListener(() =>
            {
                onLobbyClicked?.Invoke();
                if (!isMap3)
                {
                    Time.timeScale = 1;
                    ToLobby();
                }
                else
                {
                    Time.timeScale = 1;
                    ToEnd();
                    print("엔딩씬으로");
                }
            });
        }
    }

    private void ToEnd()
    {
        FadeOutManager.Instance.FadeStart(2f, Color.black, () => SceneManager.LoadScene("Ending"), null);
    }

    private void ToLobby()
    {
        FadeOutManager.Instance.FadeStart(1f, Color.black, () => {SceneManager.LoadScene("Lobby");}, null );
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        panel.SetActive(false);
    }

    public void ShowResult(bool isOver)
    {
        Time.timeScale = 0;
        resultText.text = isOver? "기절하였습니다..." : "정화 완료!";
        resultImage.sprite = isOver?failSprite:clearSprite;
        continueBtn.gameObject.SetActive(!isOver);
        


        panel.SetActive(true);
    }
}

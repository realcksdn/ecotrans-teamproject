using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TravelUI : MonoBehaviour
{
    private GameManager gameManger;
    public Button map1;
    public Button map2;
    public Button map3;

    public TextMeshProUGUI map1Process;
    public TextMeshProUGUI map2Process;
    public TextMeshProUGUI map3Process;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            map2.interactable = true;
            map3.interactable = true;
        }
    }

    private void Start()
    {
        gameManger = GameManager.Instance;

        map1Process.text = $"{gameManger.GetClearProgress("Map1")} %";
        map2Process.text = $"{gameManger.GetClearProgress("Map2")} %";
        map3Process.text = $"{gameManger.GetClearProgress("Map3")} %";

        map2.interactable = false;
        map3.interactable = false;

        if (gameManger.GetClearProgress("Map1") >= 100)
        {
            map2.interactable = true;
        }
        if (gameManger.GetClearProgress("Map2") >= 100)
        {
            map3.interactable = true;
        }
    }

    public void LoadMap(string sceneName)
    {
        FadeOutManager.Instance.FadeStart(1f, Color.black, () => SceneManager.LoadScene(sceneName), null);
    }


}

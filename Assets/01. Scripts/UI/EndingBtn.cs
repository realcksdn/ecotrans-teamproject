using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingBtn : MonoBehaviour
{
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.gameObject.SetActive(false);

        if (GameManager.Instance.IsStageCleared("Map3"))
        {
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(() => FadeOutManager.Instance.FadeStart(2f, Color.black, () => SceneManager.LoadScene("Ending"), null));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

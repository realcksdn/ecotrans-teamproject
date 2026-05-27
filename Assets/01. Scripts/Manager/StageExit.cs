using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageExit : MonoBehaviour
{
    public GameObject exitPanel;
    public Button exitBtn;

    // Start is called before the first frame update
    void Start()
    {
        exitBtn.onClick.AddListener(()=> FadeOutManager.Instance.FadeStart(1f, Color.black, () => SceneManager.LoadScene("Lobby"), null));
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            exitPanel.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            exitPanel.SetActive(true);
        }
    }
}

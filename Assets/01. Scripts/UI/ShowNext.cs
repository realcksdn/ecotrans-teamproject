using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowNext : MonoBehaviour
{
    //인생대충산다
    public GameObject showPanel;
    public GameObject closePanel;

    public void ShowNextPanel()
    {
        if(showPanel != null)
            showPanel.SetActive(true);
        if (closePanel != null)
            closePanel.SetActive(false);
    }
}

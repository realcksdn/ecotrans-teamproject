using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOxyzen : MonoBehaviour
{
    public Slider oxySlider;
    public ClearPanel clearPanel;

    private float maxOxy;
    private float curOxy;

    private bool isOver;

    private void Start()
    {
        maxOxy = PlayerStats.Instance.Oxyzen;

        curOxy = maxOxy;
        oxySlider.maxValue = maxOxy;
        oxySlider.value = curOxy;

        isOver = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
            curOxy = maxOxy;

        if (isOver) return;

        curOxy -= Time.deltaTime;
        oxySlider.value = curOxy;

        if (curOxy < 0 )
        {
            isOver = true;
            clearPanel.ShowResult(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TrashUI : MonoBehaviour
{
    public GameObject panel;
    public Image trashIcon;
    public TextMeshProUGUI trashName;
    public TextMeshProUGUI trashExplain;

    private void Start()
    {
        TrashInventory.Instance.onInventoryChanges.AddListener(SetPanel);
    }

    private void OnDisable()
    {
        TrashInventory.Instance.onInventoryChanges.RemoveListener(SetPanel);
    }

    public void SetPanel(TrashData trashData)
    {
        panel.SetActive(true);
        trashIcon.sprite = trashData.icon;
        trashName.text = trashData.trashName;
        trashExplain.text = trashData.explanation;
    }
}

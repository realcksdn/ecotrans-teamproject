using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EquipmentUI : MonoBehaviour
{
    [Serializable]
    public class EquipTextSet
    {
        public EquipmentData data;
        public List<TextMeshProUGUI> resourceTexts;
        public List<Image> resourceImages;
    }

    private Inventory inventory;
    public EquipmentManager equipmentManager;

    [Header ("Buttons 순서, TextSets, 순서, equips 순서 맞춰야함!!!")]
    public List<EquipmentData> equips;
    public List<EquipTextSet> equipTextSets;
    public List<Button> buttons;

    private Dictionary<EquipmentData, Button> equipmentButtons = new Dictionary<EquipmentData, Button>();
    private Dictionary<EquipmentData, List<TextMeshProUGUI>> equipmentTexts = new Dictionary<EquipmentData, List<TextMeshProUGUI>>();
    private Dictionary<EquipmentData, List<Image>> equipmentImages = new Dictionary<EquipmentData, List<Image>>();

    private bool isSell;

    private void OnEnable()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();
        if (equipmentManager == null)
            equipmentManager = FindObjectOfType<EquipmentManager>();

        inventory.onInventoryChanged.AddListener(UpdateUI);
        InitialzeUI();
        UpdateUI();
    }

    private void OnDisable()
    {
        inventory.onInventoryChanged.RemoveListener(UpdateUI);
    }

    private void InitialzeUI()
    {
        equipmentButtons.Clear();
        equipmentTexts.Clear();
        equipmentImages.Clear();

        //딕셔너리에 저장 (버튼은 덤으로
        for (int i = 0; i < equipTextSets.Count; i++)
        {
            var set = equipTextSets[i];
            var equip = set.data;
            set.data = equip; //혹시몰라서

            if (equip != null)
            {
                buttons[i].onClick.AddListener(() => OnCraftClicked(equip));
                equipmentButtons[equip] = buttons[i];
                equipmentTexts[equip] = set.resourceTexts;
                equipmentImages[equip] = set.resourceImages;

            }
        }
    }
    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (var set in equipTextSets)
        {
            var equip = set.data;

            var texts = equipmentTexts[equip];
            var images = equipmentImages[equip];
            var resources = equip.requiredResources;
            int index =0;

            foreach (var resource in resources)
            {
                //Key = ResourceData, Value = int
                int curAmount = inventory.GetResourceAmount(resource.Key);
                int requiredAmount = resource.Value;
                texts[index].text = $"{curAmount} / {requiredAmount}";
                images[index].sprite = resource.Key.icon;

                index ++;
            }

            if (!isSell)
                equipmentButtons[equip].interactable = inventory.HasEnoughResources(equip.requiredResources);
        }
        
    }

    private void OnCraftClicked(EquipmentData equip)
    {
        //만들기
        if (equipmentManager.CraftEquipment(equip))
        {
            isSell = true;


            equipmentButtons[equip].interactable = false;
            UpdateUI();
        }
    }
}

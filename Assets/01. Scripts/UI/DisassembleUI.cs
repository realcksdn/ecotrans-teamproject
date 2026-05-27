using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;

public class DisassembleUI : MonoBehaviour
{
    //얻는 오브젝트 (equipData와 유사)
    [Serializable]
    public class GetResource
    {
        public ResourceData getResource;
        public int getAmount;
        public List<ResourceRequirement> requipredResourcesList;
        public Dictionary<ResourceData, int> requiredResources => requipredResourcesList.ToDictionary(r=>r.resource, r=>r.amount);
    }

    [Serializable]
    public class ResourceTextSet
    {
        public GetResource data;
        public List<TextMeshProUGUI> resourceTexts;
        public List<Image> resourceImages;
    }

    private Inventory inventory;    

    [Header("Buttons, TextSets, getResources 순서 맞춰야 함!!!")]
    public List<GetResource> getResources;
    public List<ResourceTextSet> resourceTextSets; //Data, Text 할당 가능 클래스
    public List<Button> buttons;

    private Dictionary<GetResource, Button> resourceButtons = new Dictionary<GetResource, Button>();
    private Dictionary<GetResource, List<TextMeshProUGUI>> resourceTexts = new Dictionary<GetResource, List<TextMeshProUGUI>>();
    private Dictionary<GetResource, List<Image>> resourceImages = new Dictionary<GetResource, List<Image>>();

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        if (inventory == null)
            inventory = FindObjectOfType<Inventory>();  
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
        resourceButtons.Clear();
        resourceTexts.Clear();
        resourceImages.Clear();

        for (int i = 0; i < getResources.Count; i++)
        {
            var set = resourceTextSets[i];
            var resource = getResources[i];
            set.data = resource;

            buttons[i].onClick.AddListener(() => OnDisassembleClick(resource));
            resourceButtons[resource] = buttons[i];
            resourceTexts[resource] = set.resourceTexts;
            resourceImages[resource] = set.resourceImages;
        }
    }

    private void UpdateUI()
    {
        foreach (var set in resourceTextSets)
        {
            var resource = set.data;

            if (!resourceTexts.ContainsKey(resource)) continue;
            var texts = resourceTexts[resource];
            var images = resourceImages[resource];
            var resources = resource.requiredResources;
            int index = 0;

            foreach (var curResource in resources)
            {
                print(index);
                //curAmount = Dictionary<Data, int>
                int curAmount = inventory.GetResourceAmount(curResource.Key);
                int requiredAmount = curResource.Value;
                texts[index].text = $"{curAmount} / {requiredAmount}";
                images[index].sprite = curResource.Key.icon;

                index++;
            }

            resourceButtons[resource].interactable = inventory.HasEnoughResources(resource.requiredResources);
        }
    }

    private void OnDisassembleClick(GetResource resource)
    {
        if (inventory.HasEnoughResources(resource.requiredResources))
        {
            inventory.ConsumeResources(resource.requiredResources);
            inventory.GetResource(resource. getResource, resource.getAmount);
            UpdateUI();
        }
    }
}

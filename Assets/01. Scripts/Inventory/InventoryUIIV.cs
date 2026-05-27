using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIIV : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab; [SerializeField] private Transform slotContainer; private TrashInventoryIV inventory;

    private void Awake()
    {
        inventory = TrashInventoryIV.Instance; 
        inventory.onInventoryChanged.AddListener(UpdateUI);
        inventory.onInventoryFull.AddListener(OnInventoryFull);
    }

    private void Start()
    {
        UpdateUI(null);
    }

    private void UpdateUI(TrashDataIV changedItem)
    {
        // 기존 슬롯 제거
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }

        // 인벤토리 슬롯 생성
        foreach (var item in inventory.GetInventory())
        {
            var slot = Instantiate(slotPrefab, slotContainer);
            var image = slot.GetComponentInChildren<Image>();
            var text = slot.GetComponentInChildren<TextMeshProUGUI>();

            if (image != null) image.sprite = item.data.icon;
            if (text != null) text.text = $"{item.data.itemName}: {item.amount}";
        }
    }

    private void OnInventoryFull()
    {
        Debug.Log("UI: Inventory is full!");
        // TODO: 인벤토리 가득 찼을 때 UI 알림 (예: 텍스트 표시)
    }

}
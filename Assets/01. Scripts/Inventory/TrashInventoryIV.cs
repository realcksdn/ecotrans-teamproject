
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashInventoryIV : SingleTone<TrashInventoryIV> { [Serializable] public class TrashItem { public TrashDataIV data; public int amount; }
[SerializeField] private List<TrashItem> trashs = new List<TrashItem>();
public UnityEvent<TrashDataIV> onInventoryChanged = new UnityEvent<TrashDataIV>();
public UnityEvent onInventoryFull = new UnityEvent();

    private void Start()
    {
        LoadInventory();
    }

    public bool CanAddItem(TrashDataIV trash, int amount)
    {
        float currentSlotsUsed = trashs.Count;
        float maxSlots = PlayerStats.Instance.BagSpace;

        var existingItem = trashs.Find(r => r.data == trash);
        if (existingItem != null)
        {
            // 기존 아이템이 있고, 스택에 여유가 있는 경우
            if (existingItem.amount + amount <= trash.maxStackSize)
            {
                return true;
            }
            // 스택이 가득 찼다면 새 슬롯 필요
            return currentSlotsUsed < maxSlots;
        }
        // 새 아이템 추가 시 슬롯 여유 확인
        return currentSlotsUsed < maxSlots;
    }

    public void GetTrash(TrashDataIV trash, int amount)
    {
        if (!CanAddItem(trash, amount))
        {
            onInventoryFull.Invoke();
            Debug.Log("Inventory is full!");
            return;
        }

        var item = trashs.Find(r => r.data == trash);
        if (item == null)
        {
            item = new TrashItem { data = trash, amount = 0 };
            trashs.Add(item);
        }
        item.amount += amount;

        // 스택 초과 시 새 아이템 생성
        while (item.amount > trash.maxStackSize)
        {
            item.amount -= trash.maxStackSize;
            trashs.Add(new TrashItem { data = trash, amount = trash.maxStackSize });
        }

        onInventoryChanged.Invoke(trash);
        SaveInventory();
    }

    public void RemoveTrash(TrashDataIV trash, int amount)
    {
        var item = trashs.Find(r => r.data == trash);
        if (item == null) return;

        item.amount -= amount;
        if (item.amount <= 0)
        {
            trashs.Remove(item);
        }
        onInventoryChanged.Invoke(trash);
        SaveInventory();
    }

    public List<TrashItem> GetInventory()
    {
        return trashs;
    }

    private void SaveInventory()
    {
        string json = JsonUtility.ToJson(new InventoryWrapper { items = trashs });
        PlayerPrefs.SetString("TrashInventoryIV", json);
    }

    private void LoadInventory()
    {
        string json = PlayerPrefs.GetString("TrashInventoryIV", "");
        if (!string.IsNullOrEmpty(json))
        {
            var wrapper = JsonUtility.FromJson<InventoryWrapper>(json);
            trashs = wrapper.items ?? new List<TrashItem>();
        }
    }

    [Serializable]
    private class InventoryWrapper
    {
        public List<TrashItem> items;
    }



}
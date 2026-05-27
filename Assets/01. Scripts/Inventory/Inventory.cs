using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Inventory : SingleTone<Inventory>
{
    [Serializable]
    public class ResourceItem
    {
        public ResourceData data;
        public int amount;
    }

    //현재 갖고있는 자원
    public List<ResourceItem> resources;
    public UnityEvent setInventory;

    public UnityEvent onInventoryChanged;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            foreach (var curResource in resources)
            {
                curResource.amount += 100;
            }
        }
        
    }

    public void GetResource(ResourceData resource, int amount)
    {
        print("얍");
        var item = resources.Find(r => r.data == resource);
        if (item == null)
        {
            print("추가");
            item = new ResourceItem { data = resource, amount = 0 };
            resources.Add(item);
        }
        item.amount += amount;
        onInventoryChanged.Invoke();
    }

    public bool HasEnoughResources(Dictionary<ResourceData, int> required)
    {
        foreach (var req in required)
        {
            //ResourceData 있는지 찾기
            var item = resources.Find(r => r.data == req.Key);
            if (item == null || item.amount < req.Value) return false;
        }
        return true;
    }

    public void ConsumeResources(Dictionary<ResourceData, int> required)
    {
        foreach (var req in required)
        {
            var item = resources.Find(r => r.data == req.Key);
            if (item != null)
                item.amount -= req.Value;
        }
        onInventoryChanged.Invoke();
    }

    public int GetResourceAmount(ResourceData resource)
    {
        var item = resources.Find(r => r.data == resource);
        return item != null ? item.amount : 0;
    }
}

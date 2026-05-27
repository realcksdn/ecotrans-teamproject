using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrashInventory : SingleTone<TrashInventory>
{
    [Serializable]
    public class TrashItem
    {
        public TrashData data;
        public int amount;
    }

    public float bagSpace;
    private float curSpace;

    public List<TrashItem> trashs;
    public UnityEvent<TrashData> onInventoryChanges;

    private void Start()
    {
        bagSpace = PlayerStats.Instance.BagSpace;
        curSpace = trashs.Count;
    }

    private void Update()
    {
    }

    public void GetTrash(TrashData trash, int amount)
    {
        if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                if (curSpace >= bagSpace) return;

                TrashItem newItem = new TrashItem { data = trash, amount = 1 };
                trashs.Add(newItem);
                curSpace++;
            }
        }
        else if (amount < 0)
        {
            int removeCount = -amount;

            for (int i = 0; i < removeCount; i++)
            {
                var item = trashs.Find(r => r.data == trash);
                if (item == null) break;

                trashs.Remove(item);
                curSpace--;
            }
        }

        onInventoryChanges.Invoke(trash);
    }
}

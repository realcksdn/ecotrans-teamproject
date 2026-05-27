using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : SingleTone<EquipmentManager>
{
    public Inventory inventory;
    public PlayerStats playerStats;
    public UnityEvent onCrafted;

    private void OnEnable()
    {
        inventory = FindObjectOfType<Inventory>();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public bool CraftEquipment(EquipmentData equipment)
    {
        if (inventory.HasEnoughResources(equipment.requiredResources))
        {
            inventory.ConsumeResources(equipment.requiredResources);
            playerStats.UpgradeStats(equipment.value, equipment);
            onCrafted.Invoke();
            return true;
        }
        return false;
    }
}

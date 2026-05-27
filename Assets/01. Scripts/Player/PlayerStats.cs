using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : SingleTone<PlayerStats>
{
    [Header ("플레이어 스탯")]
    private float progressSpeed = 4f;
    [SerializeField] private float progressSpeedModifier = 1f;
    private float speed = 5f;
    [SerializeField] private float speedModifier = 1f;
    private int bagSpace = 4;
    [SerializeField] private float bagModifier = 1f;
    private float oxyzen = 90f;
    [SerializeField] private float oxyzenModifier = 1f;
    [SerializeField] private bool isFetilizerBag = false;
    [SerializeField] private bool isGlassGoggle = false;

    public float ProgressSpeed => progressSpeed / progressSpeedModifier;
    public float Speed => speed * speedModifier;
    public float BagSpace => bagSpace * bagModifier;
    public float Oxyzen => oxyzen * oxyzenModifier;
    public bool IsFetilizerBag => isFetilizerBag;
    public bool IsGlassGoggle => isGlassGoggle;
    

    public void UpgradeStats(float amount, EquipmentData equip )
    {
        switch (equip.category)
        {
            case "ProgressSpeed":
                progressSpeedModifier = amount;
                break;
            case "Oxyzen":
                oxyzenModifier = amount;
                break;
            case "Speed":
                speedModifier = amount;
                break;
            case "BagSpace":
                bagModifier = amount;
                TrashInventory.Instance.bagSpace = BagSpace;
                break;
            case "Fertilizer":
                isFetilizerBag = true;
                break;
            case "Goggle":
                isGlassGoggle = true;
                break;
        }

        SaveStats();
    }

    public void SaveStats()
    {
        PlayerPrefs.SetFloat("ProgressSpeedModifier", progressSpeedModifier);
    }

    public void LoadStarts()
    {
        progressSpeedModifier = PlayerPrefs.GetFloat("ProgressSpeedModifier", 1f);        
    }
}

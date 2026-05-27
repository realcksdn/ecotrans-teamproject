using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class InventoryUI : MonoBehaviour
{
    public List<Image> slots;
    public List<GameObject> lockImage;
    public Sprite nullSprite;
    
    private float activeAmount;
    private TrashInventory inventory;

    private void Awake()
    {
        
    }

    private void Start()
    {
        activeAmount = PlayerStats.Instance.BagSpace;
        inventory = TrashInventory.Instance;
        inventory.onInventoryChanges.AddListener(UpdateUI);

        for (int i = 0; i < activeAmount; i++)
        {
            lockImage[i].SetActive(false);
        }

        UpdateUI(null);
    }

    public void UpdateUI(TrashData dontUse)
    {

        for (int i = 0; i < inventory.trashs.Count; i++)
        {
                print("¹Ù²Þ");
            if (inventory.trashs[i] == null)
            {
                slots[i].sprite = nullSprite;
                continue;
            }
            slots[i].sprite = inventory.trashs[i].data.icon;

        }
    }


}

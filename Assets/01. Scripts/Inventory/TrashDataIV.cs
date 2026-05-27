using UnityEngine;

[CreateAssetMenu(fileName = "NewTrashData", menuName = "Inventory/TrashData")] public class TrashDataIV : ScriptableObject { public string itemName; public Sprite icon; public int maxStackSize = 99; }
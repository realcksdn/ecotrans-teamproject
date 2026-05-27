using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewEquipment", menuName ="Game/Equipment")]
public class EquipmentData : ScriptableObject
{
    

    public string equipmentName;
    public float value;
    public string category;

    public UnityEvent equipEvent;

    //인스펙터용
    public List<ResourceRequirement> requiredResourcesList;
    //코드용
    public Dictionary<ResourceData, int> requiredResources => requiredResourcesList.ToDictionary(r=>r.resource, r=>r.amount);
}

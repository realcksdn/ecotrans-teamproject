using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName ="NewResource", menuName ="Game/Resource")]
public class ResourceData : ScriptableObject
{
    public string resourceName;
    public Sprite icon;

    //[Header ("필요 시에만")]
    ////인스펙터용
    //public List<ResourceRequirement> requiredResourcesList;
    ////코드용
    //public Dictionary<ResourceData, int> requiredResources => requiredResourcesList.ToDictionary(r => r.resource, r => r.amount);
}

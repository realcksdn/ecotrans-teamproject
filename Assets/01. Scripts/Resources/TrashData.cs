using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="NewTrash", menuName = "Game/Trash")]
public class TrashData : ScriptableObject
{
    public Sprite icon;
    public ResourceData category;
    public int value;
    public string trashName;

    [TextArea] public string explanation;

    public bool isRemove;
    public bool isClean;
    public bool isPressure;
}

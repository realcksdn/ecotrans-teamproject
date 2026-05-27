using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDatabase : SingleTone<TrashDatabase>
{
    public List<TrashData> trashs;

    public TrashData RandomTrash()
    {
        int index = Random.Range(0, trashs.Count);
        return trashs[index];
    }
}

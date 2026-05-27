using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private float distance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStats.Instance.IsGlassGoggle)
            distance += 5f;
    }

    private void LateUpdate()
    {
        transform.position = target.position + transform.rotation*new Vector3(0,0,-distance);
    }
}

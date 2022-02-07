using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARCustomObject : MonoBehaviour
{
    Vector3 originPos;
    Quaternion originRot;

    Vector3 lastTrackedPos;
    Quaternion lastTrackedRot;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        originRot = transform.rotation;
    }

    public void SetPosAndRot()
    {
        lastTrackedPos = transform.position;
        lastTrackedRot = transform.rotation;
    }

    public void ResetPosAndRot()
    {
        transform.position = originPos;
        transform.rotation = originRot;
    }

    public void PlaceToLastPosAndRot()
    {
        transform.position = lastTrackedPos;
        transform.rotation = lastTrackedRot;
    }
}

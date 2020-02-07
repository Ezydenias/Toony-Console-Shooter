using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignWithCamera : MonoBehaviour
{
    public GameObject PlayerCameraPivot;

    public float Offset = 90f;

    // Start is called before the first frame update

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation =
            Quaternion.Euler(new Vector3((PlayerCameraPivot.transform.localRotation.eulerAngles.x + Offset)*(-1), transform.localRotation.eulerAngles.y,  transform.localRotation.eulerAngles.z));
        // transform.rotation = Quaternion.LookRotation(PlayerCamera.transform.localRotation.eulerAng);
    }
}
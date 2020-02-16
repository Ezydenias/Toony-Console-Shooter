using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void LateStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.current)
        {
            cam=Camera.current;
            transform.LookAt(cam.transform);
        }
        
    }
}

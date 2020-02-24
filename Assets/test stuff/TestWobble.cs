using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWobble : MonoBehaviour
{
    public AnimationCurve myCurve;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation=Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, myCurve.Evaluate((Time.time % myCurve.length)),
            transform.rotation.eulerAngles.z));
   
    }
}

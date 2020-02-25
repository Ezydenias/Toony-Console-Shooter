using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testchancecolor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().materials[3].color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

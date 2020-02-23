using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoubleDance : MonoBehaviour
{
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        transform.position=transform.position+(new Vector3(Random.Range(0, .1f), Random.Range(0, .1f), Random.Range(0, .1f)));
    }
}
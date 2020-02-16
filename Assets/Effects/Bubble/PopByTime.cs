using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopByTime : MonoBehaviour
{

    // Start is called before the first frame update
    [Range(.01f, 1000)] public float lifetime = 1;

    void Start()
    {
        GetComponent<PopSound>().pop();
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            GetComponent<PopSound>().pop();
            Destroy(gameObject);
        }
    }


}
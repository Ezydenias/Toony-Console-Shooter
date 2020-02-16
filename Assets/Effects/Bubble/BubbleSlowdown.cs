using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSlowdown : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0, 5)] public float slowDown = 2;
    [Range(.1f, 1)] public float minimumSpeed = 1;

    // Update is called once per frame
    void Start()
    {
        slowDown = Random.Range(0, slowDown);
    }

    void Update()
    {
        if (GetComponent<ProjectileMove>().speed > minimumSpeed)
            GetComponent<ProjectileMove>().speed -= slowDown * Time.deltaTime;
    }
}
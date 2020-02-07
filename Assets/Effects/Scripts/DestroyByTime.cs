using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(.01f, 1000)] public float lifetime = 1;
    public float damage = 100;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}

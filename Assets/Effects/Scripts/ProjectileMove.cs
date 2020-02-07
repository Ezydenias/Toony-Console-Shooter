using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    [Range(1, 1000)] public float speed = 1;
    [Range(1, 1000)] public float fireRate = 1;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision co)
    {
        Destroy(gameObject);
    }

}
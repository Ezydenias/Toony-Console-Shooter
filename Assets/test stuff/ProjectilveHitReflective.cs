using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilveHitReflective : MonoBehaviour
{
    void OnCollisionEnter(Collision co)
    {
        Debug.Log(co.collider.tag);
        if (co.collider.tag == "Reflective")
        {

            


           transform.rotation = Quaternion.LookRotation(co.collider.transform.position);
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
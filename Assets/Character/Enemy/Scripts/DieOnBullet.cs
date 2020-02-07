using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnBullet : MonoBehaviour
{
    public float life = 1000;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            life -= col.gameObject.GetComponent<DestroyByTime>().damage;
            if (life <= 0)
                Destroy(gameObject);
        }
    }
}
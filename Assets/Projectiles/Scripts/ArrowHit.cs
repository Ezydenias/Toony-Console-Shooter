﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public float damage = 100;


    void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag == "Enemy")
        {
            co.gameObject.GetComponent<EnemyStatus>().life -= damage;

            if (co.gameObject.GetComponent<EnemyStatus>().life < 0)
                Destroy(co.gameObject);
        }

        transform.SetParent(co.gameObject.transform);
        GetComponent<ProjectileMove>().enabled = false;

        GetComponent<PopSound>().pop();
    }
}
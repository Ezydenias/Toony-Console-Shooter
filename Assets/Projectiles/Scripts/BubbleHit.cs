﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleHit : MonoBehaviour
{
    public float damage = 100;


    // Start is called before the first frame update
    void OnCollisionEnter(Collision co)
    {
        Debug.Log(co.gameObject.tag);
        if (co.gameObject.tag == "Enemy")
        {
            
            co.gameObject.GetComponent<EnemyStatus>().life -= damage;
            if (co.gameObject.GetComponent<EnemyStatus>().life < 0)
                Destroy(co.gameObject);
            GetComponent<PopSound>().pop();
            Destroy(this.gameObject);
        }
        else if (co.gameObject.tag != "Bubble")
        {
            GetComponent<PopSound>().pop();
            Destroy(this.gameObject);
        }
    }


}
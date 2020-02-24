using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoPickup : MonoBehaviour
{
    public GameObject gunEmpty;


    void Start()
    {
        if (gunEmpty == null)
            gunEmpty = GameObject.Find("Gun Empty");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Ammunition")
        {
            if (gunEmpty.GetComponent<AmmunitionInventory>().addAmmunition(other.GetComponent<AmmoScript>().ammunition,
                other.GetComponent<AmmoScript>().clipSize))
                other.GetComponent<AmmoScript>().pickUp();
        }
    }
}
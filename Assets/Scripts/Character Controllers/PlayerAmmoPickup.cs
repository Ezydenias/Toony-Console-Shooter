using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoPickup : MonoBehaviour
{
    public GameObject gunEmpty;
    public GameObject floatingHead;

    private Animator animator;

    void Start()
    {
        if (gunEmpty == null)
            gunEmpty = GameObject.Find("Gun Empty");
        if (!floatingHead)
            floatingHead = GameObject.Find("Floating Head");
        animator = floatingHead.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammunition")
        {
            if (gunEmpty.GetComponent<AmmunitionInventory>().addAmmunition(other.GetComponent<AmmoScript>().ammunition,
                other.GetComponent<AmmoScript>().clipSize))
            {
                other.GetComponent<AmmoScript>().pickUp();
                animator.SetTrigger("Happy");
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using GameEnumSpace;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    public SwpGun gunEmpty;

    protected CharacterController controller;
    protected bool ledge = true;
    public SwpMelee MeleeEmpty;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = player.GetComponent<CharacterController>();
        if (!gunEmpty)
            gunEmpty = GameObject.Find("Gun Empty").GetComponent<SwpGun>();
        if (!MeleeEmpty)
            MeleeEmpty = GameObject.Find("Melee Empty").GetComponent<SwpMelee>();
    }

    // Update is called once per frame
    void Update()
    {
        getGrounded();
        getVertical();
        getLedgeGrab();
        standardSetMethods();
        meeleAtack();
    }

    protected void meeleAtack()
    {
        if (MeleeEmpty.active)
        {
            animator.SetBool("Melee", true);
            if (Input.GetButton("Fire1"))
                animator.SetBool("Melee Attack", true);
            else
                animator.SetBool("Melee Attack", false);
        }
        else
        {
            animator.SetBool("Melee", false);
            animator.SetBool("Melee Attack", false);

        }
    }

    protected void getLedgeGrab()
    {
        if (player.GetComponent<PlayerController>().getLedgeGrab())
        {
            if (ledge == true)
            {
                animator.SetTrigger("LedgeGrab");
                ledge = false;
            }
        }
        else
        {
            ledge = true;
        }
    }

    protected void getGrounded()
    {
        if (player.GetComponent<PlayerController>().getGrounded())
        {
            animator.SetBool("Grounded", true);
            //if (Input.GetButtonDown("Jump"))
            //    animator.SetTrigger("Jump");
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    protected void getVertical()
    {
        var vertical = Input.GetAxis("Vertical");

        if (vertical >= 0)
        {
            animator.SetFloat("Speed", controller.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", controller.velocity.magnitude * (-1));
        }
    }

    protected void standardSetMethods()
    {
        animator.SetBool("Swimming", player.GetComponent<PlayerController>().getSwimming());
        animator.SetBool("Climbing", player.GetComponent<PlayerController>().getOnLadder());
        animator.SetFloat("VerticalSpeed", controller.velocity.y);
    }
}
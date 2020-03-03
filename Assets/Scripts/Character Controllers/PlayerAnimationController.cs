using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public GameObject player;

    protected CharacterController controller;
    protected bool ledge = true;


    void Start()
    {
        animator = GetComponent<Animator>();
        controller = player.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        getGrounded();
        getVertical();
        getLedgeGrab();
        standardSetMethods();
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
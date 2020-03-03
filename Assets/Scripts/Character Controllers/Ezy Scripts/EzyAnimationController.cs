using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EzyAnimationController : PlayerAnimationController
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<EzyController>().getGrounded())
        {
            animator.SetBool("Grounded", true);
            //if (Input.GetButtonDown("Jump"))
            //    animator.SetTrigger("Jump");
        }
        else
        {
            animator.SetBool("Grounded", false);
        }

        getVertical();

        if (player.GetComponent<EzyController>().getLedgeGrab())
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

        animator.SetBool("Swimming", player.GetComponent<EzyController>().getSwimming());
        animator.SetBool("Climbing", player.GetComponent<EzyController>().getOnLadder());
        animator.SetFloat("VerticalSpeed", controller.velocity.y);
    }
}

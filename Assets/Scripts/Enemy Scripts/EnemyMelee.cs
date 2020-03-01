using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : followPlayerOnSight
{
    // Start is called before the first frame update
    public Animator animator;

    private bool meleeAttack = false;

    void Start()
    {
        parentStartFunction();
        if (!animator)
            animator = parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!closeToPlayer)
        {
            getPlayerPosition();
            chhasePlayerFunction();
        }
        else
        {
            melee();
        }

        orientateCharacter();
        moveCharacter();
    }

    protected void melee()
    {
        animator.SetTrigger("punch");
        parent.transform.position = Vector3.zero;
        if (meleeAttack == true && !this.animator.GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            meleeAttack = false;
            closeToPlayer = false;
        }

        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("New State 0"))
        {
            moveDirection = Vector3.zero;
            meleeAttack = true;
        }
    }
}
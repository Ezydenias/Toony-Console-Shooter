using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followButKeepDistance : followPlayerOnSight
{
    [Header("Make Stop larger than React")] [Range(1f, 10f)]
    public float maxFleeDistance = 5;

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
            flee();
        }

        orientateCharacter();
        moveCharacter();
    }

    private void flee()
    {
        foreach (GameObject Player in PlayerList)
        {
            if (Vector3.Distance(transform.position, Player.transform.position) >= maxFleeDistance)
            {
                moveDirection = Vector3.zero;
                closeToPlayer = false;
            }
        }
//
//        if (Vector3.Distance(parent.transform.position, currentTarget.transform.position) >= maxFleeDistance)
//        {
//            moveDirection = Vector3.zero;
//            closeToPlayer = false;
//        }

        moveDirection = (parent.transform.position - currentTarget.transform.position);
        moveDirection.y = 0;
    }
}
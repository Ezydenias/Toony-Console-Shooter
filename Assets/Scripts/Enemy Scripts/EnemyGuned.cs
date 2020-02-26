using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor.VersionControl;
using UnityEngine;

public class EnemyGuned : EnemyMelee
{
    // Start is called before the first frame update
    [Range(1, 20)] public int maximumShootingTime = 5;
    [Range(10, 80)] public int chanceToFire = 50;

    private bool shooting = false;

    private float shoots;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (!shooting)
        {
            if (!closeToPlayer)
            {
                getPlayerPosition();
                chhasePlayerFunction();
                getTurretMode();
            }
            else
            {
                melee();
            }
        }
        else
        {
            getPlayerPosition();
            shoot();
        }

        orientateCharacter();
        moveCharacter();
    }

    private void shoot()
    {
        moveDirection = Vector3.zero;
        animator.SetBool("shoot", true);
        shoots -= Time.deltaTime;
        if (shoots <= 0)
            shooting = false;
    }

    private void getTurretMode()
    {
        if ((time += Time.deltaTime) >= 1)
        {
            time = 0;
            int chance = Random.Range(0, 100);
            if (chance <= chanceToFire && seesPlayer())
            {
                moveDirection = Vector3.zero;
                shoots = Random.Range(0, maximumShootingTime);
                shooting = true;
            }
        }
    }
}
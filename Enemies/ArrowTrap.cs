using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{

    // THIS IS VERY SIMILAR TO PLAYER ATTACK . LOOK AT THAT FOR COMMENTS AND UNDERSTANDING :)

    [SerializeField] private float attackCooldown; // cooldown of the arrow launcher
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0; // reset cooldown timer when we shoot

        arrows[FindArrow()].transform.position = firePoint.position; // reset position of the projectile to fire position after its fired
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile(); // set direction of the projectile

    }

    private int FindArrow()
    {
        // cycles through fireballs/arrows and returns the first one that isn't being used, so it can be fired
        for (int i = 0; i < arrows.Length; i++)
        {
            if(!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime; // increment cooldown timer

        if(cooldownTimer >= attackCooldown)
        {
            Attack(); // shoot
        }
    }



}

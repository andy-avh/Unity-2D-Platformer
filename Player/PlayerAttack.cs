using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown; // time before you can fire another shot
    [SerializeField] private Transform firePoint; // position of the fireball when fired
    [SerializeField] private GameObject[] fireballs; // an array where we can store the duplicated fireballs (look at Attack() about object pooling)
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity; // = to infinity so player can attack when they spawn, otherwise cooldown timer will be 0 (< attackCooldown)

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    
    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) // if LMB is clicked and cooldown is complete, attack
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime; // increment the cooldown timer
    }

    private void Attack()
    {
        anim.SetTrigger("attack"); // player the attack animation when you attack
        cooldownTimer = 0; // when we attack, the cooldown timer will go back to 0

        // gonna use object pooling, used when creating lots of objects
        // rather than instantiating & destroy, we will have multiple fireballs already created, which will be deactivated on hit, then wait to be reused
        fireballs[FindFireball()].transform.position = firePoint.position; // reset position of 1 fireball to the position of the firepoint
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));  // send fireball in direction the player is facing
    }

    // implement the object pooling of the fireballs, to fire more than 1 at any time
    private int FindFireball()
    {
        // loops through fireballs created previously
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i; // if fireball i is not active, return back to attack, use it
            }
        }
        return 0;
    }
}


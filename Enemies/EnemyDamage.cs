using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    // using protected because we want to access them from other scripts that will inherit enemyDamage

    [SerializeField] protected float damage; // how much damage the enemy will do to the player

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") // check if enemy has collided with the player
        {
            collision.GetComponent<Health>().TakeDamage(damage);   // damage player     
        }
    }
}

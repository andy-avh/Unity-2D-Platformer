using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : EnemyDamage // inherit from enemy damage | damage player every time it touches player
{


    [SerializeField] private float speed; // speed of the projectile
    [SerializeField] private float resetTime; // deactivate object after period of time
    private float lifetime;

    public void ActivateProjectile()
    {
        lifetime = 0; // reset lifetime
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime; // calculate movement speed of projectile
        transform.Translate(movementSpeed, 0, 0); // move projectile on the x axis

        lifetime += Time.deltaTime; // increment lifetime 
        if (lifetime > resetTime)
        {
            gameObject.SetActive(false); // deactivate 
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision); // execut logic from parent script first
        gameObject.SetActive(false); // deactivate when arrow hits another object (player or wall for example)
    }


}

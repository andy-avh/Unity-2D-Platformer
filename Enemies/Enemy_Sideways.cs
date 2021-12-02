using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideways : MonoBehaviour
{
    [SerializeField] private float movementDistance; // how far a moving saw/enemy can move
    [SerializeField] private float speed; // speed of this movement
    [SerializeField] private float damage; // how much damage the enemy will do

    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;

    private void Awake()
    {
        // range of movement for a saw or enemy
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }


    private void Update()
    {
        // move enemy in the right direction
        if(movingLeft)
        {
            // moving left
            if (transform.position.x > leftEdge) // enemy hasn't reached the left edge
            {
                // actually move the enemy, only on the x-axis, y and z stay the same (left)
                transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = false;
            }
        }
        else
        {
            // moving right
            if (transform.position.x < rightEdge) // enemy hasn't reached the left edge
            {
                // actually move the enemy, only on the x-axis, y and z stay the same (right)
                transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
            }
            else
            {
                movingLeft = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the enemy (saw) has collided with the player
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage); // reduce health of player by damage of the enemy
        }
    }
}

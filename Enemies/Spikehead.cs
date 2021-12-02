using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage // inhertit , want contact to do damage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed; // speed of the spikehead
    [SerializeField] private float range; // how far the spikehead can 'see'
    [SerializeField] private LayerMask playerLayer;

    // add a delay to the spikehead, like a cooldown
    [SerializeField] private float checkDelay;
    private float checkTimer;

    private Vector3 destination; // store player position when contact is made
    

    private bool attacking; // see if spikehead is attacking

    private Vector3[] directions = new Vector3[4]; // array for checking directions

    Vector3 originalPos;


    private void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
    }

    // called every time the object is activated
    //private void OnEnable()
    //{
      //  Stop(); // so object starts in an idle position
        
        

    //}

    private void Update()
    {
        // move spikehead to destination (player) only if attacking
        if (attacking)
        {
            //startingPostition = this.transform.position;
            transform.Translate(destination * Time.deltaTime * speed);  // move spike head to player (variable: destination)
            
        }
        else // not attacking
        {
            checkTimer += Time.deltaTime; // increment the checkTimer
            if (checkTimer > checkDelay)
            {

                CheckForPlayer();
            }
        }
        
    }

    private void CheckForPlayer()
    {
        CalculateDirection();
        // check if spikehead can see player in all 4 directions
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            // detect player
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking) // hit the player, and not currently attacking
            {
                attacking = true; // attack!
                destination = directions[i]; // go to player's location
                checkTimer = 0; // reset checkTimer
            }
        }
    }

    // calculate the range, how far the spike head can see
    private void CalculateDirection()
    {
       // directions[0] = transform.right * range; // right direction
       // directions[1] = -transform.right * range; // left direction
        //directions[1] = transform.up * range; // up direction
        directions[0] = -transform.up * range; // down direction
    }


    private void OnTriggerEnter2D(Collider2D collision)
    { 
        base.OnTriggerEnter2D(collision);
        Stop(); // stop spikehead once it hits something
    }

    private void Stop()
    {
        // stop the spike head, so it doesn't travel forever
        destination = transform.position; // stop moving
        attacking = false; // no longer attacking
        Invoke("restart", 2);//this will happen after 2 seconds
    }

    private void restart()
    {
        gameObject.transform.position = originalPos;
    }
}

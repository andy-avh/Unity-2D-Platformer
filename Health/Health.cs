using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header ("Health")] // puts "health" above startingHealth in Unity, makes it a bit easier to understan what's going on, especially when there is a lot of variables on the script
    [SerializeField] private float startingHealth; // how much health player has at the start
    public float currentHealth { get; private set; } // so can only changed in TakeDamage, but can be accessed in HealthBar.cs
    private Animator anim; // reference to animator for damage and death animation
    private bool dead; // check if dead

    [Header("iFrames")]
    [SerializeField] private float invulnerabilityDuration; // how long the player is invulnerable after being hit, so they don't get hit instanly after and die quickly
    [SerializeField] private int numberOfFlashes; // how many times the player will flash red before returning to normal/hitable state
    private SpriteRenderer spriteRend; // needed to change the colour of the player (red) when hit

    private void Awake()
    {
        currentHealth = startingHealth; // how we set the player's starting health
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        // reduce player's health
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); // safeguard so health doesn't go below 0 or above max value (min = 0, max = startingHealth)
                                                                                 // currentHealth -= _damage; // simply take damage value from the current health value

        // check if player is dead or hurt
        if (currentHealth > 0)
        {
            // player just hurt
            anim.SetTrigger("hurt"); // hurt animation
            StartCoroutine(Invunerability());  // iframes display (player flashes red)
        }
        else
        {
            // player died from damage
            if (!dead)
            {
                // this if statement is to make sure the die animation only plays once
                anim.SetTrigger("die"); // die animation
                GetComponent<PlayerMovement>().enabled = false; // disable playerMovement, can't move when dead
                dead = true;
            }
        }

    }

    // this is for the health collectibles
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    // method for iFramers - player flashes red when hit, and has a brief period of Invunerability
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true); // ignore collisions with enemy objects (saw for example) | 8 is player layer, 9 is enemy layer, true for ignore collisions
        for (int i = 0; i < numberOfFlashes; i++) // loop for flashes, change number of flashes in Unity
        {
            spriteRend.color = new Color(1,0,0, 0.5f); // change player sprite to red, signifying damage | 1, 0, 0 = red | 0.5f is transparency
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // wait a bit
            spriteRend.color = Color.white; // changes back to original colour, finishing the flash
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOfFlashes * 2)); // wait a bit

        }
        Physics2D.IgnoreLayerCollision(8, 9, false); // turn collisions back on, ending player invunerability
    }




}

// test to see if TakeDamage works, press E to take damage
//private void Update()
//{
//    if (Input.GetKeyDown(KeyCode.E))
//    {
//        TakeDamage(1); // take 1 damage
//    }
//}


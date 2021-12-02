using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue; // how much health will the heart actually restore


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // check if the player has collided with the heart
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().AddHealth(healthValue); // increase player health by healthValue, using AddHealth method from Health.cs
            gameObject.SetActive(false); // disable the heart after collision, so can only be used once
        }
    }










}

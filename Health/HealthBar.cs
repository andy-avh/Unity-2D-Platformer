using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed for the image variable
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10; // totalHealth onlt needs to be updated once, so in Start()
    }

    private void Update()
    {
        currenthealthBar.fillAmount = playerHealth.currentHealth / 10; // keep the currenthealthBar updated
    }
}

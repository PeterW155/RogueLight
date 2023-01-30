using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Text healthText;

    public int maxHealth = 3;
    public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = "Health: " + currentHealth.ToString();
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthText.text = "Health: " + currentHealth.ToString();

        if (currentHealth <= 0)
        {
            //game over
        }
    }
}

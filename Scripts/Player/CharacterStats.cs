using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Player Health stats
public class CharacterStats : PlayerStats
{
    //Holding values for the player to have maxHealth at start and set healthbar to max
    void Start()
    {
        MaxHealth = 100;
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        audioSource = GetComponent<AudioSource>();
    }

    //if the player is taking damage, show the damage border image, 
    //else return the image back to its 0 alpha to stay hidden and return false
    private void Update()
    {
        if(isTakingDamage)
        {
            damageImg.color = damageColour;
        }
        else
        {
            damageImg.color = Color.Lerp(damageImg.color, Color.clear, colourSmoothing * Time.deltaTime);
        }

        isTakingDamage = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Variables")]
    public float currentHealth;
    public float MaxHealth;
    public HealthBarScript healthBar;

    [Header("DamageScreen")]
    public Color damageColour;
    public Image damageImg;
    public float colourSmoothing = 6;
    public bool isTakingDamage = false;

    [Header("DamageAudio")]
    public AudioClip playerHit;
    protected AudioSource audioSource;

    [Header("Game Over Variables")]
    public GameObject gameOver;
    public GameObject gun;
    public bool isDead = false;

    //make sure the current health cant overlap the max health
    //if the health is less than or = 0, trigger the die function and change the bool
    public virtual void realtimeHealth()
    {
        if (currentHealth >= MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        if (currentHealth <= 0)
        {
            isTakingDamage = true;
            currentHealth = 0;
            isDead = true;
            Die();
        }
    }

    //Die loads the game over scene as well as stop time and trigger the bool
    public void Die()
    {
        SceneManager.LoadScene("GameOver");
        Time.timeScale = 0f;
        Debug.Log("PlayerDead");
        gun.SetActive(false);
        isDead = true;

    }

    //if the player is taking damage, play the audio and set the health in logic and the healthbar
    public void takeDamage(float damage)
    {
        isTakingDamage = true;
        audioSource.PlayOneShot(playerHit);
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    } 
}

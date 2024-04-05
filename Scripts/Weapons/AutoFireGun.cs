using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//AutoFireGun is the logic behind Guns that are automatic in the game
public class AutoFireGun : MonoBehaviour
{
    [Header("Damage Variables")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    [Header("Ammo Variables")]
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    public Text ammoCounter;

    [Header("Audio Variables")]
    public AudioClip gunSound;
    public AudioClip dryClip;
    private AudioSource audioSource;

    [Header("Graphics Variables")]
    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    public ParticleSystem bulletEject;
    public GameObject impactEffect;
    public GameObject bloodEffect;
    public Animator anmimator;
    public Camerashake cameraShake;

    //At the start, make sure the guns ammo is full
    private void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
    }

    //Method to call reloading animations
    private void OnEnable()
    {
        isReloading = false;
        anmimator.SetBool("Reloading", false);
    }

    /*Update to handle the ammo to be displayed on the GUI
    * if the game is not paused:
    * press R to reload and trigger the reload animation
    * if user has clicked left mouse button shoot raycast & add camera shake animation
    */
    void Update()
    {
        ammoCounter.text = currentAmmo.ToString() + " / " + maxAmmo.ToString();

        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            StartCoroutine(cameraShake.Shake(.01f, .2f));
        }
    }

    /*shoot handles the raycast logic behind the guns. When one click occurs one frame is called and ammo is taken
    play animations, decrement ammo by 1, shoot raycast & if the raycast hits zombie, cause damage.
    if ammo is 0, play audio*/
    void Shoot()
    {
        audioSource.PlayOneShot(gunSound);
        muzzleFlash.Play();
        bulletEject.Play();
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            AIBehaviour target = hit.transform.GetComponent<AIBehaviour>();
            if (target != null)
            {
                target.shootZombie(damage);
                GameObject bloodShot = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bloodShot, 2f);
            }

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 2f);
        }

        if (currentAmmo <= 0)
        {
            audioSource.PlayOneShot(dryClip);
        }
    }

    //Handles the reloading animation when triggered, returns current ammo as Max
    IEnumerator Reload()
    {
        isReloading = true;
        FindObjectOfType<AudioManager>().Play("Reload");
        Debug.Log("Reload");

        anmimator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);
        anmimator.SetBool("Reloading", false);
        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Zombie"))
        {
            other.GetComponent<AIBehaviour>().OnAware();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The Scope script is appended to the Sniper gun where inputing the right mouse allows a scopeOverlay
public class Scope : MonoBehaviour
{
    [Header ("Graphics")]
    public Animator animator;

    [Header("Game Objects")]
    public GameObject WeaponCam;
    public GameObject scopeOverlay;
    public Camera mainCam;
    public CameraScript sensitivityScopein;

    [Header("Scoping Variables")]
    public float scopeFOV = 1.2f;
    private float fov;
    
    /*The update function checks per frame if the right mouse button has been pressed
     * if the player has scoped then play the scoping animation with the scopeOverlay
     */
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("Scoped", true);
            StartCoroutine(onScoped());
            sensitivityScopein.mouseSensitivity = 50f;
            
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("Scoped", false);
            unScoped();
            sensitivityScopein.mouseSensitivity = 150f;
        }
    }

    //onScoped creates a delay for scope time and removes the Sniper from scope view
    IEnumerator onScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        WeaponCam.SetActive(false);

        //fov = mainCam.fieldOfView;
        //mainCam.fieldOfView = scopeFOV;
    }

    //Unscoped ensures that the gun is visible again with no overlay present
    void unScoped()
    {
        scopeOverlay.SetActive(false);
        WeaponCam.SetActive(true);
        //mainCam.fieldOfView = fov;
    }
}

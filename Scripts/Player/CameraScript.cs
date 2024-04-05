using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handing the user mouse input and ensure the fps camera works
public class CameraScript : MonoBehaviour
{
    [Header("Mouse Sensitivity")]
    public float mouseSensitivity = 100f;
    float xRotation = 0f;

    [Header("Player Variables")]
    public Transform player;
    public PlayerStats fpsPlay;

    
    void Start()
    {
    }

    //check the current state of the cursor in the game,
    //Rotate the players local rotation in only the camera depending on the x and y axis the mouse is on 
    void Update()
    {
        GOState();
        CursorState();

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
    }

    //ensuring that the cursor is locked and unlocked corretly for the pause screen
    public void CursorState()
    {
        if (!PauseMenu.GamePaused && fpsPlay.isDead == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    //ensuring that the cursor is locked and unlocked corretly for the Game Over screen
    public void GOState()
    {
        if (fpsPlay.isDead == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

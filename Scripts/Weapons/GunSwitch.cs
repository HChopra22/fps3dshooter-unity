using UnityEngine;


//Gun switch is the handler to allow changing fluidly between guns.
public class GunSwitch : MonoBehaviour
{
    public int currentWeapon = 0;

    void Start()
    {
        weaponSelect();
    }

    //continuously updates and takes user input to switch weapon
    void Update()
    {
       //Uses mouse input by incrementing and decrementing.
        int previousWeapon = currentWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeapon >= transform.childCount - 1)
                currentWeapon = 0;
            else
                currentWeapon++;
                FindObjectOfType<AudioManager>().Play("GunChange");

        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeapon <= 0)
                currentWeapon = transform.childCount -1;
            else
                currentWeapon--;
                FindObjectOfType<AudioManager>().Play("GunChange");
        }

       //Takes number keys to allow switching, labels each gun with their own unique number
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
            FindObjectOfType<AudioManager>().Play("GunChange");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            currentWeapon = 1;
            FindObjectOfType<AudioManager>().Play("GunChange");
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            currentWeapon = 2;
            FindObjectOfType<AudioManager>().Play("GunChange");
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            currentWeapon = 3;
            FindObjectOfType<AudioManager>().Play("GunChange");
        }

        if (previousWeapon != currentWeapon)
        {
            weaponSelect();
        }
    }

    //a for loop stating when i is currentweapon, remove other options.
    void weaponSelect ()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == currentWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}

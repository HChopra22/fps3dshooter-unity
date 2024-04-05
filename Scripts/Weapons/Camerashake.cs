using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//A simple camera shaking animation that resembles recoil
public class Camerashake : MonoBehaviour
{
    //transform the position of the camera based on magnitude in the X/Y plane. This lasts for a sepcific duration
    public IEnumerator Shake (float duration, float magnitude)
    {
        Vector3 mainPos = transform.localPosition;

        float time = 0.0f;

        while (time < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, mainPos.z);

            time += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = mainPos;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public float scrollSpeed = 5;

    void Update()
    {
        float newPos = Mathf.Repeat(Time.time * scrollSpeed, 20);
    }
}

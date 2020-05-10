using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform target;
    public Transform bg1;
    public Transform bg2;
    public float size = 20; //bglerin uzunluğu

    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3(transform.position.x, target.position.y, transform.position.z);
        transform.position = targetPos;

        //if (transform.position.y >= bg2.position.y)
        //{
            //bg1.position = new Vector3(bg1.position.x, bg2.position.y + size, bg1.position.z);
            //switchBG();
        //}
        //if (transform.position.y < bg1.position.y)
        //{
            //bg2.position = new Vector3(bg2.position.x, bg1.position.y - size, bg2.position.z);
            //switchBG();
        //}
    }
    void switchBG()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationEffect : MonoBehaviour
{
    protected GameObject player;
    protected float rot_z;
    [HideInInspector]
    public bool isDisable = false;
    public float RotationSpeed = 1;

    void Start()
    {
        player = this.gameObject;
        if(RotationSpeed >= 10)
        {
            RotationSpeed = 10;
        }
    }

    void Update()
    {
        if(isDisable == false)
        {
            Debug.Log("I'm working");
            rot_z = (Random.Range(0f, 2f) * RotationSpeed);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, rot_z), Time.deltaTime);
        }
    }
}

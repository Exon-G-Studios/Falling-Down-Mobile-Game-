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
        if(RotationSpeed >= 15) { RotationSpeed = 15; }
    }

    void Update()
    {
        if(RotationSpeed >= 15) { RotationSpeed = 15; }
        if(isDisable == false)
        {
            rot_z += (Random.Range(0f, 5f) * RotationSpeed);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, rot_z * -1f), 2);
        }
        else{
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, 0), 0.1f);
            rot_z = 0;
        }
    }
}

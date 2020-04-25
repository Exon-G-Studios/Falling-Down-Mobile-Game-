using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEffect : MonoBehaviour
{
    protected GameObject player;
    public bool isDisable = false;
    public int SpacingAmount = 1;

    void Start()
    {
        player = this.gameObject;
        if (SpacingAmount >= 10)
        {
            SpacingAmount = 10;
        }
    }

    void Update()
    {
        
    }
}

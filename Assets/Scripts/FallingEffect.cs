using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEffect : MonoBehaviour
{
    protected GameObject player;
    protected float cor_x, cor_y;
    public bool isDisable = false;
    public float SpacingAmount = 1;

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
        if(isDisable == false)
        {
            cor_x = (Random.Range(-1.3f, 1.3f) * SpacingAmount) / 65;
            cor_y = (Random.Range(-0.8f, 0.8f) * SpacingAmount) / 65;
            player.transform.Translate(cor_x, cor_y, 0);
        }
    }
}

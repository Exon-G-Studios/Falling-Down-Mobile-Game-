using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    public float speed = 5;
    public bool disableEffect = false;
    Vector2 vec;
    int slot;

    protected ParticleSystem particelSys;
    protected Animator animator;
    protected FallingEffect fallingEffect;

    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        particelSys = GetComponent<ParticleSystem>();
        particelSys.Stop(true);
        animator = this.gameObject.GetComponent<Animator>();
        fallingEffect = this.gameObject.GetComponent<FallingEffect>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        vec = new Vector2(horizontal, vertical);

        rb2d.velocity = vec * speed;


        //Partikül Sistemini Devreye Sokan Yapı
        if(vertical > 0)
        {
            animator.SetBool("isFalling", false);
            particelSys.Emit(1);
            if(disableEffect == false) { fallingEffect.isDisable = true; }
        }
        else
        {
            animator.SetBool("isFalling", true);
            if(disableEffect == true) { fallingEffect.isDisable = false; }
        }

    }
}

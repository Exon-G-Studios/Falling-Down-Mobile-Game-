using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    public float speed = 5;
    Vector2 vec;
    int slot;

    protected ParticleSystem particelSys;
    Animator animator;

    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        particelSys = GetComponent<ParticleSystem>();
        particelSys.Stop(true);
        animator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        vec = new Vector2(horizontal, vertical);

        rb2d.velocity = vec * speed;

        Debug.Log(animator.GetBool("isFalling"));

        //Partikül Sistemini Devreye Sokan Yapı
        if(vertical > 0)
        {
            animator.SetBool("isFalling", false);
            particelSys.Emit(1);
        }
        else
        {
            animator.SetBool("isFalling", true);
        }

    }
}

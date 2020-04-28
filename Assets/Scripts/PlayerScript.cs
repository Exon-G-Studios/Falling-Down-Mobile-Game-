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

    protected float horizontal, vertical;

    protected ParticleSystem particelSys;
    protected Animator animator;
    protected ShakingEffect shakingEffect;

    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        particelSys = GetComponent<ParticleSystem>();
        particelSys.Stop(true);
        animator = this.gameObject.GetComponent<Animator>();
        shakingEffect = this.gameObject.GetComponent<ShakingEffect>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical") * 1.25f;
        vec = new Vector2(horizontal, vertical);

        rb2d.velocity += (vec / 50) * speed;            //Yoktan Aklıma Gelmiş Olan Bir Velocity Formülü, Sürüklenmeyede Yarıyor
        Debug.Log(rb2d.velocity);


        //Partikül Sistemini ve Düşme Efektini Devreye Sokan Yapı
        if(vertical > 0)
        {
            animator.SetBool("isFalling", false);
            particelSys.Emit(1);
            if(disableEffect == false) { shakingEffect.isDisable = false; }
        }
        else
        {
            animator.SetBool("isFalling", true);
            if(disableEffect == true) { shakingEffect.isDisable = true; }
        }

    }
}

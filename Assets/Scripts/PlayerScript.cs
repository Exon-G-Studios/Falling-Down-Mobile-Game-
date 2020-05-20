using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerScript : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    public float speed = 5;
    public bool disableParticulSystems = false ,disableShakingEffect = false, disableRotationEffect = false;
    Vector2 vec;
    int slot;

    protected float horizontal, vertical;

    protected ParticleSystem particelSysMain;
    protected Animator animator;
    protected ShakingEffect shakingEffect;
    protected RotationEffect rotationEffect;
    public ParticleSystem[] particleSysMulti;

    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        if(GetComponent<ParticleSystem>() != null) { particelSysMain = GetComponent<ParticleSystem>(); }
        animator = this.gameObject.GetComponent<Animator>();
        if(this.gameObject.GetComponent<ShakingEffect>() != null) { shakingEffect = this.gameObject.GetComponent<ShakingEffect>(); }
        if(this.gameObject.GetComponent<RotationEffect>() != null) { rotationEffect = this.gameObject.GetComponent<RotationEffect>(); }
        try{
            particelSysMain.Stop();
            for(int particles = 0; particles < particleSysMulti.Length; particles++){
                particleSysMulti[particles].Stop();
            }
        }
        catch{}
    }

    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical") * 1.25f;
        vec = new Vector2(horizontal, vertical);

        rb2d.velocity += (vec / 5) * speed;            //Yoktan Aklıma Gelmiş Olan Bir Velocity Formülü, Sürüklenmeyede Yarıyor

        //Partikül Sistemini ve Düşme Efektini Devreye Sokan Yapı
        if(vertical > 0)
        {
            try{
                animator.SetBool("isFalling", false);
                if(disableParticulSystems != true){
                    particelSysMain.Emit(10);
                    for(int particles = 0; particles < particleSysMulti.Length; particles++){
                        particleSysMulti[particles].Emit(10);
                    }
                }
                if(disableShakingEffect == false) { shakingEffect.isDisable = false;}
                if(disableRotationEffect == false) { rotationEffect.isDisable = false;}
            }
            catch{}
        }
        else
        {
            try{
                animator.SetBool("isFalling", true);
                if(disableShakingEffect == true) { shakingEffect.isDisable = true; }
                if(disableRotationEffect == false) { rotationEffect.isDisable = true; }
            }
            catch{}
        }
    }
}

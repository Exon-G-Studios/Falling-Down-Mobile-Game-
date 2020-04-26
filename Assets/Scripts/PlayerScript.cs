using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    public float speed = 5;
    Vector2 vec;
    int slot;

    protected ParticleSystem particelSys;

    void Start()
    {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        particelSys = GetComponent<ParticleSystem>();
        particelSys.Stop(true);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        vec = new Vector2(horizontal, vertical);

        rb2d.velocity = vec * speed;

        if(vertical > 0)
        {
            particelSys.Emit(1);
        }

    }
}

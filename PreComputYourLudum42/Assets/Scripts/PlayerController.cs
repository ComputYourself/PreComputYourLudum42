using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float move;
    public float speed, jumpForce;
    private Rigidbody2D rb;

    private bool grounded;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        move = Input.GetAxis("Horizontal");

        //rb.AddForce(new Vector3(move, 0, 0) * speed, ForceMode.Impulse);


        // Changer pour un add force pour laisser le grappin marcher
        rb.velocity = new Vector3(move * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }
}

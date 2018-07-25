using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    float move;
    public float speed, jumpForce;
    private Rigidbody rb;

    private bool grounded;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        move = Input.GetAxis("Horizontal");

        //rb.AddForce(new Vector3(move, 0, 0) * speed, ForceMode.Impulse);

        rb.velocity = new Vector3(move * speed, rb.velocity.y, rb.velocity.z);

        if(Input.GetButtonDown("Jump") && grounded) rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag == "Ground") grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
    }
}

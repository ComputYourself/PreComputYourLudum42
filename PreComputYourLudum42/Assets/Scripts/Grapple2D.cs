using UnityEngine;

public class Grapple2D : MonoBehaviour {

    public float minDistance = 0.1f;
    public float force = 10f;
    [Tooltip("Things that the grapple can attach to")]
    public LayerMask grapplable;

    Vector2 GrapplePoint;
    bool isGrappled;
    Rigidbody2D rb;
    Collider2D coll;

    private void Start()
    {
        GrapplePoint = this.transform.position;
        isGrappled = false;
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("Grapple"))
        {
            isGrappled = false;
            RaycastHit2D[] hit = new RaycastHit2D[1];
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (coll.Raycast(target - this.transform.position, hit) > 0)
            {
                GrapplePoint = hit[0].point;
                isGrappled = true;
            }
        }

        if (isGrappled)
        {
            if (Vector2.Distance(this.transform.position, GrapplePoint) > minDistance)
            {
                Vector2 pos = this.transform.position;
                rb.AddForce((GrapplePoint - pos).normalized * force / Vector2.Distance(GrapplePoint,  pos), ForceMode2D.Impulse);
            }
            else
            {
                isGrappled = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(GrapplePoint, this.transform.position);
    }
}
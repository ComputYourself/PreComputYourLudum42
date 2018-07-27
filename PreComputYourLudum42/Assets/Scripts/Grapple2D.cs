using UnityEngine;


public class Grapple2D : MonoBehaviour {

    public float minDistance = 0.1f;
    public float force = 10f;
    [Tooltip("Things that the grapple can attach to")]
    public LayerMask grapplable;
    public GameObject rope;

    GameObject currentRope;
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
            Destroy(currentRope);
            RaycastHit2D[] hit = new RaycastHit2D[1];
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (coll.Raycast(target - this.transform.position, hit) > 0)
            {
                GrapplePoint = hit[0].point;
                isGrappled = true;
                currentRope = Instantiate(rope);
            }
        }

        if (isGrappled)
        {
            if (Vector2.Distance(this.transform.position, GrapplePoint) > minDistance)
            {
                Vector2 pos = this.transform.position;

                Debug.Log((GrapplePoint - pos).normalized);

                rb.AddForce((GrapplePoint - pos).normalized * force / Vector2.Distance(GrapplePoint,  pos), ForceMode2D.Impulse);
                // Pourquoi latéralement ça foire ? Plus on s'approche et plus c'est dur de s'approcher, à courte distance
                // De plus il semblerait que le grappin ne fonctionne pas du tout latértalement de manière générale
                // Le problème viens de la gestion des déplacements, idiot !

                LineRenderer line = currentRope.GetComponent<LineRenderer>();
                Vector3[] plop = { this.transform.position, GrapplePoint };
                line.SetPositions(plop);
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
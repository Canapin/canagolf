using UnityEngine;

public class Sand : MonoBehaviour
{
    private float initialDrag = 1.5f;
    public float sandDrag = 5f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("BasicBall"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 ballCenter = other.bounds.center;
                if (Physics2D.OverlapPoint(ballCenter, LayerMask.GetMask("Ground")))
                {
                    if (rb.linearDamping != sandDrag)
                    {
                        initialDrag = rb.linearDamping;
                        rb.linearDamping = sandDrag;
                    }
                }
                else
                {
                    rb.linearDamping = initialDrag;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BasicBall"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearDamping = initialDrag;
            }
        }
    }
}
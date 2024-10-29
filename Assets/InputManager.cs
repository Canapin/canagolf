using UnityEngine;

public class InputManager : MonoBehaviour
{
    public float maxLaunchForce = 100f; // Maximum possible launch force
    public float forceMultiplier = 20f;
    private bool isLaunched = false;
    private Rigidbody2D rb;
    private LineRenderer lineRenderer;

    void Start()
    {
        Time.timeScale = 1f;
        rb = GameObject.Find("BasicBall").GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (!isLaunched)
        {
            // Get current mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 ballPos = rb.position;
            Vector2 direction = (mousePos - new Vector3(ballPos.x, ballPos.y));
            
            float distance = Vector2.Distance(mousePos, ballPos);

            // Update line renderer positions
            lineRenderer.SetPosition(0, new Vector3(ballPos.x, ballPos.y, -1f));  // Set z to be above the ball
            lineRenderer.SetPosition(1, new Vector3(ballPos.x + direction.x * 0.5f, ballPos.y + direction.y * 0.5f, -1f));

            if (Input.GetMouseButtonDown(0))
            {
                LaunchBall();
            }
        }
        
        if (rb.linearVelocity.magnitude < Physics2D.bounceThreshold)
        {
            isLaunched = false;
        }
    }

    void LaunchBall()
    {
        GameObject.DestroyImmediate(lineRenderer);
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 ballPos = rb.position;
        Vector2 direction = (mousePos - new Vector3(ballPos.x, ballPos.y)).normalized;

        float distance = Vector2.Distance(mousePos, ballPos) * forceMultiplier;

        float launchForce = Mathf.Min(distance, maxLaunchForce);

        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        
        isLaunched = true;
    }
}

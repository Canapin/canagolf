using UnityEngine;
using System.Collections;

public class WallDampCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BasicBall"))
        {
            BasicBall ball = collision.gameObject.GetComponent<BasicBall>();

            if (!ball.hasCollided)
            {
                ball.hasCollided = true;
                Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                Vector2 velocity = ballRigidbody.linearVelocity;
                Debug.LogFormat("{0} collided : {1}", this.gameObject, ballRigidbody.linearVelocity);
                ballRigidbody.linearVelocity = velocity * 0.2f;
                Debug.LogFormat("{0} collided : {1}", this.gameObject, ballRigidbody.linearVelocity);
                StartCoroutine(ResetCollisionState(ball));
            }
        }
    }

    private IEnumerator ResetCollisionState(BasicBall ball)
    {
        yield return new WaitForSeconds(0.05f);
        ball.hasCollided = false;
    }
}
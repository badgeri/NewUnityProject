using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private float mMaxVelocity = 10.0f;
    private float mCurrentVelocity = 0;

    /// <summary>
    /// Gametick
    /// </summary>
    void Update ()
    {
        moveEnemyBall();
    }

    /// <summary>
    /// Move enemy ball.
    /// </summary>
    private void moveEnemyBall()
    {
        Vector3 direction = getDirectionOfClosestPlayerBall();
        CalculateAndUpdateVelocity(direction);
    }

    /// <summary>
    /// Calculate and update velocity based on a direction.
    /// </summary>
    /// <param name="direction"></param>
    private void CalculateAndUpdateVelocity(Vector3 direction)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        mCurrentVelocity = Mathf.Min(rb.velocity.magnitude + 1, mMaxVelocity);
        var angle = Mathf.Atan2(direction.z, direction.x);
        var Vz = Mathf.Sin(angle) * mCurrentVelocity;
        var Vx = Mathf.Cos(angle) * mCurrentVelocity;
        rb.velocity = new Vector3(Vx, rb.velocity.y, Vz);
    }

    /// <summary>
    /// Get direction of the closest player ball.
    /// </summary>
    /// <returns></returns>
    private Vector3 getDirectionOfClosestPlayerBall()
    {
        float closestBallDistance = float.MaxValue;
        var playerBalls = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestBall = playerBalls[0];
        foreach (GameObject playerBall in playerBalls)
        {
            float dist = Vector3.Distance(playerBall.transform.position, transform.position);
            if (dist < closestBallDistance)
            {
                closestBallDistance = dist;
                closestBall = playerBall;
            }
        }
        var heading = closestBall.transform.position - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        return direction;
    }
}

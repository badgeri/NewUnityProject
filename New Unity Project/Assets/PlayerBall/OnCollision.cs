using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle collisions for Player Ball
/// </summary>
public class OnCollision : MonoBehaviour {

    private bool mTouchingGround = false;
    private bool mTouchingPlayerBall = false;
    private bool mTouchingEnemyBall = false;

    /// <summary>
    /// Entering collision.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            setTouchingGround(true);
        }
        else if (collision.gameObject.tag == "Player")
        {
            setTouchingPlayerBall(true);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            setTouchingEnemyBall(true);
        }
	}

    /// <summary>
    /// Exit collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            setTouchingGround(false);
        }
        else if (collision.gameObject.tag == "Player")
        {
            setTouchingPlayerBall(false);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            setTouchingEnemyBall(false);
        }
    }

    /// <summary>
    /// Set touching ground - ball can only jump if it is touching the ground.
    /// </summary>
    /// <param name="isTouchingGround"></param>
    public void setTouchingGround(bool isTouchingGround)
    {
        mTouchingGround = isTouchingGround;
    }

    /// <summary>
    /// Get touching ground - ball can only jump if it is touching the ground.
    /// </summary>
    /// <returns></returns>
    public bool getTouchingGround()
    {
        return mTouchingGround;
    }

    /// <summary>
    /// Get touching enemy ball.
    /// </summary>
    /// <returns></returns>
    public bool getTouchingEnemyBall()
    {
        return mTouchingEnemyBall;
    }

    /// <summary>
    /// Set touching enemy ball.
    /// </summary>
    /// <param name="isTouchingEnemyBall"></param>
    public void setTouchingEnemyBall(bool isTouchingEnemyBall)
    {
        mTouchingEnemyBall = isTouchingEnemyBall;
    }

    /// <summary>
    /// Get touching player ball.
    /// </summary>
    /// <returns></returns>
    public bool getTouchingPlayerBall()
    {
        return mTouchingPlayerBall;
    }

    /// <summary>
    /// Set touching player ball.
    /// </summary>
    /// <param name="isTouchingPlayerBall"></param>
    public void setTouchingPlayerBall(bool isTouchingPlayerBall)
    {
        mTouchingPlayerBall = isTouchingPlayerBall;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle collisions for Player Object
/// </summary>
public class OnCollision : MonoBehaviour
{
    /// <summary>
    /// Entering collision.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        int i = 3;
        i++;
    }

    /// <summary>
    /// Exit collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
    }

}

using UnityEngine;

public static class RaycastHandler
{
    private static RaycastHit m_hitInfo_s;
    private static int lastUpdate = 0;
    public static bool Update()
    {
        if (!Valid)
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out m_hitInfo_s, 100))
                lastUpdate = Time.frameCount;
        return Valid;
    }
    public static bool Valid { get { return Time.frameCount == lastUpdate; } }
    public static RaycastHit HitInfo { get { return m_hitInfo_s; } }
}
using UnityEngine;

public static class ExtendLayerMask
{
    private static bool Updated = false;
    private static int m_GhostMask;
    private static int m_UI;
    private static int m_UnWalkable;
    private static int m_Ground;
    private static LayerMask m_NotGroundMask;

    public static int GhostMask()
    {
        return m_GhostMask;
    }
    public static int UI()
    {
        return m_UI;
    }
    public static int UnWalkable()
    {
        return m_UnWalkable;
    }
    public static int Ground()
    {
        return m_Ground;
    }
    public static bool NotGroundMask(int layer)
    {
        return (((1 << layer) & m_NotGroundMask) == 0);
    }
    public static LayerMask getNotGround()
    {
        return m_NotGroundMask;
    }

    public static void Update()
    {
        if (Updated) return;

        m_GhostMask  = LayerMask.NameToLayer("GhostMask");
        m_UI         = LayerMask.NameToLayer("UI");
        m_UnWalkable = LayerMask.NameToLayer("unWalkableMask");
        m_Ground     = LayerMask.NameToLayer("Ground");

        m_NotGroundMask = 1 << m_UI | 1 << m_UnWalkable;
        Updated = true;
    }
}

using UnityEngine;

public class ExtendLayerMask{
    public static int GhostMask { get { return LayerMask.NameToLayer("GhostMask"); } }
    public static int UI { get { return LayerMask.NameToLayer("UI"); } }
    public static int UnWalkable { get { return LayerMask.NameToLayer("unWalkableMask"); } }
    public static int Ground { get { return LayerMask.NameToLayer("Ground"); } }
}

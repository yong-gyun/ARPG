using UnityEngine;

public static partial class Extension
{
    public static void Initialized(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static Vector2 Plane02(this Vector3 vt) { return new Vector2(vt.x, vt.z); }
    public static Vector3 Plane03(this Vector3 vt) { return new Vector3(vt.x, 0f, vt.z); }
}
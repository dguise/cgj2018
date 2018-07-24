using UnityEngine;
using System.Collections;

public static class CustomExtensions
{
    public static Vector2 MaakepRotate(this Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public static Transform FindWithTagInChildren(this Transform t, string tag)
    {
        foreach (Transform child in t)
            if (child.tag == tag)
                return child;

        return null;
    }

    public static Vector3 Offset(this Vector3 vector, float x = 0, float y = 0)
    {
        vector.x += x;
        vector.y += y;
        return vector;
    }
}

//MangeMath, lets go!
//

using UnityEngine;
using System.Collections;

public static class MMath
{

    public static Vector2 DFT(int i, int length) {
        Vector2 v = new Vector2();
        v.x = Mathf.Cos(2 * i * Mathf.PI / length);
        v.y = Mathf.Sin(2 * i * Mathf.PI / length);
        return v;
    }

    public static Vector2 BeizerCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return Mathf.Pow((1 - t), 3) * p0
            + 3 * Mathf.Pow((1 - t), 2) * t * p1 
            + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 
            + Mathf.Pow(t, 3) * p3;
    }

    public static Vector2 BeizerCurveDerivative(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return 3 * Mathf.Pow((1 - t), 2) * (p1 - p0) + 6 * (1 - t) * t * (p2 - p1) + 3 * t * t * (p3 - p2);
    }

}

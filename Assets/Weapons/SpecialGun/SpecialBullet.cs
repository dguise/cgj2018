using UnityEngine;

public class SpecialBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 3;
    private static float damage = 10;
    private float life = 0;
    public float bulletRange = 2.0f;
    public float bulletSpeed = 2.0f;
    private Vector2 startPos = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private Vector2 pos2 = Vector2.zero;
    private Vector2 pos3 = Vector2.zero;
    private Vector2 pos4 = Vector2.zero;
    Vector2 vectorFromGraph;

    public AnimationCurve bulletCurve;

    private Vector2 PrevVectorFromGraph;

    public SpecialBullet() : base(speed, lifetime, damage)
    { }


    public void Update()
    {
        life += Time.deltaTime * bulletSpeed;
        var rbody = this.GetComponent<Rigidbody2D>();
        Vector2 vectorFromGraph =  new Vector2(Time.deltaTime * bulletSpeed, (bulletCurve.Evaluate((life + Time.deltaTime * bulletSpeed) / lifetime) - bulletCurve.Evaluate(life / lifetime)));
        
        vectorFromGraph = vectorFromGraph.MaakepRotate(Vector2.SignedAngle(Vector2.right, direction));
        rbody.velocity = vectorFromGraph * bulletRange * 100f;
    }


    public void SetStartAndDirectionVectorAndSpeed(Vector2 start, Vector2 dir, float aSpeed)
    {
        startPos = start;
        direction = dir;
        speed = aSpeed;

        //pos2 = Vector2Extender.Rotate(direction, -30).normalized * speed * (0.40f * lifetime);
        //pos3 = Vector2Extender.Rotate(direction, 30).normalized * speed * (0.80f * lifetime);
        //pos4 = dir * speed * lifetime;

        //startPos = start;
        //pos2 = start + Vector2.right;
        //pos3 = pos2 + Vector2.up;
        //pos4 = start + Vector2.up;
    }

    private Vector2 CatmullRomTangent(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
    {
        return 0.5f * ((-p1 + p3) + 2f * (2f * p1 - 5f * p2 + 4f * p3 - p4) * t + 3f *
                  (-p1 + 3f * p2 - 3f * p3 + p4) * Mathf.Pow(t, 2f));
    }

    private Vector2 BeizerCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return Mathf.Pow((1 - t), 3) * p0
                + 3 * Mathf.Pow((1 - t), 2) * t * p1 
                + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 
                + Mathf.Pow(t, 3) * p3;
    }

    private Vector2 BeizerCurveDerivative(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3, float t)
    {
        return 3 * Mathf.Pow((1 - t), 2) * (p1 - p0) + 6 * (1 - t) * t * (p2 - p1) + 3 * t * t * (p3 - p2);
    }

}

public static class Vector2Extender
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }

    public static Vector2 RotateVector(Vector2 v, float angle)  //Ska vara mer precis
    {
        float radian = angle * Mathf.Deg2Rad;
        float _x = v.x * Mathf.Cos(radian) - v.y * Mathf.Sin(radian);
        float _y = v.x * Mathf.Sin(radian) + v.y * Mathf.Cos(radian);
        return new Vector2(_x, _y);
    }
}


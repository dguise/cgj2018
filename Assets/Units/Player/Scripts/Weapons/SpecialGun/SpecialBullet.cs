using UnityEngine;

public class SpecialBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 3;
    private static float damage = 10;
    private float life = 0;

    private Vector2 startPos = Vector2.zero;
    private Vector2 direction = Vector2.zero;
    private Vector2 pos2 = Vector2.zero;
    private Vector2 pos3 = Vector2.zero;
    private Vector2 pos4 = Vector2.zero;

    public SpecialBullet() : base(speed, lifetime, damage)
    { }

    public void FixedUpdate()
    {
        life += Time.deltaTime;

        this.GetComponent<Rigidbody2D>().velocity = CatmullRomTangent(startPos, pos2, pos3, pos4, life / lifetime) * speed;
    }

    public void SetStartAndDirectionVectorAndSpeed(Vector2 start, Vector2 dir, float aSpeed)
    {
        startPos = start;
        direction = dir;
        speed = aSpeed;

        pos2 = Vector2Extender.Rotate(direction, -30) * speed * (0.40f * lifetime);
        pos3 = Vector2Extender.Rotate(direction, 30) * speed * (0.80f * lifetime);
        pos4 = dir * speed * lifetime;

        startPos = start;
        //pos2 = start + Vector2.right;
        //pos3 = pos2 + Vector2.up;
        //pos4 = start + Vector2.up;
    }

    private Vector2 CatmullRomTangent(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, float t)
    {
        return 0.5f * ((-p1 + p3) + 2f * (2f * p1 - 5f * p2 + 4f * p3 - p4) * t + 3f *
                  (-p1 + 3f * p2 - 3f * p3 + p4) * Mathf.Pow(t, 2f));
    }

}

public static class Vector2Extender
{

    public static Vector2 Rotate(this Vector2 v, float degrees)
    {
        return Quaternion.Euler(0, 0, degrees) * v;
    }
}


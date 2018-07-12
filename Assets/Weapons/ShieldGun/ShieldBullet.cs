﻿using UnityEngine;

public class ShieldBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 10;
    private static float damage = 10;
    private float life = 0;
    private float range = 2;

    private Vector2 direction;
    private Vector2 pos1 = Vector2.right;
    private Vector2 pos2 = Vector2.right + Vector2.up * 2;
    private Vector2 pos3 = Vector2.left + Vector2.up * 2;
    private Vector2 pos4 = Vector2.left;
    private static int sound = 5;

    public bool ShouldCirculate = true;

    Transform ownerTransform;
    //You can evaluate the curve(get the y for an x value) with:
    //float y = this.myCurve.Evaluate(x);

    public ShieldBullet() : base(speed, lifetime, damage, sound)
    { }

    public void Awake() {
		SoundManager sm = SoundManager.instance;
        sm.PlayRandomize(0.05f, Sound);
    }

    public void Start() {
        ownerTransform = Owner.GetComponent<Transform>();
    }

    public void FixedUpdate()
    {
        if (ShouldCirculate && ownerTransform != null)
        {
            life += Time.deltaTime;
            Vector2 beizerVector = Vector2.zero;

            if (life % 2 < 1) {
                beizerVector = BeizerCurve(pos1, pos2, pos3, pos4, life % 1).normalized * range;
            } else {
                beizerVector = BeizerCurve(pos4, -1 * pos3, -1 * pos2, pos1, life % 1).normalized * range;
            }

            transform.position = ownerTransform.position + new Vector3(beizerVector.x, beizerVector.y, 0);
        }
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


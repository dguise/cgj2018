using UnityEngine;

public class ImmolationBullet : Projectile
{
    private static float speed = 10;
    private static float lifetime = 10;
    private static float damage = 1;
    private float range = 1;

    private Vector2 direction;
    private Vector2 pos1 = Vector2.right;
    private Vector2 pos2 = Vector2.right + Vector2.up * 2;
    private Vector2 pos3 = Vector2.left + Vector2.up * 2;
    private Vector2 pos4 = Vector2.left;
    private static int sound = 14;

    public bool ShouldCirculate = true;

    Transform ownerTransform;
    //You can evaluate the curve(get the y for an x value) with:
    //float y = this.myCurve.Evaluate(x);

    public ImmolationBullet() : base(speed, lifetime, damage, sound)
    { }

    public void Awake() {
		//SoundManager sm = SoundManager.instance;
        //sm.PlayRandomize(0.05f, Sound);
    }

    public void Start() {
        ownerTransform = Owner.GetComponent<Transform>();
    }

    public void Rotate(Vector2 direction) {
        pos1 = pos1.MaakepRotate(Vector2.SignedAngle(Vector2.right, direction));
        pos2 = pos2.MaakepRotate(Vector2.SignedAngle(Vector2.right, direction));
        pos3 = pos3.MaakepRotate(Vector2.SignedAngle(Vector2.right, direction));
        pos4 = pos4.MaakepRotate(Vector2.SignedAngle(Vector2.right, direction));
    }

    public void FixedUpdate()
    {
        if (ShouldCirculate && ownerTransform != null)
        {
            lifetime += Time.deltaTime;
            Vector2 beizerVector = Vector2.zero;

            if (lifetime % 2 < 1) {
                beizerVector = MMath.BeizerCurve(pos1, pos2, pos3, pos4, lifetime % 1).normalized * range;
            } else {
                beizerVector = MMath.BeizerCurve(pos4, -1 * pos2, -1 * pos3, pos1, lifetime % 1).normalized * range;
            }

            transform.position = ownerTransform.position + new Vector3(beizerVector.x, beizerVector.y, 0);
        }
    }

}



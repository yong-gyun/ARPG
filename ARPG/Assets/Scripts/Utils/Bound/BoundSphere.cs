using UnityEngine;

public class BoundSphere : BoundObject
{
    public float Radius { get { return _radius;  } }
    [SerializeField] private float _radius;

    public override void Refersh()
    {
        _position = transform.position + Offset;

        float d = Radius * 2f;
        Vector3 size = new Vector3(d, d, d);
        _bounds = new Bounds(Center, size);
    }

    public override bool IsHitBox(BoundBox target)
    {
        float cx = Mathf.Clamp(Center.x, target.Min.x, target.Max.x);
        float cy = Mathf.Clamp(Center.y, target.Min.y, target.Max.y);
        float cz = Mathf.Clamp(Center.z, target.Min.z, target.Max.z);

        float dx = Center.x - cx;
        float dy = Center.y - cy;
        float dz = Center.z - cz;

        float distSq = dx * dx + dy * dy + dz * dz;
        return distSq <= Radius * Radius;
    }

    public override bool IsHitCapsule(BoundCapsule target)
    {
        Vector3 a = new Vector3(target.Center.x, target.PointTop, target.Center.z);
        Vector3 b = new Vector3(target.Center.x, target.PointBottom, target.Center.z);

        Vector3 ab = b - a;
        float abLenSq = ab.sqrMagnitude;
        float t = 0f;

        if (abLenSq > 1e-12f)
        {
            float proj = Vector3.Dot(Center - a, ab);
            t = Mathf.Clamp01(proj / abLenSq);
        }

        Vector3 q = a + ab * t;
        Vector3 diff = Center - q;
        float distSq = diff.sqrMagnitude;
        float radius = Radius + target.Radius;
        return distSq <= radius * radius;
    }

    public override bool IsHitSphere(BoundSphere target)
    {
        Vector3 ac = Center;
        Vector3 bc = target.Center;

        float dx = ac.x - bc.x;
        float dy = ac.y - bc.y;
        float dz = ac.z - bc.z;

        float distSq = dx * dx + dy * dy + dz * dz;
        float radius = Radius + target.Radius;

        return distSq <= radius * radius;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Offset, Radius);

        Gizmos.color = Color.yellow;

        float d = Radius * 2f;
        Vector3 size = new Vector3(d, d, d);
        Gizmos.DrawWireCube(transform.position + Offset, size);
    }

    protected override void Reset()
    {
        base.Reset();
        _type = Define.BoundObjectType.Sphere;
        _radius = 0.5f;
    }
#endif
}
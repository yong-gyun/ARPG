using UnityEngine;

public class BoundCapsule : BoundObject
{
    public float PointTop { get { return Center.y + Half; } }
    public float PointBottom { get { return Center.y - Half; } }

    public float Half { get { return _height * 0.5f; } }
    public float Height { get { return _height; } }
    public float Radius { get { return _radius; } }

    [SerializeField] private float _height;
    [SerializeField] private float _radius;

    public override bool IsHitCapsule(BoundCapsule target)
    {
        // 내 선분
        Vector3 a0 = new Vector3(Center.x, PointTop, Center.z);
        Vector3 a1 = new Vector3(Center.x, PointBottom, Center.z);

        // 상대 선분
        Vector3 b0 = new Vector3(target.Center.x, target.PointTop, target.Center.z);
        Vector3 b1 = new Vector3(target.Center.x, target.PointBottom, target.Center.z);

        // 두 선분 사이 최단거리 제곱 계산
        Vector3 u = a1 - a0;
        Vector3 v = b1 - b0;
        Vector3 w = a0 - b0;

        float a = Vector3.Dot(u, u); // |u|^2
        float b = Vector3.Dot(u, v);
        float c = Vector3.Dot(v, v); // |v|^2
        float d = Vector3.Dot(u, w);
        float e = Vector3.Dot(v, w);

        float denom = a * c - b * b;

        float s, t;

        if (denom < 1e-12f)
        {
            // 선분이 거의 평행한 경우
            s = 0f;
            t = Mathf.Clamp01(e / c);
        }
        else
        {
            s = Mathf.Clamp01((b * e - c * d) / denom);
            t = (b * s + e) / c;

            if (t < 0f) { t = 0f; s = Mathf.Clamp01(-d / a); }
            else if (t > 1f) { t = 1f; s = Mathf.Clamp01((b - d) / a); }
        }

        Vector3 pA = a0 + u * s;
        Vector3 pB = b0 + v * t;

        float distSq = (pA - pB).sqrMagnitude;
        float radius = Radius + target.Radius;
        return distSq <= radius * radius;
    }

    public override bool IsHitBox(BoundBox target)
    {
        return base.IsHitBox(target);
    }

    public override bool IsHitSphere(BoundSphere target)
    {
        return target.IsHitCapsule(this);
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _type = Define.BoundObjectType.Capsule;
        _radius = 0.5f;
        _height = 2f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Vector3 top = new Vector3(transform.position.x, transform.position.y + (Height * 0.5f - Radius), transform.position.z);
        Vector3 bottom = new Vector3(transform.position.x, transform.position.y - (Height * 0.5f - Radius), transform.position.z);

        Gizmos.DrawWireSphere(top, Radius);
        Gizmos.DrawWireSphere(bottom, Radius);

        Gizmos.DrawLine(top + Vector3.forward * Radius, bottom + Vector3.forward * Radius);
        Gizmos.DrawLine(top - Vector3.forward * Radius, bottom - Vector3.forward * Radius);
        Gizmos.DrawLine(top + Vector3.right * Radius, bottom + Vector3.right * Radius);
        Gizmos.DrawLine(top - Vector3.right * Radius, bottom - Vector3.right * Radius);
    }
#endif
}

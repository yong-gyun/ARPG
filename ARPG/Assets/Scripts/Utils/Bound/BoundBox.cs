using System.Drawing;
using UnityEngine;

public class BoundBox : BoundObject
{
    public Vector3 Center { get { return transform.position + _offset; } }
    public Vector3 Half { get { return _size * 0.5f; } }
    public Vector3 Min { get { return Center - Half; } }
    public Vector3 Max { get { return Center + Half; } }
    
    public Vector3 Size { get { return _size; } }
    
    [SerializeField] private Vector3 _size;
    
    private void Reset()
    {
        _type = Define.BoundObjectType.Box;
        _size = Vector3.one;
    }

    public override bool IsHitBox(BoundBox target)
    {
        return (Min.x <= target.Min.x) && (Max.x >= target.Min.x) &&
               (Min.y <= target.Min.y) && (Max.y >= target.Min.y) &&
               (Min.z <= target.Min.z) && (Max.z >= target.Min.z);
    }

    public override bool IsHitShpere(BoundShpere target)
    {
        Vector3 sphereCenter = target.transform.position + target.Offset;
        float sphereRadius = target.Radius;

        float cx = Mathf.Clamp(sphereCenter.x, Min.x, Max.x);
        float cy = Mathf.Clamp(sphereCenter.y, Min.y, Max.y);
        float cz = Mathf.Clamp(sphereCenter.z, Min.z, Max.z);

        //center - 센터 최소 값
        float dx = sphereCenter.x - cx;
        float dy = sphereCenter.y - cy;
        float dz = sphereCenter.z - cz;

        float distSq = dx * dx + dy * dy + dz * dz;
        return distSq <= sphereRadius * sphereRadius;
    }

    public override bool IsHitCapsule(BoundCapsule target)
    {
        return false;
    }

#if UNITY_EDITOR
    private readonly int[,] _edges =
    {
        {0,1},{1,3},{3,2},{2,0},
        {4,5},{5,7},{7,6},{6,4},
        {0,4},{1,5},{2,6},{3,7}
    };

    public Vector3[] GetCorners(Vector3 c, Quaternion r, Vector3 size)
    {
        Vector3 h = size * 0.5f;
        Vector3[] v =
        {
            new(-h.x,-h.y,-h.z), new(-h.x,-h.y, h.z),
            new(-h.x, h.y,-h.z), new(-h.x, h.y, h.z),
            new( h.x,-h.y,-h.z), new( h.x,-h.y, h.z),
            new( h.x, h.y,-h.z), new( h.x, h.y, h.z),
        };

        Vector3[] result = new Vector3[v.Length];
        for (int i = 0; i < 8; i++)
            result[i] = c + r * v[i];

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.blue;

        Vector3 center = transform.position + _offset;
        var corners = GetCorners(center, Quaternion.identity, _size);

        for (int i = 0; i < 12; i++)
        {
            int a = _edges[i, 0];
            int b = _edges[i, 1];
            Gizmos.DrawLine(corners[a], corners[b]);
        }
    }
#endif
}

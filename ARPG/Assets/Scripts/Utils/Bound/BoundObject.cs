using UnityEngine;

public class BoundObject : MonoBehaviour
{
    public Define.BoundObjectType Type { get { return _type; } }
    [SerializeField] protected Define.BoundObjectType _type;

    public LayerMask LayerMask { get { return _layerMask; } }
    [SerializeField] protected LayerMask _layerMask;

    public Vector3 Offset { get { return _offset; } }
    [SerializeField] protected Vector3 _offset;

    public Bounds Bounds { get { return _bounds; } }
    protected Bounds _bounds = new Bounds(Vector3.zero, Vector3.one);

    public Vector3 Position { get { return _position; } }
    protected Vector3 _position;

    public Vector3 Center { get { return transform.position + _offset; } }
    
    public bool IsHit(BoundObject target)
    {
        var targetType = target.Type;
        switch (targetType)
        {
            case Define.BoundObjectType.Box: return IsHitBox((BoundBox)target);
            case Define.BoundObjectType.Sphere: return IsHitSphere((BoundSphere)target);
            case Define.BoundObjectType.Capsule: return IsHitCapsule((BoundCapsule)target);
        }

        return false;
    }

    public virtual void Refersh() { _position = transform.position + transform.rotation * Offset; }
    public virtual bool IsHitBox(BoundBox target) { return false; }
    public virtual bool IsHitSphere(BoundSphere target) { return false; }
    public virtual bool IsHitCapsule(BoundCapsule target) { return false; }

    public virtual bool IsHitBound(Bounds a, Bounds b)
    {
        return (a.min.x <= b.min.x) && (a.max.x >= b.min.x) &&
               (a.min.y <= b.min.y) && (a.max.y >= b.min.y) &&
               (a.min.z <= b.min.z) && (a.max.z >= b.min.z);
    }

    protected virtual void Reset()
    {

    }
}

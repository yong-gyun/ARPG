using UnityEngine;

public class BoundObject : MonoBehaviour
{
    public Define.BoundObjectType Type { get { return _type; } }
    [SerializeField] protected Define.BoundObjectType _type;

    public LayerMask LayerMask { get { return _layerMask; } }
    [SerializeField] protected LayerMask _layerMask;

    public Vector3 Offset { get { return _offset; } }
    [SerializeField] protected Vector3 _offset;

    public virtual bool IsHit(BoundObject target)
    {
        var targetType = target.Type;
        switch (targetType)
        {
            case Define.BoundObjectType.Box: return IsHitBox((BoundBox)target);
            case Define.BoundObjectType.Sphere: return IsHitShpere((BoundShpere)target);
            case Define.BoundObjectType.Capsule: return IsHitCapsule((BoundCapsule)target);
        }

        return false;
    }

    public virtual bool IsHitBox(BoundBox target) { return false; }
    public virtual bool IsHitShpere(BoundShpere target) { return false; }
    public virtual bool IsHitCapsule(BoundCapsule target) { return false; }
}

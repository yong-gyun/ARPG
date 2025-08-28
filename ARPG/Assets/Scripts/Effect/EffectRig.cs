using UnityEngine;

public class EffectRig : MonoBehaviour
{
    public Effect Owner { get { return _owner; } }
    public Define.EffectRigType EffectRigType { get { return _effectRigType; } }

    [SerializeField] private Define.EffectRigType _effectRigType;

    [SerializeField] private HumanBodyBones _boneType;
    private Effect _owner;

    [SerializeField] private float _offset;
    [SerializeField] private float _height;

    public Transform TargetTranform
    {
        get
        {
            switch (_effectRigType)
            {
                case Define.EffectRigType.Owner:
                case Define.EffectRigType.OwnerLink:    return _owner.transform ?? null;
                case Define.EffectRigType.OwnerBone:    return _owner.Owner.GetBone(_boneType) ?? null;
                case Define.EffectRigType.Target:
                case Define.EffectRigType.TargetLink:   return _owner.Target.transform ?? null;
                case Define.EffectRigType.TargetBone:   return _owner.Target.GetBone(_boneType) ?? null;
            }

            return null;
        }
    }

    public void SetInfo(Effect owner)
    {
        _owner = owner;
    }

    public void Initialized()
    {
        Transform target = TargetTranform;
        if (target == null)
            return;

        switch (_effectRigType)
        {
            case Define.EffectRigType.Owner:
            case Define.EffectRigType.Target:
                {
                    Vector3 position = target.transform.position + transform.transform.forward * _offset;
                    position = position.Plane03();
                    position.y += _height;

                    transform.localPosition = position;
                    transform.localRotation = target.rotation;
                }
                break;
            case Define.EffectRigType.OwnerLink:
            case Define.EffectRigType.TargetLink:
                {
                    transform.SetParent(target.transform, false);
                    transform.Initialized();
                    _owner.externalRigs.Add(this);
                }
                break;
            case Define.EffectRigType.OwnerBone:
                {
                    _owner.Owner.transform.SetParent(target);
                }
                break;
            case Define.EffectRigType.TargetBone:
                {
                    transform.SetParent(target);
                    _owner.externalRigs.Add(this);
                }
                break;
        }
    }
}
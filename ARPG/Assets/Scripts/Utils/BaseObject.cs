using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseObject : MonoBehaviour
{
    public Define.ObjectType ObjcetType { get { return _objectType; } }
    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    public bool LockGravity { get; set; }
    public bool IsInitialized { get { return _init; } }
    public int TemplateID { get; protected set; }

    [SerializeField] protected Define.ObjectType _objectType;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected GameObject _model;
    [SerializeField] protected Vector3 _dir;

    protected float _elapsed = 0f;
    protected bool _init;

    public virtual bool Initialized()
    {
        if (_init == true)
            return false;

        _init = true;
        return true;
    }

    public virtual void SetInfo(int templateID)
    {
        TemplateID = templateID;
    }

    public abstract UniTask SetObject();

    protected virtual void Update()
    {
        OnUpdate(Managers.Time.DeltaTime);
    }

    public void SetAnimation(string animationName, float duration = 0.1f, int layer = 0, float normalizedTimeOffset = 0.1f)
    {
        _anim.CrossFade(animationName, duration, layer, normalizedTimeOffset);
        Debug.Log($"Set animation {animationName}");
    }
    
    protected virtual void OnUpdate(float deltaTime)
    {
        _elapsed += deltaTime;
    }
}

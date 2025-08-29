using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IObject
{
    public UniTask SetObject();
}

public abstract class BaseObject : MonoBehaviour
{
    public Define.ObjectType ObjcetType { get { return _objectType; } }
    public Vector3 Dir { get { return _dir.normalized; } set { _dir = value; } }
    public EventActionRunner Runner { get { return _runner; } }
    public int TemplateID { get; protected set; }
    public float Elapsed { get { return _elapsed; } }

    [SerializeField] protected Define.ObjectType _objectType;
    
    [SerializeField] protected Vector3 _dir;

    protected float _elapsed = 0f;
    protected bool _init;

    protected EventActionRunner _runner;

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

    protected virtual void Update()
    {
        OnUpdate(Managers.Time.DeltaTime);
    }
    
    protected virtual void OnUpdate(float deltaTime)
    {
        _elapsed += deltaTime;
    }
}

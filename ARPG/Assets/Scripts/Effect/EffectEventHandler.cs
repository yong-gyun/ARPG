using UniRx;
using UnityEngine;

public class EffectEventHandler : MonoBehaviour
{
    public Subject<Unit> OnEnterEffectEvent { get; private set; } = new Subject<Unit>();

    public Subject<Unit> OnUpdateEffectEvent { get; private set; } = new Subject<Unit>();

    public Subject<Unit> OnExitEffectEvent { get; private set; } = new Subject<Unit>();


}

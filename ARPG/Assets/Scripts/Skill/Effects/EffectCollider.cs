using System.Collections.Generic.Serialized;
using UnityEngine;
using UniRx;
using System.Linq;

public class EffectCollider : EffectBase
{
    [SerializeField] private ColliderEventHandler _eventHandler;

    [SerializeField] private SerializedDictionary<Define.ColliderEventType, EffectBase> _eventEffectList = new SerializedDictionary<Define.ColliderEventType, EffectBase>();        //충돌 후 발생할 이벤트 이펙트들

    public override void Start()
    {
        _eventHandler.OnTriggerEnterCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Enter)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Start();
                }
            }
        });

        _eventHandler.OnTriggerStayCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Stay)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Update();
                }
            }
        });

        _eventHandler.OnTriggerExitCallback.Subscribe(others =>
        {
            var targets = others.GetComponents<Creature>().ToList();
            foreach ((Define.ColliderEventType key, EffectBase value) mit in _eventEffectList)
            {
                if (mit.key == Define.ColliderEventType.Exit)
                {
                    if (mit.value is EffectSkillBase)
                    {
                        EffectSkillBase es = mit.value as EffectSkillBase;
                        es.SetTargets(targets, _parent.Owner);
                    }

                    mit.value.Destory();
                }
            }
        });
    }

    public override void Destory()
    {
        _eventHandler.Clear();
    }
}
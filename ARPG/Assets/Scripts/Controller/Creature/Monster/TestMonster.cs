using UnityEngine;

public class TestMonster : Creature
{
    public override void ChangeState(Define.CreatureState state)
    {
        switch (state)
        {
            case Define.CreatureState.Idle:
                break;
            case Define.CreatureState.Hit:
                break;
            case Define.CreatureState.Dead:
                Destroy(gameObject);
                break;
        }
    }
}

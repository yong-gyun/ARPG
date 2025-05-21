using Cysharp.Threading.Tasks;
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

#if UNITY_EDITOR

    [Button("ĳ���� ���� ����")]
    public void SetCharacterStat()
    {
        CreatureType = Define.CreatureType.Monster;
        SetStat(_templateID);
        Debug.Log($"ĳ���� ���� ���� �Ϸ�: {_templateID}");
    }
#endif

    [Button("ü�� 500 ȸ��")]
    public void HealMonster()
    {

    }
}

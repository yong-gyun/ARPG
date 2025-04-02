using Creatures.Statemachine;
using UnityEngine;

public class Hunter : Creature
{
    public enum ESTATE
    {
        IDLE,
        MOVE,
        SKILL,
        HIT,
        DASH,
        DEAD,
    }

    private HunterStatemachine _statemachine = new HunterStatemachine();

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _statemachine.Init(this);
        return true;
    }

    private void Update()
    {
        _statemachine.Update();
    }
}

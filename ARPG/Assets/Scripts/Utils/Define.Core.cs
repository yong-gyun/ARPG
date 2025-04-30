using UnityEngine;

public partial class Define
{
    public enum SceneType
    {
        StartScene,
        GameScene
    }

    public enum CreatureType
    {
        Hunter = 1,
        Monster = 2,
        Boss = 3,
    }

    public enum InputState
    {
        Down,
        Pressed,
        Up
    }

    public enum InputEnv
    {
        Mouse,
        Keyboard,
    }

    public enum KeyID
    {
        None = 0,               //없음
        MoveForward = 1,        //정면 이동
        MoveBack = 2,           //뒤로 이동
        MoveRight = 3,          //오른쪽으로 이동
        MoveLeft = 4,           //왼쪽으로 이동
        Dash = 5,               //대쉬
        NormalAttack = 6,       //기본 공격
        NormalSkill_1 = 7,      //스킬 1
        NormalSkill_2 = 8,      //스킬 2
        BreakSkill = 9,         //브레이크 스킬
        UltSkill = 10,          //궁극기
    }
}
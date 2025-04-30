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
        None = 0,               //����
        MoveForward = 1,        //���� �̵�
        MoveBack = 2,           //�ڷ� �̵�
        MoveRight = 3,          //���������� �̵�
        MoveLeft = 4,           //�������� �̵�
        Dash = 5,               //�뽬
        NormalAttack = 6,       //�⺻ ����
        NormalSkill_1 = 7,      //��ų 1
        NormalSkill_2 = 8,      //��ų 2
        BreakSkill = 9,         //�극��ũ ��ų
        UltSkill = 10,          //�ñر�
    }
}
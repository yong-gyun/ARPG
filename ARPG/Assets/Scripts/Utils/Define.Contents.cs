public partial class Define
{
    public enum ConstDefType
    {
        RunSpeed = 1,           //�̵��ӵ� * �޸��� �ӵ�      
        DashDistance = 2,       //�뽬 �Ÿ�
        DashTime = 3,           //�뽬 �ð�
        HunterMoveSpeed = 4,    //���� �̵� �ӵ�
    }

    public enum ClassType
    {
        None,
        Assassin,       //�ϻ���
        Tanker,         //��Ŀ
        Fighter,        //���� ���� ĳ��
        Mage,           //������
        Ranger,         //�ü�
        Healer          //����
    }

    public enum ColliderEventType
    {
        Enter,
        Stay,
        Exit
    }

    public enum OverClockStatType
    {
        Str,        //�ٷ�: �߰� ���ݷ�
        Int,        //����: ��ų �� + ���� ����
        Dex,        //��ø: ũ�� ���� + ũȮ ����
        Reg,        //���׷�: �߰� ü�� + �߰� ����
        Pet,        //����: ���� ����
    }

    public enum StatType
    {
        Hp,                 //ü��
        Mp,                 //����
        Attack,             //���ݷ�
        Defense,            //����
        CooltimeDown,       //��Ÿ�� ����
        CriticalPercent,    //ũ��Ƽ�� Ȯ��
        CriticalDamage,     //ũ��Ƽ�� �����
        Penetration,        //���� ����
    }

    public enum EffectType
    {
        Damage,
        Damage_Fixed,
        Dot,
        Buff,
        Debuff,
    }

    public enum DamageType
    {
        None,
        Damage,
        Dot,
    }

    public enum PopupType
    {

    }

    public enum SkillType
    {
        Passive = 1,
        Combat_Attack_1 = 2,
        Combat_Attack_2 = 3,
        Combat_Attack_3 = 4,
        Combat_Attack_4 = 5,
        NormalSkill_1 = 6,      
        NormalSkill_2 = 7,      
        BreakSkill = 8,         
        UltSkill = 9,           
    }

    public enum CreatureState
    {
        Idle,
        Move,
        Skill,
        Hit,
        Dead
    }
}
public partial class Define
{
    public enum ConstDefType
    {
        RunSpeed = 1,       //�̵��ӵ� * �޸��� �ӵ�      
        DashDistance,       //�뽬 �Ÿ�
        DashTime,           //�뽬 �ð�
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
    }

    public enum PopupType
    {

    }

    public enum SkillType
    {
        Passive = 1,
        Attack = 2,
        NormalSkill_1 = 3,      
        NormalSkill_2 = 4,      
        BreakSkill = 5,         
        UltSkill = 6,           
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
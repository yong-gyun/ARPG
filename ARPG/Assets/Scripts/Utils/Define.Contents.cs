public partial class Define
{
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
        Abs,            //���밪
        Percent,        //���
    }

    public enum PopupType
    {

    }
}

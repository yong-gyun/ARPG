public partial class Define
{
    public enum ClassType
    {
        None,
        Assassin,       //암살자
        Tanker,         //탱커
        Fighter,        //근접 공격 캐릭
        Mage,           //마법사
        Ranger,         //궁수
        Healer          //힐러
    }

    public enum OverClockStatType
    {
        Str,        //근력: 추가 공격력
        Int,        //지능: 스킬 쿨감 + 마력 증가
        Dex,        //민첩: 크댐 증가 + 크확 증가
        Reg,        //저항력: 추가 체력 + 추가 방어력
        Pet,        //정밀: 방어력 관통
    }

    public enum StatType
    {
        Hp,                 //체력
        Mp,                 //마력
        Attack,             //공격력
        Defense,            //방어력
        CooltimeDown,       //쿨타임 감소
        CriticalPercent,    //크리티컬 확률
        CriticalDamage,     //크리티컬 대미지
        Penetration,        //방어력 관통
    }

    public enum EffectType
    {
        Abs,            //절대값
        Percent,        //계수
    }

    public enum PopupType
    {

    }
}

public partial class Define
{
    public enum ConstDefType
    {
        RunSpeed = 1,           //이동속도 * 달리기 속도      
        DashDistance = 2,       //대쉬 거리
        DashTime = 3,           //대쉬 시간
        HunterMoveSpeed = 4,    //헌터 이동 속도
    }

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

    public enum ColliderEventType
    {
        Enter,
        Stay,
        Exit
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
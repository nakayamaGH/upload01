namespace HellRoad
{
    public abstract class UsableSkill
    {
        public abstract UsableSkillID ID { get;  }
        public abstract int CD { get; }

        public BattleAction Action { get => new BattleAction(Play); }

        public abstract void Play(BattleActionArgs args);
    }
}
namespace HellRoad
{
    public enum When
    {
        OnPhysicAttack,
        OnMagicAttack,
        OnAttackTo,
        OnAttackedBy,
        OnDamage,
        OnHealed,
        OnDead,
        OnTurnEnd,
        OnAddSkill,
        OnRemoveBuff,

    }

    public enum Who
    {
        Me,
        Enemy,
    }

    public enum Players
    {
        Me,
        Enemy,
        NULL,
	}

    public static class WhoExtension
    {
        public static Who GetEnemy(this Who who)
        {
            switch(who)
            {
                case Who.Enemy:
                    return Who.Me;
                case Who.Me:
                    return Who.Enemy;
			}
            return Who.Enemy;
		}
	}

    public static class PlayersExtension
    {
        public static Players GetEnemy(this Players who)
        {
            switch (who)
            {
                case Players.Enemy:
                    return Players.Me;
                case Players.Me:
                    return Players.Enemy;
            }
            return Players.Enemy;
        }

        public static int ToDirection(this Players players)
        {
            switch (players)
            {
                case Players.Enemy:
                    return 1;
                case Players.Me:
                    return -1;
            }
            return 0;
        }
    }
}
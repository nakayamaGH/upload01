namespace HellRoad
{
    public enum TurnActionType
    {
        Attack              = 0,
        UseSkill_0          = 10,
        UseSkill_1          = 11,
        UseSkill_2          = 12,
        UseSkill_3          = 13,
        UseSkill_4          = 14,
        UseSkill_5          = 15,
        UseSkill_6          = 16,
        UseSkill_7          = 17,
        ChangeFontChara_Left   = 100,
        ChangeFontChara_Right   = 101,
        ExMethod            = 999,
    }

    static class TurnActionTypeExtension
    {
        public static TurnActionType UseSkill(this TurnActionType type, int skillId)
        {
            switch (skillId)
            {
                case 0:
                    return TurnActionType.UseSkill_0;
                case 1:
                    return TurnActionType.UseSkill_1;
                case 2:
                    return TurnActionType.UseSkill_2;
                case 3:
                    return TurnActionType.UseSkill_3;
                case 4:
                    return TurnActionType.UseSkill_4;
                case 5:
                    return TurnActionType.UseSkill_5;
                case 6:
                    return TurnActionType.UseSkill_6;
                case 7:
                    return TurnActionType.UseSkill_7;
            }
            return TurnActionType.UseSkill_7;
        }

        public static bool ConsumeTurns(this TurnActionType type)
        {
            if (type >= TurnActionType.UseSkill_0 && type <= TurnActionType.UseSkill_7)
            {
                return true;
            }
            if (type == TurnActionType.ChangeFontChara_Left || type == TurnActionType.ChangeFontChara_Right)
            {
                return false;
            }
            if (type == TurnActionType.Attack)
            {
                return true;
            }
            return true;
        }

        public static string GetInfo(this TurnActionType type)
        {
            switch(type)
            {
                case TurnActionType.Attack:
                    return "基本攻撃";
                case TurnActionType.ChangeFontChara_Left:
                    return "先頭の肉体を交代します（左方向）";
                case TurnActionType.ChangeFontChara_Right:
                    return "先頭の肉体を交代します（右方向）";
            }
            return null;
        }
    }
}
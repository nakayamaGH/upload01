namespace HellRoad
{
    public enum PartsBagLevel
    {
        Level_1 = 0,
        Level_2 = 1,
        Level_3 = 2,
        Level_Max = 3,
    }

    static class PartsBagLevelExtension
    {
        public static int UpgradeCost(this PartsBagLevel level)
        {
            switch (level)
            {
                case PartsBagLevel.Level_1:
                    return 10;
                case PartsBagLevel.Level_2:
                    return 50;
                case PartsBagLevel.Level_3:
                    return 200;
            }
            return 0;
        }

        public static int HeldUpCount(this PartsBagLevel level)
        {
            switch (level)
            {
                case PartsBagLevel.Level_1:
                    return 3;
                case PartsBagLevel.Level_2:
                    return 4;
                case PartsBagLevel.Level_3:
                    return 5;
                case PartsBagLevel.Level_Max:
                    return 6;
            }
            return 0;
        }
    }
}
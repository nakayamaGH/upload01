namespace HellRoad
{
    public enum StatusParamType
    {
        MaxHP,
        HP,
        PPow,
        PDef,
        MPow,
        MDef,
        Speed,
    }

    public enum PhysicOrMagic
    {
        Physic,
        Magic
    }

    static class StatusParamToString
    {
        public static string ToNameString(this StatusParamType param)
        {
            switch(param)
            {
                case StatusParamType.MaxHP:
                    return "MaxHP";
                case StatusParamType.HP:
                    return "HP";
                case StatusParamType.PPow:
                    return "ï®çU";
                case StatusParamType.PDef:
                    return "ï®ñh";
                case StatusParamType.MPow:
                    return "ñÇçU";
                case StatusParamType.MDef:
                    return "ñÇñh";
                case StatusParamType.Speed:
                    return "ë¨ìx";
            }
            return null;
        }
    }
    static class PhysicOrMagicToString
    {
        public static string ToString(this PhysicOrMagic pOrM)
        {
            switch (pOrM)
            {
                case PhysicOrMagic.Physic:
                    return "ï®óù";
                case PhysicOrMagic.Magic:
                    return "ñÇñ@";
            }
            return null;
        }
    }
}
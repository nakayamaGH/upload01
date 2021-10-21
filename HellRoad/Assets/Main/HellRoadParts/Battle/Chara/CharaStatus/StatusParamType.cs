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
                    return "���U";
                case StatusParamType.PDef:
                    return "���h";
                case StatusParamType.MPow:
                    return "���U";
                case StatusParamType.MDef:
                    return "���h";
                case StatusParamType.Speed:
                    return "���x";
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
                    return "����";
                case PhysicOrMagic.Magic:
                    return "���@";
            }
            return null;
        }
    }
}
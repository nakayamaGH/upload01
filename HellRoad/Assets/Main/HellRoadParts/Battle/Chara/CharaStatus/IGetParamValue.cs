namespace HellRoad
{
    public interface IGetParamValue
    {
        public long GetValue(StatusParamType type);
        public long GetPower(PhysicOrMagic physicOrMagic);
        public long GetDefense(PhysicOrMagic physicOrMagic);
    }
}
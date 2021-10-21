namespace HellRoad
{
    [System.Serializable]
    public class StatusParam
    {
        [UnityEngine.SerializeField] StatusParamType type = StatusParamType.MaxHP;
        [UnityEngine.SerializeField] long value = 0;

        public long Value
        {
            get => value; 
            private set => this.value = value; 
        }
        public StatusParamType Type 
        { 
            get => type;
            private set => type = value; 
        }

        public StatusParam(StatusParamType type, long value = 0)
        {
            Type = type;
            Value = value;
        }

        public void SetValue(long value)
        {
            Value = value;
        }

        public void AddValue(long value)
        {
            Value += value;
        }

        public void SubValue(long value)
        {
            Value -= value;
        }
    }
}
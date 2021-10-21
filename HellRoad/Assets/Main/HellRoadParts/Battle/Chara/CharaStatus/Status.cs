using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace HellRoad
{
    [Serializable]
    public class Status : IGetParamValue
    {
        [SerializeField] private List<StatusParam> parameters = new List<StatusParam>();

        public ReadOnlyCollection<StatusParam> GetParameters => parameters.AsReadOnly();

        public event Action<long, StatusParam> OnChangeParam;
        
        private bool canRemoveParams = true;

        public Status(bool haveBasicStatus = false, bool canRemoveParams = true)
        {
            this.canRemoveParams = canRemoveParams;
            if (haveBasicStatus)
            {
                AddValue(StatusParamType.MaxHP, 0);
                AddValue(StatusParamType.HP, 0);
                AddValue(StatusParamType.PPow, 0);
                AddValue(StatusParamType.PDef, 0);
                AddValue(StatusParamType.MPow, 0);
                AddValue(StatusParamType.MDef, 0);
                AddValue(StatusParamType.Speed, 0);
            }
		}

        private StatusParam GetParam(StatusParamType type)
        {
            return parameters.Find(x => x.Type == type);
        }

        public void SetValue(StatusParamType type, long value)
        {
            StatusParam param = GetParam(type);
            long beforeValue = 0;
            if (param != null)
            {
                beforeValue = param.Value;
                if (value == 0)
                    parameters.Remove(param);
                else
                    param.SetValue(value);
            }
            else
            {
                parameters.Add(new StatusParam(type, value));
            }
            OnChangeParam?.Invoke(beforeValue, GetParam(type));
        }

        public void AddValue(StatusParamType type, long value)
        {
            StatusParam param = GetParam(type);
            long beforeValue = 0;
            if (param != null)
            {
                beforeValue = param.Value;
                param.AddValue(value);
                if(param.Value == 0 && canRemoveParams)
                    parameters.Remove(param);
            }
            else
            {
                parameters.Add(new StatusParam(type, value));
            }
            OnChangeParam?.Invoke(beforeValue, GetParam(type));
        }

        public void SubValue(StatusParamType type, long value)
        {
            StatusParam param = GetParam(type);
            long beforeValue = 0;
            if (param != null)
            {
                beforeValue = param.Value;
                param.SubValue(value);
                if (param.Value == 0 && canRemoveParams)
                    parameters.Remove(param);
            }
            else
            {
                parameters.Add(new StatusParam(type, -value));
            }
            OnChangeParam?.Invoke(beforeValue, GetParam(type));
        }

        public long GetValue(StatusParamType type)
        {
            StatusParam param = GetParam(type);
            if (param == null) return 0;
            return param.Value;
        }

        public long GetPower(PhysicOrMagic physicOrMagic)
        {
            switch (physicOrMagic)
            {
                case PhysicOrMagic.Physic:
                    return GetValue(StatusParamType.PPow);
                case PhysicOrMagic.Magic:
                    return GetValue(StatusParamType.MPow);
            }
            return 0;
        }

        public long GetDefense(PhysicOrMagic physicOrMagic)
        {
            switch (physicOrMagic)
            {
                case PhysicOrMagic.Physic:
                    return GetValue(StatusParamType.PDef);
                case PhysicOrMagic.Magic:
                    return GetValue(StatusParamType.MDef);
            }
            return 0;
        }

        public static Status operator +(Status s1, Status s2)
        {
            foreach (StatusParam param2 in s2.parameters)
                s1.AddValue(param2.Type, param2.Value);
            return s1;
        }

        public static Status operator -(Status s1, Status s2)
        {
            foreach (StatusParam param2 in s2.parameters)
                s1.SubValue(param2.Type, param2.Value);
            return s1;
        }
    }
}
using UnityEngine;

namespace HellRoad.External
{
    public class CharaStatusView : MonoBehaviour, ICharaStatusView
    {
        [SerializeField] StatusParamType[] typeOrder = null;
        [SerializeField] StatusParamView[] paramViews = null;

        public void SetValue(StatusParamType type, long value)
        {
            int idx = IndexOf(type);
            if (idx == -1) return;
            paramViews[idx].ChangeValue(value);
		}

        public void SetValues(IGetParamValue status)
        {
            foreach(StatusParamType type in typeOrder)
            {
                SetValue(type, status.GetValue(type));
            }
        }

        private int IndexOf(StatusParamType type)
        {
            for(int i = 0;i < typeOrder.Length ;i++)
            {
                if (typeOrder[i] == type) 
                return i;
			}
            return -1;
		}
    }

    public interface ICharaStatusView
    {
        void SetValue(StatusParamType type, long value);
        void SetValues(IGetParamValue status);
    }
}
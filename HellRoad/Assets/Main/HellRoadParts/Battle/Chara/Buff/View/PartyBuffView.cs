using UnityEngine;

namespace HellRoad.External
{
    public class PartyBuffView : MonoBehaviour, IPartyBuffView
    {
        [SerializeField] private CharaBuffView[] buffs = null;

        public void ShowBuff(int idx, string name, int duration)
        {
            buffs[idx].ShowBuff(name, duration);
        }

        public void ValidateDuration(int idx, int duration)
        {
            buffs[idx].ValidateDuration(duration);
        }

        public void RemoveBuff(int idx)
        {
            buffs[idx].RemoveBuff();
        }
    }

    public interface IPartyBuffView
    {
        void ShowBuff(int idx, string name, int duration);
        void ValidateDuration(int idx, int duration);
        void RemoveBuff(int idx);
    }
}
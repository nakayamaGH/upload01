using System;

namespace HellRoad
{
    public class SoulFragmentWallet : IAddReduceSoulFragment, IAddReduceSoulFragmentEventListener
    {
        public int HaveFragments { get; private set; }

        public event Action<int> OnChangeValueAction;

        public SoulFragmentWallet(int fragments)
        {
            Add(fragments);
        }

        public void Add(int fragments)
        {
            HaveFragments += fragments;
            OnChangeValueAction?.Invoke(HaveFragments);
        }

        public void Reduce(int fragments)
        {
            HaveFragments -= fragments;
            OnChangeValueAction?.Invoke(HaveFragments);
        }

        public bool Contains(int fragments)
        {
            return HaveFragments >= fragments;
        }

        public int GetHaveFragments()
        {
            return HaveFragments;
        }
    }
}
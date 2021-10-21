using System;

namespace HellRoad
{
    public interface IAddReduceSoulFragment : IGetSoulFragment
    {
        void Add(int fragments);
        void Reduce(int fragments);
    }

    public interface IGetSoulFragment
    {
        int GetHaveFragments();
        bool Contains(int fragments);
    }

    public interface IAddReduceSoulFragmentEventListener
    {
        event Action<int> OnChangeValueAction;
    }
}
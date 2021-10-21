using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class SortingPartyView : MonoBehaviour
    {
        private ISortParty sortingChara;
        private int idx_1;
        private int idx_2;

        private int nowSequence;

        private void Awake()
        {
            sortingChara = Locater<ISortParty>.Resolve();
        }

        private void OnEnable()
        {
            idx_1 = 0;
            idx_2 = 0;
            nowSequence = 0;
        }

        //Test
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                DecideSortChara(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                DecideSortChara(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DecideSortChara(2);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                DecideSortChara(3);
            }
            if(Input.GetKeyDown(KeyCode.M))
            {
                sortingChara.Shift(Direction.Right);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                sortingChara.Shift(Direction.Left);
            }
        }

        public void Sort()
        {
            sortingChara.Sort(idx_1, idx_2);
        }

        public void DecideSortChara(int idx)
        {
            if (nowSequence == 1)
            {
                idx_2 = idx;
                nowSequence--;
                Sort();
            }
            else
            {
                idx_1 = idx;
                nowSequence++;
            }
        }
    }
}
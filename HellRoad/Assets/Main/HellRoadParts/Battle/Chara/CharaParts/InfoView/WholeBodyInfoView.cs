using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HellRoad.External
{
    public class WholeBodyInfoView : MonoBehaviour
    {
        [SerializeField] PartsInfoView headInfoView = null;
        [SerializeField] PartsInfoView bodyInfoView = null;
        [SerializeField] PartsInfoView armsInfoView = null;
        [SerializeField] PartsInfoView legsInfoView = null;

        public void ShowInfo(PartsID head, PartsID body, PartsID arms, PartsID legs)
        {
            headInfoView.Show(head);
            bodyInfoView.Show(body);
            armsInfoView.Show(arms);
            legsInfoView.Show(legs);
        }
    }
}
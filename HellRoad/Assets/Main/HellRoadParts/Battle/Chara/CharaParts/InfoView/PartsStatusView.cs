using System.Collections.ObjectModel;
using UnityEngine;

namespace HellRoad.External
{
    public class PartsStatusView : MonoBehaviour
    {
        [SerializeField] StatusParamView[] paramViews = null;

        public void Show(ReadOnlyCollection<StatusParam> statusParams)
        {
            for(int i = 0; i < paramViews.Length;i++)
            {
                StatusParamView view = paramViews[i];
                if (statusParams.Count > i)
                {
                    view.ChangeIcon(statusParams[i].Type);
                    view.ChangeValue(statusParams[i].Value);
                    if(!view.gameObject.activeSelf) 
                        view.gameObject.SetActive(true);
                }
                else
                {
                    if (view.gameObject.activeSelf)
                        view.gameObject.SetActive(false);
                }
            }
        }
    }
}
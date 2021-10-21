using UnityEngine;
using UnityEngine.Events;
using Utility;
using Utility.Audio;

namespace Menu
{
    public class MenuChild : MonoBehaviour
    {
        [SerializeField] private GameObject selectedObject = null;
        [SerializeField] private UnityEvent selectedAction = null;
        [SerializeField] private UnityEvent diselectedAction = null;
        [SerializeField] private UnityEvent decidedAction = null;
        [SerializeField] private UnityEvent canceledAction = null;

        [SerializeField] string diselectSE = "DiselectMenu";
        [SerializeField] string decideSE = "DecideMenu";
        [SerializeField] string cancelSE = "CancelMenu";

        public void SelectedActionAddListener(UnityAction ev) => selectedAction.AddListener(ev);
        public void DiselectedActionAddListener(UnityAction ev) => diselectedAction.AddListener(ev);
        public void DecidedActionAddListener(UnityAction ev) => decidedAction.AddListener(ev);
        public void CanceledActionAddListener(UnityAction ev) => canceledAction.AddListener(ev);

        public void SelectedActionRemoveListener(UnityAction ev) => selectedAction.RemoveListener(ev);
        public void DiselectedActionRemoveListener(UnityAction ev) => diselectedAction.RemoveListener(ev);
        public void DecidedActionRemoveListener(UnityAction ev) => decidedAction.RemoveListener(ev);
        public void CanceledActionRemoveListener(UnityAction ev) => canceledAction.RemoveListener(ev);

        public void Select()
        {
            selectedAction?.Invoke();
            if (selectedObject != null)
                selectedObject.SetActive(true);
        }

        public void Diselect()
        {
            diselectedAction?.Invoke();
            if(selectedObject != null)
                selectedObject.SetActive(false);
            if(!string.IsNullOrEmpty(diselectSE)) Locater<IPlayAudio>.Resolve().PlaySE(diselectSE);
        }

        public void Decide()
        {
            if (decidedAction == null) return;
            decidedAction.Invoke();
            if (!string.IsNullOrEmpty(decideSE)) Locater<IPlayAudio>.Resolve().PlaySE(decideSE);
        }

        public void Cancel()
        {
            if (canceledAction == null) return;
            canceledAction.Invoke();
            if (!string.IsNullOrEmpty(cancelSE)) Locater<IPlayAudio>.Resolve().PlaySE(cancelSE);
        }
    }
}
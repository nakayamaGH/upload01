using UnityEngine;

namespace HellRoad.External
{
    public class TakeAbandonedPartsPlayerView : MonoBehaviour
    {
        [SerializeField] RuntimeAnimatorController animCtrl = null;

        private Animator animator = null;
        
        public void SetPlayer(WholeBodyView charaView)
        {
            charaView.transform.SetParent(transform);

            charaView.ResetRotation();

            animator = charaView.GetComponent<Animator>();
            animator.runtimeAnimatorController = animCtrl;
        }

        public void TakeAnimation()
        {
            animator.SetTrigger("Take");
        }
    }
}
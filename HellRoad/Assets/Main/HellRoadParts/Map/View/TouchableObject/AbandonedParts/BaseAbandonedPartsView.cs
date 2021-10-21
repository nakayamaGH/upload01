using DG.Tweening;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public abstract class BaseAbandonedPartsView : DecideableObject
    {
        [SerializeField] private WholeBodyView wholeBodyView = null;

        public WholeBodyView WholeBodyView => wholeBodyView;

        protected abstract CharaPartsView partsView { get; }
        protected abstract PartsType Type { get; }
        
        private EnemyGroupAsset enemyGroup;
        private bool isEnemy;
        private PartsID id;

        public void DataInitalize(EnemyGroupAsset enemyGroup, bool isEnemy)
        {
            this.enemyGroup = enemyGroup;
            id = enemyGroup.Group[0].GetParts(Type);
            this.isEnemy = isEnemy;

            PartsInfo info = Locater<IGetPartsInfoFromDB>.Resolve().Get(id);
            ViewInitalize(info);
        }

        protected abstract void ViewInitalize(PartsInfo info);

        public override void Decide()
        {
            WholeBodyView playerView = Locater<IMapPlayerView>.Resolve().WholeBody;
            Locater<ITakeAbandonedPartsEvent>.Resolve().Take(playerView, this, enemyGroup, isEnemy);
        }

        public void PullUp()
        {
            partsView.gameObject.SetActive(false);
            wholeBodyView.gameObject.SetActive(true); 
            wholeBodyView.LoadCharaPartsData(enemyGroup.Group[0].Head, enemyGroup.Group[0].Body, enemyGroup.Group[0].Arms, enemyGroup.Group[0].Legs);

            float pX = Locater<IMapPlayerView>.Resolve().WholeBody.transform.position.x;
            float eX = wholeBodyView.transform.position.x;
            int dir = pX > eX ? 1 : -1;
            wholeBodyView.transform.DOLocalMove(new Vector3(-32, 64), 0.75f).SetEase(Ease.OutCubic);
            wholeBodyView.transform.DORotate(new Vector3(0, 0, 20 * dir), 0.75f);
            transform.localScale = new Vector3(-dir, 1, 1);
        }
    }
}
using UnityEngine;

namespace HellRoad.External
{
    public class TakeAwayPartsInfoUIView : MonoBehaviour
    {
        [SerializeField] private UIMover partsInfoUIMover = null;
        [SerializeField] private PartsInfoView playerPartsInfoView = null;
        [SerializeField] private PartsInfoView enemyPartsInfoView = null;
		[SerializeField] ChangeCharaInMenuChild changeCharaMenuChild = null;

		private IGetCharaInfoInParty playerParts;
		private IGetStickedParts enemyParts;


		private void Awake()
		{
			changeCharaMenuChild.ChangedEvent += () =>
			{
				Show(playerParts, enemyParts);
			};
		}

		public void Show(IGetCharaInfoInParty playerParts, IGetStickedParts enemyParts)
		{
			this.enemyParts = enemyParts;
			this.playerParts = playerParts;

			if(!gameObject.activeSelf)
			{
				gameObject.SetActive(true);
				partsInfoUIMover.SetAnchor(Vector2.one, new Vector2(2, 2));
				partsInfoUIMover.DOMoveToMinAnchor(Vector2.zero, 0.5f);
				partsInfoUIMover.DOMoveToMaxAnchor(Vector2.one, 0.5f);
			}
		}

		public void Hide()
		{
			partsInfoUIMover.DOMoveToMinAnchor(Vector2.one, 0.5f);
			partsInfoUIMover.DOMoveToMaxAnchor(new Vector2(2, 2), 0.5f).onComplete += () => gameObject.SetActive(false);
		}

		public void OnSelectedPartsTypeAction(PartsType partsType)
		{
			enemyPartsInfoView.Show(enemyParts.GetParts(partsType));
			playerPartsInfoView.Show(playerParts.GetInfo(0).GetWholeBody.GetParts(partsType));
		}

		public void OnDiselectedPartsTypeAction()
		{
			enemyPartsInfoView.Hide();
			playerPartsInfoView.Hide();
		}
	}
}
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace HellRoad.External
{
	public class TakeAwayPartsView : MonoBehaviour, ITakeAwayPartsView
	{
		[SerializeField] private TakeAwayPartsMenuChild headSelect = null;
		[SerializeField] private TakeAwayPartsMenuChild bodySelect = null;
		[SerializeField] private TakeAwayPartsMenuChild armsSelect = null;
		[SerializeField] private TakeAwayPartsMenuChild legsSelect = null;

		[SerializeField] private TakeAwayPartsInfoUIView takeAwayPartsInfoUIView = null;

		private bool selected = false;
		private bool skip = false;

		private PartsType partsType;

		public void Show(IGetCharaInfoInParty playerParts, IGetStickedParts enemyParts)
		{
			takeAwayPartsInfoUIView.Show(playerParts, enemyParts);

			headSelect.Show(OnDecidedPartsTypeAction, takeAwayPartsInfoUIView.OnSelectedPartsTypeAction, takeAwayPartsInfoUIView.OnDiselectedPartsTypeAction);
			bodySelect.Show(OnDecidedPartsTypeAction, takeAwayPartsInfoUIView.OnSelectedPartsTypeAction, takeAwayPartsInfoUIView.OnDiselectedPartsTypeAction);
			armsSelect.Show(OnDecidedPartsTypeAction, takeAwayPartsInfoUIView.OnSelectedPartsTypeAction, takeAwayPartsInfoUIView.OnDiselectedPartsTypeAction);
			legsSelect.Show(OnDecidedPartsTypeAction, takeAwayPartsInfoUIView.OnSelectedPartsTypeAction, takeAwayPartsInfoUIView.OnDiselectedPartsTypeAction);

			selected = false;
			skip = false;

			gameObject.SetActive(true);
		}

		public void Hide()
		{
			headSelect.Hide();
			bodySelect.Hide();
			armsSelect.Hide();
			legsSelect.Hide();

			gameObject.SetActive(false);
			takeAwayPartsInfoUIView.Hide();
		}

		public Task<(int, PartsType, bool)> TakeAway()
		{
			int idx = 0;
			return Task.Run(async () => 
			{
				await UniTask.WaitUntil(() => selected);
				selected = false;
				return (idx, partsType, skip); 
			});
		}

		private void OnDecidedPartsTypeAction(PartsType partsType)
		{
			selected = true;
			this.partsType = partsType;
		}

		public void OnDecidedSkipAction()
		{
			selected = true;
			skip = true;
		}
	}

	public interface ITakeAwayPartsView
	{
		void Show(IGetCharaInfoInParty playerParts, IGetStickedParts enemyParts);
		void Hide();
		Task<(int, PartsType, bool)> TakeAway();
	}
}
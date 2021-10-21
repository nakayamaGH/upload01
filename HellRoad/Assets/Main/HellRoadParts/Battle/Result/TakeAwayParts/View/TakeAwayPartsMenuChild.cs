using Menu;
using System;
using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class TakeAwayPartsMenuChild : MonoBehaviour
    {
        [SerializeField] private MenuChild menuChild = null;
        [SerializeField] private PartsType partsType = PartsType.Head;

		private BattleCharaAnimator enemyAnimator = null;

		private event Action<PartsType> OnDecidedAction;
		private event Action<PartsType> OnSelectedAction;
		private event Action OnDiselectedAction;

		private bool decided = false;

		private readonly Color SELECTED_COLOR = Color.white;
		private readonly Color DISELECTED_COLOR = new Color(0.5f, 0.5f, 0.5f, 1);

		private void Awake()
		{
			menuChild.DecidedActionAddListener(OnDecided);
			menuChild.SelectedActionAddListener(OnSelected);
			menuChild.DiselectedActionAddListener(OnDiselected);

			enemyAnimator = Locater<BattleCharaAnimator>.Resolve((int)Players.Enemy);
		}

		public void Show(Action<PartsType> OnDecidedAction, Action<PartsType> OnSelectedAction, Action OnDiselectedAction)
		{
			this.OnDecidedAction += OnDecidedAction;
			this.OnSelectedAction += OnSelectedAction;
			this.OnDiselectedAction += OnDiselectedAction;

			decided = false;
			gameObject.SetActive(true);

			enemyAnimator.CharaView.SetColor(partsType, DISELECTED_COLOR);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
			OnDecidedAction -= OnDecidedAction;
			OnSelectedAction -= OnSelectedAction;
			OnDiselectedAction -= OnDiselectedAction;
		}

		private void OnDecided()
		{
			if (decided) return;
			decided = true;
			OnDecidedAction.Invoke(partsType);
		}

		private void OnSelected()
		{
			if (decided) return;
			OnSelectedAction(partsType);
			enemyAnimator.CharaView.SetColor(partsType, SELECTED_COLOR);
		}

		private void OnDiselected()
		{
			if (decided) return;
			OnDiselectedAction();
			enemyAnimator.CharaView.SetColor(partsType, DISELECTED_COLOR);
		}

		public PartsType GetPartsType => partsType;
	}
}
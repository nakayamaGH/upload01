using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
	public class BattleCharaAnimator : MonoBehaviour
	{
		[SerializeField] private Players players = Players.Me;
		[SerializeField] private RuntimeAnimatorController battleAnimCtrl = null;

		private WholeBodyView charaView;
		private Animator charaAnimator;

		public WholeBodyView CharaView => charaView;

		public async void ChangeAnimation(AnimName name, float duration, float crossFade)
		{
			charaAnimator.CrossFade(name.ToString(), crossFade);
			await UniTask.Delay((int)(duration * 1000));
			charaAnimator.CrossFade(AnimName.FightingPose_1.ToString(), crossFade);
		}

		public void ChangeAnimation(AnimName name, float crossFade)
		{
			charaAnimator.CrossFade(name.ToString(), crossFade);
		}

		public TweenerCore<Vector3, Vector3, VectorOptions> DOLocalMove(Vector2 pos, float duration)
		{
			Vector3 p = new Vector3(pos.x, pos.y, charaAnimator.transform.position.z);
			return charaAnimator.transform.DOLocalMove(p, duration);
		}

		private TweenerCore<Vector3, Vector3, VectorOptions> DOMove(Vector2 pos, float duration)
		{
			Vector3 p = new Vector3(pos.x, pos.y, charaAnimator.transform.position.z);
			return charaAnimator.transform.DOMove(p, duration);
		}

		public TweenerCore<Vector3, Vector3, VectorOptions> DOReturnPosition(float duration)
		{
			Vector3 p = new Vector3(0, 0, charaAnimator.transform.position.z);
			return DOLocalMove(p, duration);
		}

		public TweenerCore<Vector3, Vector3, VectorOptions> DOMoveToEnemy(float duration)
		{
			Vector3 pos = Locater<BattleCharaAnimator>.Resolve((int)players.GetEnemy()).transform.position;
			return DOMove(pos, duration);
		}

		public Vector2 GetPosition()
		{
			return transform.position;
		}

		public void SetChara(WholeBodyView charaView)
		{
			this.charaView = charaView;
			charaView.ResetRotation();

			charaAnimator = charaView.GetComponent<Animator>();
			charaAnimator.runtimeAnimatorController = battleAnimCtrl;
			
			charaView.transform.SetParent(transform);
			charaView.transform.localScale = Vector3.one;
		}
	}

	public enum AnimName
	{
		Idle,
		Run,
		FightingPose_1,
		Punch_1,
		Punch_2,
		HeadMissile,
		Kick,
		Damage_1,
		Damage_2,
		Damage_3,
		Dead,
		TakeAwayParts,
		StartBattle,
		BaseMagic,
		Uppercut,
		Kick_2,
		Punch_3,
		StartJump
	}
}
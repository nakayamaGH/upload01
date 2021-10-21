using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaAnimator : MonoBehaviour, IUpdate
    {
		[SerializeField] private Animator animator = null;
		[SerializeField] private RuntimeAnimatorController mapAnimCtrl = null;

		private MapCharaViewCore core;

		private bool isRunning;
		private bool isAttacking;
		private float climbingTime;

		private AnimatorStateInfo currentAnimatorState;

		private void Start()
		{
			core = GetComponent<MapCharaViewCore>();
			Locater<IAddEventOnChangeState>.Resolve().OnBeginState += ChangeAnimCtrl;
			Locater<IAddUpdateInMap>.Resolve().AddUpdate(this, MapSceneState.Map);

			currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0);
		}

		private void SetDir(int dir)
		{
			animator.transform.localScale = new Vector3(-dir, 1, 1);
		}

		private void OnRunningAction(bool isRunning) 
		{
			animator.SetBool("Running", isRunning);
			this.isRunning = isRunning;
		}

		private void OnGroundAction(bool onGround)
		{
			animator.SetBool("OnGround", onGround);
		}

		private void OnAirAction(float velocityY)  
		{
			animator.SetFloat("AirVelocity", velocityY);
		}

		private void OnAttackAction(bool isAttacking)
		{
			if (this.isAttacking == isAttacking) return;
			animator.SetBool("Attacking", isAttacking);
			this.isAttacking = isAttacking;
		}

		private void OnClimbLadder()
		{
			climbingTime = Mathf.Repeat(climbingTime + core.ClimbLadderDirection * Time.deltaTime, 1);
			animator.PlayInFixedTime("ClimbLadder", 0, climbingTime);
		}

		void IUpdate.ManagedUpdate()
		{

			if (core.Velocity.y != 0 && !core.OnGround)
			{
				OnAirAction(core.Velocity.y);
			}
			else if (!isRunning && core.NowState == MapCharaState.RunningAndJump && core.OnGround)
			{
				OnRunningAction(true);
			}
			else if (isRunning && core.NowState != MapCharaState.RunningAndJump || !core.OnGround)
			{
				OnRunningAction(false);
			}

			OnAttackAction(core.NowState == MapCharaState.Attacking);

			if(core.NowState == MapCharaState.ClimbLadder) OnClimbLadder();

			OnGroundAction(core.OnGround);
			SetDir(core.Direction);
		}

		void IUpdate.ManagedFixedUpdate()
		{

		}

		private void ChangeAnimCtrl(MapSceneState state) 
		{
			if (state == MapSceneState.Map)
			{
				animator.runtimeAnimatorController = mapAnimCtrl;
			}
		}

		private void OnDestroy()
		{
			Locater<IAddUpdateInMap>.Resolve().RemoveUpdate(this, MapSceneState.Map);
			Locater<IAddEventOnChangeState>.Resolve().OnBeginState -= ChangeAnimCtrl;
		}
	}
}
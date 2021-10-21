using Cysharp.Threading.Tasks;
using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class MapCharaAttackView : MapCharaStateView
    {
        [SerializeField] private float dashAttackSpeed = 1000;
        [SerializeField] private float attackingTime = 0.5f;
        [SerializeField] private float endAttackWaitTime = 0.5f;

        private IPlayAudio playSE;

        private bool nowAttackAnimating = false;

        public override MapCharaState ThisState => MapCharaState.Attacking;

        private void Start()
        {
            playSE = Locater<IPlayAudio>.Resolve();
        }

        public void Attack(IMapCharaCore core)
        {
            nowAttackAnimating = true;
            playSE.PlaySE("PunchMove");
            core.RB.gravityScale = 0;
            Attacking(core);
        }

        private async void Attacking(IMapCharaCore core)
        {
            float nowTime = attackingTime;
            while (nowTime > 0)
            {
                core.RB.velocity = new Vector2(dashAttackSpeed * core.Direction * Time.deltaTime, 0);

                nowTime -= Time.deltaTime;
                await UniTask.WaitForFixedUpdate();
            }

            if (core.OnGround)
            {
                core.RB.velocity = new Vector2(0, core.RB.velocity.y);
            }
            else
            {
                core.RB.velocity = new Vector2(core.RB.velocity.x / 2, core.RB.velocity.y);
            }

            core.RB.gravityScale = core.BaseGravityScale;
            core.NowState = MapCharaState.Idle;
            await UniTask.Delay((int)(endAttackWaitTime * 1000));
            nowAttackAnimating = false;
        }

        public override bool CheckChangeState(IMapCharaCore core)
        {
            if (core.HittingTouchableObject) return false;
            if (nowAttackAnimating) return false;
            if (core.NowState == MapCharaState.Attacking || core.NowState == MapCharaState.ClimbLadder) return false;
            if (!core.GetCharaInput.GetDecideDownInput) return false;
            return true;
        }

        public override void OnStart(IMapCharaCore core)
        {
            Attack(core);
        }

        public override void OnUpdate(IMapCharaCore core)
        {
        }

        public override void OnFixedUpdate(IMapCharaCore core)
        {
        }

        public override void OnEnd(IMapCharaCore core)
        {
        }
    }
}
using UnityEngine;

namespace HellRoad.External
{
    public class MapCharaMoveView : MapCharaStateView
    {
        [SerializeField] private float moveSpeed = 100;
        [SerializeField] private float moveSpeedClampMin = 100;
        [SerializeField] private float moveSpeedClampMax = 200;

        public override MapCharaState ThisState => MapCharaState.RunningAndJump;

        public override bool CheckChangeState(IMapCharaCore core)
        {
            if (core.NowState == MapCharaState.Attacking || core.NowState == MapCharaState.ClimbLadder) return false;
            if (core.GetCharaInput.GetHoriInput == 0) return false;
            return true;
        }

        public override void OnStart(IMapCharaCore core)
        {
        }

        public override void OnUpdate(IMapCharaCore core)
        {

        }

        public override void OnEnd(IMapCharaCore core)
        {
        }

        public override void OnFixedUpdate(IMapCharaCore core)
        {
            Run(core);
        }

        public void Run(IMapCharaCore core)
        {
            if(core.GetCharaInput.GetHoriInput == 0)
            {
                core.NowState = MapCharaState.Idle;
                core.RB.velocity = new Vector2(0, core.RB.velocity.y);
                return;
            }
            Rigidbody2D rb = core.RB;
            int runDir = core.GetCharaInput.GetHoriInput;

            float x = Time.deltaTime * moveSpeed * runDir;
            rb.AddForce(new Vector2(x, 0));
            float clampedX = Mathf.Clamp(rb.velocity.x * runDir, moveSpeedClampMin, moveSpeedClampMax) * runDir;
            rb.velocity = new Vector2(clampedX, rb.velocity.y);

            core.Direction = runDir;
        }
    }
}
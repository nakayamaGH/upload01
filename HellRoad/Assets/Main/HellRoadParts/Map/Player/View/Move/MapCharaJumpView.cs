using UnityEngine;
using Utility;
using Utility.Audio;

namespace HellRoad.External
{
    public class MapCharaJumpView : MapCharaStateView
    {
        [SerializeField] private float jumpPower = 100;
        [SerializeField] private float jumpingPower = 100;
        [SerializeField] private float maxJumpInputTime = 0.075f;

        private IPlayAudio playSE;

        private bool press = false;
        private bool timeEnd = false;
        private float jumpInputTime = 0;

        public override MapCharaState ThisState => MapCharaState.RunningAndJump;

        private void Awake()
        {
            playSE = Locater<IPlayAudio>.Resolve();
        }

        public void StartJump(IMapCharaCore core)
        {
            if (core.GetCharaInput.GetVertInputDown <= 0) return;
            Rigidbody2D rb = core.RB;
            if (core.OnGround && !press)
            {
                press = true;
                timeEnd = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                playSE.PlaySE("Jump");
            }
        }

        public void EndJump(IMapCharaCore core)
        {
            if (core.GetCharaInput.GetVertInputUp > 0)
            {
                press = false;
                jumpInputTime = 0;
            }
        }

        public void Jumping(IMapCharaCore core)
        {
            float jumpButton = core.GetCharaInput.GetVertInput;
            if (press && !timeEnd)
            {
                float jip = Time.deltaTime * jumpingPower * jumpButton;
                core.RB.AddForce(new Vector2(0, jip), ForceMode2D.Impulse);
                jumpInputTime += Time.deltaTime;
            }
            if (jumpInputTime > maxJumpInputTime)
            {
                timeEnd = true;
                jumpInputTime = 0;
            }
        }

        public override bool CheckChangeState(IMapCharaCore core)
        {
            if (core.NowState == MapCharaState.Attacking || core.NowState == MapCharaState.ClimbLadder) return false;
            if (core.GetCharaInput.GetVertInput <= 0) return false;
            return true;
        }

        public override void OnStart(IMapCharaCore core)
        {
        }

        public override void OnUpdate(IMapCharaCore core)
        {
            StartJump(core);
            EndJump(core);
        }

        public override void OnFixedUpdate(IMapCharaCore core)
        {
            Jumping(core);
        }

        public override void OnEnd(IMapCharaCore core)
        {
        }
    }
}
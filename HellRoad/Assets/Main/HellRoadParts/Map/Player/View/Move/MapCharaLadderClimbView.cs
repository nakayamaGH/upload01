using UnityEngine;

namespace HellRoad.External
{
    public class MapCharaLadderClimbView : MapCharaStateView
    {
        [SerializeField] private float moveSpeed = 1000;

        private bool hitLadder = false;

        public override MapCharaState ThisState => MapCharaState.ClimbLadder;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if(col.GetComponent<LadderTilemap>() != null)
            {
                hitLadder = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            if (col.GetComponent<LadderTilemap>() != null)
                hitLadder = false;
        }

        public override bool CheckChangeState(IMapCharaCore core)
        {
            return core.GetCharaInput.GetVertInput != 0 && hitLadder;
        }

        public override void OnStart(IMapCharaCore core)
        {
            core.RB.gravityScale = 0;
        }

        public override void OnUpdate(IMapCharaCore core)
        {
        }

        public override void OnFixedUpdate(IMapCharaCore core)
        {
            if (core.GetCharaInput.GetHoriInput != 0 || !hitLadder)
            {
                core.NowState = MapCharaState.Idle;
                core.RB.gravityScale = core.BaseGravityScale;
            }
            if(core.NowState != MapCharaState.Idle)
            {
                core.RB.velocity = new Vector2(0, core.GetCharaInput.GetVertInput * moveSpeed * Time.deltaTime);
                core.ClimbLadderDirection = core.GetCharaInput.GetVertInput;
            }
        }

        public override void OnEnd(IMapCharaCore core)
        {
            core.RB.gravityScale = core.BaseGravityScale;
            core.ClimbLadderDirection = 0;
        }
    }
}
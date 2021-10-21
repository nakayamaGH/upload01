using System;
using UnityEngine;

namespace HellRoad.External
{
    public class MapCharaViewCore : MonoBehaviour, IMapCharaCore, ISetOnGround
    {
        [SerializeField] MapCharaState state = MapCharaState.Idle;
        [SerializeField] Rigidbody2D rb = null;
        public Rigidbody2D RB => rb;

        public int Direction { get; set; } = 1;

        [SerializeField] private float baseGravityScale = 96;
        public float BaseGravityScale => baseGravityScale;
        public float GravityScale { get; set; }

        public Vector2 Velocity { get; set; }

        private MapCharaState nowState;
        public MapCharaState NowState { get => nowState; set => nowState = value; }

        public IGetCharaInput GetCharaInput => inputData;
        private MapCharaInputData inputData = new MapCharaInputData();

        private bool onGround = false;
        public bool OnGround => onGround;
        bool ISetOnGround.SetOnGround { get => onGround; set => onGround = value; }

        public int ClimbLadderDirection { get; set; }
        public bool HittingTouchableObject { get; set; }

        private void Update()
        {
            this.state = NowState;
        }
    }

    public interface IMapCharaCore
    {
        public Rigidbody2D RB { get; }
        public int Direction { get; set; }
        public Vector2 Velocity { get; set; }
        public bool OnGround { get; }
        public int ClimbLadderDirection { get; set; }
        public float BaseGravityScale { get; }
        public float GravityScale { get; set; }
        public MapCharaState NowState { get; set; }
        public IGetCharaInput GetCharaInput { get; }
        public bool HittingTouchableObject { get; set; }
    }

    public interface ISetOnGround
    {
        public bool SetOnGround { get; set; }
    }

    public enum MapCharaState
    {
        Idle,
        RunningAndJump,
        Attacking,
        ClimbLadder,
    }
}
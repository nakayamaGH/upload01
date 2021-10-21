namespace HellRoad.External
{
    public class MapCharaInputData : IGetCharaInput, ISetCharaInput
    {
		public int GetHoriInput => HoriInput;
        public int GetHoriInputDown => HoriInputDown;
        public int GetHoriInputUp => HoriInputUp;
        public int GetVertInput => VertInput;
        public int GetVertInputDown => VertInputDown;
        public int GetVertInputUp => VertInputUp;
        public bool GetDecideInput => DecideInput;
        public bool GetDecideDownInput => DecideDownInput;

        public int HoriInput { get; set; }
        public int HoriInputDown { get; set; }
        public int HoriInputUp { get; set; }
        public int VertInput { get; set; }
        public int VertInputDown { get; set; }
        public int VertInputUp { get; set; }
        public bool DecideInput { get; set; }
        public bool DecideDownInput { get; set; }
    }

    public interface IGetCharaInput
	{
		public int GetHoriInput { get; }
		public int GetHoriInputDown { get; }
		public int GetHoriInputUp { get; }

		public int GetVertInput { get; }
		public int GetVertInputDown { get; }
		public int GetVertInputUp { get; }

		public bool GetDecideInput { get; }
		public bool GetDecideDownInput { get; }
	}

	public interface ISetCharaInput
	{
		public int HoriInput { get; set; }
		public int HoriInputDown { get; set; }
		public int HoriInputUp { get; set; }

		public int VertInput { get; set; }
		public int VertInputDown { get; set; }
		public int VertInputUp { get; set; }

		public bool DecideInput { get; set; }
		public bool DecideDownInput { get; set; }
	}
}
using System.Threading.Tasks;

namespace HellRoad
{
    public abstract class CharaBrain
    {
        public abstract Task<TurnActionType> GetResult(ICharasFieldGetCharaInfo field);
    }
}
using System;
using System.Threading.Tasks;

namespace HellRoad
{
    public interface ICharasField : ICharasFieldGetCharaInfo, IDoFieldTurnAction, IAddCharaEventInField
    {
        IBattleChara GetFrontChara();
        IBattleChara GetChara(int idx);
    }

    public interface ICharasFieldGetCharaInfo
    {
        IGetCharaInfo GetFrontCharaInfo();
        IGetCharaInfo GetCharaInfo(int idx);
    }

    public interface IDoFieldTurnAction
    {
        Task<TurnActionType> DecideTurnAction(ref long frontCharaSpeed);
        void DoTurnAction(TurnActionType type);
        void OnBattleStart();
        void OnTurnEnd();
        void OnBattleEnd();
        bool ContainAliveChara();
    }

    public interface IAddCharaEventInField
    {
        public void AddGetFrontEnemyChara(ICharasField enemy);
        public void RemoveGetFrontEnemyChara(ICharasField enemy);

        public void AddOnDeadAction(Action<IBattleChara> ev);
        public void RemoveOnDeadAction(Action<IBattleChara> ev);

        public void AddBattleActionArgsGetter(Func<Players, BattleActionArgs> ev);
        public void RemoveBattleActionArgsGetter(Func<Players, BattleActionArgs> ev);
    }
}
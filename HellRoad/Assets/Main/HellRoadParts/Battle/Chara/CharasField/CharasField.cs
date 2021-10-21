using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Utility;

namespace HellRoad
{
    public class CharasField : ICharasField, IDoActionByType
    {
        private IBattleParty party = null;

        private CharaBrain brain = null;

		public IBattleChara GetFrontChara() => party.GetCharas()[0];
        public IBattleChara GetChara(int idx) => party.GetCharas()[idx];

        public CharasField(IBattleParty party, CharaBrain brain)
        {
            this.party = party;
            this.brain = brain;

            Locater<ICharasFieldGetCharaInfo>.Register(this, (int)party.Players);
        }

        public void DoAction(TurnActionType type)
        {
            switch (type)
            {
                case TurnActionType.ChangeFontChara_Left:
                    ShiftParty(Direction.Left);
                    break;
                case TurnActionType.ChangeFontChara_Right:
                    ShiftParty(Direction.Right);
                    break;
                default:
                    GetFrontChara().DoAction(type);
                    break;
            }
        }

        public IGetCharaInfo GetFrontCharaInfo() => GetFrontChara();
        public IGetCharaInfo GetCharaInfo(int idx) => party.GetCharas()[idx];

        public void DoTurnAction(TurnActionType type)
        {
            DoAction(type);
        }

        public void OnTurnEnd()
        {
            BlukAction(chara => chara.OnTurnEnd());
        }

        public void OnBattleStart()
        {
            BlukAction(chara => chara.OnStartBattle());
        }

        public void OnBattleEnd()
        {
            BlukAction(chara => chara.OnEndBattle());
        }

        public bool ContainAliveChara()
        {
            ReadOnlyCollection<IBattleChara> battleCharas = party.GetCharas();
            foreach (IBattleChara chara in battleCharas)
            {
                if (chara.IsAlive()) return true;
            }
            return false;
        }

        public Task<TurnActionType> DecideTurnAction(ref long frontCharaSpeed)
        {
            Task<TurnActionType> task = Task.Run(() => brain.GetResult(this));
            frontCharaSpeed = GetFrontChara().GetValue(StatusParamType.Speed);
            return task;
        }

        private void SortParty(int idx_1, int idx_2)
        {
            party.Sort(idx_1, idx_2);
        }

        private void ShiftParty(Direction dir)
        {
            party.Shift(dir);
        }

        public void AddGetFrontEnemyChara(ICharasField enemy)
        {
            BlukAction(chara => chara.GetFrontEnemyChara += enemy.GetFrontChara);
        }

        public void RemoveGetFrontEnemyChara(ICharasField enemy)
        {
            BlukAction(chara => chara.GetFrontEnemyChara -= enemy.GetFrontChara);
        }

		public void AddOnDeadAction(Action<IBattleChara> ev)
        {
            BlukAction(chara => chara.OnDeadAction += ev);
        }

		public void RemoveOnDeadAction(Action<IBattleChara> ev)
        {
            BlukAction(chara => chara.OnDeadAction -= ev);
        }

        public void AddBattleActionArgsGetter(Func<Players, BattleActionArgs> ev)
        {
            BlukAction(chara => chara.GetBattleArgs += ev);
        }

        public void RemoveBattleActionArgsGetter(Func<Players, BattleActionArgs> ev)
        {
            BlukAction(chara => chara.GetBattleArgs -= ev);
        }

        private void BlukAction(Action<IBattleChara> action)
        {
            ReadOnlyCollection<IBattleChara> battleCharas = party.GetCharas();
            for (int i = 0; i < battleCharas.Count; i++)
            {
                action(battleCharas[i]);
            }
        }
	}
}
using System;

namespace HellRoad
{
    public class BattleAction
    {
        private Action<BattleActionArgs> act;
        
        public BattleAction(Action<BattleActionArgs> act)
        {
            this.act = act;
        }

        public void Play(BattleActionArgs args)
        {
            act(args);
        }
    }

    public class BattleActionArgs
    {
        public readonly ICharasField MyField;
        public readonly ICharasField EnemyField;

        public IBattleChara MyFrontChara => MyField.GetChara(0);
        public IBattleChara EnemyFrontChara => EnemyField.GetChara(0);

        public BattleActionArgs(ICharasField myField, ICharasField enemyField)
        {
            this.MyField = myField;
            this.EnemyField = enemyField;
        }
    }
}
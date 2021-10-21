using System.Collections.Generic;
using HellRoad.External;
using HellRoad.External.Animation;
using Utility;

namespace HellRoad
{
    public class Battle
    {
        private ICharasField myField;
        private ICharasField enemyField;

        private bool endBattle = false;
        private bool skipTurn = false;

        private IAddGameAnimation addAnim;
        private BattleResult battleResult;

        private List<IBattleChara> deadEnemyCharas = new List<IBattleChara>();

        public Battle(IBattleParty myParty, IBattleParty enemyParty)
        {
            addAnim = Locater<IAddGameAnimation>.Resolve();

            myField = new CharasField(myParty, new MyBrain());
            enemyField = new CharasField(enemyParty, new EnemyBrain());


            myField.AddGetFrontEnemyChara(enemyField);
            myField.AddOnDeadAction(OnDeadChara);
            myField.AddBattleActionArgsGetter(CreateBattleActionArgs);
            enemyField.AddGetFrontEnemyChara(myField);
            enemyField.AddOnDeadAction(OnDeadChara);
            enemyField.AddBattleActionArgsGetter(CreateBattleActionArgs);

            battleResult = new BattleResult(myParty);
        }

        public void Start()
        {
            myField.OnBattleStart();
            enemyField.OnBattleStart();
            DoTurnAction();
        }

        public async void DoTurnAction()
        {
            long eSpeed = 0;
            long mSpeed = 0;
            skipTurn = false;

            TurnActionType eAct = await enemyField.DecideTurnAction(ref eSpeed);
            AddShowTurnActionAnim(eAct, enemyField.GetCharaInfo(0).GetInfo(), Players.Enemy);
            TurnActionType mAct = await myField.DecideTurnAction(ref mSpeed);
            AddShowTurnActionAnim(mAct, myField.GetCharaInfo(0).GetInfo(), Players.Me);

            while (!mAct.ConsumeTurns())
            {
                myField.DoTurnAction(mAct);
                mAct = await myField.DecideTurnAction(ref mSpeed);
            }

            if (mSpeed >= eSpeed)
            {
                myField.DoTurnAction(mAct);
                AddHideTurnActionAnim(Players.Me);
                if (!endBattle && !skipTurn) enemyField.DoTurnAction(eAct);
                AddHideTurnActionAnim(Players.Enemy);
            }
            else
            {
                enemyField.DoTurnAction(eAct);
                AddHideTurnActionAnim(Players.Enemy);
                if (!endBattle && !skipTurn) myField.DoTurnAction(mAct);
                AddHideTurnActionAnim(Players.Me);
            }

            myField.OnTurnEnd();
            enemyField.OnTurnEnd();

            if (!endBattle)
                DoTurnAction();
            else
                EndBattleAction();
        }

        private void End()
        {
            endBattle = true;
        }

        private void Win()
        {
            End();
            battleResult.GetResult(100, 100, deadEnemyCharas[deadEnemyCharas.Count - 1], () => addAnim.Add(new WinBattleAnimation()));
        }

        private void Lose()
        {
            End();
            new GameOver().Play();
            UnityEngine.Debug.Log("Lose");
        }

        private void OnDeadChara(IBattleChara deadChara)
        {
            if(deadChara.Players == Players.Enemy) deadEnemyCharas.Add(deadChara);

            bool m = myField.ContainAliveChara();
            bool e = enemyField.ContainAliveChara();

            if (deadChara.PartyIdx == 0)
            {
                skipTurn = true;
            }

            if(m && !e)
            {
                Win();
            }
            else
            if (!m)
            {
                Lose();
            }
        }

        private BattleActionArgs CreateBattleActionArgs(Players players)
        {
            ICharasField my = players == Players.Me ? myField : enemyField;
            ICharasField enemy = players == Players.Enemy ? myField : enemyField;

            return new BattleActionArgs(my, enemy);
		}

        private void EndBattleAction()
        {
            myField.OnBattleEnd();
            enemyField.OnBattleEnd();
            myField.RemoveGetFrontEnemyChara(enemyField);
            myField.RemoveOnDeadAction(OnDeadChara);
            myField.RemoveBattleActionArgsGetter(CreateBattleActionArgs);
            enemyField.RemoveGetFrontEnemyChara(myField);
            enemyField.RemoveOnDeadAction(OnDeadChara);
            enemyField.RemoveBattleActionArgsGetter(CreateBattleActionArgs);
        }


        //OriginalAnimation
        private void AddShowTurnActionAnim(TurnActionType turnActionType, CharaInfo charaInfo, Players players)
        {
            addAnim.Add(new OriginalAnimation((anim) =>
            {
                ICharaTurnActionView charaTurnActionView = Locater<ICharaTurnActionView>.Resolve((int)players);
                charaTurnActionView.Show(turnActionType, charaInfo);
                anim.EndAnimation = true;
            }));
        }
        private void AddHideTurnActionAnim(Players players)
        {
            addAnim.Add(new OriginalAnimation((anim) =>
            {
                ICharaTurnActionView charaTurnActionView = Locater<ICharaTurnActionView>.Resolve((int)players);
                charaTurnActionView.Hide();
                anim.EndAnimation = true;
            }));
        }
    }
}

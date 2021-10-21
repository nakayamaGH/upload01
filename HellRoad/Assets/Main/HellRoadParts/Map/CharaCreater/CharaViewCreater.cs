using HellRoad.External.Animation;
using UnityEngine;
using Utility;

namespace HellRoad.External
{
    public class CharaViewCreater : MonoBehaviour, ICharaViewCreater
    {
        [SerializeField] MapPlayerView playerView = null;
        [SerializeField] MapEnemyView enemyView = null;

        public MapPlayerView CreatePlayer()
        {
            MapPlayerView view = Instantiate(playerView);

            IPartyEventListener eventListener = Locater<IPartyEventListener>.Resolve();
            IGetCharaInfoInParty getCharaInfoInParty = Locater<IGetCharaInfoInParty>.Resolve();
            SetEventByChara(Players.Me, getCharaInfoInParty, eventListener, view.WholeBody);

            Locater<IMapPlayerView>.Register(view);
            return view;
        }

        public void SetEventByChara(Players players, IGetCharaInfoInParty getCharaInfoInParty, IPartyEventListener eventListener, WholeBodyView wholeBodyView)
        {
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();

            void OnStickParts(int idx, PartsID id)
            {
                if (idx != 0) return;
                addAnim.Add(new StickPartsByCharaViewAnimation(wholeBodyView, id));
                ValidateFirstCharaStatus();
            }
            eventListener.OnStickParts += OnStickParts;

            void OnSortingParty(int idx_1, int idx_2)
            {
                if (idx_1 == 0 || idx_2 == 0)
                {
                    ValidateFirstWholeBody();
                }
                ValidateFirstCharaStatus();
            }
            eventListener.OnSortingParty += OnSortingParty;

            void OnShiftedParty()
            {
                ValidateFirstWholeBody();
                ValidateFirstCharaStatus();
            }
            eventListener.OnShiftedParty += OnShiftedParty;

            Locater<IPartyCharaIconsView>.Resolve((int)players).SetTargetParty(getCharaInfoInParty, eventListener);

            void ValidateFirstCharaStatus()
            {
                IGetParamValue getParam = getCharaInfoInParty.GetInfo(0).GetParamValue;
                addAnim.Add(new ValidateStatusViewAnimation(getParam, players));
                addAnim.Add(new ValidateHpBarAnimation(players, (int)getParam.GetValue(StatusParamType.MaxHP), (int)getParam.GetValue(StatusParamType.HP), (int)getParam.GetValue(StatusParamType.HP)));
            }

            void ValidateFirstWholeBody()
            {
                IGetWholeBodyProperty wholeBody = getCharaInfoInParty.GetInfo(0).GetWholeBody;
                addAnim.Add(new ValidateWholeBodyViewAnimation(wholeBodyView, wholeBody));
            }

            wholeBodyView.OnDestroyAction += () =>
            {
                eventListener.OnStickParts -= OnStickParts;
                eventListener.OnSortingParty -= OnSortingParty;
                eventListener.OnShiftedParty -= OnShiftedParty;
            };
            ValidateFirstWholeBody();
        }

        public MapEnemyView CreateEnemy(EnemyGroupAsset enemyGroup)
        {
            MapEnemyView view = Instantiate(enemyView);
            view.Initalize(enemyGroup);
            return view;
        }
    }
    
    public interface ICharaViewCreater
    {
        void SetEventByChara(Players players, IGetCharaInfoInParty getCharaInfoInParty, IPartyEventListener eventListener, WholeBodyView wholeBodyView);
        MapPlayerView CreatePlayer();
        MapEnemyView CreateEnemy(EnemyGroupAsset enemyGroup);
    }

}
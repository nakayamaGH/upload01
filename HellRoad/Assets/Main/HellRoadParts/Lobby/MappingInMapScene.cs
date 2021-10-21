using Utility;
using UnityEngine;
using HellRoad.External.Animation;

namespace HellRoad.External
{
    public class MappingInMapScene : MonoBehaviour
    {
        [SerializeField] GameAnimator gameAnimator = null;
        [SerializeField] CharaStatusView myStatusView = null;
        [SerializeField] CharaStatusView enemyStatusView = null;
        [SerializeField] MapUpdater mapUpdater = null;
        [SerializeField] BattleView battleView = null;
        [SerializeField] EffectPlayerView playEffect = null;
        [SerializeField] PlayerActionChooser playerActionChooser = null;
        [SerializeField] TakeAwayPartsView takeAwayPartsView = null;
        [SerializeField] StatusParamIconsAsset statusParamIconsAsset = null;
        [SerializeField] PartyCharaIconsView playerPartyCharaIconsView = null;
        [SerializeField] PartyCharaIconsView enemyPartyCharaIconsView = null;
        [SerializeField] BattleCamera battleCamera = null;
        [SerializeField] CharaViewCreater charaViewCreater = null;
        [SerializeField] PlatformActivater platformActivater = null;
        [SerializeField] MapSceneInitalizer mapSceneInitalizer = null;
        [SerializeField] CharaTurnActionView playerTurnActionView = null;
        [SerializeField] CharaTurnActionView enemyTurnActionView = null;
        [SerializeField] CharaTurnActionView playerPassiveSkillView = null;
        [SerializeField] CharaTurnActionView enemyPassiveSkillView = null;
        [SerializeField] ShowMapTextureView[] showMapTexViews = null;
        [SerializeField] AddMapIconsView addMapIconsView = null;
        [SerializeField] DecidableObjectActionAboutView decidableObjectActionAboutView = null;
        [SerializeField] TakeAbandonedPartsEventView takeAbandonedPartsEvent = null;
        [SerializeField] PartyBuffView playerBuffView = null;
        [SerializeField] PartyBuffView enemyBuffView = null;
        [SerializeField] HpBarView playerHpBar = null;
        [SerializeField] HpBarView enemyHpBar = null;

        public void Register()
        {
            RegisterView();
            RegisterModel();
        }

        private void RegisterView()
        {
            //MapUpdater
            Locater<IAddUpdateInMap>.Register(mapUpdater);
            Locater<IChangeMapState>.Register(mapUpdater);
            Locater<IAddEventOnChangeState>.Register(mapUpdater);

            //IAddGameAnimation
            Locater<IAddGameAnimation>.Register(gameAnimator);

            //StatusView
            Locater<ICharaStatusView>.Register(myStatusView, (int)Players.Me);
            Locater<ICharaStatusView>.Register(enemyStatusView, (int)Players.Enemy);

            //BattleView
            Locater<IBattleView>.Register(battleView);

            //IPlayerActionChooser
            Locater<IPlayerActionChooser>.Register(playerActionChooser);

            //IPlayEffect
            Locater<IPlayEffect>.Register(playEffect);

            //TakeAwayPartsView
            Locater<ITakeAwayPartsView>.Register(takeAwayPartsView);

            //IGetStatusParamIcon
            Locater<IGetStatusParamIcon>.Register(statusParamIconsAsset);

            //PartyCharaIconsView
            Locater<IPartyCharaIconsView>.Register(playerPartyCharaIconsView, (int)Players.Me);
            Locater<IPartyCharaIconsView>.Register(enemyPartyCharaIconsView, (int)Players.Enemy);

            //BattleCamera
            Locater<IBattleCamera>.Register(battleCamera);

            //CharaViewCreater
            Locater<ICharaViewCreater>.Register(charaViewCreater);

            //PlatformActivater
            Locater<IPlatformActivater>.Register(platformActivater);

            //MapPlayerView
            //Locater<IMapPlayerView>.Register(null);

            //MapSceneInitalizer
            Locater<IMapInitalize>.Register(mapSceneInitalizer);

            //CharaTurnActionView
            Locater<ICharaTurnActionView>.Register(playerTurnActionView, (int)Players.Me);
            Locater<ICharaTurnActionView>.Register(enemyTurnActionView, (int)Players.Enemy);
            Locater<ICharaTurnActionView>.Register(playerPassiveSkillView, (int)Players.Me + 2);
            Locater<ICharaTurnActionView>.Register(enemyPassiveSkillView, (int)Players.Enemy + 2);

            //MiniMapView
            Locater<IAddMiniMapIcon>.Register(addMapIconsView);
            for(int i = 0; i < showMapTexViews.Length; i++)
                Locater<ISetMiniMapPosition>.Register(showMapTexViews[i], i);

            //DecidableObjectActionAboutView
            Locater<IDecidableObjectActionAboutView>.Register(decidableObjectActionAboutView);

            //TakeAbandonedPartsEvent
            Locater<ITakeAbandonedPartsEvent>.Register(takeAbandonedPartsEvent);

            //PartyBuffView
            Locater<IPartyBuffView>.Register(playerBuffView, (int)Players.Me);
            Locater<IPartyBuffView>.Register(enemyBuffView, (int)Players.Enemy);

            //PartyBuffView
            Locater<IHpBarView>.Register(playerHpBar, (int)Players.Me);
            Locater<IHpBarView>.Register(enemyHpBar, (int)Players.Enemy);
        }

        private void RegisterModel()
        {
            //Party
            Party party = new Party(Players.Me, 1);
            Locater<IPartyEventListener>.Register(party);
            Locater<IAddAndRemoveCharaInParty>.Register(party);
            Locater<IGetCharaInfoInParty>.Register(party);
            Locater<IStickPartsInParty>.Register(party);
            Locater<IBattleParty>.Register(party);
            Locater<ISortParty>.Register(party);

            //MeatPieceWallet
            MeatPieceWallet meatPieceWallet = new MeatPieceWallet(150);
            Locater<IAddReduceMeatPiece>.Register(meatPieceWallet);
            Locater<IGetMeatPieces>.Register(meatPieceWallet);
            Locater<IAddReduceMeatPieceEventListener>.Register(meatPieceWallet);

            //SoulFragmentWallet
            SoulFragmentWallet soulFragmentWallet = new SoulFragmentWallet(150);
            Locater<IAddReduceSoulFragment>.Register(soulFragmentWallet);
            Locater<IGetSoulFragment>.Register(soulFragmentWallet);
            Locater<IAddReduceSoulFragmentEventListener>.Register(soulFragmentWallet);

            //PartsBag
            PartsBag partsBag = new PartsBag();
            Locater<IAddDeleteToPartsBag>.Register(partsBag);
            Locater<IUpgradePartsBag>.Register(partsBag);
            Locater<IGetToPartsBag>.Register(partsBag);

            //BagUpgrader
            BagUpgrader bagUpgrader = new BagUpgrader(soulFragmentWallet, partsBag);
            Locater<BagUpgrader>.Register(bagUpgrader);

            //StickPartsToChara
            StickPartsToChara stickPartsToChara = new StickPartsToChara(party, partsBag);
            Locater<StickPartsToChara>.Register(stickPartsToChara);

            //AddCharaInParty
            AddCharaInParty addCharaInParty = new AddCharaInParty(party, soulFragmentWallet);
            Locater<AddCharaInParty>.Register(addCharaInParty);

            //CreateParts 
            CreateParts createParts = new CreateParts(partsBag, meatPieceWallet);
            Locater<CreateParts>.Register(createParts);
        }
    }
}
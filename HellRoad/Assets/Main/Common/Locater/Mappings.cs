using Utility.Audio;
using Utility.LoadScene;
using Utility.PostEffect;
using HellRoad;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Utility.Inputer;
using HellRoad.External;

namespace Utility
{
    public class Mappings : MonoBehaviour
    {
        [SerializeField] AudioPlayer audioPlayer = null;
        [SerializeField] PostEffectCamera postEffectCamera = null;
        [SerializeField] PostEffectMaterialDB materialDB = null;

        private async void Awake()
        {
            KeyInputer keyInputer = new KeyInputer();
            Locater<IInputer>.Register(keyInputer);

            Locater<IPlayAudio>.Register(audioPlayer);
            Locater<IAudioVolumeChange>.Register(audioPlayer);
            Locater<IGetAudioVolume>.Register(audioPlayer);

            Locater<IGetMaterialData>.Register(materialDB);
            Locater<IPostEffectCamera>.Register(postEffectCamera);

            Locater<ITimeScaler>.Register(new TimeScaler());

            //HellRoad
            UsableSkillDB usableSkillDB = new UsableSkillDB();
            Locater<IGetUsableSkillFromDB>.Register(usableSkillDB);

            PassiveSkillDB passiveSkillDB = new PassiveSkillDB();
            Locater<IGetPassiveSkillFromDB>.Register(passiveSkillDB);

            List<UniTask> preloads = new List<UniTask>();

            PartsInfoDB partsDB = new PartsInfoDB();
            Locater<IGetPartsInfoFromDB>.Register(partsDB);
            preloads.Add(partsDB.Load());

            PassiveSkillInfoDB passiveSkillInfoDB = new PassiveSkillInfoDB();
            Locater<IGetPassiveSkillInfoFromDB>.Register(passiveSkillInfoDB);
            preloads.Add(passiveSkillInfoDB.Load());

            UsableSkillInfoDB usableSkillInfoDB = new UsableSkillInfoDB();
            Locater<IGetUsableSkillInfoFromDB>.Register(usableSkillInfoDB);
            preloads.Add(usableSkillInfoDB.Load());

            MapAssetDB mapAssetDB = new MapAssetDB();
            Locater<IGetMapAssetFromDB>.Register(mapAssetDB);
            preloads.Add(mapAssetDB.Load());

            await UniTask.WhenAll(preloads);

            //Test
            SceneLoader.LoadSceneAsync(SceneName.Title, (sc, md) =>
            //SceneLoader.LoadSceneAsync(SceneName.Map, (sc, md) => 
            {
                new PostEffector().Fade(PostEffectType.SimpleFade, 1, Color.white, PostEffector.FadeType.In);
            });
            Destroy(this);
        }

		private void Start()
		{
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
}
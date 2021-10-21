using Menu;
using System;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Utility.Audio;
using Utility.Inputer;

namespace HellRoad.External
{
    public class AudioSlider : MonoBehaviour, IUpdate
    {
        [SerializeField] Type type = Type.Master;
        [SerializeField] MenuChild child = null;
        [SerializeField] Slider slider = null;

        [SerializeField] Updater updater = null;

        private IInputer inputer;
        private IAudioVolumeChange audioVolumeChange;
        private IGetAudioVolume getAudioVolume;

        private Action<float> volumeChangeAction;

        private void Awake()
        {
            child.SelectedActionAddListener(() => updater.AddUpdate(this));
            child.DiselectedActionAddListener(() => updater.RemoveUpdate(this));

            inputer = Locater<IInputer>.Resolve();
            audioVolumeChange = Locater<IAudioVolumeChange>.Resolve();
            getAudioVolume = Locater<IGetAudioVolume>.Resolve();

            switch (type)
            {
                case Type.Master:
                    slider.value = getAudioVolume.GetMasterVolume();
                    volumeChangeAction = audioVolumeChange.ChangeMasterVolume;
                    break;
                case Type.BGM:
                    slider.value = getAudioVolume.GetBGMVolume();
                    volumeChangeAction = audioVolumeChange.ChangeBGMVolume;
                    break;
                case Type.SE:
                    slider.value = getAudioVolume.GetSEVolume();
                    volumeChangeAction = audioVolumeChange.ChangeSEVolume;
                    break;
            }
            slider.onValueChanged.AddListener((value) =>
            {
                Debug.Log(value);
                volumeChangeAction(value);
            });
        }

        public void ManagedFixedUpdate()
        {
        }

        public void ManagedUpdate()
        {
            float dir = inputer.HoriMoveDir();
            if (dir != 0)
            {
                slider.value += Time.deltaTime * dir;
            }
        }

        public enum Type
        {
            Master,
            BGM,
            SE,
        }
    }
}
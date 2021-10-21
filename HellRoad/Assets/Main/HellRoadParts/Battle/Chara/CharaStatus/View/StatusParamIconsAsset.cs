using System;
using UnityEngine;

namespace HellRoad.External
{
    [CreateAssetMenu(menuName = "StatusParamIcons")]
    public class StatusParamIconsAsset : ScriptableObject, IGetStatusParamIcon
    {
        [SerializeField] StatusParamIcon[] icons = null;

		Sprite IGetStatusParamIcon.GetIcon(StatusParamType type) => Array.Find(icons, x => x.Type == type).Icon;
    }

    [Serializable]
    public class StatusParamIcon
    {
        [SerializeField] private StatusParamType type;
        [SerializeField] private Sprite icon;

        public StatusParamType Type => type;
        public Sprite Icon => icon;
    } 
    
    public interface IGetStatusParamIcon
    {
        Sprite GetIcon(StatusParamType type);

    }
}
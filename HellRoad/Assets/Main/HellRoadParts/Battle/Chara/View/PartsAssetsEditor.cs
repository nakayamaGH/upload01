#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace HellRoad.External
{
    public class PartsAssetsEditor : MonoBehaviour
    {

        [SerializeField] SpriteRenderer head = null;
        [SerializeField] SpriteRenderer body = null;
        [SerializeField] SpriteRenderer backBody = null;
        [SerializeField] SpriteRenderer[] leftArms = null;
        [SerializeField] SpriteRenderer[] rightArms = null;
        [SerializeField] SpriteRenderer[] leftLegs = null;
        [SerializeField] SpriteRenderer[] rightLegs = null;

        [SerializeField] Transform headSeams = null;
        [SerializeField] Transform leftArmSeams = null;
        [SerializeField] Transform rightArmSeams = null;
        [SerializeField] Transform leftLegSeams = null;
        [SerializeField] Transform rightLegSeams = null;

        [SerializeField] HeadPartsInfoAsset headAsset = null;
        [SerializeField] BodyPartsInfoAsset bodyAsset = null;
        [SerializeField] ArmsPartsInfoAsset armsAsset = null;
        [SerializeField] LegsPartsInfoAsset legsAsset = null;

        [ContextMenu("SaveCharaData")]
        private void CloneMe()
        {
            headAsset.Info.Setup(head.sprite);
            bodyAsset.Info.Setup(body.sprite, headSeams.localPosition, leftArmSeams.localPosition, rightArmSeams.localPosition, leftLegSeams.localPosition, rightLegSeams.localPosition, backBody.sprite);
            armsAsset.Info.Setup(leftArms.Select(x => x.sprite).ToList(), rightArms.Select(x => x.sprite).ToList(), leftArms[1].transform.localPosition, rightArms[1].transform.localPosition);
            legsAsset.Info.Setup(leftLegs.Select(x => x.sprite).ToList(), rightLegs.Select(x => x.sprite).ToList(), leftLegs[1].transform.localPosition, leftLegs[2].transform.localPosition, rightLegs[1].transform.localPosition, rightLegs[2].transform.localPosition);

            EditorUtility.SetDirty(headAsset);
            EditorUtility.SetDirty(bodyAsset);
            EditorUtility.SetDirty(armsAsset);
            EditorUtility.SetDirty(legsAsset);
            AssetDatabase.SaveAssets();
        }

        [ContextMenu("LoadCharaData")]
        private void ChangeParts()
        {
            GetComponent<WholeBodyView>().LoadCharaPartsData(headAsset.Info, bodyAsset.Info, armsAsset.Info, legsAsset.Info);
        }
    }
}
#endif
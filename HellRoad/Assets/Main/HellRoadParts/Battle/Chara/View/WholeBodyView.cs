using System;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    public class WholeBodyView : MonoBehaviour
    {
        [SerializeField] Transform wholeBody = null;

        [SerializeField] CharaHeadView headView = null;
        [SerializeField] CharaBodyView bodyView = null;
        [SerializeField] CharaArmsView leftArmView = null;
        [SerializeField] CharaArmsView rightArmView = null;
        [SerializeField] CharaLegsView leftLegView = null;
        [SerializeField] CharaLegsView rightLegView = null;

        private PartsID headID, bodyID, armsID, legsID;

        public event Action OnDestroyAction;

        public void LoadCharaPartsData(HeadPartsInfo hInfo, BodyPartsInfo bInfo, ArmsPartsInfo aInfo, LegsPartsInfo lInfo)
        {
            headID = hInfo.Kind;
            bodyID = bInfo.Kind;
            armsID = aInfo.Kind;
            legsID = lInfo.Kind;

            bodyView.Initalize(bInfo.Body, bInfo.BackBody);
            headView.Initalize(hInfo.Head, bInfo.HeadSeams);
            leftArmView.Initaize(aInfo.LeftArms, bInfo.LeftArmSeams, aInfo.LeftArm_2_Seams);
            rightArmView.Initaize(aInfo.RightArms, bInfo.RightArmSeams, aInfo.RightArm_2_Seams);
            leftLegView.Initaize(lInfo.LeftLegs, bInfo.LeftLegSeams, lInfo.LeftLegs_2_Seams, lInfo.LeftLegs_3_Seams, (int)bInfo.ZIndex);
            rightLegView.Initaize(lInfo.RightLegs, bInfo.RightLegSeams, lInfo.RightLegs_2_Seams, lInfo.RightLegs_3_Seams, (int)bInfo.ZIndex);
            
            wholeBody.localPosition = new Vector3(0, lInfo.LegLength, 0);
        }

        public void LoadCharaPartsData(PartsID headID, PartsID bodyID, PartsID armsID, PartsID legsID)
        {
            IGetPartsInfoFromDB db = Locater<IGetPartsInfoFromDB>.Resolve();
            HeadPartsInfo hInfo = (HeadPartsInfo)db.Get(headID);
            BodyPartsInfo bInfo = (BodyPartsInfo)db.Get(bodyID);
            ArmsPartsInfo aInfo = (ArmsPartsInfo)db.Get(armsID);
            LegsPartsInfo lInfo = (LegsPartsInfo)db.Get(legsID);
            LoadCharaPartsData(hInfo, bInfo, aInfo, lInfo);
        }

        public void LoadCharaAPartData(PartsID id)
        {
            PartsType type = id.ToPartsType();
            switch(type)
            {
                case PartsType.Head:
                    headID = id;
                    break;
                case PartsType.Body:
                    bodyID = id;
                    break;
                case PartsType.Arms:
                    armsID = id;
                    break;
                case PartsType.Legs:
                    legsID = id;
                    break;
            }
            LoadCharaPartsData(headID, bodyID, armsID, legsID);
        }

        public void BlowAway(float power, float toque, float duration)
        {
            float RandomToque() 
            {
                return Random.Range(-toque, toque); 
            }

            Vector2 RandomVec(float xmin, float xmax, float ymin, float ymax) 
            {
                return new Vector2(Random.Range(xmin, xmax), Random.Range(ymin, ymax)) * power;
            }

            Vector2 headAddForce        = RandomVec(-2f, 2f, 2f, 3f);
            Vector2 bodyAddForce        = RandomVec(-2f, 2f, 2f, 3f);
            Vector2 leftArmsAddForce    = RandomVec(-2f, -5f, 2f, 5f);
            Vector2 rightArmsAddForce   = RandomVec(2f, 5f, 2f, 5f);
            Vector2 leftLegsAddForce    = RandomVec(-2f, -5f, -2f, -5f);
            Vector2 rightLegsAddForce   = RandomVec(2f, 5f, -2f, -5f);

            headView.BlowAway(headAddForce, RandomToque(), duration);
            bodyView.BlowAway(bodyAddForce, Random.Range(-24, 24), duration);
            leftArmView.BlowAway(leftArmsAddForce, RandomToque(), duration);
            rightArmView.BlowAway(rightArmsAddForce, RandomToque(), duration);
            leftLegView.BlowAway(leftLegsAddForce, RandomToque(), duration);
            rightLegView.BlowAway(rightLegsAddForce, RandomToque(), duration);
        }

        public void ResetRotation()
        {
            headView.ResetRotation();
            bodyView.ResetRotation();
            leftArmView.ResetRotation();
            rightArmView.ResetRotation();
            leftLegView.ResetRotation();
            rightLegView.ResetRotation();
		}

        public Vector2 GetHeadPosition => headView.transform.position;
        public Vector2 GetBodyPosition => bodyView.transform.position;
        public Vector2 GetLeftArmPosition => leftArmView.transform.position;
        public Vector2 GetRightArmPosition => rightArmView.transform.position;
        public Vector2 GetLeftLegPosition => leftLegView.transform.position;
        public Vector2 GetRightLegPosition => rightLegView.transform.position;

        private void OnDestroy()
        {
            OnDestroyAction?.Invoke();
        }

        public void SetColor(PartsType partsType, Color color)
        {
            Array.ForEach(GetInfoView(partsType), x => x.SetColor(color));
		}

        public void DOFadeColor(PartsType partsType, Color color, float duration)
        {
            Array.ForEach(GetInfoView(partsType), x => x.DOFadeColor(color, duration));
        }

        public CharaPartsView[] GetInfoView(PartsType partsType)
        {
            switch(partsType)
            {
                case PartsType.Head:
                    return new [] { headView };
                case PartsType.Body:
                    return new[] { bodyView };
                case PartsType.Arms:
                    return new[] { leftArmView, rightArmView };
                case PartsType.Legs:
                    return new[] { leftLegView, rightLegView };
            }
            return null;
		}

        public void GenerateAura(EffectID id)
        {
            headView.GenerateAura(id);
            bodyView.GenerateAura(id);
            leftArmView.GenerateAura(id);
            rightArmView.GenerateAura(id);
            leftLegView.GenerateAura(id);
            rightLegView.GenerateAura(id);
        }

        public void DestroyAura()
        {
            headView.DestroyAura();
            bodyView.DestroyAura();
            leftArmView.DestroyAura();
            rightArmView.DestroyAura();
            leftLegView.DestroyAura();
            rightLegView.DestroyAura();
        }
    }
}
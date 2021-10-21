using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using Random = UnityEngine.Random;

namespace HellRoad.External
{
    public class RandomizeWholeBodyView : MonoBehaviour
    {
        [SerializeField] WholeBodyView wholeBody = null;
        [SerializeField] Animator animator = null;

        [SerializeField] List<PartsID> heads;
        [SerializeField] List<PartsID> bodies;
        [SerializeField] List<PartsID> arms;
        [SerializeField] List<PartsID> legs;

#if UNITY_EDITOR
        [MenuItem("CONTEXT/RandomizeWholeBodyView/SetAllPartsID")]
        static void SetAllPartsID(MenuCommand command)
        {
            RandomizeWholeBodyView view = command.context as RandomizeWholeBodyView;

            var allParts = Enum.GetValues(typeof(PartsID));    
            foreach(PartsID parts in allParts)
            {
                switch(parts.ToPartsType())
                {
                    case PartsType.Head:
                        view.heads.Add(parts);
                        break;
                    case PartsType.Body:
                        view.bodies.Add(parts);
                        break;
                    case PartsType.Arms:
                        view.arms.Add(parts);
                        break;
                    case PartsType.Legs:
                        view.legs.Add(parts);
                        break;
                }
            }
        }
#endif

        public void Randomize()
        {
            wholeBody.LoadCharaPartsData(
                heads[Random.Range(0, heads.Count)],
                bodies[Random.Range(0, bodies.Count)],
                arms[Random.Range(0, arms.Count)],
                legs[Random.Range(0, legs.Count)]);
        }

        public void PlayAnim(AnimName name)
        {
            animator.CrossFade(name.ToString(), 0.1f);
        }
    }

}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using UnityEngine;

using Random = System.Random;

namespace HellRoad
{
    public class EnemyBrain : CharaBrain
    {
        public async override Task<TurnActionType> GetResult(ICharasFieldGetCharaInfo field)
        {
            return await Task.Run(() => { return Thinking(field); });
        }

        private TurnActionType Thinking(ICharasFieldGetCharaInfo field)
        {
            Random rnd = new Random();
            List<TurnActionType> actionTypes = new List<TurnActionType>() { TurnActionType.Attack };

            //スキル使用
            {
                ReadOnlyCollection<int> usableSkill = field.GetFrontCharaInfo().GetInfo().GetUsableSkills.CanUseSkills();
                if (usableSkill.Count != 0)
                {
                    int skillId = usableSkill[rnd.Next(0, usableSkill.Count)];
                    actionTypes.Add(TurnActionType.ExMethod.UseSkill(skillId));
                }
            }
            return actionTypes[rnd.Next(0, actionTypes.Count)];
        }
    }
}
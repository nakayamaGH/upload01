using HellRoad.External.Animation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utility;

namespace HellRoad
{
    public class Party : IParty
    {
        private IChara[] charas = new IChara[] { new NullChara(0), new NullChara(1), new NullChara(2), new NullChara(3) };

        public event Action<int> OnAddChara;
        public event Action<int> OnRemoveChara;
        public event Action<int, PartsID> OnStickParts;
        public event Action<int, int> OnSortingParty;
        public event Action OnShiftedParty;

        public Players Players { get; private set; }

        public Party(Players players, int charaCount = 0)
        {
            Players = players;
            for (int i = 0; i < charaCount; i++)
                AddChara(i);
        }

        public Party(EnemyGroupAsset enemyGroup)
        {
            Players = Players.Enemy;
            for (int i = 0; i < enemyGroup.Group.Count; i++)
                AddChara(i, enemyGroup.Group[i]);
        }

        public void AddChara(int idx)
        {
            AddCharaTemplate(idx, new Chara(Players, idx));
        }

        public void AddChara(int idx, CharaTemplate template)
        {
            AddCharaTemplate(idx, new Chara(Players, idx, template));
        }

        private void AddCharaTemplate(int idx, IChara chara)
        {
            IAddGameAnimation addAnim = Locater<IAddGameAnimation>.Resolve();
            charas[idx] = chara;
            charas[idx].OnDeadAction += (battleChara) =>
            {
                if (battleChara.PartyIdx == 0) addAnim.Add(BaseBattleCharaAnimationDatas.DeadAnimation(Players, ContainsCharaInParty()));
                ((IAddAndRemoveCharaInParty)this).RemoveChara(battleChara.PartyIdx);
                if (battleChara.PartyIdx == 0)
                {
                    OnDeadCharaSort();
                }
            };
            charas[idx].OnChangeParamAction += (before, param, chara) =>
            {
                if (chara.PartyIdx == 0)
                {
                    if(param != null)
                    {
                        if (param.Type == StatusParamType.HP)
                        {
                            addAnim.Add(new ValidateHpBarAnimation(chara.Players, (int)chara.GetValue(StatusParamType.MaxHP), (int)chara.GetValue(StatusParamType.HP), (int)before));
                        }
                        else
                        {
                            addAnim.Add(new ValidateStatusParamViewAnimation(param.Type, param.Value, chara.Players));
                        }
                    }
                    
                }
            };
            chara.OnAddParty();
            OnAddChara?.Invoke(idx);
        }

        public void RemoveChara(int idx)
        {
            charas[idx] = new NullChara(idx);
            OnRemoveChara?.Invoke(idx);
        }

        public CharaInfo GetInfo(int idx)
        {
            return charas[idx].GetInfo();
        }

        public void StickParts(int idx, PartsID parts)
        {
            charas[idx].StickParts(parts);
            OnStickParts?.Invoke(idx, parts);
        }

        public bool ContainsChara(int idx)
        {
            return charas[idx].IsAlive();
        }

        ReadOnlyCollection<IBattleChara> IBattleParty.GetCharas()
        {
            return Array.AsReadOnly<IBattleChara>(charas);
        }

        public void Sort(int idx_1, int idx_2)
        {
            if (idx_1 == idx_2) return;

            IChara chara_1 = charas[idx_1];
            IChara chara_2 = charas[idx_2];

            if (chara_1 is NullChara || chara_2 is NullChara) return;

            charas[idx_1] = charas[idx_2];
            charas[idx_1].OnSorted(idx_1);

            charas[idx_2] = chara_1;
            charas[idx_2].OnSorted(idx_2);

            OnSortingParty?.Invoke(idx_1, idx_2);
        }

        public void Shift(Direction dir)
        {
            List<IChara> nonNullCharas = new List<IChara>();
            for (int i = 0; i < charas.Length; i++)
            {
                if (!(charas[i] is NullChara))
                {
                    nonNullCharas.Add(charas[i]);
                }
            }
            int count = nonNullCharas.Count;
            if (count == 0) return;
            if (dir == Direction.Left || dir == Direction.Bottom)
            {
                IChara chara_0 = nonNullCharas[0];
                for (int i = 0; i < count - 1; i++)
                {
                    nonNullCharas[i] = nonNullCharas[i + 1];
                }
                nonNullCharas[count - 1] = chara_0;
            }
            else
            if (dir == Direction.Right || dir == Direction.Top)
            {
                IChara chara_Max = nonNullCharas[count - 1];
                for (int i = count - 1; i > 0; i--)
                {
                    nonNullCharas[i] = nonNullCharas[i - 1];
                }
                nonNullCharas[0] = chara_Max;
            }
            for (int i = 0; i < charas.Length; i++)
            {
                if (i < nonNullCharas.Count)
                {
                    charas[i] = nonNullCharas[i];
                    charas[i].OnSorted(i);
                }
                else
                {
                    charas[i] = new NullChara(i);
                }
            }
            OnShiftedParty?.Invoke();
        }

        private void OnDeadCharaSort()
        {
            for(int i = 0; i < charas.Length; i++)
            {
                if(!(charas[i] is NullChara))
                {
                    charas[0] = charas[i];
                    charas[i] = new NullChara(i);
                    charas[0].OnSorted(0);
                    charas[i].OnSorted(i);
                    OnShiftedParty?.Invoke();
                    return;
				}
			}
		}


        IGetStickedParts IStickPartsInParty.GetStickedParts(int idx)
        {
            return charas[idx];
        }

        public bool ContainsCharaInParty()
        {
            for (int i = 0; i < PartyConstantValiable.MAX_PARTY_CHARA_LIMIT; i++)
            {
                if (ContainsChara(i))
                    return true;
            }
            return false;
        }
    }
}
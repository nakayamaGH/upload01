using Cysharp.Threading.Tasks;
using HellRoad.External;
using HellRoad.External.Animation;
using System;
using Utility;

namespace HellRoad
{
    public abstract class PassiveSkill
    {
        public abstract PassiveSkillID ID { get;  }
        public abstract When When { get;  }
        public abstract Who Who { get;}

        public bool CanPlay(When When, Who Who)
        {
            return this.When == When && this.Who == Who;
        }

        protected abstract void PlayAction(BattleActionArgs args);
        public abstract void OnRemoveSkill(RemovePassiveSkillBattleActionArgs args);

        public void Play(BattleActionArgs args)
        {
            if (CanPlay(args))
            {
                ShowUsePassiveSkillAnim(ID, args.MyFrontChara.Players);
                PlayAction(args);
            }
        }

        public virtual bool CanPlay(BattleActionArgs args) => true;

        //Animation
        private void ShowUsePassiveSkillAnim(PassiveSkillID id, Players players)
        {
            Locater<IAddGameAnimation>.Resolve().Add(new OriginalAnimation(async (anim) =>
            {
                anim.EndAnimation = true;
                ICharaTurnActionView charaTurnActionView = Locater<ICharaTurnActionView>.Resolve((int)players + 2);
                charaTurnActionView.Show(id);
                await UniTask.Delay(TimeSpan.FromSeconds(3f));
                charaTurnActionView.Hide();
            }));
        }
    }
}
namespace HellRoad
{
    public interface IGetCharaInfo : IGetParamValue
    {
        CharaInfo GetInfo();
    }

    public class CharaInfo
    {
        public readonly IGetParamValue GetParamValue;
        public readonly IGetHavePassiveSkillsProperty GetPassiveSkills;
        public readonly IGetUsableSkillBarProperty GetUsableSkills;
        public readonly IGetHaveBuffsProperty GetBuff;
        public readonly IGetWholeBodyProperty GetWholeBody;

        public CharaInfo(IGetParamValue getParamValue, IGetWholeBodyProperty getWholeBody, IGetHavePassiveSkillsProperty getPassiveSkills, IGetUsableSkillBarProperty getUsableSkills, 
            IGetHaveBuffsProperty getBuff)
        {
            GetParamValue = getParamValue;
            GetWholeBody = getWholeBody;
            GetPassiveSkills = getPassiveSkills;
            GetUsableSkills = getUsableSkills;
            GetBuff = getBuff;
        }
    }
}
namespace HellRoad
{
    public class WholeBody : IGetWholeBodyProperty
    {
        public PartsID Head { get; private set; } = PartsID.None;
        public PartsID Body { get; private set; } = PartsID.None;
        public PartsID Arms { get; private set; } = PartsID.None;
        public PartsID Legs { get; private set; } = PartsID.None;

        public WholeBody(PartsID head, PartsID body, PartsID arms, PartsID legs)
        {
            Head = head;
            Body = body;
            Arms = arms;
            Legs = legs;
        }

        public WholeBody()
        {

        }

        public void Stick(PartsID parts)
        {
            PartsType type = parts.ToPartsType();
            switch (type)
            {
                case PartsType.Head:
                    Head = parts;
                    break;
                case PartsType.Body:
                    Body = parts;
                    break;
                case PartsType.Arms:
                    Arms = parts;
                    break;
                case PartsType.Legs:
                    Legs = parts;
                    break;
            }
        }

        public PartsID GetParts(PartsType type)
        {
            switch(type)
            {
                case PartsType.Head:
                    return Head;
                case PartsType.Body:
                    return Body;
                case PartsType.Arms:
                    return Arms;
                case PartsType.Legs:
                    return Legs;
            }
            return PartsID.None;
        }
    }
}
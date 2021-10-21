using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HellRoad
{
    public interface IGetHaveBuffsProperty
    {
        ReadOnlyCollection<IGetBuffProperty> Get();
    }
}

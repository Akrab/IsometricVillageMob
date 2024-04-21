using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Game;

namespace IsometricVillageMob.Services
{
    public interface IResourceService
    {
        IReadOnlyList<IResourceModel> GetResources();

        IResourceModel Get(ResourceType resourceType);
    }
}
using System.Collections.Generic;
using IsometricVillageMob.DataModel;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.Containers;

namespace IsometricVillageMob.Services
{
    public class ResourceService : IResourceService
    {

        private List<IResourceModel> _models;

        public ResourceService(ModelContainer modelContainer)
        {
            var items = modelContainer.Get<Resource>().Data;
            _models = new List<IResourceModel>(items.Length);
            foreach (var item in items)
                _models.Add(new ResourceModel<ResourceData>(item));
        }

        public IReadOnlyList<IResourceModel> GetResources()
        {
            return _models;
        }

        public IResourceModel Get(ResourceType resourceType)
        {
            return _models.Find(D => D.ResourceType == resourceType);
        }
    }
}
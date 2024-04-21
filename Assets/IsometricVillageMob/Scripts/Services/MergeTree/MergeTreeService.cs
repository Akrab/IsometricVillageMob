using System.Collections.Generic;
using IsometricVillageMob.DataModel.Merge.MergeTree;
using IsometricVillageMob.Game;
using IsometricVillageMob.Infrastructure.Containers;

namespace IsometricVillageMob.IsometricVillageMob.Scripts.Services.MergeTree
{
    public interface IMergeTreeService
    {
        IReadOnlyList<IMergeTree> GetMergeTrees();
        IMergeTree Get(ItemType itemType);
        IMergeTree Get(ResourceType r1, ResourceType r2);
    }

    public class MergeTreeService : IMergeTreeService
    {
        private List<IMergeTree> _models;

        public MergeTreeService(ModelContainer modelContainer)
        {
            var items = modelContainer.Get<MergeTreas>().Data;
            _models = new List<IMergeTree>(items.Length);
            foreach (var item in items)
                _models.Add(new MergeTreeModel<TreeData>(item));
        }

        public IReadOnlyList<IMergeTree> GetMergeTrees()
        {
            return _models;
        }

        public IMergeTree Get(ItemType itemType)
        {
            return _models.Find(D => D.ItemType == itemType);
        }

        public IMergeTree Get(ResourceType r1, ResourceType r2)
        {
            return  _models.Find(D =>
                (D.Resource[0].Resource == r1 && D.Resource[1].Resource == r2) || 
                (D.Resource[1].Resource == r1 && D.Resource[0].Resource == r2)
            );
        }
    }
}
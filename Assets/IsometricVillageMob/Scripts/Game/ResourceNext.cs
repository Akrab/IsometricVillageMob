using System;
using System.Collections.Generic;

namespace IsometricVillageMob.Game
{
    public class ResourceNext
    {
        private List<ResourceType> _data;

        private ResourceType _resourceType;
        
        public ResourceNext()
        {
            _data =  new List<ResourceType>((ResourceType[])Enum.GetValues(typeof(ResourceType)));
            _data.Remove(ResourceType.None);
            _resourceType = ResourceType.None;
        }

        public ResourceType Next()
        {
            int index = 0;
            if (_resourceType != ResourceType.None)
                index =_data.IndexOf(_resourceType) + 1;
            
            if (index >= _data.Count)
                index = 0;

            _resourceType = _data[index];
            return _resourceType;

        }
    }
}
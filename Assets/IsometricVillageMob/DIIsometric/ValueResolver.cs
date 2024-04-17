namespace IsometricVillageMob.DIIsometric
{
    public class ValueResolver : IResolver
    {
        private readonly object _value;
        public ValueResolver(object data)
        {
            _value = data;
        } 
        public object Resolve()
        {
            return _value;
        }
    }
}
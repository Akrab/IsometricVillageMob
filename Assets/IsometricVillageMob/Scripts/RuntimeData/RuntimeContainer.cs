namespace IsometricVillageMob.RuntimeData
{

    public enum ResourceCountMode
    {
        One = 0,
        Two,
        Three,
    }
    
    public class RuntimeContainer
    {
        public ResourceCountMode ResourceCountMode = ResourceCountMode.One;
    }
}
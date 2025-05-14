using Unity.Entities;

public struct EarthquakeStateTag : IComponentData
{
}
    public struct EarthquakeState : IComponentData
{
    public bool Active;
    public float Timer;
}

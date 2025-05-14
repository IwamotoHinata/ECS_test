using Unity.Entities;

public struct EarthquakeState : IComponentData
{
    public bool Active;
    public float Timer;
}

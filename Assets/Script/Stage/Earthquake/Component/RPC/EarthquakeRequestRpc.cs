using Unity.NetCode;
using Unity.Entities;
using Unity.Collections;

public struct EarthquakeRequestRpc : IRpcCommand
{
    public FixedString32Bytes SenderId;
}
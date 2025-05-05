using Unity.Mathematics;
using Unity.NetCode;


public struct ConnectRequest : IRpcCommand
{
    public int DeviceMode;
    public float3 SpawnPositioin;
}

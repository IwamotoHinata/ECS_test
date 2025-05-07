using Unity.Mathematics;
using Unity.NetCode;
using Unity.Collections;

public struct ConnectRequest : IRpcCommand
{
    public int DeviceMode;
    public FixedString128Bytes AvatorURL;
    public float3 SpawnPositioin;
}

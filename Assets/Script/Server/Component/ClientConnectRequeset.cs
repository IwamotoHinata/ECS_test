using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

/// <summary>
/// クライアントがサーバー接続時にスポーン場所を指定する用コンポーネント
/// </summary>
public struct ClientConnectRequest : IComponentData
{
    public int DeviceMode;
    public FixedString128Bytes AvatorURL;
    public float3 SpawnPosition;
}
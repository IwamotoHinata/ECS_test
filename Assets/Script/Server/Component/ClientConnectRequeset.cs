using Unity.Entities;
using Unity.Mathematics;
using Unity.Collections;

/// <summary>
/// �N���C�A���g���T�[�o�[�ڑ����ɃX�|�[���ꏊ���w�肷��p�R���|�[�l���g
/// </summary>
public struct ClientConnectRequest : IComponentData
{
    public int DeviceMode;
    public FixedString128Bytes AvatorURL;
    public float3 SpawnPosition;
}
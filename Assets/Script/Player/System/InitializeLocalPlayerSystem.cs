using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Collections;

public partial struct InitializeLocalPlayerSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<NetworkId>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = new EntityCommandBuffer(Allocator.Temp);

        // GhostOwnerIsLocal�R���|�[�l���g��GhostOwner��NetworkId�������̂ƈ�v����ꍇ�ɗL�����������ۂ��̂ŁA���ꂪ�L���ȃG���e�B�e�B��OwnerPlayerTag������
        foreach (var (_, entity) in SystemAPI.Query<PlayerTag>().WithAll<GhostOwnerIsLocal>().WithNone<OwnerPlayerTag>().WithEntityAccess())
        {
            ecb.AddComponent<OwnerPlayerTag>(entity);
            ecb.SetComponent(entity, new PlayerInput { MoveValue = float2.zero });
        }
        ecb.Playback(state.EntityManager);
    }
}

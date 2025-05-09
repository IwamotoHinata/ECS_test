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

        // GhostOwnerIsLocalコンポーネントはGhostOwnerのNetworkIdが自分のと一致する場合に有効化されるっぽいので、これが有効なエンティティにOwnerPlayerTagをつける
        foreach (var (_, entity) in SystemAPI.Query<PlayerTag>().WithAll<GhostOwnerIsLocal>().WithNone<OwnerPlayerTag>().WithEntityAccess())
        {
            ecb.AddComponent<OwnerPlayerTag>(entity);
            ecb.SetComponent(entity, new PlayerInput { MoveValue = float2.zero });
        }
        ecb.Playback(state.EntityManager);
    }
}

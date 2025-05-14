using Unity.Entities;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class EarthquakeRpcReceiveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        foreach (var (rpc, entity) in SystemAPI.Query<EarthquakeRequestRpc>().WithEntityAccess())
        {
            // SubSceneのエンティティ操作（例：EarthquakeStateの変更）
            if (SystemAPI.TryGetSingletonEntity<EarthquakeStateTag>(out var stateEntity))
            {
                var state = EntityManager.GetComponentData<EarthquakeState>(stateEntity);
                if (!state.Active)
                {
                    state.Active = true;
                    state.Timer = 90f;
                    EntityManager.SetComponentData(stateEntity, state);
                    // 受信ログ
                    UnityEngine.Debug.Log($"地震リクエスト受信 from {rpc.SenderId}");
                }
            }

            // RPCは1回使い切りなので削除
            ecb.DestroyEntity(entity);
        }

        ecb.Playback(EntityManager);
    }
}
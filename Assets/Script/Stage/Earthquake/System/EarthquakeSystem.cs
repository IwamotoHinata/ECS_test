using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

//OnUpdate関数が呼ばれない理由は8行目の属性のせい。解決法を調べろ
//[UpdateInGroup(typeof(PhysicsSimulationGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
//後でISystemにしろ。今は開発のためにSystemBaseに
public partial class EarthquakeSystem : SystemBase
{
    /*
    public void OnUpdate(ref SystemState state)
    {
        Debug.Log("gseges");
        var earthquakeState = SystemAPI.GetSingleton<EarthquakeState>();
        Debug.Log("aaaaaa");
        if (!earthquakeState.Active)
            return;

        Debug.Log("地震の処理実行中");

        foreach (var (velocity, entity) in SystemAPI.Query<RefRW<PhysicsVelocity>>().WithAll<EarthquakeObjectTag>().WithEntityAccess())
        {
            velocity.ValueRW.Linear += new float3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        }

        earthquakeState.Timer -= SystemAPI.Time.DeltaTime;
        if (earthquakeState.Timer <= 0f)
        {
            earthquakeState.Active = false;
        }

        SystemAPI.SetSingleton(earthquakeState);
        
    }
*/

    EarthquakeState state;
    protected override void OnUpdate()
    {
        Debug.Log("gseges");
        //var earthquakeState = SystemAPI.GetSingleton<EarthquakeState>();
        if (SystemAPI.TryGetSingletonEntity<EarthquakeStateTag>(out var stateEntity))
        {
            state = EntityManager.GetComponentData<EarthquakeState>(stateEntity);
            if (!state.Active)
            {
                Debug.Log("まだやぞ");
                return;
            }

        }
        else
            return;

        Debug.Log("aaaaaa");
        //if (!earthquakeState.Active)
            //return;

        Debug.Log("地震の処理実行中");

        foreach (var (velocity, entity) in SystemAPI.Query<RefRW<PhysicsVelocity>>().WithAll<EarthquakeObjectTag>().WithEntityAccess())
        {
            velocity.ValueRW.Linear += new float3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        }

        state.Timer -= SystemAPI.Time.DeltaTime;
        Debug.Log(state.Timer);
        if (state.Timer <= 0f)
        {
            state.Active = false;
        }

        SystemAPI.SetSingleton(state);
    }
}

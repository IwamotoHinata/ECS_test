using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(PhysicsSimulationGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial struct EarthquakeSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        Debug.Log("gseges");
        var earthquakeState = SystemAPI.GetSingleton<EarthquakeState>();
        Debug.Log("aaaaaa");
        if (!earthquakeState.Active)
            return;

        Debug.Log("ínêkÇÃèàóùé¿çsíÜ");

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
}

using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class EarthquakeSystemAuthoring : MonoBehaviour
{
    public class Baker : Baker<EarthquakeSystemAuthoring>
    {
        public override void Bake(EarthquakeSystemAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent<EarthquakeStateTag>(entity);
            AddComponent(entity, new PhysicsStep
            {
                SimulationType = SimulationType.UnityPhysics,
                Gravity = new float3(0, -9.81f, 0),
                SolverIterationCount = 4,
            });
            AddComponent(entity, new EarthquakeState
            {
                Active = false,
                Timer = 0
            });
        }
    }
}

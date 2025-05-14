using Unity.Entities;
using UnityEngine;

public class EarthquakeSystemAuthoring : MonoBehaviour
{
    public class Baker : Baker<EarthquakeSystemAuthoring>
    {
        public override void Bake(EarthquakeSystemAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new EarthquakeState
            {
                Active = false,
                Timer = 0
            });
        }
    }
}

using UnityEngine;
using Unity.Entities;

public class EarthquakeAuthoring : MonoBehaviour
{
    public struct EarthquakeSystemTag : IComponentData { }

    public class Baker : Baker<EarthquakeAuthoring>
    {
        public override void Bake(EarthquakeAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);//EntityÇ∆ÇµÇƒê∂ê¨
            AddComponent<EarthquakeSystemTag>(entity);
        }
    }
}

using Unity.Entities;
using UnityEngine;

public class EarthquakeObjectAuthoring : MonoBehaviour
{
    public class Baker : Baker<EarthquakeObjectAuthoring>
    {
        public override void Bake(EarthquakeObjectAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<EarthquakeObjectTag>(entity);            
        }
    }
}

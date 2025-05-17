using Unity.Entities;
using UnityEngine;

public class DisasterObjectAuthoring : MonoBehaviour
{
    public class Baker : Baker<DisasterObjectAuthoring>
    {
        public override void Bake(DisasterObjectAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<DisasterObjectTag>(entity);
        }
    }
}

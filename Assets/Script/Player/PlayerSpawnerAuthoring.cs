using Unity.Entities;
using UnityEngine;

public struct PlayerSpawner : IComponentData
{
    public Entity Player;
}

public class PlayerSpawnerAuthoring : MonoBehaviour
{
    public GameObject Player;

    public class PlayerSpawnerAuthoringBaker : Baker<PlayerSpawnerAuthoring>
    {
        public override void Bake(PlayerSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new PlayerSpawner
            {
                Player = GetEntity(authoring.Player, TransformUsageFlags.Dynamic)
            });
        }
    }
}

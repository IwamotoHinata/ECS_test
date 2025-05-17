using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct DisasterObjectMap
{
    public BlobArray<FixedString32Bytes> Keys;
    public BlobArray<Entity> Values;
}

public struct DisasterObjectSpawner : IComponentData
{
    public BlobAssetReference<DisasterObjectMap> DisasterObjectEntities;
    public bool IsSpawned;
}

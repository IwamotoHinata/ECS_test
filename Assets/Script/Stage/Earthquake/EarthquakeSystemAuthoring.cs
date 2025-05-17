using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
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

            if (Resources.Load<DisasterObjectDataBase>("DisasterDataBase"))
            {
                Debug.Log("DisasterObjectDataBaseを読み込めた");
            }
            else
                Debug.LogError("DisasterObjectDataBaseを読み込めませんでした");

            //ここにBlobAssetReferenceを作成する処理を入れる
            /*
            int count = Resources.Load<DisasterObjectDataBase>("DisasterDataBase").DisasterObjectsDataBase.GetDictionary.Count;
            using var builder = new BlobBuilder(Allocator.Temp);
            ref var root = ref builder.ConstructRoot<DisasterObjectMap>();
            var keys = builder.Allocate(ref root.Keys, count);//FixedString32Bytes型
            var values = builder.Allocate(ref root.Values, count);//Entity型

            int nowIndex = 0;
            foreach (KeyValuePair<string, GameObject> disasterObject in Resources.Load<DisasterObjectDataBase>("DisasterDataBase").DisasterObjectsDataBase.GetDictionary)
            {
                keys[nowIndex] = new FixedString32Bytes(disasterObject.Key);
                values[nowIndex] = GetEntity(disasterObject.Value, TransformUsageFlags.Dynamic);

                Debug.Log(values[nowIndex].Index);
                nowIndex++;
            }

            var blobAsset = builder.CreateBlobAssetReference<DisasterObjectMap>(Allocator.Persistent);
            AddBlobAsset(ref blobAsset, out var hash); //Unityが内部でblobAssetの参照カウントとクリーンアップを管理
            
            AddComponent(entity, new DisasterObjectSpawner
            {
                DisasterObjectEntities = blobAsset,
                IsSpawned = false,
            });

            builder.Dispose();
            */
        }
    }
}

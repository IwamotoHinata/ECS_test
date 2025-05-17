using System.Collections.Generic;
using System.IO;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
public partial class DisasterObjectSpawnSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<HostPlayerTag>();
        RequireForUpdate<DisasterObjectSpawner>();

        //Xmlファイルの読み込み（今後）
    }

    protected override void OnStartRunning()
    {

    }

    protected override void OnUpdate()
    {
        //必要なEntityやIComponentDataのコピーを取得
        SystemAPI.TryGetSingletonEntity<EarthquakeStateTag>(out var stateEntity);

        var disasterObjectSpawner = SystemAPI.GetSingleton<DisasterObjectSpawner>();
        var earthquakeState = SystemAPI.GetSingleton<EarthquakeState>();

        Debug.Log("OnUpdate動いてる");

        //地震がActive状態でなければスルー
        if (!earthquakeState.Active)
        {
            if (disasterObjectSpawner.IsSpawned)
            {
                disasterObjectSpawner.IsSpawned = false;
                //SystemAPI.SetSingleton(disasterObjectSpawner);
                EntityManager.SetComponentData(stateEntity, disasterObjectSpawner);
            }
            return;
        }
        else //Active状態になったときに災害オブジェクトをスポーン
        {
            if (!disasterObjectSpawner.IsSpawned)
            {
                for (int i = 0; i < 1; i++)
                { 
                    //スポーン処理(Entityの生成からInstantiateまで全てここで実行)
                    var cmdBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
                    //var prefabData = Resources.Load<DisasterObjectDataBase>("DisasterDataBase").DisasterObjectsDataBase.GetDictionary["Sample"];//今後はxmlファイルで読み込んだ内容を[]の中に入れる
                }
                


                disasterObjectSpawner.IsSpawned = true;
                //SystemAPI.SetSingleton(disasterObjectSpawner);
                EntityManager.SetComponentData(stateEntity, disasterObjectSpawner);
            }
        }
    }
}

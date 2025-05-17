using System.Collections.Generic;
using System.IO;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

//OnUpdate関数が呼ばれない理由は8行目の属性のせい。解決法を調べろ
//[UpdateInGroup(typeof(PhysicsSimulationGroup))]
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
//後でISystemにしろ。今は開発のためにSystemBaseに
public partial class EarthquakeSystem : SystemBase
{
    List<string[]> _accelerations = new List<string[]>();
    int _nowIndex = 0;
    float _readTime;

    double _changeAccelerationTime = 0;

    EarthquakeState state;

    protected override void OnCreate()
    {
        //RequireForUpdate<Rigidbody>();
        //地震に関する情報を読み込む
        TextAsset csvFile = null;
        switch (UnityEngine.Random.Range(0, 2))
        {
            case 0:
                csvFile = Resources.Load("EarthquakeCSV/東日本大震災CSV") as TextAsset; // Resouces下のCSV読み込み
                Debug.Log("東日本大震災を再現します");
                _readTime = 0.0066f;
                break;
            case 1:
                csvFile = Resources.Load("EarthquakeCSV/能登半島地震CSV") as TextAsset; // Resouces下のCSV読み込み
                Debug.Log("能登半島地震を再現します");
                _readTime = 0.0016f;
                break;
        }

        StringReader reader = new StringReader(csvFile.text);
        _accelerations = new List<string[]>();

        //CSVファイルから各加速度を読み込み、反映させる
        while (reader.Peek() != -1)
        {
            Debug.Log("CSV読み込み");

            //読み込み処理
            string line = reader.ReadLine(); // 一行ずつ読み込み
            _accelerations.Add(line.Split(',')); // , 区切りでリストに追加
        }
    }


    protected override void OnUpdate()
    {
        //var earthquakeState = SystemAPI.GetSingleton<EarthquakeState>();
        if (SystemAPI.TryGetSingletonEntity<EarthquakeStateTag>(out var stateEntity))
        {
            state = EntityManager.GetComponentData<EarthquakeState>(stateEntity);
            if (!state.Active)
            {
                Debug.Log("まだやぞ");
                return;
            }

        }
        else
            return;

        Debug.Log("地震の処理実行中");

        if (_changeAccelerationTime <= 0)
        {
            foreach (var (velocity, entity) in SystemAPI.Query<RefRW<PhysicsVelocity>>().WithAll<EarthquakeObjectTag>().WithEntityAccess())
            {
                velocity.ValueRW.Linear += new float3(float.Parse(_accelerations[_nowIndex][0]), 0, float.Parse(_accelerations[_nowIndex][1]));
                //Debug.Log(entity.Index);
            }
            _nowIndex++;
            _changeAccelerationTime = _readTime;
            //Debug.Log(_nowIndex);
        }
        else
        {
            _changeAccelerationTime -= SystemAPI.Time.DeltaTime;
        }

        state.Timer -= SystemAPI.Time.DeltaTime;
        if (state.Timer <= 0f)
        {
            state.Active = false;
            Debug.Log("地震の処理終了！");
        }

        SystemAPI.SetSingleton(state);
    }
}

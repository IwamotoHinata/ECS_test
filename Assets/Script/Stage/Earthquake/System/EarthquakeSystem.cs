using UnityEngine;
using Unity.Entities;
using Unity.NetCode;
using System.IO;
using System.Collections.Generic;
using static EarthquakeAuthoring;
using Unity.Physics;

/*
 * サーバーのみに実行させる
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup))]
*/

//ClientSimulation（クライアント側）じゃないとユーザの入力を検知できない
[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial class EarthquakeSystem : SystemBase
{
    /// <summary>
    /// 徳島大学のEntity
    /// </summary>
    private Entity _tu;
    private PhysicsVelocity _physicsVelocity;
    private PhysicsMass _physicsMass;

    private bool _isDisaster = false;

    List<string[]> _accelerations = new List<string[]>();
    int _nowIndex = 0;
    float _readTime;
    double _startTime;
    double _shakingTime;
    double _changeAccelerationTime;

    protected override void OnCreate()
    {
        //RequireForUpdate<Rigidbody>();
        //地震に関する情報を読み込む
        TextAsset csvFile = null;
        switch (Random.Range(0, 2))
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

    // Update is called once per frame
    protected override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F1) && !_isDisaster)
        { 
            //Enabled = false;//一度しかこの処理を発動させないようにする
            _isDisaster = true;
            _shakingTime = 5.0f;
            _startTime = SystemAPI.Time.ElapsedTime;

            _tu = SystemAPI.GetSingletonEntity<EarthquakeSystemTag>();


            //shakingTimeに設定した時間（秒）の間揺れ続ける
            //while文やコルーチンは使えない
        }

        //地震の処理を走らせる
        if (_isDisaster)
        {
            Debug.Log("地震発生中！");
            if (SystemAPI.Time.ElapsedTime - _startTime >= _shakingTime)
            { 
                _isDisaster = false;
                Debug.Log("地震終了！");
            }
            else
            {
                _changeAccelerationTime += SystemAPI.Time.DeltaTime;
                if (_changeAccelerationTime >= _readTime)
                {
                    _changeAccelerationTime = 0;//初期化
                    
                    
                }
            }
        }
    }
}

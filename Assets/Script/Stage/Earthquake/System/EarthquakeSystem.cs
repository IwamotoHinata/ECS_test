using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

// サーバー・クライアント両方で実行される
//Todo: サーバーのみに実行させる
[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
public partial class EarthquakeSystem : SystemBase
{
    protected override void OnCreate()
    {

    }

    // Update is called once per frame
    protected override void OnUpdate()
    {
        
    }
}

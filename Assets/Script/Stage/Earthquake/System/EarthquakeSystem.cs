using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

//�T�[�o�[�݂̂Ɏ��s������
[WorldSystemFilter(WorldSystemFilterFlags.ServerSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup))]
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

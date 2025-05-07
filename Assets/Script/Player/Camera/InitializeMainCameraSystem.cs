using UnityEngine;
using Unity.Entities;


[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class InitializeMainCameraSystem : SystemBase
{
    protected override void OnCreate()
    {
        //MainCameraTag���Ȃ�����OnUpdate�֐��͌Ă΂�Ȃ�
        RequireForUpdate<MainCameraTag>();
    }
    protected override void OnUpdate()
    {
        Enabled = false;//����ň�x�����Ă΂�Ȃ�
        var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
        EntityManager.SetComponentData(mainCameraEntity, new MainCamera { Value = Camera.main });
    }
}

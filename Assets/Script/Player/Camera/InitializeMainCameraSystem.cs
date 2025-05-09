using UnityEngine;
using Unity.Entities;


[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class InitializeMainCameraSystem : SystemBase
{
    protected override void OnCreate()
    {
        //MainCameraTagがない限りOnUpdate関数は呼ばれない
        RequireForUpdate<MainCameraTag>();
    }
    protected override void OnUpdate()
    {
        Enabled = false;//これで一度しか呼ばれない
        var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
        EntityManager.SetComponentData(mainCameraEntity, new MainCamera { Value = Camera.main });
    }
}

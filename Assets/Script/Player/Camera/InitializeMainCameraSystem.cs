using UnityEngine;
using Unity.Entities;


[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class InitializeMainCameraSystem : SystemBase
{
    protected override void OnCreate()
    {
        //MainCameraTag‚ª‚È‚¢ŒÀ‚èOnUpdateŠÖ”‚ÍŒÄ‚Î‚ê‚È‚¢
        RequireForUpdate<MainCameraTag>();
    }
    protected override void OnUpdate()
    {
        Enabled = false;//‚±‚ê‚Åˆê“x‚µ‚©ŒÄ‚Î‚ê‚È‚¢
        var mainCameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
        EntityManager.SetComponentData(mainCameraEntity, new MainCamera { Value = Camera.main });
    }
}

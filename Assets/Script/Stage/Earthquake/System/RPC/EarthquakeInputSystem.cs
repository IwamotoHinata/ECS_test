using Unity.Entities;
using Unity.NetCode;

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class EarthquakeInputSystem : SystemBase
{
    private PlayerInputAction _inputActions;

    protected override void OnCreate()
    {
        _inputActions = new PlayerInputAction();
    }

    protected override void OnStartRunning()
    {
        _inputActions.Enable();
    }

    protected override void OnUpdate()
    {
        if (_inputActions.RightHandLocomotion.EarthquakeTrigger.IsPressed())
        {
            var cmdBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
            var entity = cmdBuffer.CreateEntity();
            cmdBuffer.AddComponent(entity, new EarthquakeRequestRpc
            {
                SenderId = "Client" // Å© éØï ÇµÇΩÇØÇÍÇŒIDÇ»Ç«
            });
            cmdBuffer.AddComponent(entity, new SendRpcCommandRequest());
            cmdBuffer.Playback(EntityManager);
            UnityEngine.Debug.Log("RPCèàóù");
        }
    }
}
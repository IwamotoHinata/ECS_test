using Unity.Entities;

//クライアント側のみで実行する処理
[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class EarthquakeInputSysytem : SystemBase
{
    private PlayerInputAction _inputActions;
    private Entity _ownerPlayerEntity;
    /*
    protected override void OnCreate()
    {
        RequireForUpdate<HostPlayerTag>();
        _inputActions = new PlayerInputAction();
        
    }

    protected override void OnStartRunning()
    {
        //_inputActions.Enable();
        _ownerPlayerEntity = SystemAPI.GetSingletonEntity<OwnerPlayerTag>();
    }

    protected override void OnUpdate()
    {
        var input = default(EarthquakeInput);
        input.EarthquakeTrigger = _inputActions.RightHandLocomotion.EarthquakeTrigger.IsPressed();
        EntityManager.SetComponentData(_ownerPlayerEntity, input);
    }
    */
    protected override void OnUpdate()
    { }
}

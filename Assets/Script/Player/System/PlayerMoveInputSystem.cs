using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

// このグループはクライアント側でのみ実行される
[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial class PlayerMoveInputSystem : SystemBase
{
    // InputSystemで設定したアクション
    private PlayerInputAction _inputActions;
    private Entity _ownerPlayerEntity;

    protected override void OnCreate()
    {
        //OwnerPlayerTagが無いと動かないよ
        RequireForUpdate<OwnerPlayerTag>();
        _inputActions = new PlayerInputAction();
    }

    protected override void OnStartRunning()
    {
        _inputActions.Enable();
        _ownerPlayerEntity = SystemAPI.GetSingletonEntity<OwnerPlayerTag>();
    }

    protected override void OnStopRunning()
    {
        _inputActions.Disable();
        _ownerPlayerEntity = Entity.Null;
    }

    protected override void OnUpdate()
    {
        // プレイヤーの入力をコンポーネントにセット
        EntityManager.SetComponentData(_ownerPlayerEntity, new PlayerInput
        {
            MoveValue = _inputActions.LeftHandLocomotion.Move.ReadValue<Vector2>(),
            IsJump = _inputActions.RightHandInterction.Jump.ReadValue<bool>()
        }) ;
    }
}

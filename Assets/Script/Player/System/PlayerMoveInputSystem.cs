using UnityEngine;
using Unity.Entities;
using Unity.NetCode;

// ���̃O���[�v�̓N���C�A���g���ł̂ݎ��s�����
[UpdateInGroup(typeof(GhostInputSystemGroup))]
public partial class PlayerMoveInputSystem : SystemBase
{
    // InputSystem�Őݒ肵���A�N�V����
    private PlayerInputAction _inputActions;
    private Entity _ownerPlayerEntity;

    protected override void OnCreate()
    {
        //OwnerPlayerTag�������Ɠ����Ȃ���
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
        // �v���C���[�̓��͂��R���|�[�l���g�ɃZ�b�g
        EntityManager.SetComponentData(_ownerPlayerEntity, new PlayerInput
        {
            MoveValue = _inputActions.LeftHandLocomotion.Move.ReadValue<Vector2>(),
            IsJump = _inputActions.RightHandInterction.Jump.ReadValue<bool>()
        }) ;
    }
}

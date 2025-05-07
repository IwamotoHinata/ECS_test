using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;

// �T�[�o�[�E�N���C�A���g�����Ŏ��s�����
[UpdateInGroup(typeof(PredictedSimulationSystemGroup))]
[BurstCompile]
public partial struct PlayerMoveSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;

        new PlayerMoveJob
        {
            DeltaTime = deltaTime
        }.ScheduleParallel();
    }

    [BurstCompile]
    public partial struct PlayerMoveJob : IJobEntity
    {
        public float DeltaTime;

        private void Execute(ref LocalTransform transform, in PlayerInput playerInput, in PlayerStatus playerStatus, in Simulate simulate)
        {
            //UnityEngine.Debug.Log("(x,y) = " + "(" + playerInput.MoveValue.x + "," + playerInput.MoveValue.y + ")");

            //��]
            transform = transform.RotateY(math.radians(playerInput.LookValue.x * playerStatus.LookSpeed) * DeltaTime);//transform.RotateY�̕Ԃ�l��LocalTransform�^�Ȃ̂ŁA���̂悤�ɂ���K�v����
                                                                                                                      //��̊֐���Rotate�Z�́Z�������ɉ�]������Ƃ������iPlayer���͉̂������̂݉�]�j

            //�ړ�
            //transform.Position.xz += playerInput.MoveValue * playerStatus.MoveSpeed * DeltaTime;
            transform.Position += (transform.Forward() * playerInput.MoveValue.y * playerStatus.MoveSpeed * DeltaTime) + (transform.Right() * playerInput.MoveValue.x * playerStatus.MoveSpeed * DeltaTime);
        }
    }
}
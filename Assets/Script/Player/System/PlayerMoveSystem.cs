using Unity.Burst;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;

// サーバー・クライアント両方で実行される
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

            //回転
            transform = transform.RotateY(math.radians(playerInput.LookValue.x * playerStatus.LookSpeed) * DeltaTime);//transform.RotateYの返り値はLocalTransform型なので、このようにする必要あり
                                                                                                                      //上の関数はRotate〇の〇軸方向に回転させるという物（Player自体は横方向のみ回転）

            //移動
            //transform.Position.xz += playerInput.MoveValue * playerStatus.MoveSpeed * DeltaTime;
            transform.Position += (transform.Forward() * playerInput.MoveValue.y * playerStatus.MoveSpeed * DeltaTime) + (transform.Right() * playerInput.MoveValue.x * playerStatus.MoveSpeed * DeltaTime);
        }
    }
}
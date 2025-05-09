using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.InputSystem;

[WorldSystemFilter(WorldSystemFilterFlags.ClientSimulation)]
public partial class CameraMoveSystem : SystemBase
{
    private readonly float3 MAIN_CAMERA_OFFSET = new float3(0, 1.1176f, 0);

    private Entity _ownerPlayerEntity;
    private Camera _mainCamera;

    private float3 playerLocalPosition;

    /*
     * コメントアウトの部分はQuaternionをオイラー角（float3型）に変換させるための関数
    public static float3 QuaternionToEuler(quaternion q)
    {
        float3 euler;

        // Roll (X-axis rotation)
        float sinr_cosp = 2f * (q.value.w * q.value.x + q.value.y * q.value.z);
        float cosr_cosp = 1f - 2f * (q.value.x * q.value.x + q.value.y * q.value.y);
        euler.x = math.atan2(sinr_cosp, cosr_cosp);

        // Pitch (Y-axis rotation)
        float sinp = 2f * (q.value.w * q.value.y - q.value.z * q.value.x);
        if (math.abs(sinp) >= 1f)
            euler.y = Copysign(math.PI / 2f, sinp); // Use custom copysign
        else
            euler.y = math.asin(sinp);

        // Yaw (Z-axis rotation)
        float siny_cosp = 2f * (q.value.w * q.value.z + q.value.x * q.value.y);
        float cosy_cosp = 1f - 2f * (q.value.y * q.value.y + q.value.z * q.value.z);
        euler.z = math.atan2(siny_cosp, cosy_cosp);

        return math.degrees(euler);
    }

    private static float Copysign(float magnitude, float sign)
    {
        return math.abs(magnitude) * math.select(-1f, 1f, sign >= 0f);
    }

    */

    protected override void OnCreate()
    {
        RequireForUpdate<OwnerPlayerTag>();
        RequireForUpdate<MainCameraTag>();
    }

    protected override void OnStartRunning()
    {
        // 操作しているプレイヤーのエンティティをキャッシュ
        _ownerPlayerEntity = SystemAPI.GetSingletonEntity<OwnerPlayerTag>();


        //操作しているプレイヤーの視点（CameraOffset）をキャッシュ
        //_ownerCameraOffsetEntity = _ownerPlayerEntity.

        //メインカメラをキャッシュ
        var cameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
        _mainCamera = EntityManager.GetComponentObject<MainCamera>(cameraEntity).Value;
    }

    protected override void OnStopRunning()
    {
        _ownerPlayerEntity = Entity.Null;
    }

    protected override void OnUpdate()
    {
        // 操作しているプレイヤーの現在位置を取得
        playerLocalPosition = EntityManager.GetComponentData<LocalTransform>(_ownerPlayerEntity).Position;

        //操作しているプレイヤーのPlayerInput（LookValue）とPlayerStatus(LookSpeed)を取得
        var ownerPlayerLookValue = EntityManager.GetComponentData<PlayerInput>(_ownerPlayerEntity).LookValue;
        var ownerPlayerLookSpeed = EntityManager.GetComponentData<PlayerStatus>(_ownerPlayerEntity).LookSpeed;

        // カメラのTransformを変更していく
        _mainCamera.transform.position = playerLocalPosition + MAIN_CAMERA_OFFSET;

        //カメラの回転を計算

        float x = ownerPlayerLookValue.y * ownerPlayerLookSpeed * SystemAPI.Time.DeltaTime;
        float y = ownerPlayerLookValue.x * ownerPlayerLookSpeed * SystemAPI.Time.DeltaTime;

        _mainCamera.transform.Rotate(-x, y, 0);
        _mainCamera.transform.eulerAngles = new Vector3(_mainCamera.transform.eulerAngles.x, _mainCamera.transform.eulerAngles.y, 0);//Rotate関数のみの場合,Z方向に少し回転するので補正
    }
}
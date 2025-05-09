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
     * �R�����g�A�E�g�̕�����Quaternion���I�C���[�p�ifloat3�^�j�ɕϊ������邽�߂̊֐�
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
        // ���삵�Ă���v���C���[�̃G���e�B�e�B���L���b�V��
        _ownerPlayerEntity = SystemAPI.GetSingletonEntity<OwnerPlayerTag>();


        //���삵�Ă���v���C���[�̎��_�iCameraOffset�j���L���b�V��
        //_ownerCameraOffsetEntity = _ownerPlayerEntity.

        //���C���J�������L���b�V��
        var cameraEntity = SystemAPI.GetSingletonEntity<MainCameraTag>();
        _mainCamera = EntityManager.GetComponentObject<MainCamera>(cameraEntity).Value;
    }

    protected override void OnStopRunning()
    {
        _ownerPlayerEntity = Entity.Null;
    }

    protected override void OnUpdate()
    {
        // ���삵�Ă���v���C���[�̌��݈ʒu���擾
        playerLocalPosition = EntityManager.GetComponentData<LocalTransform>(_ownerPlayerEntity).Position;

        //���삵�Ă���v���C���[��PlayerInput�iLookValue�j��PlayerStatus(LookSpeed)���擾
        var ownerPlayerLookValue = EntityManager.GetComponentData<PlayerInput>(_ownerPlayerEntity).LookValue;
        var ownerPlayerLookSpeed = EntityManager.GetComponentData<PlayerStatus>(_ownerPlayerEntity).LookSpeed;

        // �J������Transform��ύX���Ă���
        _mainCamera.transform.position = playerLocalPosition + MAIN_CAMERA_OFFSET;

        //�J�����̉�]���v�Z

        float x = ownerPlayerLookValue.y * ownerPlayerLookSpeed * SystemAPI.Time.DeltaTime;
        float y = ownerPlayerLookValue.x * ownerPlayerLookSpeed * SystemAPI.Time.DeltaTime;

        _mainCamera.transform.Rotate(-x, y, 0);
        _mainCamera.transform.eulerAngles = new Vector3(_mainCamera.transform.eulerAngles.x, _mainCamera.transform.eulerAngles.y, 0);//Rotate�֐��݂̂̏ꍇ,Z�����ɏ�����]����̂ŕ␳
    }
}
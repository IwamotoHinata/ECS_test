using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Rendering;
using UnityEngine;

// �v���C���[�F���p�i�C���X�y�N�^�[��Player�^�O���Ă���悤�Ȃ��́j
public struct PlayerTag : IComponentData { }

// InitializePlayerSystem�ŏ���������p�̔F���^�O
public struct NewPlayerTag : IComponentData { }

// ���������삷��I�u�W�F�N�g�����肷��p�̃^�O
public struct OwnerPlayerTag : IComponentData { }

// ���͊֌W�̃R���|�[�l���g��AllPredicted
// ���͓����p��IInputComponentData���p��
[GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
public struct PlayerInput : IInputComponentData
{
    // Quantization = 0�Ő��m�ȃf�[�^�𓯊�����
    [GhostField(Quantization = 0)] public float2 MoveValue;//�ړ��������i�[
    [GhostField(Quantization = 0)] public bool IsJump;//�W�����v���߂̊i�[
}


public class PlayerAuthoring : MonoBehaviour
{
    public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<NewPlayerTag>(entity);
            AddComponent<PlayerInput>(entity);
            AddComponent(entity, new PlayerStatus
            {
                BurningValue = 0,
                SuckingValue = 0,
                MoveSpeed = 5,
            });
        }
    }
}
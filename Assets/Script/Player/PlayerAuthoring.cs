using Unity.Entities;
using Unity.NetCode;
using Unity.Rendering;
using UnityEngine;

// �v���C���[�F���p�i�C���X�y�N�^�[��Player�^�O���Ă���悤�Ȃ��́j
public struct PlayerTag : IComponentData { }

// InitializePlayerSystem�ŏ���������p�̔F���^�O
public struct NewPlayerTag : IComponentData { }


public class PlayerAuthoring : MonoBehaviour
{
    public class PlayerAuthoringBaker : Baker<PlayerAuthoring>
    {
        public override void Bake(PlayerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<PlayerTag>(entity);
            AddComponent<NewPlayerTag>(entity);
            AddComponent<PlayerStatus>(entity);
        }
    }
}
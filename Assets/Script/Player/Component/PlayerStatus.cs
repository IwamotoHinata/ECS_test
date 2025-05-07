using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

// �v���C���[�X�e�[�^�X
public struct PlayerStatus : IComponentData
{
    // GhostField�����邱�ƂŁA�T�[�o�[����N���C�A���g�ɒl�����������

    [GhostField] public float BurningValue;//�Ă�������
    [GhostField] public float SuckingValue;//��_���Y�f���z������
    [GhostField] public float MoveSpeed;
    [GhostField] public float LookSpeed;
}
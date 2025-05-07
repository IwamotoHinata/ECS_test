using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Rendering;
using UnityEngine;

// プレイヤー認識用（インスペクターでPlayerタグつけているようなもの）
public struct PlayerTag : IComponentData { }

// InitializePlayerSystemで初期化する用の認識タグ
public struct NewPlayerTag : IComponentData { }

// 自分が操作するオブジェクトか判定する用のタグ
public struct OwnerPlayerTag : IComponentData { }

// 入力関係のコンポーネントはAllPredicted
// 入力同期用のIInputComponentDataを継承
[GhostComponent(PrefabType = GhostPrefabType.AllPredicted)]
public struct PlayerInput : IInputComponentData
{
    // Quantization = 0で正確なデータを同期する
    [GhostField(Quantization = 0)] public float2 MoveValue;//移動方向を格納
    [GhostField(Quantization = 0)] public bool IsJump;//ジャンプ命令の格納
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
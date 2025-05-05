using Unity.Entities;
using Unity.NetCode;
using Unity.Rendering;
using UnityEngine;

// プレイヤー認識用（インスペクターでPlayerタグつけているようなもの）
public struct PlayerTag : IComponentData { }

// InitializePlayerSystemで初期化する用の認識タグ
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
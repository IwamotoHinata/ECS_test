using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

// プレイヤーステータス
public struct PlayerStatus : IComponentData
{
    // GhostFieldをつけることで、サーバーからクライアントに値が同期される

    [GhostField] public float BurningValue;//焼けた割合
    [GhostField] public float SuckingValue;//一酸化炭素を吸った量
    [GhostField] public float MoveSpeed;
    [GhostField] public float LookSpeed;
}
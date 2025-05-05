using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

// プレイヤーステータス
public struct PlayerStatus : IComponentData
{
    // GhostFieldをつけることで、サーバーからクライアントに値が同期される
    [GhostField] public int HP;
}
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 接続モード
/// </summary>
public enum ConnectionMode
{
    /// <summary>サーバー＋クライアント</summary>
    ServerClient = 0,
    /// <summary>サーバーのみ</summary>
    Server,
    /// <summary>クライアントのみ</summary>
    Client,
}

/// <summary>
/// 接続モード
/// </summary>
public enum DevideMode
{
    /// <summary>VR</summary>
    VR = 0,
    /// <summary>PC</summary>
    PC
}


//参考:https://chimugame.com/unity-netcode-for-entities-1/

public class ClientConnectionManager : MonoBehaviour
{
    #region 定数

    private const string MAIN_SCENE_NAME = "MainScene";
    private const string SERVER_WORLD_NAME = "Tokudiverse Server";
    private const string CLIENT_WORLD_NAME = "Tokudiverse Client";
    private const string CONNECTION_MODE_ERROR_MESSAGE = "存在しない接続モードが指定されています。";

    #endregion

    #region インスペクターに表示される変数

    // ロビーのUI群をインスペクターで設定しておく
    [SerializeField] private TMP_Dropdown _connectionModeDropdown;
    [SerializeField] private TMP_InputField _addressField;
    [SerializeField] private TMP_InputField _portField;
    [SerializeField] private Button _connectButton;

    //[SerializeField] private TMP_InputField _avatorURL;
    [SerializeField] private TMP_Dropdown _useDeviceDropdown;

    #endregion

    /// <summary>
    /// ポート番号(ushort)
    /// </summary>
    /// <remarks>入力されたポート番号をushortに変換して返却</remarks>
    private ushort Port => ushort.Parse(_portField.text);

    #region 自動呼び出しされるメソッド

    private void OnEnable()
    {
        // StartボタンにOnButtonConnectメソッドを登録
        _connectButton.onClick.AddListener(OnButtonConnect);
    }

    private void OnDisable()
    {
        // Startボタンのイベントを全て削除
        _connectButton.onClick.RemoveAllListeners();
    }

    #endregion

    #region プライベートメソッド

    private void OnButtonConnect()
    {
        // デフォルトのワールドを全て破棄
        DestroyLocalSimulationWorld();
        // 遷移先のシーン名を指定してロード
        SceneManager.LoadScene(MAIN_SCENE_NAME);

        // 接続モードに応じてワールドを作成
        switch (_connectionModeDropdown.value)
        {
            case (int)ConnectionMode.ServerClient:
                StartServer();
                StartClient();
                break;
            case (int)ConnectionMode.Server:
                StartServer();
                break;
            case (int)ConnectionMode.Client:
                StartClient();
                break;
            default:
                Debug.LogError(CONNECTION_MODE_ERROR_MESSAGE, gameObject);
                break;
        }
    }

    private void DestroyLocalSimulationWorld()
    {
        foreach (var world in World.All)
        {
            if (world.Flags == WorldFlags.Game)
            {
                world.Dispose();
                break;
            }
        }
    }

    private void StartServer()
    {
        // サーバーワールド作成
        var serverWorld = ClientServerBootstrap.CreateServerWorld(SERVER_WORLD_NAME);

        // 指定されたポートで接続できるようにする
        var serverEndpoint = NetworkEndpoint.AnyIpv4.WithPort(Port);
        {
            using var networkDriverQuery = serverWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Listen(serverEndpoint);
        }
    }

    private void StartClient()
    {
        // クライアントワールド作成
        var clientWorld = ClientServerBootstrap.CreateClientWorld(CLIENT_WORLD_NAME);

        // サーバーワールドのエンドポイントに接続
        var connectionEndpoint = NetworkEndpoint.Parse(_addressField.text, Port);
        {
            using var networkDriverQuery = clientWorld.EntityManager.CreateEntityQuery(ComponentType.ReadWrite<NetworkStreamDriver>());
            networkDriverQuery.GetSingletonRW<NetworkStreamDriver>().ValueRW.Connect(clientWorld.EntityManager, connectionEndpoint);
        }

        // サーバー接続リクエスト用エンティティを作成
        var connectRequestEntity = clientWorld.EntityManager.CreateEntity();

        int random = UnityEngine.Random.Range(0, 2);

        if (random == 0)
        {
            // わかりやすいように少しずれた場所(10, 0, 0)をスポーン地点に指定
            clientWorld.EntityManager.AddComponentData(connectRequestEntity, new ClientConnectRequest
            {
                DeviceMode = _useDeviceDropdown.value,
                SpawnPosition = new float3(10, 0, 0)
            });
        }
        else
        {
            // わかりやすいように少しずれた場所(5, 0, 0)をスポーン地点に指定
            clientWorld.EntityManager.AddComponentData(connectRequestEntity, new ClientConnectRequest
            {
                DeviceMode = _useDeviceDropdown.value,
                SpawnPosition = new float3(5, 0, 0)
            });
        }

        // Default Worldは破壊しているので、clientworldを入れておく
        World.DefaultGameObjectInjectionWorld = clientWorld;
    }

    #endregion
}

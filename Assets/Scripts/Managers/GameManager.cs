using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string GAME_SCENE = "Game";

    [Header("Role lists")]
    [SerializeField] List<SO_Role> allRolesList;
    [SerializeField] List<SO_Role> defaultRolesList;

    [Header("Listeners")]
    [SerializeField] SO_Event modifyPlayerListEvent;
    [SerializeField] SO_Event startGameEvent;
    [SerializeField] SO_Event changeGameType;
    [SerializeField] SO_Event changeRoleType;

    private List<PlayerData> playerDataList = new List<PlayerData>();
    private bool isSingleDevice = false;
    private bool isCustomGame = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        modifyPlayerListEvent.OnEventRaised += ModifyPlayerList_OnEventRaised;
        startGameEvent.OnEventRaised += StartGameEvent_OnEventRaised;
        changeGameType.OnEventRaised += ChangeGameType_OnEventRaised;
        changeRoleType.OnEventRaised += ChangeRoleType_OnEventRaised;
    }

    #region EVENTS
    private void StartGameEvent_OnEventRaised(object arg1, object arg2)
    {
        StartGame();
    }

    private void ModifyPlayerList_OnEventRaised(object isAdding, object playerName)
    {
        bool isAddingToList = (bool)isAdding;
        string name = (string)playerName;

        if (isAddingToList)
        {
            AddPlayer(name);
        }
        else
        {
            RemovePlayer(name);
        }
    }

    private void ChangeRoleType_OnEventRaised(object isCustomGameType, object arg2)
    {
        isCustomGame = (bool)isCustomGameType;
    }

    private void ChangeGameType_OnEventRaised(object isSingleDeviceGame, object arg2)
    {
        isSingleDevice = (bool)isSingleDeviceGame;
    }
    #endregion

    public void StartGame()
    {
        if (isCustomGame)
        {
            List<SO_Role> roleList = allRolesList;
        }
        else
        {
            List<SO_Role> roleList = defaultRolesList;
        }

        foreach (PlayerData player in playerDataList)
        {

        }

        SceneLoader.Instance.LoadSceneByName(GAME_SCENE);
    }

    public void AddPlayer(string playerName)
    {
        PlayerData player = new PlayerData(playerName);

        playerDataList.Add(player);
    }

    public void RemovePlayer(string playerName)
    {
        PlayerData player = new PlayerData(playerName);

        playerDataList.Remove(player);
    }

    public void RemoveAllPlayers()
    {
        playerDataList.Clear();
    }

    public int GetNumberOfPlayers()
    {
        return playerDataList.Count;
    }
}

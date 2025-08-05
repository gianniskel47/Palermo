using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string GAME_SCENE = "Game";

    [Header("Default Roles")]
    [SerializeField] SO_Role hiddenKiller;
    [SerializeField] SO_Role shownKiller;
    [SerializeField] SO_Role cop;
    [SerializeField] SO_Role citizen;

    [Header("Listeners")]
    [SerializeField] SO_Event modifyPlayerListEvent;
    [SerializeField] SO_Event startGameEvent;
    [SerializeField] SO_Event changeGameType;
    [SerializeField] SO_Event changeRoleType;

    public List<SO_Role> rolesToUseInGame = new List<SO_Role>(); //PRIVATEEEE

    private List<PlayerData> playerDataList = new List<PlayerData>();
    private bool isSingleDevice = true;
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
        if (!isCustomGame)
        {
            rolesToUseInGame.Clear();
            rolesToUseInGame = GetDefaultRoleList(GetNumberOfPlayers());
        }

        foreach (PlayerData player in playerDataList)
        {
            player.SetupRole(GetRandomRole());
        }

        SceneLoader.Instance.LoadSceneByName(GAME_SCENE);
    }

    public void InitializeRoles(List<SO_Role> selectedRoles)
    {
        rolesToUseInGame.Clear();

        rolesToUseInGame = new List<SO_Role>(selectedRoles);
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

    public List<PlayerData> GetPlayerDataList()
    {
        return playerDataList;
    }

    private List<SO_Role> GetDefaultRoleList(int playerCount)
    {
        List<SO_Role> roles = new List<SO_Role>();

        roles.Add(hiddenKiller);
        roles.Add(shownKiller);
        roles.Add(cop);

        int remainingRoles = playerCount - roles.Count;
        for (int i = 0; i < remainingRoles; i++)
        {
            roles.Add(citizen);
        }

        return roles;
    }

    public SO_Role GetRandomRole()
    {
        SO_Role randomRole = rolesToUseInGame[Random.Range(0, rolesToUseInGame.Count)];
        rolesToUseInGame.Remove(randomRole);

        return randomRole;
    }
}

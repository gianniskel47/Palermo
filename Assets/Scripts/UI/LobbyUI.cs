using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button addPlayerButton;
    [SerializeField] GameObject playerSingleTemplate;
    [SerializeField] GameObject playerTemplatesContainer;
    [SerializeField] TextMeshProUGUI playerCountText;
    [SerializeField] Button startGameButton;
    [SerializeField] Button backButton;
    [SerializeField] GameObject lobbyUiPanel;
    [SerializeField] GameObject createGamePanel;

    [Header("Broadcasters")]
    [SerializeField] SO_Event modifyPlayerListEvent;
    [SerializeField] SO_Event startGameEvent;


    private void Awake()
    {
        addPlayerButton.onClick.AddListener(() => AddPlayerToGame());
        backButton.onClick.AddListener(() => Back());
        startGameButton.onClick.AddListener(() =>
        {
            startGameEvent.RaiseEvent();
            startGameButton.interactable = false;
        });
    }

    private void Start()
    {
        ClearTemplates();
    }

    private void AddPlayerToGame()
    {
        string playerName = playerNameInputField.text;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.Log("INVALID NAME");
            return;
        }

        // true is for adding player to the list
        // player name is the name to be added for the player
        // gamemanager.cs is listening to this
        modifyPlayerListEvent.RaiseEvent(true, playerName);

        GameObject templateInstance = Instantiate(playerSingleTemplate, playerTemplatesContainer.transform);

        templateInstance.GetComponent<PlayerSingleTemplate>().SetPlayer(playerName);

        UpdatePlayerCountText();
        UpdateStartGameButton();

        playerNameInputField.text = "";
    }

    // this is called when creating the room to instantiate the 1st player template
    // the playerName is never null or empty
    public void AddFirstPlayer(string playerName)
    {
        // true is for adding player to the list
        // player name is the name to be added for the player
        // gamemanager.cs is listening to this
        modifyPlayerListEvent.RaiseEvent(true, playerName);

        GameObject templateInstance = Instantiate(playerSingleTemplate, playerTemplatesContainer.transform);

        templateInstance.GetComponent<PlayerSingleTemplate>().SetPlayer(playerName);

        UpdatePlayerCountText();
        UpdateStartGameButton();
    }

    public void RemovePlayer(PlayerSingleTemplate playerTemplate)
    {
        // false is for removing player from the list
        // player name is the name to be added for the player
        // gamemanager.cs is listening to this
        modifyPlayerListEvent.RaiseEvent(false, playerTemplate.GetPlayerName());

        Destroy(playerTemplate.gameObject);

        UpdatePlayerCountText();
        UpdateStartGameButton();
    }

    private void UpdatePlayerCountText()
    {
        playerCountText.text = $"Players ({GameManager.Instance.GetNumberOfPlayers()})";
    }

    private void UpdateStartGameButton()
    {
        if(GameManager.Instance.GetNumberOfPlayers() <= 4)
        {
            Debug.Log("Need at least 5 players");
            startGameButton.interactable = false;
        }
        else
        {
            startGameButton.interactable = true;
        }
    }

    private void Back()
    {
        ClearTemplates();
        GameManager.Instance.RemoveAllPlayers();

        lobbyUiPanel.gameObject.SetActive(false);
        createGamePanel.gameObject.SetActive(true);
    }

    private void ClearTemplates()
    {
        foreach (Transform template in playerTemplatesContainer.transform)
        {
            Destroy(template.gameObject);
        }
    }
}

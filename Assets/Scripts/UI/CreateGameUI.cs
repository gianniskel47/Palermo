using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateGameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField playerNameInputField;
    [SerializeField] Button singleDeviceButton;
    [SerializeField] Button multipleDevicesButton;
    [SerializeField] Button clasicGameButton;
    [SerializeField] Button selectRolesButton;
    [SerializeField] Button createGameButton;
    [SerializeField] GameObject lobbyUiPanel;
    [SerializeField] GameObject createGamePanel;
    [SerializeField] LobbyUI lobbyUI;
    [SerializeField] RoleSelectionUI roleSelectionUI;

    [Header("Broadcasters")]
    [SerializeField] SO_Event changeGameType;
    [SerializeField] SO_Event changeRoleType;


    private void Awake()
    {
        singleDeviceButton.onClick.AddListener(() => changeGameType.RaiseEvent(true));
        multipleDevicesButton.onClick.AddListener(() => changeGameType.RaiseEvent(false));

        clasicGameButton.onClick.AddListener(() => changeRoleType.RaiseEvent(true));
        selectRolesButton.onClick.AddListener(() => 
        {
            changeRoleType.RaiseEvent(false);
            roleSelectionUI.ShowRoleSelection();
        });

        createGameButton.onClick.AddListener(() => CreateGame());
    }

    private void CreateGame()
    {
        string playerName = playerNameInputField.text;

        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.Log("Please put a name");
            return;
        }

        lobbyUI.AddFirstPlayer(playerName);
        lobbyUiPanel.SetActive(true);
        createGamePanel.SetActive(false);
    }
}

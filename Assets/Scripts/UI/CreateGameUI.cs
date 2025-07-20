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

    private bool isSingleDevice = false;
    private bool isClasicGame = false;

    private void Awake()
    {
        singleDeviceButton.onClick.AddListener(() => isSingleDevice = true);
        multipleDevicesButton.onClick.AddListener(() => isSingleDevice = false);

        clasicGameButton.onClick.AddListener(() => isClasicGame = true);
        selectRolesButton.onClick.AddListener(() => isClasicGame = false);

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

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
    [SerializeField] Button customGameButton;
    [SerializeField] Button createGameButton;
    [SerializeField] GameObject lobbyUiPanel;
    [SerializeField] GameObject createGamePanel;
    [SerializeField] LobbyUI lobbyUI;
    [SerializeField] RoleSelectionUI roleSelectionUI;

    [Header("Config")]
    [SerializeField] Color selectedColor;
    [SerializeField] Color unselectedColor;

    [Header("Broadcasters")]
    [SerializeField] SO_Event changeGameType;
    [SerializeField] SO_Event changeRoleType;


    private void Awake()
    {
        roleSelectionUI.onRoleConfirmation += CheckIfCreateButtonCanBeClicked;

        singleDeviceButton.onClick.AddListener(() =>
        {
            changeGameType.RaiseEvent(true);
            singleDeviceButton.GetComponent<Image>().color = selectedColor;
            multipleDevicesButton.GetComponent<Image>().color = unselectedColor;
        });

        multipleDevicesButton.onClick.AddListener(() =>
        {
            changeGameType.RaiseEvent(false);
            singleDeviceButton.GetComponent<Image>().color = unselectedColor;
            multipleDevicesButton.GetComponent<Image>().color = selectedColor;
        });

        clasicGameButton.onClick.AddListener(() =>
        {
            changeRoleType.RaiseEvent(false);
            clasicGameButton.GetComponent<Image>().color = selectedColor;
            customGameButton.GetComponent<Image>().color = unselectedColor;
            createGameButton.interactable = true;
        });

        customGameButton.onClick.AddListener(() => 
        {
            changeRoleType.RaiseEvent(true);
            roleSelectionUI.ShowRoleSelection();
            clasicGameButton.GetComponent<Image>().color = unselectedColor;
            customGameButton.GetComponent<Image>().color = selectedColor;
            CheckIfCreateButtonCanBeClicked();
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

    private void CheckIfCreateButtonCanBeClicked()
    {
        if (roleSelectionUI.GetSelectedRoles().Count > 4)
        {
            createGameButton.interactable = true;
        }
        else
        {
            createGameButton.interactable = false;
        }
    }
}

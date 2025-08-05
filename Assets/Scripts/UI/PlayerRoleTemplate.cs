using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoleTemplate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] GameObject roleInfoPanel;

    [Header("Config")]
    [SerializeField] float doubleClickTime = 0.3f;

    private Button playerButton;
    private SO_Role soRole;
    private float lastClickTime = -1f;

    private GameUI gameUI;

    private void Awake()
    {
        playerButton = GetComponent<Button>();    
        gameUI = GetComponentInParent<GameUI>();
    }

    private void Start()
    {
        playerButton.onClick.AddListener(OnButtonClicked);
    }


    public void Setup(SO_Role soRole, string playerName)
    {
        this.soRole = soRole;
        this.playerName.text = playerName;
    }

    public bool GetIsClicked()
    {
        return !playerButton.IsInteractable();
    }

    private void OnButtonClicked()
    {
        float currentTime = Time.time;

        if(currentTime - lastClickTime < doubleClickTime)
        {
            playerButton.interactable = false;
            ShowRoleInfo();
        }
        else
        {
            lastClickTime = currentTime;
        }
    }

    private void ShowRoleInfo()
    {
        gameUI.ShowRoleInfoPanel(soRole);
    }
}

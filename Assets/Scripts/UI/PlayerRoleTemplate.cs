using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRoleTemplate : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI playerName;

    [Header("Config")]
    [SerializeField] float doubleClickTime = 0.3f;

    private Button playerButton;
    private SO_Role soRole;
    private float lastClickTime = -1f;

    private void Awake()
    {
        playerButton = GetComponent<Button>();    
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

    private void OnButtonClicked()
    {
        float currentTime = Time.time;

        if(currentTime - lastClickTime < doubleClickTime)
        {
            ShowRoleInfo();
            playerButton.interactable = false;
        }
        else
        {
            lastClickTime = currentTime;
        }
    }

    private void ShowRoleInfo()
    {
        Debug.Log($"Player {playerName.text} is {soRole.name}");
    }
}

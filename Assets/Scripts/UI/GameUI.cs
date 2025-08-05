using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerRoleTemplateContainer;
    [SerializeField] GameObject playerRoleTemplatePrefab;
    [SerializeField] GameObject roleInfoPanelObject;
    [SerializeField] Button continueButton;

    private RoleInfoPanel roleInfoPanel;
    private List<PlayerRoleTemplate> playerRoleTemplateList = new List<PlayerRoleTemplate>();

    private void Awake()
    {
        roleInfoPanel = roleInfoPanelObject.GetComponent<RoleInfoPanel>();
    }

    private void Start()
    {
        ClearTemplates();

        foreach (PlayerData playerData in GameManager.Instance.GetPlayerDataList())
        {
            GameObject templateInstance = Instantiate(playerRoleTemplatePrefab, playerRoleTemplateContainer);
            PlayerRoleTemplate playerRoleTemplate = templateInstance.GetComponent<PlayerRoleTemplate>();
            playerRoleTemplate.Setup(playerData.Role, playerData.PlayerName);

            playerRoleTemplateList.Add(playerRoleTemplate);
        }
    }

    public void ShowRoleInfoPanel(SO_Role soRole)
    {
        roleInfoPanelObject.SetActive(true);
        roleInfoPanel.Setup(soRole);

        CheckIfCanContinue();
    }

    private void CheckIfCanContinue()
    {
        foreach (var playerRoleTemplate in playerRoleTemplateList)
        {
            if (!playerRoleTemplate.GetIsClicked()) return;
        }

        continueButton.interactable = true;
    }

    private void ClearTemplates()
    {
        foreach (Transform child in playerRoleTemplateContainer)
        {
            Destroy(child.gameObject);
        }
    }
}

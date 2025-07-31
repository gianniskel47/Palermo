using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] Transform playerRoleTemplateContainer;
    [SerializeField] GameObject playerRoleTemplatePrefab;

    private void Start()
    {
        ClearTemplates();

        foreach (PlayerData playerData in GameManager.Instance.GetPlayerDataList())
        {
            GameObject templateInstance = Instantiate(playerRoleTemplatePrefab, playerRoleTemplateContainer);
            PlayerRoleTemplate playerRoleTemplate = templateInstance.GetComponent<PlayerRoleTemplate>();
            playerRoleTemplate.Setup(playerData.Role, playerData.PlayerName);
        }
    }

    private void ClearTemplates()
    {
        foreach (Transform child in playerRoleTemplateContainer)
        {
            Destroy(child.gameObject);
        }
    }
}

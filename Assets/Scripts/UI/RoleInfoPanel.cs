using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleInfoPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI roleName;
    [SerializeField] Image roleImage;
    [SerializeField] TextMeshProUGUI roleDescription;

    public void Setup(SO_Role soRole)
    {
        roleName.text = soRole.GetRoleName();
        roleDescription.text = soRole.GetRoleDescription();
        roleImage.sprite = soRole.GetRoleSprite();
    }
}

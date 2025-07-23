using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_Role", menuName = "Scriptable Objects/Roles/SO_Role")]
public class SO_Role : ScriptableObject
{
    [Header("Config")]
    [SerializeField] string roleName;
    [TextArea(5,5)]
    [SerializeField] string description;
    [SerializeField] Sprite roleSprite;
    [SerializeField] E_Roles role;
    [SerializeField] bool isForSingleDevice;

    public Sprite GetRoleSprite()
    {
        return roleSprite;
    }

    public string GetRoleName() 
    {
        return roleName;
    }

    public string GetRoleDescription()
    {
        return description;
    }
}

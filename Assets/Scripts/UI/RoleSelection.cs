using UnityEngine;

[System.Serializable]
public class RoleSelection
{
    public SO_Role role;
    public int count;

    public RoleSelection(SO_Role role)
    {
        this.role = role;
        count = 0;  
    }
}

using UnityEngine;

public class PlayerData
{
    public string PlayerName { get; private set; }

    public SO_Role Role { get; private set; }

    public PlayerData(string name)
    {
        PlayerName = name;
    }
}

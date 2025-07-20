using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private List<string> playerNames = new List<string>();

    private int playerAmount;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        foreach (var player in playerNames)
        {
            Debug.Log(player);
        }
    }

    public void AddPlayer(string playerName)
    {
        playerNames.Add(playerName);
    }

    public void RemovePlayer(string playerName)
    {
        playerNames.Remove(playerName);
    }

    public void RemoveAllPlayers()
    {
        playerNames.Clear();
    }

    public int GetNumberOfPlayers()
    {
        return playerNames.Count;
    }
}

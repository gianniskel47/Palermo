using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSingleTemplate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] Button deleteButton;

    private void Awake()
    {
        deleteButton.onClick.AddListener(() => RemovePlayer());
    }

    public void SetPlayer(string name)
    {
        playerName.text = name;
    }

    private void RemovePlayer()
    {
        GetComponentInParent<LobbyUI>().RemovePlayer(this);
    }

    public string GetPlayerName()
    {
        return playerName.text;
    }
}

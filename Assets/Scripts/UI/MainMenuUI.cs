using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button quitButton;

    private void Awake()
    {
        quitButton.onClick.AddListener(Application.Quit);
    }
}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoleItemUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] Image roleImage;
    [SerializeField] TextMeshProUGUI roleNameText;
    [SerializeField] TextMeshProUGUI roleDescriptionText;
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] Button increaseButton;
    [SerializeField] Button decreaseButton;

    private RoleSelection roleSelection;
    private Action<RoleSelection, int> onCountChanged;

    public void Setup(RoleSelection selection, Action<RoleSelection,int> countChangeCallback)
    {
        roleSelection = selection;
        onCountChanged = countChangeCallback;

        // setup UI
        roleImage.sprite = roleSelection.role.GetRoleSprite();
        roleNameText.text = roleSelection.role.GetRoleName();
        roleDescriptionText.text = roleSelection.role.GetRoleDescription();

        // setup buttons
        increaseButton.onClick.AddListener(() => ChangeCount(1));
        decreaseButton.onClick.AddListener(() => ChangeCount(-1));
    }

    private void ChangeCount(int change)
    {
        onCountChanged?.Invoke(roleSelection, change);
        UpdateCountOnDisplay();
    }

    private void UpdateCountOnDisplay() 
    {
        countText.text = roleSelection.count.ToString();
        decreaseButton.interactable = roleSelection.count > 0;
    }
}

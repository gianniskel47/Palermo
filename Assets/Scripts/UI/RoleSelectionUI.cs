using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelectionUI : MonoBehaviour
{
    public event Action onRoleConfirmation;

    [Header("UI References")]
    [SerializeField] GameObject roleSelectionPanel;
    [SerializeField] Transform roleListParent;
    [SerializeField] GameObject roleItemPrefab;
    [SerializeField] Button confirmButton;
    [SerializeField] Button backButton;

    [Header("Data")]
    [SerializeField] List<SO_Role> allRolesList;

    private List<RoleSelection> roleSelections = new List<RoleSelection>();

    private void Awake()
    {
        confirmButton.onClick.AddListener(ConfirmRoleSelection);
        backButton.onClick.AddListener(CloseRoleSelection);
    }

    private void SetupRoleList()
    {
        foreach (Transform child in roleListParent)
        {
            Destroy(child.gameObject);
        }

        roleSelections.Clear();

        foreach (SO_Role role in allRolesList)
        {
            roleSelections.Add(new RoleSelection(role));
            CreateRoleUIItem(roleSelections.Last());
        }
    }

    private void CreateRoleUIItem(RoleSelection roleSelection)
    {
        GameObject roleItem = Instantiate(roleItemPrefab, roleListParent);
        RoleItemUI roleItemUI = roleItem.GetComponent<RoleItemUI>();
        roleItemUI.Setup(roleSelection, UpdateRoleCount);
    }

    private void UpdateRoleCount(RoleSelection roleSelection, int change)
    {
        roleSelection.count = Mathf.Max(0, roleSelection.count + change);
    }

    private void ConfirmRoleSelection()
    {
        GameManager.Instance.InitializeRoles(GetSelectedRoles());
        CloseRoleSelection();
        onRoleConfirmation?.Invoke();
    }

    public List<SO_Role> GetSelectedRoles()
    {
        List<SO_Role> selectedRoles = new List<SO_Role>();
        foreach (RoleSelection selection in roleSelections)
        {
            for (int i = 0; i < selection.count; i++)
            {
                selectedRoles.Add(selection.role);
            }
        }
        return selectedRoles;
    }

    public void ShowRoleSelection()
    {
        roleSelectionPanel.SetActive(true);
        SetupRoleList();
    }

    private void CloseRoleSelection()
    {
        roleSelectionPanel.SetActive(false);
    }
}

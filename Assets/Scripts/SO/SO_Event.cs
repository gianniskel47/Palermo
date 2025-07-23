using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Event", menuName = "Scriptable Objects/Events/SO_Event")]
public class SO_Event : ScriptableObject
{
    [TextArea(5, 5)]
    [SerializeField] private string description;
    [SerializeField] private string Listeners;
    [SerializeField] private string broadcasters;

    public event Action<object, object> OnEventRaised;

    public void RaiseEvent(object arg = null, object arg2 = null)
    {
        if (OnEventRaised == null)
        {
            Debug.Log($"No listeners found for {name} !");
        }

        OnEventRaised?.Invoke(arg, arg2);
    }
}

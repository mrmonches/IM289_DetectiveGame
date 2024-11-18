using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionName Name;

    private PhoneManager _phoneManager;

    private void OnEnable()
    {
        switch(Name)
        {
            case InteractionName.Phone: _phoneManager = GetComponent<PhoneManager>(); break;

            default: break;
        }
    }

    private enum InteractionName
    {
        Phone
    }

    public void CallInteraction()
    {
        switch (Name)
        {
            case InteractionName.Phone:
                if (_phoneManager.IsRinging)
                    _phoneManager.PickupPhone();
                break;
            default: break;
        }
    }
}

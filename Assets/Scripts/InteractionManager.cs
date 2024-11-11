using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionName Name;

    private enum InteractionName
    {
        Phone
    }

    public void CallInteraction()
    {
        switch (Name)
        {
            case InteractionName.Phone:
                GetComponent<PhoneManager>().PickupPhone();
                break;
            default: break;
        }
    }
}

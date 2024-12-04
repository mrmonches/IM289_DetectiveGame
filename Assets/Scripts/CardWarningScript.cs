using UnityEngine;

// Written by Nolan
public class CardWarningScript : MonoBehaviour
{
    [SerializeField] private Animator WarningAnimator;

    public void ActivateWarning()
    {
        WarningAnimator.SetTrigger("ActivateWarning");
    }
}
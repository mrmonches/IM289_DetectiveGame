using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private float BeginTimer;

    [SerializeField] private Animator PhoneAnimator;

    private void Start()
    {
        StartCoroutine("DelayPhoneCall");
    }

    private IEnumerator DelayPhoneCall()
    {
        if (true)
        {
            yield return new WaitForSeconds(BeginTimer);

            StartPhoneRing();
        }
    }

    private void StartPhoneRing()
    {
        PhoneAnimator.SetTrigger("Ring");
    }

    public void PickupPhone()
    {
        PhoneAnimator.SetTrigger("Pickup");
    }
}

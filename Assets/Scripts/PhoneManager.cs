using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    [SerializeField] private float BeginTimer;

    [SerializeField] private Animator PhoneAnimator;

    [SerializeField] private EventReference RingEvent;
    [SerializeField] private EventReference CallEvent;

    [SerializeField] private StudioEventEmitter _eventEmitter;

    private bool isRinging;

    public bool IsRinging { get => isRinging; private set => isRinging = value; }

    private void Start()
    {
        IsRinging = false;
        
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
        IsRinging = true;

        PhoneAnimator.SetTrigger("Ring");

        StartCoroutine("RingTimer");

        AudioManager.instance.PlayEvent(_eventEmitter, RingEvent);
    }

    private IEnumerator RingTimer()
    {
        if (true)
        {
            yield return new WaitForSeconds(5);

            isRinging = false;

            PhoneAnimator.SetTrigger("Pickup");

            AudioManager.instance.StopEvent(_eventEmitter);
        }
    }

    public void PickupPhone()
    {
        PhoneAnimator.SetTrigger("Pickup");

        StopCoroutine("RingTimer");

        IsRinging = false;

        AudioManager.instance.StopEvent(_eventEmitter);

        AudioManager.instance.PlayOneShot(CallEvent, transform.position);
    }
}

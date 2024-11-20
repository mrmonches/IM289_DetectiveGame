using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] private Light LampLight;

    public void SetLampActive()
    {
        if (LampLight.isActiveAndEnabled)
        {
            LampLight.enabled = false;
        }
        else
        {
            LampLight.enabled = true;
        }
    }
}

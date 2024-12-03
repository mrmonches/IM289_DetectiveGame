using UnityEngine;

public class RedCircle : MonoBehaviour
{
    public void Appear()
    {
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false); 
    } 
}

using UnityEngine;

//By Nolan

public class EvidenceBoardManager : MonoBehaviour
{
    [SerializeField] private float EvidencePlacePos;

    public float EvidencePlacePos1 { get => EvidencePlacePos; private set => EvidencePlacePos = value; }
}

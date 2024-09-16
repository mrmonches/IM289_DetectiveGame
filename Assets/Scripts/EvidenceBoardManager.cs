using System.Collections.Generic;
using UnityEngine;

//By Nolan

public class EvidenceBoardManager : MonoBehaviour
{
    [SerializeField] private float EvidencePlacePos;

    [SerializeField] private List<ConnectionData> dataList = new List<ConnectionData>();

    public float EvidencePlacePos1 { get => EvidencePlacePos; private set => EvidencePlacePos = value; }
    public List<ConnectionData> DataList { get => dataList; set => dataList = value; }
}

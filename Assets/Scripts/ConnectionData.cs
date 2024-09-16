using UnityEngine;

public class ConnectionData : MonoBehaviour
{
    private YarnController _yarnController;
    private GameObject _firstObject;
    private GameObject _secondObject;
    private bool connectionStatus;
    public ConnectionData(YarnController yarn, GameObject firstObject, GameObject secondObject, bool status)
    {
        this._yarnController = yarn;
        this._firstObject = firstObject;
        this._secondObject = secondObject;
        this.connectionStatus = status;
    }

    public YarnController GetYarnController()
    {
        return _yarnController;
    }

    public GameObject GetFirstObject()
    {
        return _firstObject;
    }

    public GameObject GetSecondObject()
    {
        return _secondObject;
    }

    public bool GetConnectionStatus()
    {
        return connectionStatus;
    }
}

using Assets.Scripts;
using UnityEngine;

abstract public class Agent : MonoBehaviour {

    private Location _location;
    public Location Location
    {
        get { return _location; }
        set { _location = value; }
    }

    abstract public void Update ();


}
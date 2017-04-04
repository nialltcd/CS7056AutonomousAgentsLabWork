using Assets.Scripts;
using System.Collections;
using UnityEngine;

abstract public class Agent : MonoBehaviour {

    public float moveTime = 5f;           //Time it will take object to move, in seconds.
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    private Vector2 position;
    public Vector2 CurrentPosition
    {
        get { return position; }
        set { position = value; }
    }
    public Location Location
    {
        get { return LocationProperties.GetLocation(position); }
        set { position = LocationProperties.LocationCoords[(int)value]; }
    }
    protected virtual void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    abstract public void Update ();

    public void MoveAgent(Location location)
    {
        var newPosition = LocationProperties.GetLocationCoordinates(location);
        Debug.Log(newPosition.x + ", "+newPosition.y);
        StartCoroutine(SmoothMovement(newPosition));
        //SmoothMovement(newPosition);
        //rb2D.MovePosition(newPosition);        
    }


    protected IEnumerator SmoothMovement(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, inverseMoveTime * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
}
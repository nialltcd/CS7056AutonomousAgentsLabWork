using Assets.Scripts;
using Assets.Scripts.AStarService;
using System.Collections;
using System.Linq;
using UnityEngine;

abstract public class Agent : MonoBehaviour {

    public float moveTime = .001f;           //Time it will take object to move, in seconds.
    private Rigidbody2D rb2D;
    private float inverseMoveTime;
    private AStar _aStar;
    public bool IsTravelling { get; set; }

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
        _aStar = new AStar();
    }

    abstract public void Update ();

    public void MoveAgent(Location location)
    {
        this.IsTravelling = true;
        this.Location = location;
        var newPosition = LocationProperties.GetLocationCoordinates(location);
        //Debug.Log(newPosition.x + ", "+newPosition.y);
        Debug.Log("Before Journey: " + "(" + rb2D.position.x + ", " + rb2D.position.y + ")");
        AstarMove(newPosition);
        Debug.Log("After Journey: " + "(" + rb2D.position.x + ", " + rb2D.position.y + ")");

        this.IsTravelling = false;
        //StartCoroutine(SmoothMovement(newPosition));
        //SmoothMovement(newPosition);
        //rb2D.MovePosition(newPosition);        
    }

    protected IEnumerator AstarMove(Vector3 end)
    {
        Debug.Log("Travelling to the " + LocationProperties.GetLocation(end));
        var tiles = _aStar.FindPath(rb2D.position,end);
        foreach(var tile in tiles)
        {
            Debug.Log("Current tile: " + "(" + tile.Coordinates.x + ", " + tile.Coordinates.y + ")");
            //Find a new position proportionally closer to the end, based on the moveTime
            //Vector3 newPosition = Vector3.MoveTowards(rb2D.position, tile.Coordinates, inverseMoveTime * Time.deltaTime);

            StartCoroutine(SmoothMovement(tile.Coordinates));
            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            //rb2D.MovePosition(newPostion);
        }
        rb2D.MovePosition(end);
        return null;
    }

    protected IEnumerator Movement(Vector3 end)
    {
        rb2D.MovePosition(end);
        Debug.Log("Moved to position: " + "(" + end.x + ", " + end.y + ")");
        yield return null;
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
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, 0.1f);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            
        }
        yield return null;
    }
}
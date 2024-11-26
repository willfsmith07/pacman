using System;
using UnityEngine;

// these two sections of code would also work for being able to queue the direction of pacman by reading its current direction however
// this method is code i have found online and lots of different pacman code use a method like this involving raycast therefore i have
// set up the different coloured nodes as i wanted to figure outa different way of doing this that would be original.

/*public LayerMask wallLayer;

public bool currentDirection(Vector2 direction)
{
    RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.8f, 0, direction, 1.5f, wallLayer);
    return hit.collider != null;
}*/

public class Pacman : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    public float speed;

    public float speedMultiplier;

    private float angle = 0.0f;

    private string checkDirection = "R";

    private bool wait = true;

    private bool check180 = false;

    private bool isColliding = false;

    public Vector2 initialDirection;

    private Vector2 direction;

    private Vector2 nextDirection;

    private Vector3 startPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    private void Start()
    {
        resetState();
    }

    private void resetState()
    {
        speedMultiplier = 1.0f;
        rigidBody.isKinematic = false;
        enabled = true;
        transform.position = startPosition;
        direction = initialDirection;
        nextDirection = Vector2.zero;
    }

    private void Update()
    {

        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                checkDirection = "L";
                angle = 180.0f;
                check180 = false;
                setDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                checkDirection = "R";
                angle = 0.0f;
                check180 = false;
                setDirection(Vector2.right);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                checkDirection = "U";
                angle = 90.0f;
                check180 = false;
                setDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                checkDirection = "D";
                angle = 270.0f;
                check180 = false;
                setDirection(Vector2.down);
            }
        }

        if (nextDirection != Vector2.zero)
        {
            setDirection(nextDirection);
        }

        isColliding = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidBody.MovePosition(position + translation);
    }

    public void setDirection(Vector2 direction)
    {
        if (Math.Abs(transform.eulerAngles.z - angle) == 180)
        {
            this.direction = direction;
            nextDirection = Vector2.zero;
            setRotation(angle);
            check180 = true;
        }
        else if (isColliding == true)
        {
            Invoke("isWaiting", 0.06f / speedMultiplier);
            if (wait == false)
            {
                this.direction = direction;
                nextDirection = Vector2.zero;
                setRotation(angle);
                Invoke("isNotWaiting", 0.065f / speedMultiplier);
            }
        }
        else
        {
            nextDirection = direction;
        }
    }

    public void setRotation(float angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void isWaiting()
    {
        wait = false;
    }

    private void isNotWaiting()
    {
        wait = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag.Contains(checkDirection) && check180 == false)
        {
            isColliding = true;
        }
    }
}


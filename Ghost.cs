using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private GameObject pacman;

    public float speed;

    public float speedMultiplier;

    private float targetTileX;

    private float targetTileY;

    private double lengthTT;

    private int points = 200;

    private bool isColliding = false;

    private string currentNode;

    private Vector3 currentNodeXY;

    public Vector2 initialDirection;

    private Vector2 chosenDirection;

    private Vector2 direction;

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
    }

    private void Update()
    {
        if (isColliding == true)
        {
            Invoke("setDirection", 0.065f / speedMultiplier);
        }
        isColliding = false;
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidBody.position;
        Vector2 translation = direction * speed * speedMultiplier * Time.fixedDeltaTime;
        rigidBody.MovePosition(position + translation);
    }

    public void setDirection()
    {
        wichDirection();
        direction = chosenDirection;
    }

    /* the order in wich it checks what the shortest distance to tile does matter as if to lengths are equal
    it will prioritise in the order,up,left,down,right that is why these if statements are in this specific order */
    public void wichDirection()
    {
        targetTile();
        /* shortestlengthTT(length to target) is set to 100 as because of the size of the map, 100 will always be larger than any lengthTT value
        this ensures that it is only comparing the new shortestlengthTT values and not using the one that was left from the previos time
        this function was called. Aslong as shortestlengthTT is set to a value that is larger than the largest possible value
        lengthToTarget() can produce, the value can be anything */
        double shortestLengthTT = 100;
        print(currentNode);
        if (currentNode.Contains("U") && direction != Vector2.down)
        {
            lengthToTarget("above");
            print(lengthTT + "U");
            if (lengthTT < shortestLengthTT) { chosenDirection = Vector2.up; shortestLengthTT = lengthTT; }
            print(chosenDirection);
        }
        if (currentNode.Contains("L") && direction != Vector2.right)
        {
            lengthToTarget("left");
            print(lengthTT + "L");
            if (lengthTT < shortestLengthTT) { chosenDirection = Vector2.left; shortestLengthTT = lengthTT; }
            print(chosenDirection);
        }
        if (currentNode.Contains("D") && direction != Vector2.up)
        {
            lengthToTarget("below");
            print(lengthTT + "D");
            if (lengthTT < shortestLengthTT) { chosenDirection = Vector2.down; shortestLengthTT = lengthTT; }
            print(chosenDirection);
        }
        if (currentNode.Contains("R") && direction != Vector2.left)
        {
            lengthToTarget("right");
            print(lengthTT + "R");
            if (lengthTT < shortestLengthTT) { chosenDirection = Vector2.right; shortestLengthTT = lengthTT; }
            print(chosenDirection);
        }

        print(chosenDirection);
    }

    public void targetTile()
    {
        pacman = GameObject.Find("Pacman");
        if (tag == "Red")
        {
            targetTileX = pacman.transform.position.x;
            targetTileY = pacman.transform.position.y;
        }
    }

    public void lengthToTarget(string adjascentTile)
    {
        if (adjascentTile == "above")
        {
            lengthTT = Math.Sqrt(Math.Pow((transform.position.x - targetTileX), 2) +
                                 Math.Pow((transform.position.y+1 - targetTileY), 2));
        }
        else if (adjascentTile == "left")
        {
            lengthTT = Math.Sqrt(Math.Pow((transform.position.x-1 - targetTileX), 2) +
                                 Math.Pow((transform.position.y - targetTileY), 2));
        }
        else if (adjascentTile == "below")
        {
            lengthTT = Math.Sqrt(Math.Pow((transform.position.x - targetTileX), 2) +
                                 Math.Pow((transform.position.y-1 - targetTileY), 2));
        }
        else if (adjascentTile == "right")
        {
            lengthTT = Math.Sqrt(Math.Pow((transform.position.x+1 - targetTileX), 2) +
                                 Math.Pow((transform.position.y - targetTileY), 2));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("U") || other.tag.Contains("D") || other.tag.Contains("L") || other.tag.Contains("R"))
        {
            if (currentNodeXY != other.transform.position)
            {
                isColliding = true;
                currentNode = other.tag;
                currentNodeXY = other.transform.position;
            }
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformAction : AbstractAction, IResettable
{
    public Transform moveTo;
    public float speed;

    Vector2 endPoint;
    Vector2 startPoint;
    Vector2 directionCheck;
    float xCheck, yCheck, defaultSpeed;
    bool isMoving;
    bool movingToEnd;

    public Vector2 velocity
    {
        get
        {
            if (isMoving)
            {
                return directionCheck * speed;
            }
            else
            {
                return Vector2.zero;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.position;
        endPoint = moveTo.position;
        directionCheck = (endPoint - startPoint).normalized;
        xCheck = directionCheck.x;
        yCheck = directionCheck.y;
        isMoving = false;
        movingToEnd = true;
        defaultSpeed = speed;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            DirectionCheck();
            Vector2 newPosition = (Vector2)transform.position + directionCheck * speed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    void DirectionCheck()
    {
        Vector2 nearPosition;

        Vector2 deltaPosition;
        float deltaX, deltaY;

        if (movingToEnd)
        {
            deltaPosition = endPoint - (Vector2)transform.position;
            deltaX = deltaPosition.x;
            deltaY = deltaPosition.y;
            nearPosition = endPoint;
        }
        else
        {
            deltaPosition = (Vector2)transform.position - startPoint;
            deltaX = deltaPosition.x;
            deltaY = deltaPosition.y;
            nearPosition = startPoint;
        }

        // Checking if platform reached target point (startPoint or endPoint) and changing direction if needed
        if (xCheck != 0f && xCheck * deltaX <= 0f)
        {
            transform.position = nearPosition;
            movingToEnd = !movingToEnd;
            speed *= -1;
        }
        else if (yCheck != 0f && yCheck * deltaY <= 0f)
        {
            transform.position = nearPosition;
            movingToEnd = !movingToEnd;
            speed *= -1;
        }
    }

    public override void ActionOn()
    {
        isMoving = true;
    }

    public override void ActionOff()
    {
        isMoving = false;
    }

    public void ResetObject()
    {
        transform.position = startPoint;
        speed = defaultSpeed;
        Start();
    }
}

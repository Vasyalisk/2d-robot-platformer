using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAction : AbstractAction
{
    public float moveDistance;
    public float speedUp;
    public float speedDown;

    Rigidbody2D rb;
    Vector2 _startPoint, _endPoint, currentSpeed;
    bool isMoving, movingDown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _startPoint = transform.position;
        _endPoint = (Vector2)transform.position + Vector2.down * moveDistance;
        movingDown = true;
        SetSpeed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving)
        {
            CheckDirection();
            SetSpeed();
            rb.velocity = currentSpeed;
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

    void SetSpeed()
    {
        if (movingDown)
        {
            currentSpeed = Vector2.down * speedDown;
        }
        else
        {
            currentSpeed = Vector2.up * speedUp;
        }
    }

    void CheckDirection()
    {
        if (movingDown && transform.position.y <= _endPoint.y)
        {
            movingDown = false;
        }
        else if (!movingDown && transform.position.y >= _startPoint.y)
        {
            movingDown = true;
        }
    }
}

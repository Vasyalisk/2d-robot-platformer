using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingAction : AbstractAction, IResettable
{
    public float rotationAngle;
    public float angularSpeed;

    Quaternion startRotation;
    Quaternion endRotation;
    Quaternion rotateTo;
    float rotationTime;
    bool isRotating;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(startRotation.eulerAngles.x, startRotation.eulerAngles.y, startRotation.eulerAngles.z + rotationAngle);
        isRotating = false;
        rotationTime = Mathf.Abs(angularSpeed / (rotationAngle - startRotation.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            Rotate();
            RotationCheck();
        }
    }

    void Rotate()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotateTo, rotationTime);
    }

    void RotationCheck()
    {
        if (transform.localRotation == rotateTo)
        {
            isRotating = false;
        }
    }

    public override void ActionOn()
    {
        isRotating = true;
        rotateTo = endRotation;
        
    }

    public override void ActionOff()
    {
        isRotating = true;
        rotateTo = startRotation;
    }

    public void ResetObject()
    {
        isRotating = false;
        transform.rotation = startRotation;
    }
}

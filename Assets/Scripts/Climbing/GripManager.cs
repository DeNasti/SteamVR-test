using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GripManager : MonoBehaviour
{
    public Pull lController;
    public Pull rController;
    public Rigidbody playerBody;

    void FixedUpdate()
    {
        if ((lController.isGrabGripPressed && lController.canGrip) || (rController.isGrabGripPressed && rController.canGrip))
        {
            UseGravity(false);

            if (lController.isGrabGripPressed && lController.canGrip)
            {
                lController.TryGrab();
            }

            if (rController.isGrabGripPressed && rController.canGrip)
            {
                rController.TryGrab();
            }
        }
        else
        {
            UseGravity(true);
        }
    }


    private void UseGravity(bool grav)
    {
        playerBody.useGravity = grav;
        playerBody.isKinematic = !grav;
    }

}

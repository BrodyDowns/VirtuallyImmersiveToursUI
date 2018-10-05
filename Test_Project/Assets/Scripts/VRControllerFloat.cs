using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class that controls the up and down movement of the user using the grip button*/
public class VRControllerFloat: MonoBehaviour
{
    [SerializeField]
    private Transform rig;
    private SteamVR_TrackedObject trackedObj;
    private bool gripPressed = false;
    public float direction = -1;  //Specifies direction (negative = down, positive = up)

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check if grip button is pressed or not. 
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            gripPressed = true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip)) {
            gripPressed = false;
        }

        // Transform y position if grip button is pressed.
        if (gripPressed == true) {

            if (rig != null)
            {
                rig.position += transform.up * Time.deltaTime * direction;
            }
        }
    }
}

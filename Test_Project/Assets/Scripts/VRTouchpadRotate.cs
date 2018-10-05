using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Class that rotates the scene around the user with the touchpad about the y-axis*/
public class VRTouchpadRotate : MonoBehaviour
{
    [SerializeField]
    private Transform rig;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_TrackedObject trackedObj;
    public float speed = 1f;

    private SteamVR_Controller.Device controller
    {
        get
        {
            return SteamVR_Controller.Input((int)trackedObj.index);
        }
    }

    private Vector2 axis = Vector2.zero;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized.");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);

        
        //Rotate rig around camera eye position when touchpad is active.
        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {     
                var cameraObj = GameObject.Find("Camera (eye)");
                rig.RotateAround(cameraObj.transform.position,
                                 new Vector3(0,axis.x, 0), 20f * Time.deltaTime * speed);          
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Class that controls forward/backward left/right movement with touchpad. 
 Source: https://www.youtube.com/watch?v=EUnQ4whsQcU */

public class VRTouchpadMove : MonoBehaviour {
    [SerializeField]
    private Transform rig;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device controller {
        get {
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
        if (controller == null) {
            Debug.Log("Controller not initialized.");
            return;
        }

        var device = SteamVR_Controller.Input((int) trackedObj.index);

        //Transform rig position based on touchpad coordinates.
        if (controller.GetTouch(touchpad)) {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null) {
                float currentY = rig.position.y;
                rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime;
                rig.position = new Vector3(rig.position.x, currentY, rig.position.z);
            }
        }

    }

}


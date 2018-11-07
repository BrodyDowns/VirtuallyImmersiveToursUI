using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/* 
 * Class that handles the Menu and Laser Pointers on Controllers
 */
public class MenuHandler : MonoBehaviour {
    //Controller and Menu
    //public GameObject leftController;
    public GameObject rightController;
    public GameObject menu;

    //Laser Pointer on Controller
    //private SteamVR_LaserPointer leftLaserPointer;
    private SteamVR_LaserPointer rightLaserPointer;

    //Tracked Controller component on controller
    //private SteamVR_TrackedController leftTracked;
    private SteamVR_TrackedController rightTracked;

    //Menu starts turned off
    bool menuState = false;

    private void OnEnable()
    {
        //set menu to false to when starting
        menu.SetActive(menuState);

        //set up laser pointers
        //leftLaserPointer = leftController.GetComponent<SteamVR_LaserPointer>();
        rightLaserPointer = rightController.GetComponent<SteamVR_LaserPointer>();

        //left
        //leftLaserPointer.active = false;
        //leftLaserPointer.PointerIn -= HandlePointerIn;
        //leftLaserPointer.PointerIn += HandlePointerIn;
        //leftLaserPointer.PointerOut -= HandlePointerOut;
        //leftLaserPointer.PointerOut += HandlePointerOut;

         //right
        rightLaserPointer.active = false;
        rightLaserPointer.PointerIn -= HandlePointerIn;
        rightLaserPointer.PointerIn += HandlePointerIn;
        rightLaserPointer.PointerOut -= HandlePointerOut;
        rightLaserPointer.PointerOut += HandlePointerOut;

        //set up tracked controllers
        //leftTracked = leftController.GetComponent<SteamVR_TrackedController>();
        //if (leftTracked == null)
        //{
        //    leftTracked = leftController.GetComponentInParent<SteamVR_TrackedController>();
        //}

        
        rightTracked = rightController.GetComponent<SteamVR_TrackedController>();
        if (rightTracked == null)
        {
            rightTracked = rightController.GetComponentInParent<SteamVR_TrackedController>();
        }

            //left
        //leftTracked.TriggerClicked -= HandleTriggerClicked;
        //leftTracked.TriggerClicked += HandleTriggerClicked;
        //leftTracked.MenuButtonClicked -= HandleMenuButton;
        //leftTracked.MenuButtonClicked += HandleMenuButton;

            //right
        rightTracked.TriggerClicked -= HandleTriggerClicked;
        rightTracked.TriggerClicked += HandleTriggerClicked;
        rightTracked.MenuButtonClicked -= HandleMenuButton;
        rightTracked.MenuButtonClicked += HandleMenuButton;

    }

    //Toggles menu and Laser pointer
    private void HandleMenuButton(object sender, ClickedEventArgs e)
    {
        menuState = !menuState;
        //leftLaserPointer.active = menuState;
        rightLaserPointer.active = menuState;

        //Debug.Log(leftLaserPointer.active + " " + rightLaserPointer.active);
        
        menu.SetActive(menuState);

    }

    //Handles Trigger being clicked
    private void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
       // Debug.Log("trigger clicked");
        //activates button
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new PointerEventData(EventSystem.current), ExecuteEvents.submitHandler);
        }
    }

    //Hover on
    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            button.Select();
            Debug.Log("HandlePointerIn", e.target.gameObject);
        }
    }

    //Hover off
    private void HandlePointerOut(object sender, PointerEventArgs e)
    {

        var button = e.target.GetComponent<Button>();
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            Debug.Log("HandlePointerOut", e.target.gameObject);
        }
    }


}

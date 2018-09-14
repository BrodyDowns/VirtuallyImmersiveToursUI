using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleFloorX : MonoBehaviour {

    public float width = 4f;
    public bool foundCoin = false;
    public float cameraX;
    public float cameraZ;
    public float coinX;
    public float coinZ;

    

	// Update is called once per frame
	void Update () {

        if (!foundCoin)
        {
            GetValues();

            // if we get close to coin, deactive line
            if (Mathf.Abs(coinX - cameraX) < 3 && Mathf.Abs(coinZ - cameraZ) < 3)
            {
                foundCoin = true;
                gameObject.SetActive(false);
            }
            else //else resize/position line
            {

                //Debug.Log(Mathf.Abs(cameraX - coinX));
                float distance = cameraX - coinX;
                float lineScale = Mathf.Abs(distance / width);

                transform.localScale = new Vector3(lineScale, transform.localScale.y, transform.localScale.z);
                transform.localPosition = new Vector3(coinX + distance/2, transform.localPosition.y, transform.localPosition.z);
            }
        }
     }

    //Gets the X and Z values for main camera and coin
    void GetValues()
    {
        cameraX = 0;
        var cameraObj = GameObject.Find("Main Camera");
        if (cameraObj)
        {
            cameraX = cameraObj.transform.position.x;
        }

        cameraZ = 0;
        if (cameraObj)
        {
            cameraZ = cameraObj.transform.position.z;
        }

        coinX = 0;
        var coinObj = GameObject.Find("Pickup");
        if (coinObj)
        {
            coinX = coinObj.transform.position.x;
        }

        coinZ = 0;
        if (coinObj)
        {
            coinZ = coinObj.transform.position.z;
        }
    }
}

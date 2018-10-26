using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowTarget : MonoBehaviour
    {
        public Transform target;
 

        private void LateUpdate()
        {
            transform.rotation = target.rotation;
            transform.position = target.position;
        }
    }
}

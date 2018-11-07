using System;
using UnityEngine;

/*
 * Copies another targets position AND rotation
 * */
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

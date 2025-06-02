using System;
using UnityEngine;

namespace _01Scripts.UI
{
    public class ObjectUI : MonoBehaviour
    {
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
        #endif
        
    }
}

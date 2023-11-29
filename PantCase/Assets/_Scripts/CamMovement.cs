using System;
using UnityEngine;

 
    public class CamMovement : MonoBehaviour
    {
        public float speed = 5f;

        public void Update()
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            
            transform.position += new Vector3(horizontal, vertical, 0) * (speed * Time.deltaTime);
        }
    }
 
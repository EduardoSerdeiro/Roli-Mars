using System;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use

        private float PowerJump = 150;

        [Header("Cooldown Jump")] public float coolDownJump;
        private bool isGrounded = true;
        private float timeJump;
        
        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

            if (coolDownJump == null)
                coolDownJump = 2;
            
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Jump();
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }


        private void Jump()
        {
            //Debug.Log(isGrounded);
            if (isGrounded && Input.GetKeyDown(KeyCode.E))
            {
                this.GetComponent<Rigidbody>().AddForce(Vector3.up * (GetComponent<Rigidbody>().mass * PowerJump), ForceMode.Force);
                isGrounded = false;
                timeJump = 0;
            }
            timeJump += Time.fixedDeltaTime;
            if (timeJump > coolDownJump)
            {
                timeJump = coolDownJump;
                isGrounded = true;
            }

          
        }

        
    }
}

using UnityEngine;
using System.Collections;

public class MouseLookRotation : MonoBehaviour {

   
    [SerializeField] private float minX = -30;
    [SerializeField] private float maxX = 30;
   
    private float rotationX;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        rotationX += Input.GetAxis("Mouse X") * 0.3f;
        rotationX = Mathf.Clamp(rotationX, minX, maxX);

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
       // transform.Rotate(0, rotationX, 0);
    }

   
}

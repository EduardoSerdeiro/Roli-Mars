using UnityEngine;
using System.Collections;

public class FollowGround : MonoBehaviour {

     public float  distance= 1f;
    public float smoothRatio = 0.2f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        HitTestWithRoad();
    }


   
public bool HitTestWithRoad() {
    Vector3 position = transform.position + transform.TransformDirection(Vector3.up) * 0.2f;
    Vector3 direction = transform.TransformDirection(Vector3.down);
    Ray ray = new Ray(position, direction);
    RaycastHit hit;
    
    Debug.DrawLine(ray.origin, ray.origin + ray.direction * distance, Color.red);
    bool inGround = false;

    if (Physics.Raycast(ray, out hit, distance)) {
        if (hit.collider.tag == "Road"){
            inGround = true;
            this.transform.position = hit.point;
            
            Debug.DrawLine(hit.point, hit.point + hit.normal, Color.green);
            
            Vector3 current = position - hit.point;
            Vector3  target= hit.normal;
            Debug.DrawLine(hit.point, hit.point + current.normalized, Color.white);

            Quaternion targetQ = new Quaternion();
            //TODO: Using "velocity.normalize" instead of "Vector3(0, 1.0, 1.0)"
            Vector3 fPosition= transform.position + transform.TransformDirection(new Vector3(0, 1.0f, 1.0f));
            Vector3  fDirection= transform.TransformDirection(Vector3.down);
            Ray  fRay= new Ray(fPosition, fDirection);
            RaycastHit fHit;
            float  fDistance= 2;
            Debug.DrawLine(fRay.origin, fRay.origin + fRay.direction * fDistance, Color.cyan);
            if (Physics.Raycast(fRay, out fHit, fDistance)) {
                if (fHit.collider.tag == "Road"){
                    Debug.DrawLine(fHit.point, fHit.point + fHit.normal * fDistance, Color.magenta);
                    targetQ.SetLookRotation(fHit.point - transform.position, target);
                }
            }
            if (targetQ == null) {
                targetQ.SetLookRotation(transform.TransformDirection(Vector3.forward), target);
            }
            this.gameObject.transform.rotation = Quaternion.Slerp(this.gameObject.transform.rotation, targetQ, smoothRatio);
        }
    }
    
    return inGround;
}

}

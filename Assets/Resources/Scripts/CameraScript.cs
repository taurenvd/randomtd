using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    public Camera Mcamera;
   
    int[] id;
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey(KeyCode.A) &&Mcamera.transform.position.x >215)
        {
            Mcamera.transform.position=new Vector3(Mcamera.transform.position.x-3,Mcamera.transform.position.y,Mcamera.transform.position.z );
        }
        if (Input.GetKey(KeyCode.D)&& Mcamera.transform.position.x < 305)
        {
            Mcamera.transform.position = new Vector3(Mcamera.transform.position.x + 3, Mcamera.transform.position.y, Mcamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W)&& Mcamera.transform.position.z <295)
        {
            Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z+3);
        }
        if (Input.GetKey(KeyCode.S)&&Mcamera.transform.position.z>-12f)
        {
            Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z-3);
        }
        if (Input.GetKey(KeyCode.Q)&& Mcamera.transform.position.y<300)
        {
            Mcamera.transform.position= new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y+3, Mcamera.transform.position.z);
        }
        if (Input.GetKey(KeyCode.E) && Mcamera.transform.position.y > 175)
        {
            Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y - 3, Mcamera.transform.position.z);
        }
      //  Debug.Log( Input.mousePosition);
      
      
    }
}

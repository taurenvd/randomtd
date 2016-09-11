using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Camera Mcamera;
    public int edge;
    public float step; 
    int[] id;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)|| Input.mouseScrollDelta.y<0)
            
            if (Mcamera.transform.position.y < 300)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y + step, Mcamera.transform.position.z);
            }

        if (Input.GetKey(KeyCode.Q) || Input.mouseScrollDelta.y > 0)
            if (Mcamera.transform.position.y > 175)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y - step, Mcamera.transform.position.z);
            }
     //   Debug.Log(Input.mousePosition);
        if (Input.mousePosition.x > Screen.width - edge|| Input.GetKey(KeyCode.D))
        {
         //   Debug.Log("right edge");
            if (Mcamera.transform.position.x < 305)
            {

                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x + step, Mcamera.transform.position.y, Mcamera.transform.position.z);
            }
        }
        if (Input.mousePosition.x < edge|| Input.GetKey(KeyCode.A))
        {
         //   Debug.Log("left edge");
            if (Mcamera.transform.position.x >215)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x - step, Mcamera.transform.position.y, Mcamera.transform.position.z);
            }
        }
        if (Input.mousePosition.y > Screen.height - edge || Input.GetKey(KeyCode.W))
        {
         //   Debug.Log("top edge");
            if (Mcamera.transform.position.z < 300)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z+ step);
            }
        }
        if (Input.mousePosition.y < edge || Input.GetKey(KeyCode.S))
        {
         //   Debug.Log("botom edge");
            if (Mcamera.transform.position.z > -12f)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z- step);
            }
        }
    }
}

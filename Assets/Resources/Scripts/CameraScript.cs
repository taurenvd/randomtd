using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    public Camera Mcamera;
    public int edgeStep;
    public float step;
    public Vector3 leftEdge;
    public Vector3 rightEdge; 
    int[] id;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)|| Input.mouseScrollDelta.y<0)
            
            if (Mcamera.transform.position.y < rightEdge.y)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y + step, Mcamera.transform.position.z);
            }

        if (Input.GetKey(KeyCode.E) || Input.mouseScrollDelta.y > 0)
            if (Mcamera.transform.position.y > leftEdge.y)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y - step, Mcamera.transform.position.z);
            }
     //   Debug.Log(Input.mousePosition);
        if (Input.mousePosition.x > Screen.width - edgeStep|| Input.GetKey(KeyCode.D))
        {
         //   Debug.Log("right edge");
            if (Mcamera.transform.position.x < rightEdge.x)
            {

                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x + step, Mcamera.transform.position.y, Mcamera.transform.position.z);
            }
        }
        if (Input.mousePosition.x < edgeStep|| Input.GetKey(KeyCode.A))
        {
         //   Debug.Log("left edge");
            if (Mcamera.transform.position.x >leftEdge.x)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x - step, Mcamera.transform.position.y, Mcamera.transform.position.z);
            }
        }
        if (Input.mousePosition.y > Screen.height - edgeStep || Input.GetKey(KeyCode.W))
        {
         //   Debug.Log("top edge");
            if (Mcamera.transform.position.z < rightEdge.z)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z+ step);
            }
        }
        if (Input.mousePosition.y < edgeStep || Input.GetKey(KeyCode.S))
        {
         //   Debug.Log("botom edge");
            if (Mcamera.transform.position.z > leftEdge.z)
            {
                Mcamera.transform.position = new Vector3(Mcamera.transform.position.x, Mcamera.transform.position.y, Mcamera.transform.position.z- step);
            }
        }
    }
}

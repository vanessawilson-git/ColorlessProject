using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class CameraController : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public bool useOffsetValues;
    public float rotateSpeed;
    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertyY;

    // Start is called before the first frame update
    void Start()
    {

        if (!useOffsetValues)
        {
            offset = target.position = transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;

        //removes the pivot as a child of the camera so the player can move correctly. But this way no need for extra setup when making a prefab
        pivot.transform.parent = null;

        //Hides the mosue while playing (if you press escape it reapears)
       // Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Make sure pivot always follows the player
        pivot.transform.position = target.transform.position;


        //get the x position of the mouse and rotate to target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);


        //Get Y position of the mouse and rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        

        //Make the option to ivert y camera possilbe
        if (invertyY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }



        //Limit the up/down camera rotation so it doesn't flip
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0,0);
        }

        if (pivot.rotation.eulerAngles.x < 360f  + minViewAngle && pivot.rotation.eulerAngles.x > 180f)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }


        //Move the camera based on the current rotation of the target and the original offset
        float desiredAngley = pivot.eulerAngles.y;
        float desiredAnglex = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredAnglex, desiredAngley, 0);
        
       
        transform.position = target.position - (rotation * offset);

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y -0.5f,transform.position.z);
        }

        transform.LookAt(target);
    }
}

// Credit to damien_oconnell from http://forum.unity3d.com/threads/39513-Click-drag-camera-movement
// for using the mouse displacement for calculating the amount of camera movement and panning code.

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MoveCamera : MonoBehaviour 
{
	//
	// VARIABLES
	//
	public Camera myCamera;
	public float turnSpeed = 40.0f;		// Speed of camera turning when mouse moves in along an axis
	//public float panSpeed = 4.0f;		// Speed of the camera when being panned
	public float zoomSpeed = 10.0f;		// Speed of the camera going back and forth
	
	private Vector3 mouseOrigin;	// Position of cursor when mouse dragging starts
	//private bool isPanning;		// Is the camera being panned?
	private bool isRotating;	// Is the camera being rotated?
	private bool isZooming;		// Is the camera zooming?

    public float rightMargine = 200;

	public static bool isZoomIn;
	public static bool isZoomOut;
    public static bool isResetCam;

    Vector3 resetPosition;
    Quaternion resetRotation;

    int lastWidth = 0;

	void Awake() {
		Application.targetFrameRate = 30;
        drag.rootParent = transform.parent;

        resetPosition = myCamera.transform.position;
        resetRotation = myCamera.transform.rotation;
        screenSizeUpdate();
    }


    void screenSizeUpdate()
    {
        if (lastWidth != Screen.width)
        {
            lastWidth = Screen.width;

            float percent = rightMargine/ lastWidth;
            Camera.main.rect = new Rect(0.0f, 0.0f, 1.0f - percent, 1.0f);
            print(rightMargine + "::" + Screen.width);
            print(rightMargine / lastWidth);
        }
    }

	//
	// UPDATE
	//
	
	void Update () 
	{

        screenSizeUpdate();

        if (drag.objDragged)
			return;



        // Get the right mouse button
        //		if(Input.GetMouseButtonDown(1))
        //		{
        //			// Get mouse origin
        //			mouseOrigin = Input.mousePosition;
        //			isPanning = true;
        //		}

        //		if (Input.GetKey(KeyCode.UpArrow)) {
        //			Vector3 pos = myCamera.transform.position;
        //
        //			Vector3 move = pos.y * zoomSpeed * transform.forward; 
        //			transform.Translate(move, Space.World);
        //		}

        //		if (Input.GetKey(KeyCode.DownArrow)) {
        //			Vector3 pos = myCamera.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
        //
        //			Vector3 move = pos.y * zoomSpeed * transform.forward; 
        //			transform.Translate(move, Space.World);
        //		}

        // Camera reset position and rotation
        resetCam();

		// Get the middle mouse button
		if(Input.GetMouseButtonDown(2))
		{
			// Get mouse origin
			mouseOrigin = Input.mousePosition;
			isZooming = true;
		}
		
		// Disable movements on button release

		//if (!Input.GetMouseButton(1)) isPanning=false;
		//if (!Input.GetMouseButton(2)) isZooming=false;

		if(!EventSystem.current.IsPointerOverGameObject ())
		{
			if (!Input.GetMouseButton(0)) isRotating=false;
			// Get the left mouse button
			if(Input.GetMouseButtonDown(0))
			{
				// Get mouse origin
				mouseOrigin = Input.mousePosition;
				isRotating = true;
			}

			// Rotate camera along X and Y axis
			if (isRotating)
			{
				Vector3 pos = myCamera.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

				transform.RotateAround(Vector3.zero, transform.right, -pos.y * turnSpeed);
				transform.RotateAround(Vector3.zero, Vector3.up, pos.x * turnSpeed);
				mouseOrigin = Input.mousePosition;
			}
		}

		
		// Move the camera on it's XY plane
//		if (isPanning)
//		{
//			Vector3 pos = myCamera.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
//
//	        	Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
//	        	transform.Translate(move, Space.Self);
//		}
		
		// Move the camera linearly along Z axis
//		if (isZooming)
//		{
//			Vector3 pos = myCamera.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);
//
//	        	Vector3 move = pos.y * zoomSpeed * transform.forward; 
//	        	transform.Translate(move, Space.World);
//		}


		if (isZoomIn) {
			
			Vector3 pos1 = myCamera.ScreenToViewportPoint(transform.position);
			Vector3 move = pos1.y * zoomSpeed * transform.forward; 
			transform.Translate(move, Space.World);

		} else if (isZoomOut) {
			
			Vector3 pos1 = myCamera.ScreenToViewportPoint(transform.position);
			Vector3 move = pos1.y * zoomSpeed * -transform.forward; 
			transform.Translate(move, Space.World);

		}



	}

    void resetCam()
    {
        if(isResetCam)
        {


            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, resetPosition, Time.deltaTime * 12 );
            myCamera.transform.rotation = Quaternion.Lerp(myCamera.transform.rotation, resetRotation, Time.deltaTime * 12);

            if(myCamera.transform.position == resetPosition && myCamera.transform.rotation == resetRotation)
            {
                isResetCam = false;
            }
            
        }
    }
}
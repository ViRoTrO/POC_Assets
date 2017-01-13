using UnityEngine;
using System.Collections;

public class ZoomTrigger : MonoBehaviour {


	public void startZoomIn()
	{
		MoveCamera.isZoomIn = true;
	}

	public void startZoomOut()
	{
		MoveCamera.isZoomOut = true;
	}

	public void stopZoomIn()
	{
		MoveCamera.isZoomIn = false;
	}

	public void stopZoomOut()
	{
		MoveCamera.isZoomOut = false;
	}



}

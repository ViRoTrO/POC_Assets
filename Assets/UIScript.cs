using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public GameObject frame;
	public GameObject door;

	public static GameObject currentSelection;
    public static Vector3 currentSelectionPosition;
    public static bool showDelete;

 	Button zoomIn;
	Button zoomOut;
    Texture deleteTexture;

    // Buttons
    Button resetCamBtn;
    Button fullScreenBtn;

    void Start()
    {
        resetCamBtn = GameObject.Find("resetCam").GetComponent<Button>();
        fullScreenBtn = GameObject.Find("fullScreen").GetComponent<Button>();

        deleteTexture = Resources.Load("delete") as Texture;

        addListeners();
    }

    void addListeners()
    {
        resetCamBtn.onClick.AddListener(resetCamClicked);
        fullScreenBtn.onClick.AddListener(fullScreen);
    }

    void resetCamClicked()
    {
        MoveCamera.isResetCam = true;
        
    }

    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void clicked_1(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("1");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("1");
		}

	}

	private void clicked_2(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("2");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("2");
		}
	}

	private void clicked_3(){
		if (currentSelection.transform.tag == "frame") {
			FrameDragHandler frameDrag = (FrameDragHandler)currentSelection.GetComponent ("FrameDragHandler");
			frameDrag.changeTexture ("3");
		} 
		else 
		{
			DoorDragHandler doorDrag = (DoorDragHandler)currentSelection.GetComponent ("DoorDragHandler");
			doorDrag.changeTexture ("3");
		}
	}

	private void createFrame(){
		Instantiate (frame);
	}

	private void createDoor(){
		Instantiate (door);

	}

	// Use this for initialization
	void OnGUI () {

        //		btn_1 = GameObject.find("btn_1");
        //		btn_2 = GameObject.find("btn_2");
        //		btn_3 = GameObject.find("btn_3");

        if (!showDelete)
            return;

        Vector3 pos = Camera.main.WorldToScreenPoint(currentSelectionPosition);
        pos.x = pos.x - 12.5f;
        pos.y = (Screen.height - pos.y) - 55;

        Rect rect = new Rect(pos.x, pos.y, 25, 25);
        

        if (GUI.Button(rect, deleteTexture, GUIStyle.none))
        {
            if (currentSelection != null)
            {
                Destroy(currentSelection);
                currentSelection = null;
            }

            showDelete = false;
        }

    }
	

}

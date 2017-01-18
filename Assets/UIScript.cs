using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

	public GameObject frame;
	public GameObject door;

	public static GameObject currentSelection;
    public static Vector3 currentSelectionPosition;
    public static bool showEditUI;

 	// Buttons
    Button resetCamBtn;
    Button fullScreenBtn;
    Button copy;
    Button delete;
    Button plus;
    RectTransform editUI;

    void Start()
    {
        resetCamBtn = GameObject.Find("resetCam").GetComponent<Button>();
        fullScreenBtn = GameObject.Find("fullScreen").GetComponent<Button>();

        editUI = GameObject.Find("editUI").GetComponent<RectTransform>();
        copy = GameObject.Find("copy").GetComponent<Button>();
        delete = GameObject.Find("delete").GetComponent<Button>();
        plus = GameObject.Find("plus").GetComponent<Button>();

        addListeners();
    }

    void addListeners()
    {
        resetCamBtn.onClick.AddListener(resetCamClicked);
        fullScreenBtn.onClick.AddListener(fullScreen);

        copy.onClick.AddListener(copyFun);
        delete.onClick.AddListener(deleteFun);
        plus.onClick.AddListener(plusFun);
    }

    void resetCamClicked()
    {
        MoveCamera.isResetCam = true;
        
    }

    public void fullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void copyFun()
    {
        GameObject temp = Instantiate(currentSelection);

        drag drag = (drag)temp.transform.GetComponent("drag");
        drag.dragOnMouseMove = true;
        drag.isDragging = true;


    }

    public void deleteFun()
    {
        if (currentSelection != null)
        {
            Destroy(currentSelection);
            currentSelection = null;
        }

        showEditUI = false;
    }

    public void plusFun()
    {

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
   }

	private void createDoor(){
		Instantiate (door);

	}

	// Use this for initialization
	void OnGUI () {

        //		btn_1 = GameObject.find("btn_1");
        //		btn_2 = GameObject.find("btn_2");
        //		btn_3 = GameObject.find("btn_3");

        if (showEditUI)
        {
            editUI.transform.gameObject.SetActive(true);
        }
        else
        {
            editUI.transform.gameObject.SetActive(false);
            return;
        }


        Vector3 pos = Camera.main.WorldToScreenPoint(currentSelectionPosition);
        editUI.transform.position = new Vector3(pos.x - 25, pos.y + 40, pos.z);

        //pos.x = pos.x - 12.5f;
        //pos.y = (Screen.height - pos.y) - 55;
        //Rect rect = new Rect(pos.x, pos.y, 25, 25);
    }
	

}

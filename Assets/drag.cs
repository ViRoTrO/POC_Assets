using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class drag : MonoBehaviour {

	// Static
	public static bool objDragged = false;
    public static Transform rootParent;

    Vector3 dist;
	float posX;
	float posY;
	float posZ;

	protected Vector3 worldPos;

	// protected
	protected bool isColliding;
	protected bool isDragging;
	protected bool isSnapped;
	protected Collider col;
	protected string currentTexture;

	protected Vector3 lastPosition;
	protected Transform lastParent;
	protected Vector3 lastValidSnap;
    
    protected Color shaderColor;
	protected Vector3 offsetVec3;
	protected Vector2 offsetVec2;

	public List <SnapInfo> snapPoints = new List<SnapInfo>();
	public Renderer rend;

	// private
	private bool onWall = false;
	private Vector3 size;


	float oldMousePosY;


	float minX;
	float maxX;

	float minZ;
	float maxZ;

	string currentWall;


	protected virtual void Awake(){
		
		rend = GetComponent<Renderer> ();
		col = GetComponent<Collider> ();
		size = rend.bounds.size;
        
        if (rend.sharedMaterial)
		 shaderColor = rend.sharedMaterial.color;
		minX = -10.0f + size.x/2;
		maxX = 10.0f - size.x/2;

		minZ = -10.0f + size.z/2;
		maxZ = 10.0f - size.z/2;
	}

	void Update () {

//		float scroll = Input.GetAxis("Mouse ScrollWheel");
//		myCamera.transform.Translate(0, scroll * zoomSpeed, scroll * zoomSpeed, Space.World);
	}


	void OnMouseDown()
	{
        DragDummyObject.activeCollisions.Clear();

        if (EventSystem.current.IsPointerOverGameObject ())
			return;

		DragDummyObject.currentDraggedObject = transform.gameObject;
		DragDummyObject.newScale (size);	

		isDragging = true;

		// pos of transform on 2d
		Vector3 offsetFloor = new Vector3(transform.position.x,transform.position.y - size.y/2 ,transform.position.z);
		dist = Camera.main.WorldToScreenPoint(offsetFloor); 
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
		posZ = Input.mousePosition.z - dist.z;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		// for calculating the offset
		RaycastHit[] hits = Physics.RaycastAll(ray,100);
		RaycastHit hit;
		for (int i = 0; i < hits.Length; i++) {
			hit = hits [i];
			if (hit.transform.tag == "floor" || hit.transform.tag.Contains ("wall")) {
				offsetVec3 = transform.position - hit.point;
				break;
			}
		}

		lastPosition = transform.position;
		oldMousePosY = Input.mousePosition.y;
		objDragged = true;

		//rend.sha
		rend.material.color = new Color (0.5f,0.8f,1.0f); //Color.blue; new Color(
		UIScript.currentSelection = transform.gameObject;
        UIScript.currentSelectionPosition = transform.position;
        UIScript.showDelete = true;
    }
	  
	void OnMouseUp()
	{
		if (EventSystem.current.IsPointerOverGameObject ())
			return;

		DragDummyObject.visible (false);
        DragDummyObject.isColliding = false;

        UIScript.currentSelectionPosition = transform.position;
        DragDummyObject.obj.transform.rotation = transform.rotation;
        DragDummyObject.obj.transform.position = transform.position;

        isDragging = false;
		objDragged = false;
		rend.material.color = shaderColor;

        // Only for doors
        if(transform.tag.Contains("door") || transform.tag.Contains("Door"))
        {
            if (!isSnapped && lastPosition == null)
            {
                Destroy(transform.gameObject);
            }
            else
            {
                transform.position = lastPosition;
                transform.parent = lastParent;
                UIScript.currentSelectionPosition = transform.position;
            }
               
        }

	}

	void OnMouseDrag()
	{
		if (EventSystem.current.IsPointerOverGameObject () || !isDragging) {
			return;

		}
//		if (isSnapped)
//			return;
		
//		dist.z = dist.z - (oldMousePosY - Input.mousePosition.y) * 0.09f  ;
//		oldMousePosY = Input.mousePosition.y;

		checkRayCast ();
        UIScript.currentSelectionPosition = transform.position;
    }

    //RaycastHit currentHitPoint;
    protected virtual void checkRayCast ()
	{
		Vector3 test = new Vector3 (Input.mousePosition.x - posX, Input.mousePosition.y - posY);

		Ray ray = Camera.main.ScreenPointToRay(test);

		RaycastHit hit;
		RaycastHit[] hits = Physics.RaycastAll(ray,100);

		for (int i = 0; i < hits.Length; i++) {
			hit = hits[i];
            
            if (hit.transform.tag == "floor" || hit.transform.tag.Contains ("wall")) {
				worldPos = hit.point;
				Debug.DrawLine(ray.origin,hit.point,Color.yellow);
				checkWall (hit.transform.tag);
			}
        }

		checkFloor ();
		if (onWall)
			worldPos.y += size.y / 2;

		DragDummyObject.obj.transform.rotation = transform.rotation;
		DragDummyObject.obj.transform.position = worldPos;

       
	}

	protected virtual void checkWall(string collidingObject)
	{
		if(collidingObject == "wallLeft")
		{
			transform.rotation = Quaternion.Euler (0, 90, 0);
			minX = -10.0f + size.z/2;
			maxX = 10.0f - size.z/2;

			minZ = -10.0f + size.x/2;
			maxZ = 10.0f - size.x/2;

			currentWall = collidingObject;
			onWall = true;
		}
		else if(collidingObject == "wallRight")
		{
			transform.rotation = Quaternion.Euler (0,-90, 0);
			minX = -10.0f + size.z/2;
			maxX = 10.0f - size.z/2;

			minZ = -10.0f + size.x/2;
			maxZ = 10.0f - size.x/2;

			currentWall = collidingObject;
			onWall = true;
		}
		else if(collidingObject == "wallBack") 
		{

			transform.rotation = Quaternion.Euler (0, 180, 0);
			minX = -10.0f + size.x/2;
			maxX = 10.0f - size.x/2;

			minZ = -10.0f + size.z/2;
			maxZ = 10.0f - size.z/2;

			currentWall = collidingObject;
			onWall = true;
		}

		if (worldPos.x < minX) 
			worldPos.x = minX;
		
		if (worldPos.x > maxX) 
			worldPos.x = maxX;
		
		if (worldPos.z > maxZ) 
			worldPos.z = maxZ;

	}

	protected virtual void checkFloor()
	{
		if (!onWall || worldPos.y < size.y / 2) {
			worldPos.y = size.y/2;
			onWall = false;
		}
			
		
		if (worldPos.y > 5.5f)
			worldPos.y = 5.5f;
		
	}


	/*------------------------------------------------Snap----------------------------------------------------------------------*/


	//protected  // instead of null check for 10000.0 value
	protected float nearestSnapDist;
	public Vector3 nearestSnap(Vector3 currentGlobalPoint, Vector3 currentSanppedLocalPosition, string type)
	{
		nearestSnapDist = 0.0f;
		SnapInfo nearest = null;
		foreach(SnapInfo snap in snapPoints)
		{
			
			float dist = Vector3.Distance (currentGlobalPoint, transform.TransformPoint(snap.snap));
			// Executes only first time
			if (nearestSnapDist == 0.0f && type == snap.type) {
				nearest = snap;
				nearestSnapDist = dist;
			}

			if (dist <= nearestSnapDist  && type == snap.type) 
			{
				nearestSnapDist = dist;
				nearest = snap;
			}
		}

		// Check the nearest first and see if its valid or available
		if (nearest != null && !nearest.isSnapped) {
			return nearest.snap;
		}
		else {
			return new Vector3 (10000.0f, 0.0f, 0.0f);
		}
	}

	public void removeExistingSnap(Vector3 currentPoint, string type)
	{
		if (currentPoint.x == 10000.0f)
			return;

		foreach(SnapInfo snap in snapPoints)
		{
			if (snap.type == type && snap.snap.x == currentPoint.x && snap.snap.y == currentPoint.y && snap.snap.z == currentPoint.z) {
				snap.isSnapped = false;
			}
		}
	}

	public void addSnap(Vector3 currentPoint, string type)
	{
		if (currentPoint.x == 10000.0f)
			return;

		foreach(SnapInfo snap in snapPoints)
		{
			if (snap.type == type && snap.snap.x == currentPoint.x && snap.snap.y == currentPoint.y && snap.snap.z == currentPoint.z) {
				snap.isSnapped = true;
			}
		}
	}


    void OnDestroy()
    {
        DragDummyObject.activeCollisions.Remove(col);
       
    }

}
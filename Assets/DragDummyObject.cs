using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DragDummyObject : MonoBehaviour {

	public static GameObject obj;
	public static bool isColliding;
	public static GameObject currentDraggedObject;
	public static Transform currentCollidedTransform;
    public static List<Collider> activeCollisions;

    public static Renderer rend;

	public static void newScale(Vector3 newSize) {

		Vector3 size = obj.GetComponent<Renderer> ().bounds.size;

		Vector3 rescale = obj.transform.localScale;

		rescale.x = newSize.x * rescale.x / size.x;
		rescale.y = newSize.y * rescale.y / size.y;
		rescale.z = newSize.z * rescale.z / size.z;

		obj.transform.localScale = rescale;

	}

	public static void visible(bool val)
	{
		rend.enabled = val;
	}

	void Awake()
	{
		obj = gameObject;
		rend = GetComponent<Renderer> ();
		rend.enabled = false;
        activeCollisions = new List<Collider>();

    }

	void OnTriggerEnter(Collider other)
	{

       if (currentDraggedObject != other.transform.gameObject && !isParent(other.transform) && !isChild(other.transform)
            && other.tag != "floor")
        {
            activeCollisions.Add(other);
            isColliding = true;
            currentCollidedTransform = other.transform;
           
        }
    }

	void OnTriggerExit(Collider other)
	{
        activeCollisions.Remove(other);
        if (currentDraggedObject != other.transform.gameObject && !isParent(other.transform) && !isChild(other.transform)
             && other.tag != "floor")
        {
           isColliding = false;
		   rend.enabled = false;
		}
	}

    bool isParent(Transform obj)
    {
        if (currentDraggedObject == null || obj == null)
            return false;

        if (currentDraggedObject.transform.parent == obj)
            return true;

        return false;
    }

    bool isChild(Transform obj)
    {
        if (currentDraggedObject == null || obj == null)
            return false;

        if (obj.parent == currentDraggedObject.transform)
            return true;
        
        return false;
    }
}

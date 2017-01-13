using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DoorDragHandler : drag {

	//snapping
	Vector3 snapVec;

	override protected void Awake()
	{
		base.Awake ();
		//snapVec = new Vector3 (0.0f, 0.00f, 0.6f);
		addSnapPoints();
	}

	void addSnapPoints()
	{
		
	}

	// Collision and snapping
	void OnTriggerEnter(Collider other) {

		if (other.transform.tag == "frame")
		{

//			if (!isDragging) {
//				//rend.material.color = Color.red;
//			} else if (!isSnapped) {
//				FrameDragHandler frameDrag = (FrameDragHandler)other.transform.GetComponent ("FrameDragHandler");
//				snapVec = frameDrag.nearestSnap (transform.position,new Vector3 (10000.0f, 0.0f, 0.0f), "door");
//
//				if (snapVec.x != 10000.0f) {
//					transform.parent = other.transform;
//					transform.localPosition = snapVec;
//					isSnapped = true;
//					lastParent = other.transform.gameObject;
//					lastPosition = snapVec;
//				}
//			}
//			else {
//				isColliding = true;
//				FrameDragHandler frameDrag1 = (FrameDragHandler)other.transform.GetComponent ("FrameDragHandler");
//				frameDrag1.rend.material.color = Color.red;
//
//				for (int i = 0; i < other.transform.childCount; i++) {
//					GameObject temp = (GameObject)other.transform.GetChild (i).gameObject;
//					temp.transform.GetComponent<Renderer> ().material.color = Color.red;
//
//				}
//			}
		}
	}

	public void changeTexture (string str)
	{
		Texture txt;
		if (str == "1") {
			txt = Resources.Load ("DR08_1") as Texture;
			rend.material.mainTexture = txt;
		}
		else if (str == "2") {
			txt = Resources.Load ("DR08_2") as Texture;
			rend.material.mainTexture = txt;
		} 
		else if (str == "3") {
			txt = Resources.Load ("DR08_3") as Texture;
			rend.material.mainTexture = txt;
		}

	}

    override protected void checkRayCast()
    {
        base.checkRayCast();

        snapVec = new Vector3(10000.0f, 0.0f, 0.0f);
        if (DragDummyObject.activeCollisions.Count != 0)
        {
            FrameDragHandler frameDragScript = null;
            GameObject tempObject = null;
            float dist = 0.0f;
            float nearest = 0.0f;


            foreach (Collider coll in DragDummyObject.activeCollisions)
            {
                if (frameDragScript == null)
                {
                    frameDragScript = (FrameDragHandler)coll.transform.GetComponent("FrameDragHandler");
                    if (frameDragScript != null)
                    {
                        snapVec = frameDragScript.nearestSnap(worldPos, lastValidSnap, transform.tag);
                        dist = Vector3.Distance(snapVec, DragDummyObject.obj.transform.position);
                        nearest = dist;
                        tempObject = coll.transform.gameObject;
                    }



                    continue;
                }


                Vector3 sanp = Vector3.zero;

                frameDragScript = (FrameDragHandler)coll.transform.GetComponent("FrameDragHandler");
                if (frameDragScript != null)
                {
                    sanp = frameDragScript.nearestSnap(worldPos, lastValidSnap, transform.tag);
                    dist = Vector3.Distance(sanp, DragDummyObject.obj.transform.position);

                    if (dist < nearest)
                    {
                        nearest = dist;
                        snapVec = sanp;
                        tempObject = coll.transform.gameObject;
                    }
                }

            }

            if (snapVec.x != 10000.0f && frameDragScript != null)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                if(lastParent != null)
                {
                    FrameDragHandler script = (FrameDragHandler)lastParent.transform.GetComponent("FrameDragHandler");
                    script.removeExistingSnap(lastValidSnap, transform.tag);
                }
                
                frameDragScript.addSnap(snapVec, transform.tag);
                transform.parent = tempObject.transform;
                transform.localPosition = snapVec;
                isSnapped = true;
                lastParent = tempObject.transform;
                lastValidSnap = snapVec;
                lastPosition = transform.position;
                return;
            }

            if (!isSnapped)
            {
                DragDummyObject.visible(true);
                transform.position = lastPosition;
                transform.parent = lastParent;
            }
        }
        else
        {
            if(isSnapped)
            {
                FrameDragHandler script = (FrameDragHandler)lastParent.transform.GetComponent("FrameDragHandler");
                script.removeExistingSnap(lastValidSnap, transform.tag);
            }
            
            transform.parent = rootParent;
            transform.position = worldPos;
            isSnapped = false;

        }
    }
}

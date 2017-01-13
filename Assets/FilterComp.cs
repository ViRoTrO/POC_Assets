using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class FilterComp : MonoBehaviour
{

	// Use this for initialization



		void Start ()
	{
		
		GameObject newCanvas = new GameObject("Canvas");
		Canvas c = newCanvas.AddComponent<Canvas>();
		c.renderMode = RenderMode.ScreenSpaceOverlay;
		newCanvas.AddComponent<CanvasScaler>();
		newCanvas.AddComponent<GraphicRaycaster>();
		GameObject concont = new GameObject("Canvas");
		//newCanvas.AddComponent<HorizontalLayoutGroup>();



		//Text txt = concont.AddComponent<Text>();
	//	txt.rectTransform.sizeDelta = new Vector2 (100, 30);
		//txt.text = "Strorage Frames";
		Image i = concont.AddComponent<Image>();
		i.material.mainTexture = (Texture)Resources.Load ("BESTA_frame");
		i.rectTransform.sizeDelta = new Vector2 (100, 100);
	//	i.preserveAspect = true;
		concont.transform.SetParent(newCanvas.transform, false);

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}


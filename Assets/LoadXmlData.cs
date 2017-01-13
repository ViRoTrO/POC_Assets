using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
namespace AssemblyCSharp
{
	public class LoadXmlData : MonoBehaviour
	{
		static int actualLevel = 1;
		static int LevelMaxNumber;
		static int WaipointCounter = 0;

	

		private string finaltext = "";

		public GameObject LevelName;
		public GameObject Tutorial;
		public GameObject FinalText;
		//public GameObject FilterPanel;
		//public GameObject FilterPanelClone;
		public TextAsset GameAsset;

		List<Dictionary<string,string>> levels = new List<Dictionary<string,string>>();
		Dictionary<string,string> obj;

		public GameObject prefabObj;
		public GameObject TVBen;
		public GameObject TVBrac;
		public GameObject Propp;
		public GameObject prodFilterObj;
		public GameObject prodsObj;
		public GameObject clone;
		public Button Addprods;
		public Button RoomLayout;
		public Button strfrbtn;
		public Button tvbenbtn;
		public Button tvbracbtn;
		public Button propbtn;
		public GameObject prodPan;
		public GameObject roomPan;
		public GameObject strfrmPan;


		Button frameBtn;
		Button doorBtn;
		public GameObject frame;
		public GameObject door;

		void Start()
		{	//Timeline of the Level creator
			
			//GetData();
			//StartCoroutine(LevelStartInfo(1.0F));
			//LevelMaxNumber = levels.Count;
			//Canvas can = (Canvas)((GameObject)Instantiate(StorFr));
			//GameObject go = (GameObject)Instantiate(prefabObj);
			//go.transform.SetParent(ParentPanel, false);
			//go.transform.localScale = new Vector3(1, 1, 1);

			//Image tempimg = go.GetComponent<Image>();
			//tempimg.material.mainTexture = (Texture)Resources.Load ("BESTA_frame");
			//GameObject go1 = (GameObject)Instantiate(prefabObj);
			//clone = (GameObject)Instantiate(prefabObj);
			//clone.GetComponent<Image>().material.mainTexture = (Texture)Resources.Load ("BESTA_frame");

	
			prodPan = GameObject.Find("ProdFilterPanel");
			roomPan = GameObject.Find("RoomPanel");
			strfrmPan = GameObject.Find("strFramesPanel");
			getproducts ();
			roomPan.gameObject.SetActive(false);
			strfrmPan.gameObject.SetActive(false);
			Addprods = GameObject.Find("AddProdBtn").GetComponent<Button>();


			strfrbtn = GameObject.Find("strfrbtn").GetComponent<Button>();
			strfrbtn.onClick.AddListener(delegate { btnclick(strfrbtn,strfrbtn.name); });

			tvbenbtn = GameObject.Find("tvbenbtn").GetComponent<Button>();
			tvbenbtn.onClick.AddListener(delegate { btnclick(tvbenbtn,tvbenbtn.name); });

			tvbracbtn = GameObject.Find("tvbracbtn").GetComponent<Button>();
			tvbracbtn.onClick.AddListener(delegate { btnclick(tvbracbtn,tvbracbtn.name); });

			propbtn = GameObject.Find("propbtn").GetComponent<Button>();
			propbtn.onClick.AddListener(delegate { btnclick(propbtn,propbtn.name); });

			Addprods.onClick.AddListener(delegate { prodclick(Addprods,Addprods.name); });
			Addprods.Select();
			RoomLayout = GameObject.Find("RoomLayBtn").GetComponent<Button>();

			RoomLayout.onClick.AddListener(delegate { roomclick(RoomLayout,RoomLayout.name); });
			//FilterPanelClone.AddComponent(clone.GetType());
		}


		private void createFrame(){
			Instantiate (frame);
		}

		private void createDoor(){
			Instantiate (door);
		}



		public void getproducts(){
//			Image[] imglist = strfrmPan.GetComponentsInParent<Image> ();
//			//imglist.Length
//			foreach (Image img in imglist) {
//				print (img.name);
//			}
		}
		public void btnclick(Button btn, string ok){
			Addprods.Select ();
			strfrmPan.gameObject.SetActive(true);
			prodPan.gameObject.SetActive(false);
			roomPan.gameObject.SetActive(false);

			frameBtn = GameObject.Find("frameBtn").GetComponent<Button>();
			doorBtn = GameObject.Find("doorBtn").GetComponent<Button>();

			frameBtn.onClick.AddListener(createFrame);
			doorBtn.onClick.AddListener(createDoor);
		}
				
		public void prodclick(Button btn, string ok){
			prodPan.gameObject.SetActive(true);
			strfrmPan.gameObject.SetActive(false);
			roomPan.gameObject.SetActive(false);


		}

		public void roomclick(Button btn, string ok){
			roomPan.gameObject.SetActive(true);
			prodPan.gameObject.SetActive(false);
			strfrmPan.gameObject.SetActive(false);
		}

		//public Canvas CanvasObject;
		public void Awake()
		{
			//CanvasObject = GameObject.Find("Filterpab").GetComponent<Canvas>();
			//CanvasObject.GetComponent<Canvas> ().enabled = false;


		}

		public void GetData()
		{
			XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
			xmlDoc.Load(UnityEngine.Application.dataPath + "/definition.xml"); // load the file.
			XmlNodeList levelsList = xmlDoc.GetElementsByTagName("definition_data"); // array of the level nodes.

			for(int i=0; i<= levelsList.Count -1;i++)
			{
				XmlNodeList levelcontent = levelsList[i].ChildNodes;
				obj = new Dictionary<string,string>(); // Create a object(Dictionary) to colect the both nodes inside the level node and then put into levels[] array.

				foreach (XmlNode levelsItems in levelcontent) // levels itens nodes.
				{
					if(levelsItems.Name == "name")
					{
						obj.Add("name",levelsItems.InnerText); // put this in the dictionary.
					}

					if(levelsItems.Name == "tutorial")
					{
						obj.Add("tutorial",levelsItems.InnerText); // put this in the dictionary.
					}

					if(levelsItems.Name == "object")
					{
						switch(levelsItems.Attributes["name"].Value)
						{
						case "Cube": obj.Add("Cube",levelsItems.InnerText);break; // put this in the dictionary.
						case "Cylinder":obj.Add("Cylinder",levelsItems.InnerText); break; // put this in the dictionary.
						case "Capsule":obj.Add("Capsule",levelsItems.InnerText); break; // put this in the dictionary.
						case "Sphere": obj.Add("Sphere",levelsItems.InnerText);break; // put this in the dictionary.
						}
					}

					if(levelsItems.Name == "finaltext")
					{
						obj.Add("finaltext",levelsItems.InnerText); // put this in the dictionary.
					}
				}
				levels.Add(obj); // add whole obj dictionary in the levels[].
			}
		}


		void Update()
		{
			

		}

	}
}

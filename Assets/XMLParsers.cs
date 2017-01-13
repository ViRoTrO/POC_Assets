using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Needed for Lists

using System.Xml; //Needed for XML functionality

using System.Xml.Serialization; //Needed for XML Functionality

using System.IO;

using System.Xml.Linq; //Needed for XDocument

public class XMLParsers : MonoBehaviour {

	private XDocument xmlDoc;
	IEnumerable<XElement> items;
	//List <XMLData> data = new List <XMLData>();
	// Use this for initialization
	void Start () {
		LoadXML ();
	}

	void LoadXML()

	{

		//Assigning Xdocument xmlDoc. Loads the xml file from the file path listed. 
		xmlDoc = XDocument.Load( "Assets/definition_data.xml" );

		//This basically breaks down the XML Document into XML Elements. Used later. 

		
		items = xmlDoc.Descendants("plannerAddonFilters").Elements ();
	
		if (items != null)
			print ("Working");
	}

}

using UnityEngine;
using System.Collections;
using System;

public class SnapInfo : IComparable<SnapInfo> {

	public Vector3 snap;
	public bool isSnapped;
	public string type;

	public SnapInfo(Vector3 newSnap, string newType, bool snapped)
	{
		snap = newSnap;
		isSnapped = snapped;
		type = newType;
	}

	//This method is required by the IComparable
	//interface. 
	public int CompareTo(SnapInfo other)
	{
		if(other == null)
		{
			return 1;
		}

//		//Return the difference in power.
		return 0;
	}
}



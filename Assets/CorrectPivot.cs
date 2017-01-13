using UnityEngine;
using System.Collections;

public class CorrectPivot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, 180, 0);
	}
}

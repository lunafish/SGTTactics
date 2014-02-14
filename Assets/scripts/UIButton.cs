using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText( string text ) {
		GetComponentInChildren<TextMesh>().text = text;
	}
}

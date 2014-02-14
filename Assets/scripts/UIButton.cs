using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {
	private TextMesh _text;

	// Use this for initialization
	void Start () {
		_text = GetComponentInChildren<TextMesh> ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setText( string text ) {
		_text.text = text;
	}
}

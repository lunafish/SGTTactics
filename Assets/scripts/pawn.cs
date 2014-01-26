using UnityEngine;
using System.Collections;

public class pawn : MonoBehaviour {
	public static int SELECT_NONE = 0;
	public static int SELECT_RED = 1;
	public static int SELECT_BLUE = 2;


	// Use this for initialization
	void Start () {
		// test code
		select (SELECT_BLUE);
		//
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void select( int type ) {
		if( type == SELECT_RED ) {
			foreach (Transform child in transform) {
				child.renderer.material.color = Color.red;
			}
		} else if( type == SELECT_BLUE ) {
			foreach (Transform child in transform) {
				child.renderer.material.color = Color.blue;
			}
		} else {
			foreach (Transform child in transform) {
				child.renderer.material.color = Color.white;
			}
		}
	}
}

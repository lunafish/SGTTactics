using UnityEngine;
using System.Collections;

public class pawn : MonoBehaviour {
	public static int SELECT_NONE = 0;
	public static int SELECT_RED = 1;
	public static int SELECT_BLUE = 2;
	public static int ALLY = 0;
	public static int ENEMY = 1;

	public string _name;
	public int _hp;
	public int _mp;
	public int _sp;
	public int _index;
	public int _rmv; // range move
	public int _ratk; // range attack
	public int _atk;
	public int _def;
	public int _type;


	// Use this for initialization
	void Start () {

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

	public void initPawn( ) {
		Debug.Log (_name + " " + _hp + " " + _mp + " " + _sp);
	}
}

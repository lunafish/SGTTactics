using UnityEngine;
using System.Collections;

public class pawn : MonoBehaviour {
	public int _x;
	public int _y;
	public int _index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void select( bool bSelect ) {
		if (bSelect) {
			renderer.material.color = Color.red;
		}
		else {
			renderer.material.color = Color.white;
		}
	}
}

using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {
	public int _x;
	public int _y;
	public int _index;
	
	public tile[] _links = new tile[6]; 
	
	public static int SELECT_NONE = 0;
	public static int SELECT_RED = 1;
	public static int SELECT_GREEN = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void select( int type ) {
		if (type == tile.SELECT_RED) {
			renderer.material.color = Color.red;

			for(int i = 0; i < 6; i++) {
				if(_links[i] != null) {
					_links[i].select( tile.SELECT_GREEN );
				}
			}

		}
		if (type == tile.SELECT_GREEN) {
			renderer.material.color = Color.green;
		}
		else if(type == tile.SELECT_NONE) {
			renderer.material.color = Color.white;
		}
	}
}

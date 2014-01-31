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

	public int _select = 0;

	private GameObject _pawn = null;

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
				if(_links[i] != null && _links[i].isPawn() == false) {
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

		_select = type;
	}

	public void addPawn( GameObject pawn ) {
		if (_pawn != null) {
			removePawn();
		}

		Debug.Log ("addPawn : " + _index);
		pawn.GetComponent<pawn>()._index = _index;

		_pawn = pawn;
		_pawn.transform.position = transform.position;
	}

	public void removePawn( ) {
		_pawn = null;
	}

	public bool isPawn( ) {
		if (_pawn) {
			return true;
		}

		return false;
	}

	public GameObject getPawn( ) {
		return _pawn;
	}
}

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
	public static int SELECT_YELLOW = 3;

	public int _select = 0;

	private GameObject _pawn = null;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void recv_select( tile t, int m, int a ) {
		if (m <= 0) {
			return;	
		}
		
		for(int i = 0; i < 6; i++) {
//			if(t._links[i] != null && t._links[i].isPawn() == false) {
			if(t._links[i] != null) {

				if(a > 0) {
					t._links[i].select( tile.SELECT_YELLOW );
				} else {
					t._links[i].select( tile.SELECT_GREEN );
				}

				recv_select( t._links[i], (m-1), (a-1) );
			}
		}
	}

	public void select( int type ) {
		if (type == tile.SELECT_RED) {
			renderer.material.color = Color.red;

			recv_select( this, _pawn.GetComponent<pawn>()._rmv, _pawn.GetComponent<pawn>()._ratk );
			/*
			for(int i = 0; i < 6; i++) {
				if(_links[i] != null && _links[i].isPawn() == false) {
					_links[i].select( tile.SELECT_GREEN );
				}
			}
			*/
		}
		if (type == tile.SELECT_GREEN) {
			// recv function problem : red > yellow > green > whilte
			if(renderer.material.color == Color.white) {
				renderer.material.color = Color.green;
			} else if(renderer.material.color == Color.yellow) {
				type = tile.SELECT_YELLOW;
			}
			//
		} else if(type == tile.SELECT_YELLOW) {
			renderer.material.color = Color.yellow;
		} else if(type == tile.SELECT_NONE) {
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

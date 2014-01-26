using UnityEngine;
using System.Collections;

public class tacticsRule {
	
	public static tacticsRule _rule = null;
	public const int _tile_width = 16;
	public const int _tile_height = 16;

	private ArrayList _listTile = null;

	public static tacticsRule get( ) {
		if (_rule == null) {
			_rule = new tacticsRule ();
		}

		return _rule;
	}

	public bool makeTile( ) {
		if (_listTile != null) {
			return true;
		}

		_listTile = new ArrayList ();

		int hw = _tile_width / 2;
		int hh = _tile_height / 2;

		for (int i = 0; i < _tile_height; i++) {
			for (int j = 0; j < _tile_width; j++) {
				GameObject obj = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/tile", typeof(GameObject)));

				float m = (i % 2) + 1.0f;

				// setting object information
				obj.transform.position = new Vector3( (j - hw) * 2.0f + m, 0.0f, (i - hh) * 1.5f);
				obj.GetComponent<tile>()._x = j;
				obj.GetComponent<tile>()._y = i;
				obj.GetComponent<tile>().select( tile.SELECT_NONE );
				//

				// add object and get list index
				obj.GetComponent<tile>()._index = _listTile.Add( obj );
			}
		}

		// make tile link
		for (int i = 0; i < _listTile.Count; i++) {
			GameObject obj = (GameObject)_listTile[i];
			makeTileLink( obj.GetComponent<tile>() );
		}

		return true;
	}

	void makeTileLink( tile obj ) {
		int m = 1 - obj._y % 2; // hex margin

		int x, y;
		// upleft
		y = obj._y - 1;
		x = obj._x - m;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 0 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 0 ] = null;
		}
		// upright
		y = obj._y - 1;
		x = obj._x - m + 1;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 1 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 1 ] = null;
		}
		// left
		y = obj._y;
		x = obj._x - 1;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 2 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 2 ] = null;
		}
		// right
		y = obj._y;
		x = obj._x + 1;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 3 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 3 ] = null;
		}
		// downleft
		y = obj._y + 1;
		x = obj._x - m;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 4 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 4 ] = null;
		}
		// downright
		y = obj._y + 1;
		x = obj._x - m + 1;
		if ((x >= 0 && y >= 0) && (x < _tile_width && y < _tile_height)) {
			GameObject o = (GameObject)(_listTile[ x + (y * _tile_width) ]);
			obj._links[ 5 ] = o.GetComponent<tile>();
		} else {
			obj._links[ 5 ] = null;
		}

	}

	public void picking( GameObject obj ) {
		tile p = obj.GetComponent<tile> ();
		if (p == null) {
			return;
		}

		// deselect all
		for (int i = 0; i < _listTile.Count; i++) {
			GameObject o = (GameObject)_listTile [i];
			o.GetComponent<tile> ().select ( tile.SELECT_NONE );
		}
		//

		// select
		p.select ( tile.SELECT_RED );
		//


		Camera.main.GetComponent<MobileCamera> ().move (p.transform.position);

		Debug.Log ("select : " + p._index);
	}

	public void move( Vector2 vec ) {
		Vector3 moveDir = new Vector3 (vec.x, 0.0f, vec.y);
		Camera.main.transform.TransformDirection (moveDir);
		Camera.main.transform.position += (moveDir * 0.1f);
	}

	public bool makePawn( ) {
		// test code
		int x = Random.Range(0, _tile_width - 1);
		int y = Random.Range(0, _tile_height - 1);

		GameObject o = (GameObject)_listTile[ x + (y * _tile_width) ];
		GameObject pawn = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/cursor", typeof(GameObject)));
		o.GetComponent<tile> ().addPawn(pawn);

		//

		return true;
	}
}

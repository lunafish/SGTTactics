using UnityEngine;
using System.Collections;

public class tacticsRule {
	
	public static tacticsRule _rule = null;
	public int _tile_width = 16;
	public int _tile_height = 16;

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
				GameObject obj = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/pawn", typeof(GameObject)));

				float m = (i % 2) + 1.0f;

				// setting object information
				obj.transform.position = new Vector3( (j - hw) * 2.0f + m, 0.0f, (i - hh) * 1.5f);
				obj.GetComponent<pawn>()._x = j;
				obj.GetComponent<pawn>()._y = i;
				obj.GetComponent<pawn>().select( false );
				//

				// add object and get list index
				obj.GetComponent<pawn>()._index = _listTile.Add( obj );
			}
		}


		return true;
	}

	public void picking( GameObject obj ) {
		pawn p = obj.GetComponent<pawn> ();
		if (p == null) {
			return;
		}

		// deselect all
		for (int i = 0; i < _listTile.Count; i++) {
			GameObject o = (GameObject)_listTile [i];
			o.GetComponent<pawn> ().select (false);
		}
		//

		// select
		p.select ( true );
		//

		Camera.main.GetComponent<MobileCamera> ().move (p.transform.position);

		Debug.Log ("select : " + p._index);
	}

	public void move( Vector2 vec ) {
		Vector3 moveDir = new Vector3 (vec.x, 0.0f, vec.y);
		Camera.main.transform.TransformDirection (moveDir);
		Camera.main.transform.position += (moveDir * 0.1f);
	}
}

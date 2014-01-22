using UnityEngine;
using System.Collections;

public class tacticsRule {
	
	public static tacticsRule _rule = null;
	public int _tile_width = 16;
	public int _tile_height = 16;

	private ArrayList _listTile = null;

	public static tacticsRule get( ) {
		if (_rule == null)
			_rule = new tacticsRule();

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

				obj.transform.position = new Vector3( (j - hw) * 2.0f + m, 0.0f, (i - hh) * 1.5f);

				_listTile.Add( obj );
			}
		}


		return true;
	}
}

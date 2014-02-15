using UnityEngine;
using SimpleJSON;
using System.Collections;

public class tacticsRule {
	public static tacticsRule _rule = null;
	public const int _tile_width = 16;
	public const int _tile_height = 16;

	private ArrayList _listTile = null;
	private ArrayList _listAlly = null;
	private ArrayList _listEnemy = null;

	private tile _select = null;
	private GameObject _target = null;

	private GameObject _dialog = null;

	public static tacticsRule get( ) {
		if (_rule == null) {
			_rule = new tacticsRule ();
		}

		return _rule;
	}

	public static void message( string msg ) {
		GameObject bar = GameObject.FindGameObjectWithTag("InfoBar");
		bar.GetComponentInChildren<TextMesh>().text = msg;
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

		if (p.isPawn () == false) {

			if(_select != null) {
				if(p._select != tile.SELECT_NONE) {
					// move
					p.addPawn ( _select.getPawn() );
					_select.removePawn();
					//
				} else {
					return;
				}
			} else {
				return;
			}
		} else {
			if(_select != null) {
				// attack
				if(p._select == tile.SELECT_YELLOW) {
					if(_select.getPawn().GetComponent<pawn>()._type == p.getPawn().GetComponent<pawn>()._type) {
						message( "Ally " + _select.getPawn().GetComponent<pawn>()._name + " and " + p.getPawn().GetComponent<pawn>()._name );
					} else {
						message( "Attack from " + _select.getPawn().GetComponent<pawn>()._name + " to " + p.getPawn().GetComponent<pawn>()._name );
						_dialog.SetActive(true);
					}
				} else {
					message( "Outrange : " + p.getPawn().GetComponent<pawn>()._name );
				}
				//
				return;
			}
		}

		// deselect all
		for (int i = 0; i < _listTile.Count; i++) {
			GameObject o = (GameObject)_listTile [i];
			o.GetComponent<tile> ().select ( tile.SELECT_NONE );
		}
		//

		// select
		p.select ( tile.SELECT_RED );
		_select = p;
		message( p.getPawn().GetComponent<pawn>()._name + " seleted" );
		//


		Camera.main.GetComponent<MobileCamera> ().move (p.transform.position);

		Debug.Log ("select : " + p._index);
	}

	public void ui_picking( GameObject obj ) {
		if (obj.GetComponent<slot>() != null) {
			slot_picking(obj);
			return;
		}

		if (obj.GetComponent<UIButton> () != null) {
			dlg_picking(obj);
			return;
		}
	}

	void slot_picking( GameObject obj ) {
		_select = null;
		slot s = (slot)obj.GetComponent<slot> ();
		GameObject p = s._pawn;
		picking( (GameObject)_listTile[ p.GetComponent<pawn>()._index ] );
		
		// select slot up
		Vector3 pos = obj.transform.parent.transform.position;
		for(int i = 0; i < _listAlly.Count; i++) {
			GameObject tmp = (GameObject)_listAlly[i];
			if(tmp == obj) {
				tmp.transform.position = new Vector3(tmp.transform.position.x, pos.y -0.5f, tmp.transform.position.z);
			}
			else {
				tmp.transform.position = new Vector3(tmp.transform.position.x, pos.y -0.75f, tmp.transform.position.z);
			}
			GameObject sim = tmp.GetComponent<slot>()._sim;
			sim.transform.position = new Vector3(pos.x - 1.0f + ((tmp.GetComponent<slot>()._index - s._index) * 2.0f), sim.transform.position.y, sim.transform.position.z);
		}
		//
	}

	void dlg_picking( GameObject obj ) {
		Debug.Log ( _dialog.GetComponent<UIDialog>().getBtnIndex(obj) + " " + obj );

		// cancle
		if(_dialog.GetComponent<UIDialog>().getBtnIndex(obj) == 2) {
			_dialog.SetActive(false);
		}
	}

	public void move( Vector2 vec ) {
		Vector3 moveDir = new Vector3 (vec.x, 0.0f, vec.y);
		Camera.main.transform.TransformDirection (moveDir);
		Camera.main.transform.position += (moveDir * 0.1f);
	}

	GameObject makePawnObject( JSONNode v ) {
		int x = v["x"].AsInt;
		int y = v["y"].AsInt;
		
		int idx = x + (y * _tile_width);
		
		GameObject o = (GameObject)_listTile[ idx ];
		GameObject p = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/cursor", typeof(GameObject)));
		
		p.GetComponent<pawn>()._name = v["name"];
		p.GetComponent<pawn>()._hp = v["hp"].AsInt;
		p.GetComponent<pawn>()._mp = v["mp"].AsInt;
		p.GetComponent<pawn>()._sp = v["sp"].AsInt;
		p.GetComponent<pawn>()._rmv = v["rmv"].AsInt;
		p.GetComponent<pawn>()._ratk = v["ratk"].AsInt;
		p.GetComponent<pawn>()._atk = v["atk"].AsInt;
		p.GetComponent<pawn>()._def = v["def"].AsInt;
		p.GetComponent<pawn>()._avatar = v ["avatar"].AsInt;
		
		if( string.Compare(v["type"], "ally") == 0) {
			p.GetComponent<pawn>().select( pawn.SELECT_BLUE );
			p.GetComponent<pawn>()._type = pawn.ALLY;
		} else {
			p.GetComponent<pawn>().select( pawn.SELECT_RED );
			p.GetComponent<pawn>()._type = pawn.ENEMY;
		}
		
		p.GetComponent<pawn>().initPawn();
		o.GetComponent<tile> ().addPawn(p);

		return p;
	}

	bool makePawnAlly( JSONNode v) {
		GameObject p = makePawnObject (v);
		
		// make slot
		GameObject ui = GameObject.FindGameObjectWithTag("UI"); // ui object
		
		GameObject sim = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/sp_simcard", typeof(GameObject)));
		GameObject slot = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/sp_slot", typeof(GameObject)));
		int index = _listAlly.Add(slot);

		slot.GetComponent<slot> ()._index = index;
		sim.transform.parent = ui.transform;
		sim.transform.position = ui.transform.position;
		sim.transform.position += new Vector3(-1.0f + (index * 2.0f), -1.0f, 0.0f);
		sim.GetComponent<simcard>()._pawn = p;
		sim.GetComponent<simcard>().updateUI();
		
		slot.GetComponent<slot>()._sim = sim;
		slot.GetComponent<slot>()._pawn = p;
		slot.transform.parent = ui.transform;
		slot.transform.position = ui.transform.position;
		slot.transform.position += new Vector3(-1.0f + (index * 0.5f), -0.75f, 1.0f);
		slot.GetComponent<slot>().updateSlot();
		//

		return true;
	}

	bool makePawnEnemy( JSONNode v ) {
		// test
		GameObject p = makePawnObject (v);
		int index = _listEnemy.Add(p);
		//
		return true;
	}

	public bool makePawn( ) {
		if (_listAlly != null) {
			_listAlly.Clear();
		}
		_listAlly = new ArrayList ();

		if (_listEnemy != null) {
			_listEnemy.Clear();
		}
		_listEnemy = new ArrayList ();

		string txt;
		if (readTxt ("json/test_stage", out txt) == true) {
			var json = JSONNode.Parse( txt );
			for(int i = 0; i < json["pawns"].Count; i++) {
				var v = json["pawns"][i];
				if( string.Compare(v["type"], "ally") == 0) {
					makePawnAlly( json["pawns"][i] );
				} else {
					makePawnEnemy( json["pawns"][i] );
				}
			}

			message("Load Complite " + json["pawns"].Count + " pawns");
		}

		return true;
	}

	bool readTxt( string path, out string txt ) {
		TextAsset ta = (TextAsset)Resources.Load (path) as TextAsset;
		if (ta == null) {
			txt = "";
			return false;
		}

		txt = ta.text;

		return true;
	}

	// dialog
	public void makeDialog( ) {
		_dialog = (GameObject)MonoBehaviour.Instantiate(Resources.Load("prefab/sp_dialog", typeof(GameObject)));

		GameObject ui = GameObject.FindGameObjectWithTag("UI"); // ui object
		_dialog.transform.parent = ui.transform;
		_dialog.transform.position = ui.transform.position + new Vector3(0.0f, 0.0f, -0.5f);

		string txt;
		if (readTxt ("json/test_stage", out txt) == true) {
			var json = JSONNode.Parse( txt );
			_dialog.GetComponent<UIDialog>().loadDlg( json["battle_dlg"] );
		}

		_dialog.SetActive (false);
	}
}

using UnityEngine;
using System.Collections;

public class tactics : MonoBehaviour {
	public Camera _ui_camera;

	private tacticsRule _rule = null;

	// for mouse
	private Vector2 _mouse_start_pos;
	private Vector2 _mouse_pos;
	private bool _mouse_move;
	private float _mouse_margin = 5.0f;

	// layer
	private int layer_ui_0 = 0;
	private int layer_object = 1;

	// Use this for initialization
	void Start () {
		_rule = tacticsRule.get ();
		// test code
		_rule.makeTile ();
		_rule.makePawn ();
		//
	}
	
	// Update is called once per frame
	void Update () {
		if (_rule == null) {
			return;
		}

		// picking UI Object
		if (Picking (_ui_camera, layer_ui_0) == false) {
			// picking 3D Object
			Picking ( Camera.main, layer_object );
		}
	}

	// picking
	bool Picking( Camera cam, int layer ) {

		Vector2 pos = new Vector2(0, 0);
		bool bTouch = false;
#if UNITY_IPHONE
		if (Input.touchCount > 0 ) {
			if(Input.touches[0].phase == TouchPhase.Began) {
				_mouse_start_pos = Input.touches[0].position;
			}
			else if(Input.touches[0].phase == TouchPhase.Moved) {
				Vector2 v = Input.touches[0].position;
				v = v - _mouse_start_pos;
				
				// minimum mouse move check
				if(v.magnitude > _mouse_margin) {
					_mouse_move = true;
				}
				
				// mouse move event
				if( _mouse_move == true ) {
					Vector2 mv = Input.touches[0].position;
					_rule.move( mv - _mouse_pos );
				}
				
				_mouse_pos = Input.touches[0].position;
			}
			else if(Input.touches[0].phase == TouchPhase.Ended) {
				if(_mouse_move == false) {
					bTouch = true;
				}
				pos = Input.touches[0].position;
				_mouse_move = false;
			}
		}
#else
		if(Input.GetMouseButtonDown(0) == true) {
			_mouse_start_pos = Input.mousePosition;
		}
		else if(Input.GetMouseButton(0) == true) {
			Vector2 v = Input.mousePosition;
			v = v - _mouse_start_pos;

			// minimum mouse move check
			if(v.magnitude > _mouse_margin) {
				_mouse_move = true;
			}

			// mouse move event
			if( _mouse_move == true ) {
				Vector2 mv = Input.mousePosition;
				_rule.move( mv - _mouse_pos );
			}

			_mouse_pos = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0) == true) {
			if(_mouse_move == false) {
				bTouch = true;
			}
			pos = Input.mousePosition;
			_mouse_move = false;
		}
#endif
		if (bTouch) {
			//Debug.Log( pos );
			Ray ray = cam.ScreenPointToRay( pos );

			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				// picking event
				if( layer == layer_ui_0) {
					_rule.ui_picking( hit.transform.gameObject );
				} else {
					_rule.picking( hit.transform.gameObject );
				}
				return true;
			}
		}

		return false;
	}
}

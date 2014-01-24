using UnityEngine;
using System.Collections;

public class MobileCamera : MonoBehaviour {
	private Vector3 _target;
	private bool _isMove = false;

	// Use this for initialization
	void Start () {
		_target = transform.position;
		_isMove = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (_isMove ) {
			if( (transform.position - _target).magnitude < 0.1f ) {
				_isMove = false;
				return;
			}

			transform.position += (_target - transform.position) * 0.25f; 
		}

	}

	public void move( Vector3 target )
	{
		Vector3 vec = target - transform.position;
		vec.Normalize ();
		_target.x = target.x - vec.x * 10.0f;
		_target.z = target.z - vec.z * 10.0f;
		_isMove = true;
	}
}

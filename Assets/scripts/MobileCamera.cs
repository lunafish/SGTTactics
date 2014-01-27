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
		Vector3 vec = new Vector3 (0.0f, 0.0f, -1.0f);
		transform.TransformDirection( vec );

		_target.x = target.x - vec.x * 12.0f;
		_target.z = target.z - vec.z * 12.0f;
		_isMove = true;
	}
}

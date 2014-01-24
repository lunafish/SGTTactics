using UnityEngine;
using System.Collections;

public class tactics : MonoBehaviour {
	private tacticsRule _rule = null;


	// Use this for initialization
	void Start () {
		_rule = tacticsRule.get ();
		_rule.makeTile ();
	}
	
	// Update is called once per frame
	void Update () {
		// picking
		Picking ();


	}

	// picking
	void Picking( ) {

		Vector2 pos = new Vector2(0, 0);
		bool bTouch = false;
#if UNITY_IPHONE
		if (Input.touchCount > 0 ) {
			if(Input.touches[0].phase == TouchPhase.Began)
			{
				bTouch = true;
				pos = Input.touches[0].position;
			}
		}
#else
		if (Input.GetMouseButtonDown(0) == true) {
			bTouch = true;
			pos = Input.mousePosition;
		}
#endif
		if (bTouch) {
			Ray ray = Camera.main.ScreenPointToRay( pos );

			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				// picking event
				_rule.picking( hit.transform.gameObject );
			}
		}
	}
}

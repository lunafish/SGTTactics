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
		if (Input.GetMouseButton (0) == true) {
			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)) {
				Debug.Log("Hit!" + hit.transform.gameObject);
			}
		}
	}
}

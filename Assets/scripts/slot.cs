using UnityEngine;
using System.Collections;

public class slot : MonoBehaviour {
	public GameObject _pawn = null;

	public GameObject _hp;
	public GameObject _mp;
	public GameObject _sp;
	public GameObject _avatar;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateUI () {
		_hp.GetComponent<TextMesh> ().text = "HP " + _pawn.GetComponent<pawn>()._hp;
		_mp.GetComponent<TextMesh> ().text = "MP " + _pawn.GetComponent<pawn>()._mp;
		_sp.GetComponent<TextMesh> ().text = "SP " + _pawn.GetComponent<pawn>()._sp;
	}
}

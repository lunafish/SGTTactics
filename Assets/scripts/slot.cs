using UnityEngine;
using System.Collections;

public class slot : MonoBehaviour {
	public GameObject _pawn = null;
	public GameObject _sim = null;
	public int _index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void updateSlot () {
		GetComponent<tk2dSprite>().spriteId = _pawn.GetComponent<pawn>()._avatar;
	}
}

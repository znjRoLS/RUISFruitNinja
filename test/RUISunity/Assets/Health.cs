using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	private int hp = 2;
	public Animator a;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log ("trigger");
	}

	void OnCollisionEnter(Collision c){
		hp--;
		if (hp == 0) {
			a.SetInteger ("dieMethod", Random.Range (0, 4));
		} else {
			a.SetTrigger("wounded");
		}
	}

}

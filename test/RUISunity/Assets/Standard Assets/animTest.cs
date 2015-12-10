using UnityEngine;
using System.Collections;

public class animTest : MonoBehaviour {

	public Animator a;
	private int dyingLimit = 4;

	void Start(){

	}

	void Update(){

		if (Input.GetKeyDown(KeyCode.C)){
			a.SetTrigger("throwing");
		}

		if (Input.GetKeyDown(KeyCode.D)){
			int x = Random.Range(0, dyingLimit);
			a.SetInteger ("dieMethod", x);
			Debug.Log(x);
		}

		if (Input.GetKeyDown(KeyCode.W)){
			a.SetTrigger("wounded");
		}

	}


}

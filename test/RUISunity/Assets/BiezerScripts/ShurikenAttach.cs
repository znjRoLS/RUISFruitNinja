using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ShurikenAttach : MonoBehaviour {
	
	private Rigidbody rigidbody;

	void Start(){
		rigidbody = GetComponent<Rigidbody> ();
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == 0) {
			rigidbody.velocity = Vector3.zero;
			rigidbody.isKinematic = true;
			foreach (Collider c in GetComponents<Collider>()){
				c.enabled = false;
			}


			Destroy (gameObject, 5);
		}
	}

}

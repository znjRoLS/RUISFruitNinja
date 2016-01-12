using UnityEngine;
using System.Collections;

public class NinjaFire : MonoBehaviour {

	public FruitCanon canon;
	public GameObject target;
	public Animator a;

	void Start(){
		rotateTowardsTarget ();
		Invoke ("fireLogic", Random.Range(4, 10));
	}

	public void fire(){
		rotateTowardsTarget ();
		canon.fire ();

	}

	private void rotateTowardsTarget(){
		float y = transform.position.y;
		transform.LookAt (new Vector3(target.transform.position.x, y, target.transform.position.z));
	}

	private void fireLogic(){
		a.SetTrigger ("throwing");
		Invoke ("fireLogic", Random.Range(4, 10));
	}
	
}

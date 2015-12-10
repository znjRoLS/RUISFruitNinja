using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FruitCanon : MonoBehaviour {

	//public Rigidbody prefab;
	public GameObject[] prefabs;
	public GameObject target;
	public float angle = 45f;
	public ParticleSystem effect;

	private Vector3 target2;
	void Start () {
		//Invoke ("fire", Random.Range(1, 5));
	}

	void Update(){
		//vector3 target2 = target.transform.TransformPoint(Vector3.zero)- new Vector3 (0, target.transform.TransformPoint(Vector3.zero).y, 0);// - new Vector3 ((float)Random.Range (-50, 50) / 30, target.transform.TransformPoint(Vector3.zero).y, 0);


	}

	public void fire(){
		Vector3 startpos = transform.transform.TransformPoint (Vector3.zero);
		Vector3 endpos = target.transform.TransformPoint (Vector3.zero);
		//endpos += new Vector3 (0,- 0.5f,0);
		endpos += new Vector3 (((float)Random.Range (-100, 100))/100,((float) Random.Range (-50, 0))/100,0);
		Vector3 pos =endpos + new Vector3 (0, startpos.y - endpos.y, 0);
		transform.LookAt (pos); //(float)Random.Range (-50, 50) / 1500
		angle = 45;//Random.Range (30, 45);
		transform.Rotate (Vector3.right, -angle);
		float g = -Physics.gravity.y;

		//Vector3 start =  startpos- new Vector3 (0, startpos.y, 0);

		//Vector3 end =endpos  - new Vector3 (0,endpos.y, 0);
		//end += new Vector3 ((float)Random.Range (-150, 150) / 10, (float)Random.Range (0, 50) / 100, 0);
		float distance = Vector3.Distance (startpos, pos);
		float radians = angle * Mathf.PI / 180;
		float temp = 2 * (startpos.y - endpos.y) / g;
		temp += 2* Mathf.Tan (radians) * distance / g;
		temp = Mathf.Sqrt (temp);
		float speed = distance /( temp*Mathf.Cos (radians));


		int r = Random.Range (0, prefabs.Length);
		//effect.enableEmission = true;
		if (effect != null) {
			effect.Play ();
		}
		GameObject projectile = (GameObject)Instantiate (prefabs[r], transform.transform.TransformPoint(Vector3.zero), Quaternion.identity);
		//Physics.IgnoreCollision (t.GetComponent<Collider> (), projectile.GetComponent<Collider> ());

		projectile.GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		projectile.transform.GetChild(0).GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		//projectile.transform.GetChild(1).GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		//projectile.transform.GetChild(2).GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		Destroy (projectile, 10);

		//Invoke ("fire", Random.Range(2,8));
	}



}

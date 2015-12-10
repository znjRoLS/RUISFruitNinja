using UnityEngine;
using System.Collections;

public class NinjaCannonsAnimation : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
		Invoke ("walk", 1);//Random.Range(5, 15));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void walk(){
		Vector3 startpos = transform.transform.TransformPoint (Vector3.zero);
		Vector3 endpos = target.transform.TransformPoint (Vector3.zero);
		Vector3 pos =endpos + new Vector3 (0, startpos.y - endpos.y, 0);
		transform.LookAt (pos);
		transform.Rotate (Vector3.right, -90);
		transform.position += new Vector3 (0, 0, -0.1f);
		//projectile.GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);
		//projectile.transform.GetChild(0).GetComponent<Rigidbody> ().AddForce (transform.forward * speed, ForceMode.VelocityChange);

		
		Invoke ("walk", Random.Range(1f,2f));
	}
}

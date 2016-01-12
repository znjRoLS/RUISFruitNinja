using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public GameObject modelPrefab;
	private int hp = 2;
	private static string shurikenTag = "ShurikenTag";
	public Animator a;

	private Vector3 startPos;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		/*Debug.Log ("TAG TAG T " + other.gameObject.tag);
		Debug.Log ("TAG TAG T " + other.gameObject.tag);
		Debug.Log ("TAG TAG T " + other.gameObject.tag);*/
		if (other.gameObject.tag.Equals(shurikenTag)) {
			hp--;
			Debug.Log ("TAG TAG T " + hp);
			Debug.Log ("TAG TAG T " + hp);
			Debug.Log ("TAG TAG T " + hp);
			if (hp == 0) {
				a.SetInteger ("dieMethod", Random.Range (0, 4));
				FruitTriggerDestroy.MISSES += 2;
				Instantiate(modelPrefab, startPos, Quaternion.identity);
				foreach(Collider c in GetComponents<Collider>()){
					c.enabled = false;
				}
				Destroy(gameObject, 5);
			} else {
				a.SetTrigger ("wounded");
			}

		}
	}

	void OnCollisionEnter(Collision c){
		Debug.Log ("TAG TAG " + c.gameObject.tag);
		Debug.Log ("TAG TAG " + c.gameObject.tag);
		Debug.Log ("TAG TAG " + c.gameObject.tag);
		if (c.gameObject.tag.Equals(shurikenTag)) {
			hp--;
			if (hp == 0) {
				a.SetInteger ("dieMethod", Random.Range (0, 4));
				Instantiate(modelPrefab, startPos, Quaternion.identity);
			} else {
				a.SetTrigger ("wounded");
			}
		}
	}

}

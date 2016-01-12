using UnityEngine;
using System.Collections;

public class FruitTriggerDestroy : MonoBehaviour {
	
	private bool collided = false;
	private bool fruitCollided = false;
	public static int SCORE = 0;
	public static int MISSES = 0;
	public static int missesLimit = 20;

	public GameObject destroyAnimation;

	void Start(){
		Collider[] cols = GetComponents<Collider> ();
		foreach (Collider c in cols) {
			c.isTrigger = false;
		}

		Transform firstHalf = transform.parent.GetChild (1);
	}
	
	void OnCollisionEnter(Collision other){
		if (other.gameObject.layer == 14) {
			fruitCollided = true;
		}

		if (other.gameObject.tag.Equals ("PlayerCollider") && collided == false) {
			collided = true;

			SCORE += 100;
			FruitCanon.lowerAngle();
			Debug.Log("Score" + SCORE);


			GameObject parent = transform.parent.gameObject;

			//parent.GetComponent<ParticleSystem>().Play();

			//Debug.Log (parent.transform.position+ "  "+transform.position);

			GameObject child = parent.transform.GetChild (1).gameObject;
			//child.transform.position = transform.TransformPoint(Vector3.zero);
			//child.transform.rotation=parent.transform.rotation;

			//child.transform.parent = parent.transform;
			//child.transform.position = transform.position;
			child.AddComponent<MeshCollider> ().convex = true;
			child.SetActive (true);
			child.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (-50, -500), Random.Range (-50, 350), Random.Range (-100, 300)));
			Destroy (child, 10);

			child = parent.transform.GetChild (2).gameObject;

			//child.transform.position = transform.TransformPoint(Vector3.zero);
			//child.transform.position = transform.position;
			//child.transform.rotation=parent.transform.rotation;
			child.AddComponent<MeshCollider> ().convex = true;
			child.SetActive (true);
			child.GetComponent<Rigidbody> ().AddForce (new Vector3 (Random.Range (50, 500), Random.Range (-50, 350), Random.Range (-100, 300)));
			Destroy (child, 10);
			Destroy (gameObject);
			Destroy (parent, 11);
			/*GameObject one = (GameObject)Instantiate(half, transform.position + new Vector3(1,1,1), Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(300, 0, 300));
			Destroy(one, 3);

			one = (GameObject)Instantiate(half, transform.position, Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(-400, 400, -300));
			Destroy(one, 3);
			*/

			//Debug.Log ("anim start");
			GameObject animacija = Instantiate (destroyAnimation, other.gameObject.transform.position, other.gameObject.transform.rotation) as GameObject;



			Destroy (animacija, 3f);
		} else if (collided == false){
			collided = true;
			if (fruitCollided == false){
				Debug.Log("Misses " + MISSES);
				MISSES++;
				if (MISSES >= missesLimit){
					gameOver();
				}
			}
		}
	}

	public void gameOver(){
		Application.LoadLevel ("gameOverScene");
	}


}

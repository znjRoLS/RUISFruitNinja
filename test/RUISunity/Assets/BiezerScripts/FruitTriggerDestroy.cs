using UnityEngine;
using System.Collections;

public class FruitTriggerDestroy : MonoBehaviour {
	
	private bool collided = false;

	void Start(){
		Collider[] cols = GetComponents<Collider> ();
		foreach (Collider c in cols) {
			c.isTrigger = false;
		}

		Transform firstHalf = transform.parent.GetChild (1);

	}

	//treba da se odstiklira is trigger na collider-u i da se ovo promeni u OnCollisionEnter, 
	//da bi se omaseno voce zadrzavalo na sceni(to mozda i ne treba) 
	//takodje na polovinama treba da se namesti odgovarajuci kolajder i da se ostavi is trigger ne stiklirano
	//void OnTriggerEnter(Collider other){
	void OnCollisionEnter(Collision other){
		//Debug.Log (other.gameObject.tag);

		if (other.gameObject.tag.Equals("PlayerCollider") && collided == false){
			collided = true;

			GameObject parent = transform.parent.gameObject;

			//parent.GetComponent<ParticleSystem>().Play();

			Debug.Log (parent.transform.position+ "  "+transform.position);

			GameObject child = parent.transform.GetChild(1).gameObject;
			//child.transform.position = transform.TransformPoint(Vector3.zero);
			//child.transform.rotation=parent.transform.rotation;

			//child.transform.parent = parent.transform;
			//child.transform.position = transform.position;
			child.AddComponent<MeshCollider>().convex = true;
			child.SetActive(true);
			child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-50, -500), Random.Range(-50, 350), Random.Range(-100,300)));
			Destroy (child, 10);

			child = parent.transform.GetChild(2).gameObject;

			//child.transform.position = transform.TransformPoint(Vector3.zero);
			//child.transform.position = transform.position;
			//child.transform.rotation=parent.transform.rotation;
			child.AddComponent<MeshCollider>().convex = true;
			child.SetActive(true);
			child.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(50, 500), Random.Range(-50, 350), Random.Range(-100,300)));
			Destroy (child, 10);
			Destroy(gameObject);
			Destroy (parent, 11);
			/*GameObject one = (GameObject)Instantiate(half, transform.position + new Vector3(1,1,1), Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(300, 0, 300));
			Destroy(one, 3);

			one = (GameObject)Instantiate(half, transform.position, Quaternion.identity); 
			one.GetComponent<Rigidbody>().AddForce(new Vector3(-400, 400, -300));
			Destroy(one, 3);
			*/                                
		}
	}

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

	public GameObject modelPrefab;
	private int hp = 2;
	private static string shurikenTag = "ShurikenTag";
	public Animator a;
	public GameObject blood;
	public GameObject bloodPool;

	private Vector3 startPos;
	private PlayerHealth h;
	public GameObject limitObject;
	public GameObject ui;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		h = GameObject.Find ("Scripts").GetComponent<PlayerHealth> ();
		ui = GameObject.Find ("UI");
		limitObject = GameObject.Find ("NinjaLimitObject");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z < limitObject.transform.position.z) {
			gameOver();
		}
	}

	public void gameOver(){
		FruitTriggerDestroy.isGameOver = true;
		for (int i = 0; i < 3; i++) {
			ui.transform.GetChild(i).gameObject.SetActive(false);
		}
		for (int i = 3; i < 6; i++) {
			ui.transform.GetChild(i).gameObject.SetActive(true);
		}
		
		ui.transform.GetChild (5).GetComponent<Text> ().text = "Score : " + FruitTriggerDestroy.SCORE;

		//Application.LoadLevel (Application.loadedLevel);
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject e in enemies) {
			Destroy(e);
		}
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
				FruitTriggerDestroy.MISSES -= 2;
				h.setMisses(FruitTriggerDestroy.missesLimit - FruitTriggerDestroy.MISSES);
				Destroy(other.gameObject);
				//Instantiate(modelPrefab, startPos, Quaternion.identity);
				foreach(Collider c in GetComponents<Collider>()){
					c.enabled = false;
				}
				Destroy(gameObject, 5);
				Invoke ("newNinja", 4);
			} else {
				a.SetTrigger ("wounded");
				GameObject bloood = (GameObject)Instantiate(blood, other.transform.position, Quaternion.identity);
				bloood.transform.parent = this.gameObject.transform;
			}
		}
	}

	public void newNinja(){
		GameObject newNinja = (GameObject)Instantiate(modelPrefab, startPos, Quaternion.identity);
		foreach (Collider c in newNinja.GetComponents<Collider>()){
			c.enabled = true;
		}
	}

	void OnCollisionEnter(Collision other){
		Debug.Log ("TAG TAG COL" + other.gameObject.tag);
		Debug.Log ("TAG TAG COL" + other.gameObject.tag);
		Debug.Log ("TAG TAG COL" + other.gameObject.tag);

		if (other.gameObject.tag.Equals(shurikenTag)) {
			hp--;
			if (hp == 0) {
				a.SetInteger ("dieMethod", Random.Range (0, 4));
				FruitTriggerDestroy.MISSES -= 2;
				h.setMisses(FruitTriggerDestroy.missesLimit - FruitTriggerDestroy.MISSES);
				Invoke ("pool", 2);
				//GameObject blooooodpool = (GameObject)Instantiate(bloodPool, transform.position - 
				Destroy(other.gameObject);
				//Instantiate(modelPrefab, startPos, Quaternion.identity);
				foreach(Collider c in GetComponents<Collider>()){
					c.enabled = false;
				}
				Destroy(gameObject, 5);
				Invoke ("newNinja", 4);
			} else {
				a.SetTrigger ("wounded");
				ContactPoint cPoint = other.contacts[0];	
				GameObject bloood = (GameObject)Instantiate(blood, cPoint.point, Quaternion.identity);
				bloood.transform.parent = this.gameObject.transform;
			}
			
		}
	}

	public void pool(){
		Vector3 pos = new Vector3 (transform.position.x, -0.35f, transform.position.z);
		GameObject blooooodpool = (GameObject)Instantiate (bloodPool, pos, Quaternion.identity);

	}

}

using UnityEngine;
using System.Collections;

public class ParticleDestroy : MonoBehaviour {

	private ParticleSystem p;

	// Use this for initialization
	void Start () {
		p = GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (p) {
			if (!p.IsAlive ()) {
				Destroy (p);
			}
		}
	}
}

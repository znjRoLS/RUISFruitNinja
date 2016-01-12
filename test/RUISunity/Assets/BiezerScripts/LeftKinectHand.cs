using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;

public class LeftKinectHand : MonoBehaviour {

	public GameObject leftHand;
	public GameObject pathsColliders;
	
	private int frameRate = 3;
	
	private static int count = 4;
	private List<Vector3> vectors;
	
	public int br=0;
	// Use this for initialization
	void Start () {
		
	/*	LineRenderer lineRenderer = pathsColliders.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(Color.gray, Color.gray);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(40);*/
		vectors = new List<Vector3> ();
		
	}
	
	void Update () {
		count --;
		if (count == 0) {
			count = frameRate;
			
			vectors.Add (leftHand.transform.TransformPoint (Vector3.zero));
			if (vectors.Count > 10) {
				vectors.RemoveAt (0);
			}
			if (vectors.Count > 3) {
				Koeficijenti.followPath (vectors.ToArray (), true, pathsColliders);
			}
		}
	}
	
}

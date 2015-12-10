﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using VesnaSanja;

using LockingPolicy = Thalmic.Myo.LockingPolicy;
using Pose = Thalmic.Myo.Pose;
using UnlockType = Thalmic.Myo.UnlockType;
using VibrationType = Thalmic.Myo.VibrationType;

public class RightKinectHand : MonoBehaviour {
	
	public GameObject myo = null;
	private ThalmicMyo tMyo;
	
	//private static Pose lastPose = Pose.Unknown;

	public GameObject shuriken;
	public GameObject hand;
	public GameObject foreArm;
	public GameObject upperArm;
	public GameObject rightHandIndex1;

	public GameObject leftShoulder;

	public GameObject pathsColliders;
	
	private const int numOfDots = 6;
	private int frameRate = 4;
	private const float distanceLimit = 0.05f;

	//private const float distanceLimit1 = 0.5f;
	
	private static int count = numOfDots;
	private List<Vector3> vectors;
	private bool thrown = false;
	private float speed=20f;
	
	void Start () {
		
		/*LineRenderer lineRenderer = pathsColliders.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(Color.gray, Color.gray);
		lineRenderer.SetWidth(0.2F, 0.2F);
		lineRenderer.SetVertexCount(40);*/
		
		tMyo = myo.GetComponent<ThalmicMyo> ();
		vectors = new List<Vector3> ();
	}
	
	void Update () {
		count --;
		if (count == 0) {
			count = frameRate;
			vectors.Add(hand.transform.TransformPoint(Vector3.zero));
			if (vectors.Count > 10)
				vectors.RemoveAt(0);
		}
		/*if (tMyo.pose == Pose.Fist) {
			thrown = true;
			Debug.Log(thrown);
			//pathsColliders.SetActive(false);
			*/
		float dis1=Vector3.Distance(rightHandIndex1.transform.TransformPoint(Vector3.zero), leftShoulder.transform.TransformPoint(Vector3.zero));

		if (dis1 < 2*distanceLimit) 
		{
			thrown = true;
			Debug.Log(thrown);
		}


		else{
			if(thrown)
			{
				float dis=Vector3.Distance(hand.transform.TransformPoint(Vector3.zero), foreArm.transform.TransformPoint(Vector3.zero));
				dis+=Vector3.Distance(upperArm.transform.TransformPoint(Vector3.zero), foreArm.transform.TransformPoint(Vector3.zero));
				dis-=Vector3.Distance(hand.transform.TransformPoint(Vector3.zero), upperArm.transform.TransformPoint(Vector3.zero));
				Debug.Log (dis);
				if(dis<distanceLimit) 
				{
					thrown=false;
					float angle=0f;
					if(vectors.Count>4)
					{
						Vector3 pom= new Vector3(0,0,0);
						for (int i=5; i<vectors.Count; i++)
							pom+=vectors[vectors.Count-i];
						pom/=vectors.Count-5;
						Debug.Log ("pom "+pom+ " saka "+vectors[vectors.Count-1]);
						Vector3 a=vectors[vectors.Count-1]-pom;
						Debug.Log (a);
						angle=Mathf.Atan2(a.y, a.x);
						angle*=180/Mathf.PI;
						Debug.Log ("posle "+angle);
					}
					else
						Debug.Log ("nema");

					Debug.Log ("SHURIKEN");
					tangent(shuriken,upperArm.transform.TransformPoint(Vector3.zero), hand.transform.TransformPoint(Vector3.zero), speed, angle);
					vectors.Clear();
					//pathsColliders.SetActive(true);
				}
				
			}
			else
			{
				if (vectors.Count > 3)
					Koeficijenti.followPath(vectors.ToArray(), true, pathsColliders);
			}
		}
	}
	
	public static void tangent(GameObject shuriken, Vector3 pointOne, Vector3 pointTwo, float speed, float angle){
		GameObject shur = (GameObject)Instantiate (shuriken, pointTwo, Quaternion.identity);
		shur.GetComponent<Rigidbody>().velocity = (pointTwo - pointOne).normalized * speed;
		shur.transform.LookAt (pointOne);
		
		shur.transform.Rotate(new Vector3(0, 0, angle));
		shur.GetComponent<Rigidbody> ().AddTorque (shur.transform.up * 1000f);
		
		Destroy (shur, 5);
	}
}
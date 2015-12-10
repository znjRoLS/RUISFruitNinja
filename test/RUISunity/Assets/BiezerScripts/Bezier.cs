using UnityEngine;

namespace VesnaSanja{
	
	[System.Serializable]
	public class Bezier
	{
		
		public Vector3 p0;
		public Vector3 p1;
		public Vector3 p2;
		public Vector3 p3;
		
		public float length=0;
		
		public Vector3[] points;
		private float speed = 5f;
		private bool mode;
		
		private Transform[] colliders;

		public Bezier( Vector3 v0, Vector3 v1, Vector3 v2, Vector3 v3, bool mode, GameObject pathsColls, int _calculatePoints=0)
		{
			//LineRenderer lineRenderer = pathsColls.GetComponent<LineRenderer>();
			this.p0 = v0;
			this.p1 = v1;
			this.p2 = v2;
			this.p3 = v3;
			this.mode = mode;
			if (pathsColls != null) {
				colliders = new Transform[pathsColls.transform.childCount];
				for (int i = 0; i < colliders.Length; i++) {
					colliders [i] = pathsColls.transform.GetChild (i);
				}
			}
			
			if(_calculatePoints>0) CalculatePoints(_calculatePoints);
		}
	
		public Vector3 GetPointAtTime( float t )
		{
			float u = 1f - t;
			float tt = t * t;
			float uu = u * u;
			float uuu = uu * u;
			float ttt = tt * t;
			
			Vector3 p = uuu * p0; //first term
			p += 3 * uu * t * p1; //second term
			p += 3 * u * tt * p2; //third term
			p += ttt * p3; //fourth term
			
			return p;
			
		}
		
		//where _num is the desired output of points and _precision is how good we want matching to be
		public void CalculatePoints(int _num, int _precision=200)
		{
			if(_num>_precision) Debug.LogError("_num must be less than _precision");
			
			//calculate the length using _precision to give a rough estimate, save lengths in array
			length=0;
			//store the lengths between PointsAtTime in an array
			float[] arcLengths = new float[_precision];
			
			Vector3 oldPoint = GetPointAtTime(0);
			
			for(int p=1;p<arcLengths.Length;p++)
			{
				Vector3 newPoint = GetPointAtTime((float) p/_precision); //get next point
				arcLengths[p] = Vector3.Distance(oldPoint,newPoint); //find distance to old point
				length += arcLengths[p]; //add it to the bezier's length
				oldPoint = newPoint; //new is old for next loop
			}
			//create our points array
			points = new Vector3[_num];
			//target length for spacing
			float segmentLength = length/_num;	
			//arc index is where we got up to in the array to avoid the Shlemiel error http://www.joelonsoftware.com/articles/fog0000000319.html
			int arcIndex = 0;
			
			float walkLength=0; //how far along the path we've walked
			oldPoint = GetPointAtTime(0);
			//iterate through points and set them
			
			//following hand, with no fist
			int j = 0;
			for (int i=0; i<points.Length; i++) {
				float iSegLength = i * segmentLength; //what the total length of the walkLength must equal to be valid
				//run through the arcLengths until past it
				while (walkLength<iSegLength) {
					walkLength += arcLengths [arcIndex]; //add the next arcLength to the walk
					arcIndex++; //go to next arcLength
				}
				//walkLength has exceeded target, so lets find where between 0 and 1 it is

				points [i] = GetPointAtTime ((float)arcIndex / arcLengths.Length);
				/*if(points[i]==null || points[i].x==float.NaN || points[i].y==float.NaN ||points[i].z==float.NaN )
					points[i]=new Vector3(100,100,100);
				if(points[i]==null || points[i].x>100 ||points[i].x<-100 || points[i].y>100 ||points[i].y<-100 || points[i].z>100 ||points[i].z<-100 )
					points[i]=new Vector3(100,100,100);*/
				if (i % 5 == 0 ){
				if(points[i]!=null && points[i].x<101 && points[i].x>-101 && points[i].y<101 && points[i].y>-101 && points[i].z<101 &&points[i].z>-100) {	
					//Debug.Log(points[i]);
					colliders[j].gameObject.GetComponent<Renderer> ().material.color = Color.red;	
					colliders[j].position = points[i];
						if(j!=0)
							colliders[j-1].LookAt(colliders[j].transform.TransformPoint (Vector3.zero));
					j++;
				}
				else{
					points[i]=new Vector3(100,100,100);	
					colliders[j].position = points[i];
					j++;
				}
				}
			}
			colliders[39].LookAt(2*colliders[39].transform.TransformPoint (Vector3.zero) - colliders[38].transform.TransformPoint (Vector3.zero));
			
		}
		
	}
}
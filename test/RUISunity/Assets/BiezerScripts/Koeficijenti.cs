using UnityEngine;
using System.Collections;

namespace VesnaSanja{

public class Koeficijenti : MonoBehaviour {
	public static Vector3 p0;
	public static Vector3 p1;
	public static Vector3 p2;

	public static Vector3[] c0, c1, c2;
	public static Matrix C,T,M;
	public static MatrixVector P, N;
	public static Vector3[] Npoints;

	void Update () {
	}

	public static void findNpoints(Vector3[] points)
	{
		float[] duzineSegmenata= new float[points.Length-1];
		float length=0f;
        int		i = 0;
		Vector3  oldPoint = points [i];
		
		for (i=1; i<points.Length; i++) 
		{
			Vector3 newPoint = points[i];
			duzineSegmenata[i-1]=Vector3.Distance(oldPoint,newPoint); 
			int s=i-1;
			length+=duzineSegmenata[i-1];
			oldPoint=newPoint;
		}
		
		float segment = length / 10; //duzina desetog dela linije
		
		Npoints [0] = points [0];
		Npoints [10] = points [points.Length - 1];
		
		int k = 0;
		
		for (int j=1; j< 10; j++) 
		{
			float seg=segment;
			while (seg > duzineSegmenata[k]) 
			{
				seg-=duzineSegmenata[k];
				k++;
			}
			
			float odnos=seg/duzineSegmenata[k];

			Vector3 A=points[k];
			Vector3 B=points[k+1];
			Vector3 AB = B - A;
			//AB.Normalize();

			Npoints[j]=A + odnos * AB;

			duzineSegmenata[k]-=seg;
			points[k]=Npoints[j];
			
			if( odnos == 1) k++;
		}		
	}

	public static MatrixVector convertToMatrix(Vector3[] points){

		MatrixVector m = new MatrixVector (points.Length, 1);
		for (int i=0; i<points.Length; i++)
			m.setValueAt (i, 0, points [i]);
		return m;
	}

	public static Vector3 followPath(Vector3[] vectors, bool mode, GameObject pathsColls){
		
		Npoints= new Vector3[11];

		C = new Matrix (11, 4);
		float k = 0f;
		for(int i=0;i<=10;i++)
		{
			C.setValueAt(i,0,Mathf.Pow(1-k,3));
			C.setValueAt(i,1,3*k*(1-k)*(1-k));
			C.setValueAt(i,2,3*k*k*(1-k));
			C.setValueAt(i,3, k*k*k);
			
			k+=0.1f;
		}
		
		T = Matrix.transpose (C);
		M = Matrix.multiply(Matrix.inverse(Matrix.multiply (T,C)),T);
		
		findNpoints(vectors);
		N = convertToMatrix (Npoints);
		
		P = MatrixVector.multiplyByMatrix (M, N);
		
		new Bezier (P.getValueAt (3, 0), P.getValueAt (2, 0), P.getValueAt (1, 0), P.getValueAt (0, 0), mode, pathsColls, 200);
		
		return P.getValueAt (2, 0);
	}


}

}
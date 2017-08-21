using UnityEngine;
using System.Collections;

public class CubeMeshDeformation : MonoBehaviour {


	//CUBE

	public bool Editable;

	public GameObject point1;

	public GameObject point2;

	public GameObject point3;

	public GameObject point4;

	public GameObject point5;
	
	public GameObject point6;
	
	public GameObject point7;
	
	public GameObject point8;

	public Material MyTextureMaterial;

	void Start () 
	{
		if(transform.gameObject.GetComponent<MeshCollider>())
		{
		transform.gameObject.GetComponent<MeshCollider> ().convex = true;
		}

		if(!Editable)
		{

			//MODE 1 = classic generated mesh (easiest) Static Cube

			Vector3[] Verticles = new Vector3[] 
			{
				//6 sides
				//back side
				new Vector3(0,0,0),new Vector3(1,0,0),new Vector3(0,1,0), new Vector3(1,1,0),
				//right side
				new Vector3(1,0,0),new Vector3(1,0,1),new Vector3(1,1,0), new Vector3(1,1,1),
				//forward side
				new Vector3(1,0,1),new Vector3(0,0,1),new Vector3(1,1,1), new Vector3(0,1,1),
				//left side
				new Vector3(0,0,1),new Vector3(0,0,0),new Vector3(0,1,1), new Vector3(0,1,0),
				//up side
				new Vector3(0,1,0),new Vector3(1,1,0),new Vector3(0,1,1), new Vector3(1,1,1),
				//down side
				new Vector3(0,0,0),new Vector3(1,0,0),new Vector3(0,0,1), new Vector3(1,0,1),

			};
			
			int[] Triangles = new int[] 
			{
				0,3,1,
				3,0,2,

				4,7,5,
				7,4,6,

				8,11,9,
				11,8,10,

				12,15,13,
				15,12,14,

				16,19,17,
				19,16,18,

				21,22,20,
				22,21,23,
			};



			/*
			int[] Triangles = new int[36] ;

			Triangles[0] = 0;
			Triangles[1] = 3;
			Triangles[2] = 1;

			Triangles[3] = 3;
			Triangles[4] = 0;
			Triangles[5] = 2;

            Triangles[6] = 4;
			Triangles[7] = 7;
			Triangles[8] = 5;

			Triangles[9] = 7;
			Triangles[10] = 4;
			Triangles[11] = 6;

            Triangles[12] = 8;
			Triangles[13] = 11;
			Triangles[14] = 9;

			Triangles[15] = 11;
			Triangles[16] = 8;
			Triangles[17] = 10;

            Triangles[18] = 12;
			Triangles[19] = 15;
			Triangles[20] = 13;

			Triangles[21] = 15;
			Triangles[22] = 12;
			Triangles[23] = 14;

            Triangles[24] = 16;
			Triangles[25] = 19;
			Triangles[26] = 17;

			Triangles[27] = 19;
			Triangles[28] = 16;
			Triangles[29] = 18;

            Triangles[30] = 21;
			Triangles[31] = 22;
			Triangles[32] = 20;

            Triangles[33] = 22;
			Triangles[34] = 21;
			Triangles[35] = 23;
			*/



			Vector2[] UV = new Vector2[] 
			{
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),

				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),

				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),

				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),

				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),

				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
			};
			
			Vector3[] Normals = new Vector3[]
			{
				Vector3.back,
				Vector3.back,
				Vector3.back,
				Vector3.back,
				
				Vector3.right,
				Vector3.right,
				Vector3.right,
				Vector3.right,
				
				Vector3.forward,
				Vector3.forward,
				Vector3.forward,
				Vector3.forward,
				
				Vector3.left,
				Vector3.left,
				Vector3.left,
				Vector3.left,
				
				Vector3.up,
				Vector3.up,
				Vector3.up,
				Vector3.up,
				
				Vector3.down,
				Vector3.down,
				Vector3.down,
				Vector3.down,
			};
			
			if(!transform.GetComponent<MeshFilter>())
			{
				transform.gameObject.AddComponent<MeshFilter>();
			}
			if(!transform.GetComponent<MeshRenderer>())
			{
				transform.gameObject.AddComponent<MeshRenderer>();
			}
			
			Mesh myMesh = new Mesh();
			
			myMesh.vertices = Verticles;
			myMesh.triangles = Triangles;
			myMesh.normals = Normals;
			myMesh.uv = UV;
			;

			transform.GetComponent<MeshFilter>().mesh = myMesh;

			if(transform.GetComponent<MeshCollider>())
			{
			transform.GetComponent<MeshCollider>().sharedMesh = myMesh;
			}

			transform.GetComponent<Renderer>().material = MyTextureMaterial;
		}
	}




















	void Update () 
	{
	

		//MODE 2 = Editable Cube

		if(Editable)
		{
	        Vector3[] Verticles = new Vector3[] 
			{
				//6 Sides
				//Back Side
				point1.transform.position,point2.transform.position,point3.transform.position,point4.transform.position,
				//Right Side
				point2.transform.position,point6.transform.position,point4.transform.position,point8.transform.position,
				//Front Side
				point5.transform.position,point6.transform.position,point7.transform.position,point8.transform.position,
				//Left Side
				point5.transform.position,point1.transform.position,point7.transform.position,point3.transform.position,
				//Up Side
				point3.transform.position,point4.transform.position,point7.transform.position,point8.transform.position,
				//Down Side
				point1.transform.position,point2.transform.position,point5.transform.position,point6.transform.position,
			};


			int[] Triangles = new int[] 
			{
				2,1,0,
				1,2,3,
				
				4,7,5,
				7,4,6,
				
				9,10,8,
				10,9,11,
				
				12,15,13,
				15,12,14,
				
				16,19,17,
				19,16,18,
				
				21,22,20,
				22,21,23, 
			};


			/*
			int[] Triangles = new int[36] ;
			
			Triangles[0] = 0;
			Triangles[1] = 3;
			Triangles[2] = 1;
			
			Triangles[3] = 3;
			Triangles[4] = 0;
			Triangles[5] = 2;
			
			Triangles[6] = 4;
			Triangles[7] = 7;
			Triangles[8] = 5;
			
			Triangles[9] = 7;
			Triangles[10] = 4;
			Triangles[11] = 6;
			
			Triangles[12] = 8;
			Triangles[13] = 11;
			Triangles[14] = 9;
			
			Triangles[15] = 11;
			Triangles[16] = 8;
			Triangles[17] = 10;
			
			Triangles[18] = 12;
			Triangles[19] = 15;
			Triangles[20] = 13;
			
			Triangles[21] = 15;
			Triangles[22] = 12;
			Triangles[23] = 14;
			
			Triangles[24] = 16;
			Triangles[25] = 19;
			Triangles[26] = 17;
			
			Triangles[27] = 19;
			Triangles[28] = 16;
			Triangles[29] = 18;
			
			Triangles[30] = 21;
			Triangles[31] = 22;
			Triangles[32] = 20;
			
			Triangles[33] = 22;
			Triangles[34] = 21;
			Triangles[35] = 23;
			*/



			Vector2[] UV = new Vector2[] 
			{
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
				
				new Vector2(0,0),
				new Vector2(1,0),
				new Vector2(0,1),
				new Vector2(1,1),
			};

			Vector3[] Normals = new Vector3[]
			{
				Vector3.back,
				Vector3.back,
				Vector3.back,
				Vector3.back,
				
				Vector3.right,
				Vector3.right,
				Vector3.right,
				Vector3.right,
				
				Vector3.forward,
				Vector3.forward,
				Vector3.forward,
				Vector3.forward,
				
				Vector3.left,
				Vector3.left,
				Vector3.left,
				Vector3.left,
				
				Vector3.up,
				Vector3.up,
				Vector3.up,
				Vector3.up,
				
				Vector3.down,
				Vector3.down,
				Vector3.down,
				Vector3.down,
			};

			if(!transform.GetComponent<MeshFilter>())
			{
				transform.gameObject.AddComponent<MeshFilter>();
			}
			if(!transform.GetComponent<MeshRenderer>())
			{
				transform.gameObject.AddComponent<MeshRenderer>();
			}

			Mesh myMesh = new Mesh();

			myMesh.vertices = Verticles;
			myMesh.triangles = Triangles;
			myMesh.normals = Normals;
			myMesh.uv = UV;
			;
			
			transform.GetComponent<MeshFilter>().mesh = myMesh;

			if(transform.GetComponent<MeshCollider>())
			{
			transform.GetComponent<MeshCollider>().sharedMesh = myMesh;
			}
			
			transform.GetComponent<Renderer>().material = MyTextureMaterial;

		}

	}
}


//FULL TUTORIAL YOU CAN FIND HERE: https://www.youtube.com/watch?v=c-pqEHR1jnw
//Tutorial by matt... Thanks!













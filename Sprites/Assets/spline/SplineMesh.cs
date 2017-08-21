using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SplineMesh : MonoBehaviour {

	public BezierSpline spline;

	public MeshFilter meshFilter;

	public float width = 2;
	public float complexity = 20;

	[Range(0,1)]
	public float directionChange = 0.95f;

	public bool createVertsEveryStep = false;

	private List<Vector3> splinePos = new List<Vector3>();

	// Use this for initialization
	void Start() {
		meshFilter = GetComponent<MeshFilter>();

		SetList();

		GenMesh();
	}

	// Update is called once per frame
	void Update() {
		if (createVertsEveryStep) {
			if (spline == null || spline.points.Length == 0) {
				return;
			}
			if (splinePos.Count == 0) {
				SetList();
				return;
			}

			if (spline.points.Length != splinePos.Count) {
				GenMesh();
				SetList();
			}
			else {

				for (int i = 0; i < splinePos.Count; i++) {
					if (splinePos[i] != spline.points[i]) {
						GenMesh();
						SetList();
						break;
					}
				}
			}
		}
		else {
			GenMesh();
		}
	}

	private void SetList() {
		splinePos.Clear();

		splinePos.AddRange(spline.points);

		//for (int i = 0; i < spline.points.Length; i++) {
		//	splinePos.Add(spline.points[i]);
		//}
	}
	private void GenMesh() {
		Mesh mesh = new Mesh();
		mesh.name = "SPLINE MESH";
		meshFilter.mesh = mesh;

		List<Vector3> verts = new List<Vector3>();
		List<int> trianges = new List<int>();
		List<Vector2> uvs = new List<Vector2>();

		float increment = 1/ complexity;
		if(increment <= 0) {
			increment = 1.0f;
		}

		Vector3 start;
		Vector3 end;
		Vector3 vecWidth;
		Vector3 offset;
		Vector3 lastDir;
		Vector3 next;
		Vector3 curr;
		//Vector3 mid;
		Vector3 dir;
		//Vector3 pos;

		start = spline.GetPoint(0);
		next = spline.GetPoint(increment);
		end = spline.GetPoint(1);
		dir = spline.GetDirection(0);
		//print(dir);
		vecWidth = new Vector3(0,0,width);
		lastDir = (next - start).normalized;
		curr = start;
		//mid = (start + end) / 2;
		//pos = transform.position;

		//start
		//verts.Add(start + vecWidth);
		//verts.Add(start + -vecWidth);
		//uvs.Add(new Vector2(0, 0));
		//uvs.Add(new Vector2(1, 0));

		for (float i = 0; i < 1; i += increment) {
			bool createVert = false;

			if (createVertsEveryStep) {
				createVert = true;
			}
			else { 
				createVert = (Vector3.Dot(spline.GetDirection(i), lastDir) < directionChange || i == 0);
			}

			if (createVert) {
				lastDir = (next - curr).normalized;
				//print(i);

					offset = -Vector3.Cross(new Vector3(0, 1, 0), spline.GetDirection(i)).normalized * width;
					offset = new Vector3(offset.x, offset.y, Mathf.Abs(offset.z));

					//print(offset);


					uvs.Add(new Vector2(0, i));
					uvs.Add(new Vector2(1, i));
					verts.Add(curr + offset);
					verts.Add(curr + -offset);
				
			}

			curr = next;
			next = spline.GetPoint(i + increment);
		}


		//for (float i = 0.1f; i <= 1; i += 0.1f) {
		//	if(verts.Count % 4 == 0) {
		//		int num = verts.Count;
		//		trianges.Add(num - 3);
		//		trianges.Add(num - 1);
		//		trianges.Add(num - 2);
		//		trianges.Add(num - 1);
		//		trianges.Add(num - 0);
		//		trianges.Add(num - 2);
		//	}
		//	uvs.Add(new Vector2(i, 0));
		//	uvs.Add(new Vector2(0, i));
		//	end = spline.GetPoint(i);
		//	verts.Add(end + vecWidth);
		//	verts.Add(end + -vecWidth);
		//}

		//end
		offset = -Vector3.Cross(new Vector3(0, 1, 0), spline.GetDirection(1)).normalized * width;
		verts.Add(end + offset);
		verts.Add(end + -offset);
		uvs.Add(new Vector2(0, 1));
		uvs.Add(new Vector2(1, 1));

		for(int i = 0; i <= verts.Count-4; i+=2) {
		trianges.Add(i+0);
		trianges.Add(i+2);
		trianges.Add(i+1);
		trianges.Add(i+2);
		trianges.Add(i+3);
		trianges.Add(i+1);
		}

		//print(verts.Count);
		//print(trianges.Count);
		//print(uvs.Count);

		mesh.vertices = verts.ToArray();
		mesh.triangles = trianges.ToArray();
		mesh.uv = uvs.ToArray();


		mesh.RecalculateNormals();

	}

}

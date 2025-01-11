using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleConer : MonoBehaviour
{
	public CapsuleCollider2D shapeGivingCollider;
	private MeshFilter myMeshFilter;

	// Start is called before the first frame update
	void Start()
	{
		myMeshFilter = GetComponent<MeshFilter>();
		myMeshFilter.mesh = ConifyCollider(shapeGivingCollider, 1.0f, 20);
	}

	Mesh ConifyCollider(CapsuleCollider2D capsuleCollider, float cone_height, int corner_resolution)
	{
		corner_resolution = Mathf.Max(corner_resolution, 0);
		CapsuleDirection2D dir = capsuleCollider.direction;
		Vector2 size = capsuleCollider.size;

		float width = size.x;
		float height = size.y;

		if (dir == CapsuleDirection2D.Vertical)
		{
			width = size.y;
			height = size.x;
		}

		float inner_width = Mathf.Max(0, width - height);
		float radius = height / 2.0f;

		// generate vertices
		List<Vector3> vertexList = new List<Vector3>();
		List<int> triangleIndices = new List<int>();
		vertexList.Add(new Vector3(inner_width / 2.0f, 0.0f, -cone_height));
		vertexList.Add(new Vector3(inner_width / 2.0f, -radius, 0.0f));
		Vector3 centerRight = new Vector3(inner_width / 2.0f, 0.0f, 0.0f);
		for (int i = 0; i < corner_resolution; i++)
		{
			float curr_angle = Mathf.PI * ((float)(i + 1) / (float)(corner_resolution + 1));
			Vector3 curr_dir = new Vector3(Mathf.Cos(-Mathf.PI / 2.0f + curr_angle), Mathf.Sin(-Mathf.PI / 2.0f + curr_angle), 0.0f);
			vertexList.Add(centerRight + radius * curr_dir);
			triangleIndices.Add(0);
			triangleIndices.Add(vertexList.Count - 1);
			triangleIndices.Add(vertexList.Count - 2);
		}
		vertexList.Add(new Vector3(inner_width / 2.0f, radius, 0.0f));
		int upper_right_id = vertexList.Count - 1;
		triangleIndices.Add(0);
		triangleIndices.Add(vertexList.Count - 1);
		triangleIndices.Add(vertexList.Count - 2);
		vertexList.Add(new Vector3(-inner_width / 2.0f, 0.0f, -cone_height));
		int left_cone_id = vertexList.Count - 1;
		vertexList.Add(new Vector3(-inner_width / 2.0f, radius, 0.0f));
		int upper_left_id = vertexList.Count - 1;
		// top cone side
		triangleIndices.Add(0);
		triangleIndices.Add(upper_left_id);
		triangleIndices.Add(upper_right_id);
		triangleIndices.Add(left_cone_id);
		triangleIndices.Add(upper_left_id);
		triangleIndices.Add(0);

		Vector3 centerLeft = new Vector3(-inner_width / 2.0f, 0.0f, 0.0f);
		for (int i = 0; i < corner_resolution; i++)
		{
			float curr_angle = Mathf.PI * ((float)(i + 1) / (float)(corner_resolution + 1));
			Vector3 curr_dir = new Vector3(Mathf.Cos(Mathf.PI / 2.0f + curr_angle), Mathf.Sin(Mathf.PI / 2.0f + curr_angle), 0.0f);
			vertexList.Add(centerLeft + radius * curr_dir);
			triangleIndices.Add(left_cone_id);
			triangleIndices.Add(vertexList.Count - 1);
			triangleIndices.Add(vertexList.Count - 2);
		}

		vertexList.Add(new Vector3(-inner_width / 2.0f, -radius, 0.0f));
		int lower_left_id = vertexList.Count - 1;
		triangleIndices.Add(left_cone_id);
		triangleIndices.Add(vertexList.Count - 1);
		triangleIndices.Add(vertexList.Count - 2);

		// bottom cone side
		triangleIndices.Add(left_cone_id);
		triangleIndices.Add(1);
		triangleIndices.Add(lower_left_id);
		triangleIndices.Add(1);
		triangleIndices.Add(left_cone_id);
		triangleIndices.Add(0);

		if (dir == CapsuleDirection2D.Vertical)
		{
			for (int i = 0; i < vertexList.Count; i++)
			{
				vertexList[i] = new Vector3(vertexList[i].y, vertexList[i].x, vertexList[i].z);
			}
			for (int i = 1; i < triangleIndices.Count; i += 3)
			{
				int tmp = triangleIndices[i];
				triangleIndices[i] = triangleIndices[i + 1];
				triangleIndices[i + 1] = tmp;
			}
		}

		Mesh resultMesh = new Mesh();
		resultMesh.vertices = vertexList.ToArray();
		resultMesh.triangles = triangleIndices.ToArray();
		return resultMesh;
	}
}

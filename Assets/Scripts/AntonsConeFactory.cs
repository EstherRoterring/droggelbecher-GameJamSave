using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntonsConeFactory : MonoBehaviour
{
    public PolygonCollider2D shapeGivingCollider;
    private MeshFilter myMeshFilter;

    // Start is called before the first frame update
    void Start()
    {
        myMeshFilter = GetComponent<MeshFilter>();

        myMeshFilter.mesh = ConifyCollider(shapeGivingCollider, 1.0f);
    }

    Mesh ConifyCollider(PolygonCollider2D collider, float coneHeight)
    {
        // compute cog
        Vector2[] collider_points_ccw = collider.GetPath(0);
        int l = collider_points_ccw.Length;

        Vector2 cog = Vector2.zero;
        foreach (Vector2 vector in collider_points_ccw)
        {
            cog += vector;
        }
        cog /= collider_points_ccw.Length;

        Vector3[] coordinates = new Vector3[l + 1];

        for (int i = 0; i < l; i++)
        {
			Vector2 p_2d = collider_points_ccw[i];
            coordinates[i] = new Vector3(p_2d.x, p_2d.y, 0);
        }
        coordinates[l] = new Vector3(cog.x, cog.y, -coneHeight);

        // build triangles
        List<int> triangleIndices = new List<int>();
		for (int i = 0; i < l; i++)
        {
            triangleIndices.Add(l);
			triangleIndices.Add((i + 1) % l);
			triangleIndices.Add(i);
		}

		Mesh resultMesh = new Mesh();

        resultMesh.vertices = coordinates;
        resultMesh.triangles = triangleIndices.ToArray();

        return resultMesh;
    }

}

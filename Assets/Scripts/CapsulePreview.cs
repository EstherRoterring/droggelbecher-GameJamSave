using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePreview : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider2;

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0, 0, 0.5f);
		Gizmos.DrawCube(transform.position, new Vector3(capsuleCollider2.size.x, capsuleCollider2.size.y, 1));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePreview : MonoBehaviour
{
    public CapsuleCollider2D capsuleCollider2;

	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(0, 1, 0, 0.8f);
		Gizmos.DrawCube(transform.position, new Vector3(transform.localScale.x * capsuleCollider2.size.x, transform.localScale.y * capsuleCollider2.size.y, 1));
	}
}

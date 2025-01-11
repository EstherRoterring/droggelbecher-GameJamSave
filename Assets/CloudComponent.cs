using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class CloudComponent : MonoBehaviour
{
    public Vector3 speed;

    private Vector3 startPositionRelativeToParent;
    private bool visibleAtLeastOnce = false;

    public void Start()
    {
        startPositionRelativeToParent = this.transform.localPosition;
    }

    public void Update()
    {
        this.transform.localPosition += speed * Time.deltaTime;
        if (!this.GetComponent<SpriteRenderer>().isVisible)
        {
            if (visibleAtLeastOnce) {
                visibleAtLeastOnce = false;
                // if they are completely out of view, then we reset it
                this.transform.localPosition = startPositionRelativeToParent - new Vector3(2 * GetComponent<SpriteRenderer>().sprite.bounds.extents.x, 0, 0);
            }
        }
        else
        {
            visibleAtLeastOnce = true;
        }
    }
}

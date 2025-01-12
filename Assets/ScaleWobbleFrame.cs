using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWobbleFrame : MonoBehaviour
{
    public float global_scale;
    public float anim_time = 1.0f;
    public float min_scale = 1.0f;
    public float max_scale = 10.0f;
    public float atom_base_scale_min = 2.0f;
    public float atom_base_scale_max = 3.0f;

    public float animation_timer = 0.0f;

    private List<Transform> myChildren;
    private List<float> myChildrenBaseScales;

    public bool opening = false;
    public bool closing = false;

	private void Awake()
	{
        animation_timer = 0.0f;
        opening = false;
        closing = false;
    }

	// Start is called before the first frame update
	void Start()
    {
        //DontDestroyOnLoad(this);
        myChildren = new List<Transform>();
        myChildrenBaseScales = new List<float>();
		foreach (Transform child in transform)
        {
            myChildren.Add(child);
            myChildrenBaseScales.Add(Random.Range(atom_base_scale_min, atom_base_scale_max));
            //Debug.Log("added children");
        }
		PlayOpeningAnimation();
        DontDestroyOnLoad(this);
    }

    public void PlayOpeningAnimation()
    {
        animation_timer = anim_time;
        opening = true;
        closing = false;
    }

    public void PlayClosingAnimation()
    {
        animation_timer = 0.0f;
        closing = true;
        opening = false;
    }

    public bool FrameClosingOrClosed()
    {
        return animation_timer >= 0;  
    }

    // Update is called once per frame
    void Update()
    {
        //if (!opening && !closing)
        //{
        //    if (animation_timer == 0.0f) PlayOpeningAnimation();
        //    if (animation_timer == anim_time) PlayClosingAnimation();
        //}
        if (closing)
        {
            animation_timer += Time.deltaTime;
            if (animation_timer >= anim_time)
            {
                animation_timer = anim_time;
                closing = false;
            }
        }
        if (opening)
        {
			animation_timer -= Time.deltaTime;
			if (animation_timer <= 0)
			{
				animation_timer = 0;
				opening = false;
			}
		}

        float anim_progress = Mathf.Min(1.0f, animation_timer / anim_time);
		global_scale = (1.0f - anim_progress) * min_scale + anim_progress * max_scale;

        for (int i = 0; i < myChildren.Count; i++)
        {
            myChildren[i].transform.localScale = global_scale * myChildrenBaseScales[i] * Vector3.one;
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class circleBarForImage : MonoBehaviour {
    SpriteRenderer sprite;


	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
        sprite.material.SetFloat("_Cutoff", Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x));
	}
}

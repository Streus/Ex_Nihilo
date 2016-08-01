﻿using UnityEngine;
using System.Collections;

public class MeshDeformerInput : MonoBehaviour {

	public float force = 10f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {
			HandleInput ();
		}
	}

	public float forceOffset = 0.1f;

	void HandleInput() {
		Ray inputRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast (inputRay, out hit)) { 
			MeshDeformer deformer = hit.collider.GetComponent<MeshDeformer> ();

			if (deformer) {
				Vector3 point = hit.point;
				point += hit.normal * forceOffset;
				deformer.AddDeformingForce (point, force);
			}
		}
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour {


	Mesh deformingMesh, collisionDeformingMesh;

	Vector3[] originalVerticies, displacedVerticies;

	Vector3[] vertexVelocities;
	// Use this for initialization
	void Start () {
		//Initializing the arrays and meshes.
		deformingMesh = GetComponent<MeshFilter> ().mesh;
		collisionDeformingMesh = GetComponent<MeshCollider> ().sharedMesh;
		originalVerticies = deformingMesh.vertices;
		displacedVerticies = new Vector3[originalVerticies.Length];
		for (int i = 0; i < originalVerticies.Length; i++) {
			displacedVerticies [i] = originalVerticies [i];
		}
		vertexVelocities = new Vector3[originalVerticies.Length];
	}
	
	// Update is called once per frame
	void Update () {

		uniformScale = transform.localScale.x;
		for (int i = 0; i < displacedVerticies.Length; i++) {
			UpdateVertex (i);
		}
		deformingMesh.vertices = displacedVerticies;
		deformingMesh.RecalculateNormals ();
		collisionDeformingMesh.vertices = displacedVerticies;
		collisionDeformingMesh.RecalculateNormals ();
	}


	public void AddDeformingForce (Vector3 point, float force) {
		Debug.DrawLine (Camera.main.transform.position, point);

		point = transform.InverseTransformPoint (point);
		for (int i = 0; i < displacedVerticies.Length; i++) {
			AddForceToVertex (i, point, force);
		}
	}

	void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = displacedVerticies [i] - point;
		pointToVertex *= uniformScale;
		float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatedForce * Time.deltaTime;
		vertexVelocities [i] += pointToVertex.normalized * velocity;
	}

	void UpdateVertex(int i) {
		Vector3 velocity = vertexVelocities [i];
		Vector3 displacement = displacedVerticies [i] - originalVerticies [i];
		displacement *= uniformScale;
		velocity -= displacement * springForce * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities [i] = velocity;
		displacedVerticies[i] += velocity * (Time.deltaTime / uniformScale);
	}

	float uniformScale = 1f;
	public float springForce = 20f;
	public float damping = 5f;
}

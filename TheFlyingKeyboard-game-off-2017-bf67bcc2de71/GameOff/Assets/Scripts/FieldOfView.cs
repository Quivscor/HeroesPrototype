using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {
	public Unit unit;
	public LayerMask obstacleMask;
	public CircleCollider2D col;
	public float viewRadius;
	public float stepCount;

	public MeshFilter viewMeshFilter;
	new public MeshCollider collider;
	Mesh viewMesh;

	void Start() {
		collider = GetComponentInChildren<MeshCollider> ();
		unit = GetComponent<Unit> ();

		viewMesh = new Mesh();
		viewMesh.name = "View Mesh";
		viewMeshFilter.gameObject.tag = "Range Indicator";
		viewMeshFilter.mesh = viewMesh;

		col = GetComponent<CircleCollider2D> ();
	}

	void Update() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (!unit.isMoving && unit.isTurn) {
			DrawFieldOfView ();
		} else {
			viewMesh.Clear ();
		}
		if (Physics.Raycast (ray)) {
			unit.cursorInRange = true;
		} else {
			unit.cursorInRange = false;
		}
	}

	public void DrawFieldOfView() {
		List<Vector2> viewPoints = new List<Vector2> ();
		RaycastHit2D[] hit = new RaycastHit2D[(int)stepCount];

		float step = 360 / stepCount;
		float angle = 0;

		for (int i = 0; i < stepCount; i++) {
			angle += step;
			Vector2 dir = new Vector2 (Mathf.Sin (angle * Mathf.Deg2Rad), Mathf.Cos (angle * Mathf.Deg2Rad));
			hit [i] = Physics2D.Raycast (transform.position, dir, unit.moveDistance, obstacleMask);
			Debug.DrawRay (col.transform.position, dir * unit.moveDistance, Color.red);
			if (hit [i]) {
				viewPoints.Add (dir * hit[i].distance);
				//Debug.Log ("hit" + i + " = " + viewPoints[i]);
			} else {
				viewPoints.Add (dir * unit.moveDistance);
			}
		}
		//Debug.Log (viewPoints.Count);
		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount - 1) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = viewPoints [i];

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [(i * 3) + 1] = i + 1;
				triangles [(i * 3) + 2] = i + 2;
			}
			/*if (i == vertexCount - 1) {
				triangles [i * 3] = 0;
				triangles [(i * 3) + 1] = vertexCount - 1;
				triangles [(i * 3) + 2] = 1;
			}*/
		}
		triangles [(vertexCount - 2) * 3 + 0] = 0;
		triangles [(vertexCount - 2) * 3 + 1] = vertexCount - 1;
		triangles [(vertexCount - 2) * 3 + 2] = 1;

		viewMesh.Clear ();
		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateBounds ();

		collider.sharedMesh = null;
		collider.sharedMesh = viewMesh;
	}

	public struct ViewCastInfo {
		public bool hit;
		public Vector2 point;
		public float dst;
		public float angle;

		public ViewCastInfo(bool _hit, Vector2 _point, float _dst, float _angle) {
			hit = _hit;
			point = _point;
			dst = _dst;
			angle = _angle;
		}
	}

}

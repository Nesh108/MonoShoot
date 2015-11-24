using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour
{
	public Mesh spawnMesh;
	public Color color = Color.red;

	public void OnDrawGizmos ()
	{
		Gizmos.color = color;
		Gizmos.DrawMesh(spawnMesh, transform.position);
	}

}

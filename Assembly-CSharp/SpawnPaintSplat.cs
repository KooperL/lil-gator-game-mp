using System;
using UnityEngine;

public class SpawnPaintSplat : MonoBehaviour
{
	// Token: 0x060000EF RID: 239 RVA: 0x000065BC File Offset: 0x000047BC
	[ContextMenu("Spawn")]
	public void Spawn()
	{
		if (Time.time - this.lastSpawn < 0.1f)
		{
			return;
		}
		this.lastSpawn = Time.time;
		Vector3 vector;
		if (this.usePlayerForward)
		{
			vector = Vector3.Slerp(Player.transform.forward, -base.transform.up, 0.5f);
		}
		else
		{
			vector = -base.transform.up;
		}
		RaycastHit raycastHit;
		if (Physics.SphereCast(base.transform.position - 0.5f * vector, 0.25f, vector, out raycastHit, 1.25f, this.raycastLayers))
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.prefab, raycastHit.point + 0.1f * Random.insideUnitSphere, Random.rotation);
			gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, raycastHit.normal) * gameObject.transform.rotation;
			return;
		}
		Object.Instantiate<GameObject>(this.prefab, base.transform.position + 0.1f * Random.insideUnitSphere, base.transform.rotation);
	}

	public GameObject prefab;

	private float lastSpawn = -1f;

	public LayerMask raycastLayers;

	public bool usePlayerForward = true;
}

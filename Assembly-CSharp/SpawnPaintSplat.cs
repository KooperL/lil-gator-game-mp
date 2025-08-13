using System;
using UnityEngine;

// Token: 0x0200004D RID: 77
public class SpawnPaintSplat : MonoBehaviour
{
	// Token: 0x06000114 RID: 276 RVA: 0x0001AFB0 File Offset: 0x000191B0
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
		if (Physics.SphereCast(base.transform.position - 0.5f * vector, 0.25f, vector, ref raycastHit, 1.25f, this.raycastLayers))
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.prefab, raycastHit.point + 0.1f * Random.insideUnitSphere, Random.rotation);
			gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, raycastHit.normal) * gameObject.transform.rotation;
			return;
		}
		Object.Instantiate<GameObject>(this.prefab, base.transform.position + 0.1f * Random.insideUnitSphere, base.transform.rotation);
	}

	// Token: 0x0400018D RID: 397
	public GameObject prefab;

	// Token: 0x0400018E RID: 398
	private float lastSpawn = -1f;

	// Token: 0x0400018F RID: 399
	public LayerMask raycastLayers;

	// Token: 0x04000190 RID: 400
	public bool usePlayerForward = true;
}

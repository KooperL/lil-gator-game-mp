using System;
using UnityEngine;

// Token: 0x02000279 RID: 633
public class TerrainPreventClipping : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000D8D RID: 3469 RVA: 0x000418E0 File Offset: 0x0003FAE0
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000418FC File Offset: 0x0003FAFC
	private void Start()
	{
		this.offset = base.transform.position.y;
		Vector3 size = this.terrain.terrainData.size;
		Vector2 vector = new Vector2(size.x, size.z);
		this.terrainCenter = new Vector2(base.transform.position.x, base.transform.position.z) + 0.5f * vector;
		this.terrainRadius = (0.5f * vector).magnitude;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x00041997 File Offset: 0x0003FB97
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000419A4 File Offset: 0x0003FBA4
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000419B4 File Offset: 0x0003FBB4
	public void ManagedUpdate()
	{
		if (Player.movement == null)
		{
			return;
		}
		Vector3 vector;
		if (Player.movement.isRagdolling)
		{
			vector = Player.movement.ragdollController.rigidbodies[0].position;
		}
		else
		{
			vector = Player.rigidbody.position;
		}
		if (Vector3.Distance(new Vector2(vector.x, vector.z), this.terrainCenter) > this.terrainRadius)
		{
			return;
		}
		float num = this.terrain.SampleHeight(vector) + this.offset;
		if (num - this.clippingThreshold > vector.y)
		{
			Player.movement.SetPosition(new Vector3(vector.x, num + 1f, vector.z));
		}
		Vector3 position = PlayerOrbitCamera.active.transform.position;
		num = this.terrain.SampleHeight(position) + this.offset;
		if (num - this.cameraClippingThreshold > position.y)
		{
			PlayerOrbitCamera.active.ForceRecovery();
		}
	}

	// Token: 0x040011E1 RID: 4577
	public float clippingThreshold = 1f;

	// Token: 0x040011E2 RID: 4578
	public float cameraClippingThreshold = 0.25f;

	// Token: 0x040011E3 RID: 4579
	private float offset;

	// Token: 0x040011E4 RID: 4580
	public Terrain terrain;

	// Token: 0x040011E5 RID: 4581
	private Vector2 terrainCenter;

	// Token: 0x040011E6 RID: 4582
	private float terrainRadius;
}

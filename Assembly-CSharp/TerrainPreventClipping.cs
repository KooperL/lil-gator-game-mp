using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class TerrainPreventClipping : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06001049 RID: 4169 RVA: 0x0000E0C5 File Offset: 0x0000C2C5
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x0600104A RID: 4170 RVA: 0x00053DB0 File Offset: 0x00051FB0
	private void Start()
	{
		this.offset = base.transform.position.y;
		Vector3 size = this.terrain.terrainData.size;
		Vector2 vector;
		vector..ctor(size.x, size.z);
		this.terrainCenter = new Vector2(base.transform.position.x, base.transform.position.z) + 0.5f * vector;
		this.terrainRadius = (0.5f * vector).magnitude;
	}

	// Token: 0x0600104B RID: 4171 RVA: 0x000085DF File Offset: 0x000067DF
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x0600104C RID: 4172 RVA: 0x000085EC File Offset: 0x000067EC
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x0600104D RID: 4173 RVA: 0x00053E4C File Offset: 0x0005204C
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

	// Token: 0x0400151D RID: 5405
	public float clippingThreshold = 1f;

	// Token: 0x0400151E RID: 5406
	public float cameraClippingThreshold = 0.25f;

	// Token: 0x0400151F RID: 5407
	private float offset;

	// Token: 0x04001520 RID: 5408
	public Terrain terrain;

	// Token: 0x04001521 RID: 5409
	private Vector2 terrainCenter;

	// Token: 0x04001522 RID: 5410
	private float terrainRadius;
}

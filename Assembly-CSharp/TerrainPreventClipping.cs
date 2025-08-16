using System;
using UnityEngine;

public class TerrainPreventClipping : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060010A4 RID: 4260 RVA: 0x0000E419 File Offset: 0x0000C619
	private void OnValidate()
	{
		if (this.terrain == null)
		{
			this.terrain = base.GetComponent<Terrain>();
		}
	}

	// Token: 0x060010A5 RID: 4261 RVA: 0x00055B40 File Offset: 0x00053D40
	private void Start()
	{
		this.offset = base.transform.position.y;
		Vector3 size = this.terrain.terrainData.size;
		Vector2 vector = new Vector2(size.x, size.z);
		this.terrainCenter = new Vector2(base.transform.position.x, base.transform.position.z) + 0.5f * vector;
		this.terrainRadius = (0.5f * vector).magnitude;
	}

	// Token: 0x060010A6 RID: 4262 RVA: 0x000088E7 File Offset: 0x00006AE7
	private void OnEnable()
	{
		FastUpdateManager.fixedUpdate8.Add(this);
	}

	// Token: 0x060010A7 RID: 4263 RVA: 0x000088F4 File Offset: 0x00006AF4
	private void OnDisable()
	{
		FastUpdateManager.fixedUpdate8.Remove(this);
	}

	// Token: 0x060010A8 RID: 4264 RVA: 0x00055BDC File Offset: 0x00053DDC
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

	public float clippingThreshold = 1f;

	public float cameraClippingThreshold = 0.25f;

	private float offset;

	public Terrain terrain;

	private Vector2 terrainCenter;

	private float terrainRadius;
}

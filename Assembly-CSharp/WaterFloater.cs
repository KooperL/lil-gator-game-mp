using System;
using UnityEngine;

// Token: 0x02000354 RID: 852
public class WaterFloater : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06001077 RID: 4215 RVA: 0x00054A2C File Offset: 0x00052C2C
	private void Awake()
	{
		this.initialPosition = base.transform.position;
		this.positionSeed = Random.value * 1000f;
		this.rotationSeed = Random.value * 1000f;
		this.rotation = new Vector3(0f, 360f * Random.value, 0f);
		this.height = this.initialPosition.y;
	}

	// Token: 0x06001078 RID: 4216 RVA: 0x00054AA0 File Offset: 0x00052CA0
	private void OnEnable()
	{
		this.isInRange = Vector3.Distance(MainCamera.t.position, base.transform.position) <= 55f;
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Add(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Add(this);
	}

	// Token: 0x06001079 RID: 4217 RVA: 0x0000E288 File Offset: 0x0000C488
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x0600107A RID: 4218 RVA: 0x00054AF8 File Offset: 0x00052CF8
	public void ManagedUpdate()
	{
		if (this.isInRange != Vector3.Distance(MainCamera.t.position, base.transform.position) <= 55f)
		{
			this.isInRange = !this.isInRange;
			if (this.isInRange)
			{
				FastUpdateManager.updateEveryNonFixed.Remove(this);
				FastUpdateManager.updateEvery1.Add(this);
			}
			else
			{
				FastUpdateManager.updateEveryNonFixed.Add(this);
				FastUpdateManager.updateEvery1.Remove(this);
			}
		}
		this.UpdateFloatingPosition();
	}

	// Token: 0x0600107B RID: 4219 RVA: 0x00054B80 File Offset: 0x00052D80
	private void UpdateFloatingPosition()
	{
		Vector2 vector = this.positionVariance * PerlinUtil.Perlin2(this.positionSeed, Time.time * this.positionPerlinSpeed);
		Vector3 vector2 = this.initialPosition;
		vector2.x += vector.x;
		vector2.z += vector.y;
		Vector3 vector3 = this.rotationVariance * PerlinUtil.Perlin3(this.rotationSeed, Time.time * this.rotationPerlinSpeed);
		base.transform.rotation = Quaternion.Euler(this.rotation + vector3);
		float waterPlaneHeight = this.water.GetWaterPlaneHeight(vector2);
		this.height = Mathf.SmoothDamp(this.height, waterPlaneHeight, ref this.heightVel, 0.25f);
		vector2.y = this.height;
		base.transform.position = vector2;
	}

	// Token: 0x04001554 RID: 5460
	private static Vector3[] sampledPositions = new Vector3[4];

	// Token: 0x04001555 RID: 5461
	public MovingWater water;

	// Token: 0x04001556 RID: 5462
	private bool isInRange;

	// Token: 0x04001557 RID: 5463
	public float positionVariance = 2f;

	// Token: 0x04001558 RID: 5464
	public float positionPerlinSpeed = 0.25f;

	// Token: 0x04001559 RID: 5465
	private Vector3 initialPosition;

	// Token: 0x0400155A RID: 5466
	private float positionSeed;

	// Token: 0x0400155B RID: 5467
	private Vector3 rotation;

	// Token: 0x0400155C RID: 5468
	public float rotationVariance = 40f;

	// Token: 0x0400155D RID: 5469
	public float rotationPerlinSpeed = 0.25f;

	// Token: 0x0400155E RID: 5470
	private float rotationSeed;

	// Token: 0x0400155F RID: 5471
	private float height;

	// Token: 0x04001560 RID: 5472
	private float heightVel;
}

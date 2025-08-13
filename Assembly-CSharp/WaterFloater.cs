using System;
using UnityEngine;

// Token: 0x02000284 RID: 644
public class WaterFloater : MonoBehaviour, IManagedUpdate
{
	// Token: 0x06000DBB RID: 3515 RVA: 0x000427AC File Offset: 0x000409AC
	private void Awake()
	{
		this.initialPosition = base.transform.position;
		this.positionSeed = Random.value * 1000f;
		this.rotationSeed = Random.value * 1000f;
		this.rotation = new Vector3(0f, 360f * Random.value, 0f);
		this.height = this.initialPosition.y;
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x00042820 File Offset: 0x00040A20
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

	// Token: 0x06000DBD RID: 3517 RVA: 0x00042876 File Offset: 0x00040A76
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0004289C File Offset: 0x00040A9C
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

	// Token: 0x06000DBF RID: 3519 RVA: 0x00042924 File Offset: 0x00040B24
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

	// Token: 0x0400120D RID: 4621
	private static Vector3[] sampledPositions = new Vector3[4];

	// Token: 0x0400120E RID: 4622
	public MovingWater water;

	// Token: 0x0400120F RID: 4623
	private bool isInRange;

	// Token: 0x04001210 RID: 4624
	public float positionVariance = 2f;

	// Token: 0x04001211 RID: 4625
	public float positionPerlinSpeed = 0.25f;

	// Token: 0x04001212 RID: 4626
	private Vector3 initialPosition;

	// Token: 0x04001213 RID: 4627
	private float positionSeed;

	// Token: 0x04001214 RID: 4628
	private Vector3 rotation;

	// Token: 0x04001215 RID: 4629
	public float rotationVariance = 40f;

	// Token: 0x04001216 RID: 4630
	public float rotationPerlinSpeed = 0.25f;

	// Token: 0x04001217 RID: 4631
	private float rotationSeed;

	// Token: 0x04001218 RID: 4632
	private float height;

	// Token: 0x04001219 RID: 4633
	private float heightVel;
}

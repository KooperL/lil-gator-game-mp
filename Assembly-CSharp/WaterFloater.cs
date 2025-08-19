using System;
using UnityEngine;

public class WaterFloater : MonoBehaviour, IManagedUpdate
{
	// Token: 0x060010D2 RID: 4306 RVA: 0x0005692C File Offset: 0x00054B2C
	private void Awake()
	{
		this.initialPosition = base.transform.position;
		this.positionSeed = global::UnityEngine.Random.value * 1000f;
		this.rotationSeed = global::UnityEngine.Random.value * 1000f;
		this.rotation = new Vector3(0f, 360f * global::UnityEngine.Random.value, 0f);
		this.height = this.initialPosition.y;
	}

	// Token: 0x060010D3 RID: 4307 RVA: 0x000569A0 File Offset: 0x00054BA0
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

	// Token: 0x060010D4 RID: 4308 RVA: 0x0000E5FB File Offset: 0x0000C7FB
	private void OnDisable()
	{
		if (this.isInRange)
		{
			FastUpdateManager.updateEvery1.Remove(this);
			return;
		}
		FastUpdateManager.updateEveryNonFixed.Remove(this);
	}

	// Token: 0x060010D5 RID: 4309 RVA: 0x000569F8 File Offset: 0x00054BF8
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

	// Token: 0x060010D6 RID: 4310 RVA: 0x00056A80 File Offset: 0x00054C80
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

	private static Vector3[] sampledPositions = new Vector3[4];

	public MovingWater water;

	private bool isInRange;

	public float positionVariance = 2f;

	public float positionPerlinSpeed = 0.25f;

	private Vector3 initialPosition;

	private float positionSeed;

	private Vector3 rotation;

	public float rotationVariance = 40f;

	public float rotationPerlinSpeed = 0.25f;

	private float rotationSeed;

	private float height;

	private float heightVel;
}

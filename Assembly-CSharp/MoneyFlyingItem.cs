using System;
using UnityEngine;

// Token: 0x020001F2 RID: 498
public class MoneyFlyingItem : MonoBehaviour
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x06000938 RID: 2360 RVA: 0x00008FDE File Offset: 0x000071DE
	private bool IsDelayed
	{
		get
		{
			return Time.time - this.spawnTime < this.delay;
		}
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00039350 File Offset: 0x00037550
	private void Start()
	{
		this.velocity = (Random.insideUnitSphere + Vector3.up) * this.initialSpeed;
		this.spawnTime = Time.time;
		this.rotation = Random.insideUnitSphere * this.maxRotationSpeed;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x000393A0 File Offset: 0x000375A0
	private void Update()
	{
		if (!this.IsDelayed)
		{
			Vector3 vector = Player.Position - base.transform.position;
			if (vector.magnitude < 0.5f)
			{
				this.Collect();
			}
			else
			{
				this.velocity = Vector3.MoveTowards(this.velocity, vector * 100f, 20f * Time.deltaTime);
			}
		}
		else
		{
			this.velocity += 2f * Physics.gravity * Time.deltaTime;
		}
		this.velocity = Vector3.MoveTowards(this.velocity, Vector3.zero, 5f * Time.deltaTime);
		base.transform.position += this.velocity * Time.deltaTime;
		base.transform.Rotate(this.rotation * Time.deltaTime, 1);
		if (Time.time - this.spawnTime > 2f)
		{
			this.Collect();
		}
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x000047FB File Offset: 0x000029FB
	private void Collect()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000BE4 RID: 3044
	private const float acceleration = 20f;

	// Token: 0x04000BE5 RID: 3045
	private float initialSpeed = 10f;

	// Token: 0x04000BE6 RID: 3046
	private Vector3 velocity;

	// Token: 0x04000BE7 RID: 3047
	public int cents;

	// Token: 0x04000BE8 RID: 3048
	private float delay = 0.25f;

	// Token: 0x04000BE9 RID: 3049
	private float spawnTime;

	// Token: 0x04000BEA RID: 3050
	private Vector3 rotation;

	// Token: 0x04000BEB RID: 3051
	public float maxRotationSpeed = 30f;
}

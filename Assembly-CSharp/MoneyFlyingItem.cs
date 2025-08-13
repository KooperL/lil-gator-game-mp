using System;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class MoneyFlyingItem : MonoBehaviour
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00026290 File Offset: 0x00024490
	private bool IsDelayed
	{
		get
		{
			return Time.time - this.spawnTime < this.delay;
		}
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x000262A8 File Offset: 0x000244A8
	private void Start()
	{
		this.velocity = (Random.insideUnitSphere + Vector3.up) * this.initialSpeed;
		this.spawnTime = Time.time;
		this.rotation = Random.insideUnitSphere * this.maxRotationSpeed;
	}

	// Token: 0x060007D8 RID: 2008 RVA: 0x000262F8 File Offset: 0x000244F8
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
		base.transform.Rotate(this.rotation * Time.deltaTime, Space.Self);
		if (Time.time - this.spawnTime > 2f)
		{
			this.Collect();
		}
	}

	// Token: 0x060007D9 RID: 2009 RVA: 0x00026408 File Offset: 0x00024608
	private void Collect()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x04000A08 RID: 2568
	private const float acceleration = 20f;

	// Token: 0x04000A09 RID: 2569
	private float initialSpeed = 10f;

	// Token: 0x04000A0A RID: 2570
	private Vector3 velocity;

	// Token: 0x04000A0B RID: 2571
	public int cents;

	// Token: 0x04000A0C RID: 2572
	private float delay = 0.25f;

	// Token: 0x04000A0D RID: 2573
	private float spawnTime;

	// Token: 0x04000A0E RID: 2574
	private Vector3 rotation;

	// Token: 0x04000A0F RID: 2575
	public float maxRotationSpeed = 30f;
}

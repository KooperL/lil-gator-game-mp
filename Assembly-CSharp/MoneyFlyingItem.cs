using System;
using UnityEngine;

public class MoneyFlyingItem : MonoBehaviour
{
	// (get) Token: 0x06000979 RID: 2425 RVA: 0x0000930F File Offset: 0x0000750F
	private bool IsDelayed
	{
		get
		{
			return Time.time - this.spawnTime < this.delay;
		}
	}

	// Token: 0x0600097A RID: 2426 RVA: 0x0003ACC0 File Offset: 0x00038EC0
	private void Start()
	{
		this.velocity = (global::UnityEngine.Random.insideUnitSphere + Vector3.up) * this.initialSpeed;
		this.spawnTime = Time.time;
		this.rotation = global::UnityEngine.Random.insideUnitSphere * this.maxRotationSpeed;
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x0003AD10 File Offset: 0x00038F10
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

	// Token: 0x0600097C RID: 2428 RVA: 0x000049DF File Offset: 0x00002BDF
	private void Collect()
	{
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	private const float acceleration = 20f;

	private float initialSpeed = 10f;

	private Vector3 velocity;

	public int cents;

	private float delay = 0.25f;

	private float spawnTime;

	private Vector3 rotation;

	public float maxRotationSpeed = 30f;
}

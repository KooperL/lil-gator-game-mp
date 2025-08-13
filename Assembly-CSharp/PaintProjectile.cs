using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class PaintProjectile : MonoBehaviour
{
	// Token: 0x060000F6 RID: 246 RVA: 0x00002CA6 File Offset: 0x00000EA6
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00002CB4 File Offset: 0x00000EB4
	private void Start()
	{
		this.spawnTime = Time.time;
		if (this.isRandomized)
		{
			this.color = this.randomColor.Evaluate(Random.value);
		}
		this.setDecal = base.GetComponent<SetDecal>();
		this.UpdateDecal();
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x0001AC68 File Offset: 0x00018E68
	private void UpdateDecal()
	{
		Color color = this.color;
		this.setDecal.SetColor(color);
		ParticleSystem[] array = this.particleSystems;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].main.startColor = this.color;
		}
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x00002CF1 File Offset: 0x00000EF1
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(-collision.contacts[0].normal));
		}
	}

	// Token: 0x060000FA RID: 250 RVA: 0x0001ACB8 File Offset: 0x00018EB8
	private void OnTriggerEnter(Collider other)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(this.rigidbody.velocity.normalized));
		}
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00002D1E File Offset: 0x00000F1E
	public IEnumerator Spawn(Vector3 direction)
	{
		this.isSpawning = true;
		yield return PaintProjectile.waitForEndOfFrame;
		RaycastHit raycastHit;
		GameObject gameObject;
		if (Physics.SphereCast(base.transform.position - 0.5f * direction, 0.25f, direction, ref raycastHit, 1.25f, this.raycastLayers, 1))
		{
			gameObject = Object.Instantiate<GameObject>(this.paintsplatPrefab, raycastHit.point + 0.1f * Random.insideUnitSphere, Random.rotation);
			gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, raycastHit.normal) * gameObject.transform.rotation;
		}
		else
		{
			gameObject = Object.Instantiate<GameObject>(this.paintsplatPrefab, base.transform.position + 0.1f * Random.insideUnitSphere, base.transform.rotation);
		}
		gameObject.GetComponent<Paintsplat>().SetColor(this.color);
		this.renderer.enabled = false;
		this.rigidbody.isKinematic = true;
		base.GetComponent<DistanceTimeout>().SetTimeoutTime(2f);
		base.GetComponent<Collider>().enabled = false;
		base.StartCoroutine(this.DestroyNextFrame());
		yield break;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00002D34 File Offset: 0x00000F34
	private IEnumerator DestroyNextFrame()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400016E RID: 366
	public Renderer renderer;

	// Token: 0x0400016F RID: 367
	public ParticleSystem[] particleSystems;

	// Token: 0x04000170 RID: 368
	public GameObject paintsplatPrefab;

	// Token: 0x04000171 RID: 369
	public LayerMask raycastLayers;

	// Token: 0x04000172 RID: 370
	public bool isRandomized = true;

	// Token: 0x04000173 RID: 371
	public Gradient randomColor;

	// Token: 0x04000174 RID: 372
	public Color color;

	// Token: 0x04000175 RID: 373
	private SetDecal setDecal;

	// Token: 0x04000176 RID: 374
	private float spawnTime;

	// Token: 0x04000177 RID: 375
	private Rigidbody rigidbody;

	// Token: 0x04000178 RID: 376
	private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	// Token: 0x04000179 RID: 377
	private bool isSpawning;
}

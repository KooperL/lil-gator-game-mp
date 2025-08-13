using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class PaintProjectile : MonoBehaviour
{
	// Token: 0x060000DD RID: 221 RVA: 0x000062E4 File Offset: 0x000044E4
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000062F2 File Offset: 0x000044F2
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

	// Token: 0x060000DF RID: 223 RVA: 0x00006330 File Offset: 0x00004530
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

	// Token: 0x060000E0 RID: 224 RVA: 0x00006380 File Offset: 0x00004580
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(-collision.contacts[0].normal));
		}
	}

	// Token: 0x060000E1 RID: 225 RVA: 0x000063B0 File Offset: 0x000045B0
	private void OnTriggerEnter(Collider other)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(this.rigidbody.velocity.normalized));
		}
	}

	// Token: 0x060000E2 RID: 226 RVA: 0x000063E5 File Offset: 0x000045E5
	public IEnumerator Spawn(Vector3 direction)
	{
		this.isSpawning = true;
		yield return PaintProjectile.waitForEndOfFrame;
		RaycastHit raycastHit;
		GameObject gameObject;
		if (Physics.SphereCast(base.transform.position - 0.5f * direction, 0.25f, direction, out raycastHit, 1.25f, this.raycastLayers, QueryTriggerInteraction.Ignore))
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

	// Token: 0x060000E3 RID: 227 RVA: 0x000063FB File Offset: 0x000045FB
	private IEnumerator DestroyNextFrame()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x0400012E RID: 302
	public Renderer renderer;

	// Token: 0x0400012F RID: 303
	public ParticleSystem[] particleSystems;

	// Token: 0x04000130 RID: 304
	public GameObject paintsplatPrefab;

	// Token: 0x04000131 RID: 305
	public LayerMask raycastLayers;

	// Token: 0x04000132 RID: 306
	public bool isRandomized = true;

	// Token: 0x04000133 RID: 307
	public Gradient randomColor;

	// Token: 0x04000134 RID: 308
	public Color color;

	// Token: 0x04000135 RID: 309
	private SetDecal setDecal;

	// Token: 0x04000136 RID: 310
	private float spawnTime;

	// Token: 0x04000137 RID: 311
	private Rigidbody rigidbody;

	// Token: 0x04000138 RID: 312
	private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	// Token: 0x04000139 RID: 313
	private bool isSpawning;
}

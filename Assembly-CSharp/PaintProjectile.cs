using System;
using System.Collections;
using UnityEngine;

public class PaintProjectile : MonoBehaviour
{
	// Token: 0x060000FE RID: 254 RVA: 0x00002D0A File Offset: 0x00000F0A
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00002D18 File Offset: 0x00000F18
	private void Start()
	{
		this.spawnTime = Time.time;
		if (this.isRandomized)
		{
			this.color = this.randomColor.Evaluate(global::UnityEngine.Random.value);
		}
		this.setDecal = base.GetComponent<SetDecal>();
		this.UpdateDecal();
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0001B4A8 File Offset: 0x000196A8
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

	// Token: 0x06000101 RID: 257 RVA: 0x00002D55 File Offset: 0x00000F55
	private void OnCollisionEnter(Collision collision)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(-collision.contacts[0].normal));
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0001B4F8 File Offset: 0x000196F8
	private void OnTriggerEnter(Collider other)
	{
		if (!this.isSpawning)
		{
			base.StartCoroutine(this.Spawn(this.rigidbody.velocity.normalized));
		}
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00002D82 File Offset: 0x00000F82
	public IEnumerator Spawn(Vector3 direction)
	{
		this.isSpawning = true;
		yield return PaintProjectile.waitForEndOfFrame;
		RaycastHit raycastHit;
		GameObject gameObject;
		if (Physics.SphereCast(base.transform.position - 0.5f * direction, 0.25f, direction, out raycastHit, 1.25f, this.raycastLayers, QueryTriggerInteraction.Ignore))
		{
			gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.paintsplatPrefab, raycastHit.point + 0.1f * global::UnityEngine.Random.insideUnitSphere, global::UnityEngine.Random.rotation);
			gameObject.transform.rotation = Quaternion.FromToRotation(gameObject.transform.up, raycastHit.normal) * gameObject.transform.rotation;
		}
		else
		{
			gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.paintsplatPrefab, base.transform.position + 0.1f * global::UnityEngine.Random.insideUnitSphere, base.transform.rotation);
		}
		gameObject.GetComponent<Paintsplat>().SetColor(this.color);
		this.renderer.enabled = false;
		this.rigidbody.isKinematic = true;
		base.GetComponent<DistanceTimeout>().SetTimeoutTime(2f);
		base.GetComponent<Collider>().enabled = false;
		base.StartCoroutine(this.DestroyNextFrame());
		yield break;
	}

	// Token: 0x06000104 RID: 260 RVA: 0x00002D98 File Offset: 0x00000F98
	private IEnumerator DestroyNextFrame()
	{
		yield return new WaitForFixedUpdate();
		yield return new WaitForFixedUpdate();
		global::UnityEngine.Object.Destroy(base.gameObject);
		yield break;
	}

	public Renderer renderer;

	public ParticleSystem[] particleSystems;

	public GameObject paintsplatPrefab;

	public LayerMask raycastLayers;

	public bool isRandomized = true;

	public Gradient randomColor;

	public Color color;

	private SetDecal setDecal;

	private float spawnTime;

	private Rigidbody rigidbody;

	private static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

	private bool isSpawning;
}

using System;
using UnityEngine;

public class JunkItem : MonoBehaviour
{
	// Token: 0x06000845 RID: 2117 RVA: 0x00037A80 File Offset: 0x00035C80
	private void Start()
	{
		this.renderer = base.transform.parent.GetComponent<Renderer>();
		this.rigidbody = base.transform.parent.GetComponent<Rigidbody>();
		this.timeoutTime = Time.time + this.timeout;
		this.mainCamera = MainCamera.t;
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x00037AD8 File Offset: 0x00035CD8
	private void FixedUpdate()
	{
		this.rigidbody.velocity += 50f * Time.deltaTime * (this.mainCamera.TransformPoint(new Vector3(0f, 0f, -1f)) - this.rigidbody.position).normalized;
		if (this.mainCamera.InverseTransformPoint(this.rigidbody.position).z < 0f || Time.time >= this.timeoutTime)
		{
			this.Collect();
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x00037B78 File Offset: 0x00035D78
	private void Collect()
	{
		ItemResource itemResource = this.resource;
		int amount = itemResource.Amount;
		itemResource.Amount = amount + 1;
		if (this.soundEffect != null)
		{
			this.soundEffect.Play();
		}
		global::UnityEngine.Object.Destroy(base.transform.parent.gameObject);
	}

	private Renderer renderer;

	private Rigidbody rigidbody;

	private Transform mainCamera;

	private float timeout = 0.5f;

	private float timeoutTime;

	[HideInInspector]
	public ItemResource resource;

	public AudioSourceVariance soundEffect;
}

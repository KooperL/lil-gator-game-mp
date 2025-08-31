using System;
using UnityEngine;

public class JunkItem : MonoBehaviour
{
	// Token: 0x060006C6 RID: 1734 RVA: 0x00022688 File Offset: 0x00020888
	private void Start()
	{
		this.renderer = base.transform.parent.GetComponent<Renderer>();
		this.rigidbody = base.transform.parent.GetComponent<Rigidbody>();
		this.timeoutTime = Time.time + this.timeout;
		this.mainCamera = MainCamera.t;
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x000226E0 File Offset: 0x000208E0
	private void FixedUpdate()
	{
		this.rigidbody.velocity += 50f * Time.deltaTime * (this.mainCamera.TransformPoint(new Vector3(0f, 0f, -1f)) - this.rigidbody.position).normalized;
		if (this.mainCamera.InverseTransformPoint(this.rigidbody.position).z < 0f || Time.time >= this.timeoutTime)
		{
			this.Collect();
		}
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x00022780 File Offset: 0x00020980
	private void Collect()
	{
		ItemResource itemResource = this.resource;
		int amount = itemResource.Amount;
		itemResource.Amount = amount + 1;
		if (this.soundEffect != null)
		{
			this.soundEffect.Play();
		}
		Object.Destroy(base.transform.parent.gameObject);
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

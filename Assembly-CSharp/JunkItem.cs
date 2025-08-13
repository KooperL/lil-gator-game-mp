using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class JunkItem : MonoBehaviour
{
	// Token: 0x06000804 RID: 2052 RVA: 0x00035E50 File Offset: 0x00034050
	private void Start()
	{
		this.renderer = base.transform.parent.GetComponent<Renderer>();
		this.rigidbody = base.transform.parent.GetComponent<Rigidbody>();
		this.timeoutTime = Time.time + this.timeout;
		this.mainCamera = MainCamera.t;
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x00035EA8 File Offset: 0x000340A8
	private void FixedUpdate()
	{
		this.rigidbody.velocity += 50f * Time.deltaTime * (this.mainCamera.TransformPoint(new Vector3(0f, 0f, -1f)) - this.rigidbody.position).normalized;
		if (this.mainCamera.InverseTransformPoint(this.rigidbody.position).z < 0f || Time.time >= this.timeoutTime)
		{
			this.Collect();
		}
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x00035F48 File Offset: 0x00034148
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

	// Token: 0x04000AAB RID: 2731
	private Renderer renderer;

	// Token: 0x04000AAC RID: 2732
	private Rigidbody rigidbody;

	// Token: 0x04000AAD RID: 2733
	private Transform mainCamera;

	// Token: 0x04000AAE RID: 2734
	private float timeout = 0.5f;

	// Token: 0x04000AAF RID: 2735
	private float timeoutTime;

	// Token: 0x04000AB0 RID: 2736
	[HideInInspector]
	public ItemResource resource;

	// Token: 0x04000AB1 RID: 2737
	public AudioSourceVariance soundEffect;
}

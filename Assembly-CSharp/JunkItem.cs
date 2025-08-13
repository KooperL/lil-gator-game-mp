using System;
using UnityEngine;

// Token: 0x0200014C RID: 332
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

	// Token: 0x04000923 RID: 2339
	private Renderer renderer;

	// Token: 0x04000924 RID: 2340
	private Rigidbody rigidbody;

	// Token: 0x04000925 RID: 2341
	private Transform mainCamera;

	// Token: 0x04000926 RID: 2342
	private float timeout = 0.5f;

	// Token: 0x04000927 RID: 2343
	private float timeoutTime;

	// Token: 0x04000928 RID: 2344
	[HideInInspector]
	public ItemResource resource;

	// Token: 0x04000929 RID: 2345
	public AudioSourceVariance soundEffect;
}

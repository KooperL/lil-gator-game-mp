using System;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class SharedAudioSource : MonoBehaviour, ISharedAudioSource
{
	// Token: 0x06000208 RID: 520 RVA: 0x0000B445 File Offset: 0x00009645
	private void OnValidate()
	{
		if (this.sphereTrigger == null)
		{
			this.sphereTrigger = base.GetComponent<SphereCollider>();
		}
		this.UpdateRadius();
	}

	// Token: 0x06000209 RID: 521 RVA: 0x0000B467 File Offset: 0x00009667
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(base.transform.position, this.profile.radius * this.radiusMultiplier * 0.5f);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000B491 File Offset: 0x00009691
	private void Start()
	{
		this.cachedPosition = base.transform.position;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x0000B4A4 File Offset: 0x000096A4
	[ContextMenu("Update Radius")]
	private void UpdateRadius()
	{
		if (this.profile != null && this.sphereTrigger != null)
		{
			float num = this.profile.radius;
			if (base.transform.lossyScale != Vector3.one)
			{
				float num2 = Mathf.Max(new float[]
				{
					base.transform.lossyScale.x,
					base.transform.lossyScale.y,
					base.transform.lossyScale.z
				});
				num /= num2;
			}
			this.sphereTrigger.radius = num * this.radiusMultiplier;
		}
	}

	// Token: 0x0600020C RID: 524 RVA: 0x0000B54F File Offset: 0x0000974F
	private void OnTriggerStay(Collider collider)
	{
		if (!SharedAudioManager.nearbyAudioSources.Contains(this))
		{
			SharedAudioManager.nearbyAudioSources.Add(this);
			this.sphereTrigger.enabled = false;
		}
	}

	// Token: 0x0600020D RID: 525 RVA: 0x0000B575 File Offset: 0x00009775
	public void WasRemoved()
	{
		if (this.sphereTrigger != null)
		{
			this.sphereTrigger.enabled = true;
		}
	}

	// Token: 0x0600020E RID: 526 RVA: 0x0000B594 File Offset: 0x00009794
	public void GetAudioData(Vector3 positionReference, out SharedAudioProfile profile, out Vector3 direction, out float strength)
	{
		profile = this.profile;
		Vector3 vector = this.cachedPosition - positionReference;
		float magnitude = vector.magnitude;
		direction = vector / magnitude;
		if (base.gameObject.activeSelf)
		{
			strength = Mathf.InverseLerp(profile.radius * this.radiusMultiplier, profile.MinDistance, magnitude);
			return;
		}
		strength = 0f;
	}

	// Token: 0x040002AB RID: 683
	private Vector3 cachedPosition;

	// Token: 0x040002AC RID: 684
	public SharedAudioProfile profile;

	// Token: 0x040002AD RID: 685
	public SphereCollider sphereTrigger;

	// Token: 0x040002AE RID: 686
	[Range(0f, 5f)]
	public float radiusMultiplier = 1f;
}

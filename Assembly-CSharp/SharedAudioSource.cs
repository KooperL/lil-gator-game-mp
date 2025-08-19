using System;
using UnityEngine;

public class SharedAudioSource : MonoBehaviour, ISharedAudioSource
{
	// Token: 0x0600024D RID: 589 RVA: 0x00003E75 File Offset: 0x00002075
	private void OnValidate()
	{
		if (this.sphereTrigger == null)
		{
			this.sphereTrigger = base.GetComponent<SphereCollider>();
		}
		this.UpdateRadius();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00003E97 File Offset: 0x00002097
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(base.transform.position, this.profile.radius * this.radiusMultiplier * 0.5f);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00003EC1 File Offset: 0x000020C1
	private void Start()
	{
		this.cachedPosition = base.transform.position;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0001F9A8 File Offset: 0x0001DBA8
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

	// Token: 0x06000251 RID: 593 RVA: 0x00003ED4 File Offset: 0x000020D4
	private void OnTriggerStay(Collider collider)
	{
		if (!SharedAudioManager.nearbyAudioSources.Contains(this))
		{
			SharedAudioManager.nearbyAudioSources.Add(this);
			this.sphereTrigger.enabled = false;
		}
	}

	// Token: 0x06000252 RID: 594 RVA: 0x00003EFA File Offset: 0x000020FA
	public void WasRemoved()
	{
		if (this.sphereTrigger != null)
		{
			this.sphereTrigger.enabled = true;
		}
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0001FA54 File Offset: 0x0001DC54
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

	private Vector3 cachedPosition;

	public SharedAudioProfile profile;

	public SphereCollider sphereTrigger;

	[Range(0f, 5f)]
	public float radiusMultiplier = 1f;
}

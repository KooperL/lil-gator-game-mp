using System;
using UnityEngine;

// Token: 0x020000A1 RID: 161
public class SharedAudioSource : MonoBehaviour, ISharedAudioSource
{
	// Token: 0x06000240 RID: 576 RVA: 0x00003D89 File Offset: 0x00001F89
	private void OnValidate()
	{
		if (this.sphereTrigger == null)
		{
			this.sphereTrigger = base.GetComponent<SphereCollider>();
		}
		this.UpdateRadius();
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00003DAB File Offset: 0x00001FAB
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawSphere(base.transform.position, this.profile.radius * this.radiusMultiplier * 0.5f);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00003DD5 File Offset: 0x00001FD5
	private void Start()
	{
		this.cachedPosition = base.transform.position;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0001EF88 File Offset: 0x0001D188
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

	// Token: 0x06000244 RID: 580 RVA: 0x00003DE8 File Offset: 0x00001FE8
	private void OnTriggerStay(Collider collider)
	{
		if (!SharedAudioManager.nearbyAudioSources.Contains(this))
		{
			SharedAudioManager.nearbyAudioSources.Add(this);
			this.sphereTrigger.enabled = false;
		}
	}

	// Token: 0x06000245 RID: 581 RVA: 0x00003E0E File Offset: 0x0000200E
	public void WasRemoved()
	{
		if (this.sphereTrigger != null)
		{
			this.sphereTrigger.enabled = true;
		}
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0001F034 File Offset: 0x0001D234
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

	// Token: 0x04000337 RID: 823
	private Vector3 cachedPosition;

	// Token: 0x04000338 RID: 824
	public SharedAudioProfile profile;

	// Token: 0x04000339 RID: 825
	public SphereCollider sphereTrigger;

	// Token: 0x0400033A RID: 826
	[Range(0f, 5f)]
	public float radiusMultiplier = 1f;
}

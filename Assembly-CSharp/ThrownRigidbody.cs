using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000107 RID: 263
public class ThrownRigidbody : MonoBehaviour
{
	// Token: 0x0600055E RID: 1374 RVA: 0x0001C762 File Offset: 0x0001A962
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x0001C770 File Offset: 0x0001A970
	private void OnDrawGizmosSelected()
	{
		float num = 0.1f;
		Gizmos.color = Color.blue;
		Vector3 vector = base.transform.position;
		Vector3 vector2 = this.thrownVelocity + 0.5f * num * Physics.gravity;
		for (int i = 0; i < 40; i++)
		{
			Vector3 vector3 = vector;
			vector += num * vector2;
			vector2 += num * Physics.gravity;
			Gizmos.DrawLine(vector3, vector);
		}
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x0001C7EA File Offset: 0x0001A9EA
	[ContextMenu("PrepareToThrow")]
	public void PrepareToThrow()
	{
		this.willThrow = true;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0001C7F3 File Offset: 0x0001A9F3
	private void Start()
	{
		if (this.willThrow)
		{
			CoroutineUtil.Start(this.ThrowCoroutine());
			return;
		}
		this.Land(false);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0001C811 File Offset: 0x0001AA11
	[ContextMenu("Copy To Intended")]
	private void CopyToIntended()
	{
		this.intendedPosition = base.transform.position;
		this.intendedRotation = base.transform.rotation;
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0001C835 File Offset: 0x0001AA35
	private IEnumerator ThrowCoroutine()
	{
		float t = 0f;
		float speed = 5f;
		Vector3 initialPosition = base.transform.position;
		Quaternion initialRotation = base.transform.rotation;
		float velocity = this.thrownVelocity.y;
		float num = initialPosition.y - this.intendedPosition.y;
		float num2;
		if (this.preconfiguredThrowTime > 0f)
		{
			num2 = this.preconfiguredThrowTime;
		}
		else
		{
			num2 = ActorHeldObject.SolveQuadratic(0.5f * Physics.gravity.y, velocity, num);
		}
		if (num2 > 0f)
		{
			speed = 1f / num2;
			float fallingHeight = initialPosition.y;
			while (t < 1f)
			{
				t += speed * Time.deltaTime;
				velocity += Physics.gravity.y * Time.deltaTime;
				fallingHeight += velocity * Time.deltaTime;
				Vector3 vector = Vector3.Lerp(initialPosition, this.intendedPosition, t);
				if (t < 1f)
				{
					vector.y = fallingHeight;
				}
				base.transform.position = vector;
				base.transform.rotation = Quaternion.Slerp(initialRotation, this.intendedRotation, t);
				yield return null;
			}
		}
		else
		{
			while (t < 1f)
			{
				t += speed * Time.deltaTime;
				base.transform.position = Vector3.Lerp(initialPosition, this.intendedPosition, t);
				base.transform.rotation = Quaternion.Slerp(initialRotation, this.intendedRotation, t);
				yield return null;
			}
		}
		this.Land(true);
		yield break;
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0001C844 File Offset: 0x0001AA44
	private void Land(bool playEffects = true)
	{
		base.transform.position = this.intendedPosition;
		base.transform.rotation = this.intendedRotation;
		this.trail.emitting = false;
		base.enabled = false;
		if (playEffects)
		{
			SurfaceMaterial surfaceMaterial;
			RaycastHit raycastHit;
			MaterialManager.m.SampleSurfaceMaterial(this.intendedPosition, Vector3.down, out surfaceMaterial, out raycastHit);
			if (surfaceMaterial != null)
			{
				surfaceMaterial.PlayImpact(raycastHit.point, 1f, 1f);
			}
			Vector3 vector = ((surfaceMaterial != null) ? raycastHit.point : this.intendedPosition);
			EffectsManager.e.FloorDust(vector, 5, Vector3.up);
		}
	}

	// Token: 0x0400075C RID: 1884
	private Rigidbody rigidbody;

	// Token: 0x0400075D RID: 1885
	public Vector3 thrownVelocity;

	// Token: 0x0400075E RID: 1886
	public Vector3 thrownAngularVelocity;

	// Token: 0x0400075F RID: 1887
	public Vector3 intendedPosition;

	// Token: 0x04000760 RID: 1888
	public Quaternion intendedRotation;

	// Token: 0x04000761 RID: 1889
	public float preconfiguredThrowTime = -1f;

	// Token: 0x04000762 RID: 1890
	private float throwTime;

	// Token: 0x04000763 RID: 1891
	private bool isThrown;

	// Token: 0x04000764 RID: 1892
	private float distance = 1000f;

	// Token: 0x04000765 RID: 1893
	public TrailRenderer trail;

	// Token: 0x04000766 RID: 1894
	private bool willThrow;
}

using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200015B RID: 347
public class ThrownRigidbody : MonoBehaviour
{
	// Token: 0x06000670 RID: 1648 RVA: 0x00006A65 File Offset: 0x00004C65
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x00030C38 File Offset: 0x0002EE38
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

	// Token: 0x06000672 RID: 1650 RVA: 0x00006A73 File Offset: 0x00004C73
	[ContextMenu("PrepareToThrow")]
	public void PrepareToThrow()
	{
		this.willThrow = true;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x00006A7C File Offset: 0x00004C7C
	private void Start()
	{
		if (this.willThrow)
		{
			CoroutineUtil.Start(this.ThrowCoroutine());
			return;
		}
		this.Land(false);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00006A9A File Offset: 0x00004C9A
	[ContextMenu("Copy To Intended")]
	private void CopyToIntended()
	{
		this.intendedPosition = base.transform.position;
		this.intendedRotation = base.transform.rotation;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00006ABE File Offset: 0x00004CBE
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

	// Token: 0x06000676 RID: 1654 RVA: 0x00030CB4 File Offset: 0x0002EEB4
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

	// Token: 0x040008A7 RID: 2215
	private Rigidbody rigidbody;

	// Token: 0x040008A8 RID: 2216
	public Vector3 thrownVelocity;

	// Token: 0x040008A9 RID: 2217
	public Vector3 thrownAngularVelocity;

	// Token: 0x040008AA RID: 2218
	public Vector3 intendedPosition;

	// Token: 0x040008AB RID: 2219
	public Quaternion intendedRotation;

	// Token: 0x040008AC RID: 2220
	public float preconfiguredThrowTime = -1f;

	// Token: 0x040008AD RID: 2221
	private float throwTime;

	// Token: 0x040008AE RID: 2222
	private bool isThrown;

	// Token: 0x040008AF RID: 2223
	private float distance = 1000f;

	// Token: 0x040008B0 RID: 2224
	public TrailRenderer trail;

	// Token: 0x040008B1 RID: 2225
	private bool willThrow;
}

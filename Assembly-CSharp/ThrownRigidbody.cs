using System;
using System.Collections;
using UnityEngine;

public class ThrownRigidbody : MonoBehaviour
{
	// Token: 0x060006AA RID: 1706 RVA: 0x00006D2B File Offset: 0x00004F2B
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00032334 File Offset: 0x00030534
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

	// Token: 0x060006AC RID: 1708 RVA: 0x00006D39 File Offset: 0x00004F39
	[ContextMenu("PrepareToThrow")]
	public void PrepareToThrow()
	{
		this.willThrow = true;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00006D42 File Offset: 0x00004F42
	private void Start()
	{
		if (this.willThrow)
		{
			CoroutineUtil.Start(this.ThrowCoroutine());
			return;
		}
		this.Land(false);
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00006D60 File Offset: 0x00004F60
	[ContextMenu("Copy To Intended")]
	private void CopyToIntended()
	{
		this.intendedPosition = base.transform.position;
		this.intendedRotation = base.transform.rotation;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00006D84 File Offset: 0x00004F84
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

	// Token: 0x060006B0 RID: 1712 RVA: 0x000323B0 File Offset: 0x000305B0
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

	private Rigidbody rigidbody;

	public Vector3 thrownVelocity;

	public Vector3 thrownAngularVelocity;

	public Vector3 intendedPosition;

	public Quaternion intendedRotation;

	public float preconfiguredThrowTime = -1f;

	private float throwTime;

	private bool isThrown;

	private float distance = 1000f;

	public TrailRenderer trail;

	private bool willThrow;
}

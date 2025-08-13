using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class RagdollController : MonoBehaviour
{
	// Token: 0x06000AED RID: 2797 RVA: 0x00036644 File Offset: 0x00034844
	private void OnValidate()
	{
		if (this.colliders == null || this.colliders.Length == 0)
		{
			List<Collider> list = new List<Collider>();
			foreach (Rigidbody rigidbody in this.rigidbodies)
			{
				list.AddRange(rigidbody.GetComponents<Collider>());
			}
			this.colliders = list.ToArray();
		}
	}

	// Token: 0x06000AEE RID: 2798 RVA: 0x0003669C File Offset: 0x0003489C
	private void Awake()
	{
		Rigidbody[] array = this.rigidbodies;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].isKinematic = true;
		}
		Collider[] array2 = this.colliders;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = false;
		}
		base.enabled = false;
	}

	// Token: 0x06000AEF RID: 2799 RVA: 0x000366EC File Offset: 0x000348EC
	public void OnEnable()
	{
		this.actor.isRagdolling = true;
		this.rigidbody = base.GetComponent<Rigidbody>();
		Vector3 vector = this.movementRigidbody.velocity;
		float num = Mathf.Lerp(0.25f, 2f, vector.magnitude / 5f);
		if (this.rigidbodyParents == null || this.rigidbodyParents.Length == 0)
		{
			this.rigidbodyParents = new Transform[this.rigidbodies.Length];
			for (int i = 0; i < this.rigidbodies.Length; i++)
			{
				this.rigidbodyParents[i] = this.rigidbodies[i].transform.parent;
			}
		}
		foreach (Rigidbody rigidbody in this.rigidbodies)
		{
			rigidbody.isKinematic = false;
			rigidbody.velocity = vector + num * Random.insideUnitSphere;
		}
		Collider[] array2 = this.colliders;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].enabled = true;
		}
		ImpactSound[] array3 = this.impactSounds;
		for (int j = 0; j < array3.Length; j++)
		{
			array3[j].enabled = true;
		}
		if (this.isFlat)
		{
			KeepUpright[] array4 = this.flatUprights;
			for (int j = 0; j < array4.Length; j++)
			{
				array4[j].enabled = true;
			}
		}
		else
		{
			KeepUpright[] array4 = this.ragdollUprights;
			for (int j = 0; j < array4.Length; j++)
			{
				array4[j].enabled = true;
			}
		}
		this.movementRigidbody.isKinematic = true;
		this.animator.keepAnimatorControllerStateOnDisable = false;
		this.movement.stepsSinceRagdolling = 0;
		this.movement.enabled = false;
		if (this.toAnimator != null)
		{
			this.toAnimator.enabled = false;
		}
		this.parent = base.transform.parent;
		foreach (Rigidbody rigidbody2 in this.rigidbodies)
		{
			rigidbody2.transform.parent = null;
			rigidbody2.transform.localScale = Vector3.one;
		}
		this.animator.enabled = false;
		this.scrapingObject.SetActive(true);
		this.collisionTime = -1f;
		this.enabledTime = Time.time;
	}

	// Token: 0x06000AF0 RID: 2800 RVA: 0x00036928 File Offset: 0x00034B28
	private void Update()
	{
		if (Player.input.cancelAction || (!Game.HasControl && !Player.movement.moddedWithoutControl))
		{
			this.Deactivate();
			return;
		}
	}

	// Token: 0x06000AF1 RID: 2801 RVA: 0x00036950 File Offset: 0x00034B50
	public void Jump()
	{
		this.Deactivate();
		if (this.isAttached || Time.time - this.collisionTime < 0.05f)
		{
			this.isJumpingOut = true;
		}
	}

	// Token: 0x06000AF2 RID: 2802 RVA: 0x0003697C File Offset: 0x00034B7C
	private void FixedUpdate()
	{
		Vector3 vector = this.rigidbody.velocity;
		float num = 0f;
		Vector3 vector2 = (vector - this.velocity) / Time.deltaTime;
		if (vector.sqrMagnitude > 0.1f)
		{
			num = vector.magnitude;
		}
		this.movementRigidbody.position = this.rigidbody.position;
		Vector3 vector3 = Player.input.inputDirection;
		Vector3 vector4 = Vector3.Cross(Vector3.up, Player.input.inputDirection);
		Vector3 vector5 = this.torque * vector4;
		Vector3.Dot(vector3, vector);
		if (Player.input.inputDirection != Vector3.zero && vector3.sqrMagnitude > 0.1f)
		{
			if (this.isFlat)
			{
				vector3 *= this.flatForce;
				this.headRigidbody.AddForce(vector3);
				this.rigidbody.AddForce(0.5f * vector3);
			}
			else
			{
				vector3 *= this.force;
				foreach (Rigidbody rigidbody in this.rigidbodies)
				{
					rigidbody.AddForce(vector3);
					rigidbody.AddTorque(vector5);
				}
			}
		}
		float num2 = 0.5f;
		if (this.lastImpactTime + this.impactDelay < Time.time && (this.speed > 2f || num > 2f) && this.acceleration.magnitude > this.impactAccel)
		{
			num2 = 1f;
			this.lastImpactTime = Time.time;
			Vector3 vector6 = ((this.velocity.sqrMagnitude > vector.sqrMagnitude) ? this.velocity : (-vector));
			vector6.Normalize();
			Debug.DrawRay(this.rigidbody.position, vector6);
			SurfaceMaterial surfaceMaterial = MaterialManager.m.SampleSurfaceMaterial(this.rigidbody.position + 0.25f * Vector3.down, vector6);
			if (surfaceMaterial != null)
			{
				surfaceMaterial.PlayImpact(this.rigidbody.position, 0.5f, 1f);
			}
			if (this.doImactEffects)
			{
				EffectsManager.e.Dust(this.rigidbody.position, 5);
			}
		}
		if (num > this.minTrailSpeed)
		{
			num2 = 1f;
			this.trailCounter += num * Time.deltaTime;
		}
		if (this.trailCounter > this.trailDistance)
		{
			this.trailCounter -= this.trailDistance;
			if (this.doDustTrail)
			{
				EffectsManager.e.Dust(this.rigidbody.position, 1, 0.5f * vector, 0f);
			}
		}
		if (num > this.minHitSpeed != this.hitTrigger.activeSelf)
		{
			this.hitTrigger.SetActive(num > this.minHitSpeed);
		}
		if (this.lockMouth)
		{
			num2 = this.lockMouthOpenness;
		}
		this.mouthSmooth = Mathf.SmoothDamp(this.mouthSmooth, num2, ref this.mouthSmoothVel, (num2 > this.mouthSmooth) ? 0.05f : 0.5f);
		this.actor.SetJawOpen(this.mouthSmooth);
		this.velocity = vector;
		this.speed = num;
		this.acceleration = vector2;
	}

	// Token: 0x06000AF3 RID: 2803 RVA: 0x00036CBF File Offset: 0x00034EBF
	public void Deactivate()
	{
		if (this.isDeactivating)
		{
			return;
		}
		CoroutineUtil.Start(this.BufferedDeactivate());
	}

	// Token: 0x06000AF4 RID: 2804 RVA: 0x00036CD6 File Offset: 0x00034ED6
	private IEnumerator BufferedDeactivate()
	{
		this.isDeactivating = true;
		yield return new WaitForEndOfFrame();
		if (!this.isDeactivating)
		{
			yield break;
		}
		this.isDeactivating = false;
		this.actor.isRagdolling = false;
		this.hitTrigger.SetActive(false);
		this.scrapingObject.SetActive(false);
		this.toAnimator.enabled = true;
		this.toAnimator.ReadRagdollTransforms();
		Vector3 vector = Vector3.zero;
		foreach (Rigidbody rigidbody in this.rigidbodies)
		{
			vector += rigidbody.velocity;
			rigidbody.isKinematic = true;
		}
		Collider[] array2 = this.colliders;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].enabled = false;
		}
		ImpactSound[] array3 = this.impactSounds;
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i].enabled = false;
		}
		KeepUpright[] array4 = this.flatUprights;
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i].enabled = false;
		}
		array4 = this.ragdollUprights;
		for (int i = 0; i < array4.Length; i++)
		{
			array4[i].enabled = false;
		}
		vector /= (float)this.rigidbodies.Length;
		this.movementRigidbody.isKinematic = false;
		this.movement.isRagdolling = false;
		this.movementRigidbody.velocity = vector;
		this.movement.velocity = vector;
		this.movement.enabled = true;
		base.transform.parent = this.parent;
		for (int j = 0; j < this.rigidbodies.Length; j++)
		{
			this.rigidbodies[j].transform.parent = this.rigidbodyParents[j];
		}
		this.animator.Rebind();
		this.animator.enabled = true;
		this.movement.ClearMods();
		base.enabled = false;
		if (Time.time - this.collisionTime < 0.05f)
		{
			this.movement.stepsSinceLastGrounded = -10;
		}
		if (this.isJumpingOut)
		{
			Player.movement.Jump(true);
			this.isAttached = false;
			this.isJumpingOut = false;
		}
		yield break;
	}

	// Token: 0x06000AF5 RID: 2805 RVA: 0x00036CE5 File Offset: 0x00034EE5
	private void OnCollisionStay(Collision collision)
	{
		if (Time.time - this.enabledTime > 0.25f)
		{
			this.collisionTime = Time.time;
		}
	}

	// Token: 0x06000AF6 RID: 2806 RVA: 0x00036D08 File Offset: 0x00034F08
	public Vector3 GetForward()
	{
		Vector3 vector;
		if (Time.inFixedTimeStep)
		{
			vector = Quaternion.FromToRotation(this.rigidbody.rotation * Vector3.up, Vector3.up) * this.rigidbody.rotation * Vector3.forward;
		}
		else
		{
			vector = Quaternion.FromToRotation(base.transform.up, Vector3.up) * base.transform.forward;
		}
		vector.y = 0f;
		if (vector != Vector3.zero)
		{
			vector.Normalize();
		}
		return vector;
	}

	// Token: 0x06000AF7 RID: 2807 RVA: 0x00036DA0 File Offset: 0x00034FA0
	public void SetPosition(Vector3 newPosition)
	{
		Vector3 vector = newPosition - this.rigidbodies[0].position;
		foreach (Rigidbody rigidbody in this.rigidbodies)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.position += vector;
		}
	}

	// Token: 0x04000E78 RID: 3704
	public Transform[] rigidbodyParents;

	// Token: 0x04000E79 RID: 3705
	public Rigidbody[] rigidbodies;

	// Token: 0x04000E7A RID: 3706
	public Collider[] colliders;

	// Token: 0x04000E7B RID: 3707
	public ImpactSound[] impactSounds;

	// Token: 0x04000E7C RID: 3708
	private Rigidbody rigidbody;

	// Token: 0x04000E7D RID: 3709
	public Rigidbody headRigidbody;

	// Token: 0x04000E7E RID: 3710
	public KeepUpright[] ragdollUprights;

	// Token: 0x04000E7F RID: 3711
	public KeepUpright[] flatUprights;

	// Token: 0x04000E80 RID: 3712
	public PlayerMovement movement;

	// Token: 0x04000E81 RID: 3713
	public Rigidbody movementRigidbody;

	// Token: 0x04000E82 RID: 3714
	public Animator animator;

	// Token: 0x04000E83 RID: 3715
	public RuntimeAnimatorController ragdollController;

	// Token: 0x04000E84 RID: 3716
	public RuntimeAnimatorController playerController;

	// Token: 0x04000E85 RID: 3717
	public DialogueActor actor;

	// Token: 0x04000E86 RID: 3718
	private Transform parent;

	// Token: 0x04000E87 RID: 3719
	public float force = 20f;

	// Token: 0x04000E88 RID: 3720
	public float torque = 100f;

	// Token: 0x04000E89 RID: 3721
	public RagdollToAnimator toAnimator;

	// Token: 0x04000E8A RID: 3722
	private float collisionTime;

	// Token: 0x04000E8B RID: 3723
	public GameObject hitTrigger;

	// Token: 0x04000E8C RID: 3724
	public float minHitSpeed = 2f;

	// Token: 0x04000E8D RID: 3725
	public bool isFlat = true;

	// Token: 0x04000E8E RID: 3726
	public float flatForce = 60f;

	// Token: 0x04000E8F RID: 3727
	public float maxForcedSpeed = 4f;

	// Token: 0x04000E90 RID: 3728
	[Header("Effects")]
	public bool doDustTrail = true;

	// Token: 0x04000E91 RID: 3729
	public float minTrailSpeed = 10f;

	// Token: 0x04000E92 RID: 3730
	public float trailDistance = 0.5f;

	// Token: 0x04000E93 RID: 3731
	private float trailCounter;

	// Token: 0x04000E94 RID: 3732
	private Vector3 velocity;

	// Token: 0x04000E95 RID: 3733
	private Vector3 acceleration;

	// Token: 0x04000E96 RID: 3734
	private float speed;

	// Token: 0x04000E97 RID: 3735
	[Header("Impact")]
	public bool doImactEffects = true;

	// Token: 0x04000E98 RID: 3736
	public float impactAccel = 5f;

	// Token: 0x04000E99 RID: 3737
	public float impactDelay = 0.25f;

	// Token: 0x04000E9A RID: 3738
	private float lastImpactTime = -1f;

	// Token: 0x04000E9B RID: 3739
	[Header("Scraping")]
	public bool doScrapingEffect = true;

	// Token: 0x04000E9C RID: 3740
	public GameObject scrapingObject;

	// Token: 0x04000E9D RID: 3741
	public float minScrapingSpeed = 3f;

	// Token: 0x04000E9E RID: 3742
	private float mouthSmooth;

	// Token: 0x04000E9F RID: 3743
	private float mouthSmoothVel;

	// Token: 0x04000EA0 RID: 3744
	public bool lockMouth;

	// Token: 0x04000EA1 RID: 3745
	public float lockMouthOpenness;

	// Token: 0x04000EA2 RID: 3746
	[ReadOnly]
	public bool isAttached;

	// Token: 0x04000EA3 RID: 3747
	private float enabledTime = -1f;

	// Token: 0x04000EA4 RID: 3748
	private bool isJumpingOut;

	// Token: 0x04000EA5 RID: 3749
	public bool isDeactivating;
}

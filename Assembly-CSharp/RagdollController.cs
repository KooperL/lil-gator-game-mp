using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000287 RID: 647
public class RagdollController : MonoBehaviour
{
	// Token: 0x06000CA6 RID: 3238 RVA: 0x0004756C File Offset: 0x0004576C
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

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000475C4 File Offset: 0x000457C4
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

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00047614 File Offset: 0x00045814
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

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0000BD2F File Offset: 0x00009F2F
	private void Update()
	{
		if (Player.input.cancelAction || (!Game.HasControl && !Player.movement.moddedWithoutControl))
		{
			this.Deactivate();
			return;
		}
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0000BD57 File Offset: 0x00009F57
	public void Jump()
	{
		this.Deactivate();
		if (this.isAttached || Time.time - this.collisionTime < 0.05f)
		{
			this.isJumpingOut = true;
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00047850 File Offset: 0x00045A50
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

	// Token: 0x06000CAC RID: 3244 RVA: 0x0000BD81 File Offset: 0x00009F81
	public void Deactivate()
	{
		if (this.isDeactivating)
		{
			return;
		}
		CoroutineUtil.Start(this.BufferedDeactivate());
	}

	// Token: 0x06000CAD RID: 3245 RVA: 0x0000BD98 File Offset: 0x00009F98
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

	// Token: 0x06000CAE RID: 3246 RVA: 0x0000BDA7 File Offset: 0x00009FA7
	private void OnCollisionStay(Collision collision)
	{
		if (Time.time - this.enabledTime > 0.25f)
		{
			this.collisionTime = Time.time;
		}
	}

	// Token: 0x06000CAF RID: 3247 RVA: 0x00047B94 File Offset: 0x00045D94
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

	// Token: 0x06000CB0 RID: 3248 RVA: 0x00047C2C File Offset: 0x00045E2C
	public void SetPosition(Vector3 newPosition)
	{
		Vector3 vector = newPosition - this.rigidbodies[0].position;
		foreach (Rigidbody rigidbody in this.rigidbodies)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.position += vector;
		}
	}

	// Token: 0x040010D0 RID: 4304
	public Transform[] rigidbodyParents;

	// Token: 0x040010D1 RID: 4305
	public Rigidbody[] rigidbodies;

	// Token: 0x040010D2 RID: 4306
	public Collider[] colliders;

	// Token: 0x040010D3 RID: 4307
	public ImpactSound[] impactSounds;

	// Token: 0x040010D4 RID: 4308
	private Rigidbody rigidbody;

	// Token: 0x040010D5 RID: 4309
	public Rigidbody headRigidbody;

	// Token: 0x040010D6 RID: 4310
	public KeepUpright[] ragdollUprights;

	// Token: 0x040010D7 RID: 4311
	public KeepUpright[] flatUprights;

	// Token: 0x040010D8 RID: 4312
	public PlayerMovement movement;

	// Token: 0x040010D9 RID: 4313
	public Rigidbody movementRigidbody;

	// Token: 0x040010DA RID: 4314
	public Animator animator;

	// Token: 0x040010DB RID: 4315
	public RuntimeAnimatorController ragdollController;

	// Token: 0x040010DC RID: 4316
	public RuntimeAnimatorController playerController;

	// Token: 0x040010DD RID: 4317
	public DialogueActor actor;

	// Token: 0x040010DE RID: 4318
	private Transform parent;

	// Token: 0x040010DF RID: 4319
	public float force = 20f;

	// Token: 0x040010E0 RID: 4320
	public float torque = 100f;

	// Token: 0x040010E1 RID: 4321
	public RagdollToAnimator toAnimator;

	// Token: 0x040010E2 RID: 4322
	private float collisionTime;

	// Token: 0x040010E3 RID: 4323
	public GameObject hitTrigger;

	// Token: 0x040010E4 RID: 4324
	public float minHitSpeed = 2f;

	// Token: 0x040010E5 RID: 4325
	public bool isFlat = true;

	// Token: 0x040010E6 RID: 4326
	public float flatForce = 60f;

	// Token: 0x040010E7 RID: 4327
	public float maxForcedSpeed = 4f;

	// Token: 0x040010E8 RID: 4328
	[Header("Effects")]
	public bool doDustTrail = true;

	// Token: 0x040010E9 RID: 4329
	public float minTrailSpeed = 10f;

	// Token: 0x040010EA RID: 4330
	public float trailDistance = 0.5f;

	// Token: 0x040010EB RID: 4331
	private float trailCounter;

	// Token: 0x040010EC RID: 4332
	private Vector3 velocity;

	// Token: 0x040010ED RID: 4333
	private Vector3 acceleration;

	// Token: 0x040010EE RID: 4334
	private float speed;

	// Token: 0x040010EF RID: 4335
	[Header("Impact")]
	public bool doImactEffects = true;

	// Token: 0x040010F0 RID: 4336
	public float impactAccel = 5f;

	// Token: 0x040010F1 RID: 4337
	public float impactDelay = 0.25f;

	// Token: 0x040010F2 RID: 4338
	private float lastImpactTime = -1f;

	// Token: 0x040010F3 RID: 4339
	[Header("Scraping")]
	public bool doScrapingEffect = true;

	// Token: 0x040010F4 RID: 4340
	public GameObject scrapingObject;

	// Token: 0x040010F5 RID: 4341
	public float minScrapingSpeed = 3f;

	// Token: 0x040010F6 RID: 4342
	private float mouthSmooth;

	// Token: 0x040010F7 RID: 4343
	private float mouthSmoothVel;

	// Token: 0x040010F8 RID: 4344
	public bool lockMouth;

	// Token: 0x040010F9 RID: 4345
	public float lockMouthOpenness;

	// Token: 0x040010FA RID: 4346
	[ReadOnly]
	public bool isAttached;

	// Token: 0x040010FB RID: 4347
	private float enabledTime = -1f;

	// Token: 0x040010FC RID: 4348
	private bool isJumpingOut;

	// Token: 0x040010FD RID: 4349
	public bool isDeactivating;
}

using System;
using UnityEngine;

public class Slide : GenericPath, ICustomPlayerMovement
{
	// Token: 0x0600030C RID: 780 RVA: 0x00024148 File Offset: 0x00022348
	private void OnTriggerStay(Collider other)
	{
		if (!base.enabled && Time.time - this.lastEnabled > 0.5f && !Player.movement.JustCanceled)
		{
			Vector3 position = Player.movement.transform.position;
			int closest = base.GetClosest(position);
			Vector3 direction = base.GetDirection(closest);
			this.speed = Vector3.Dot(Player.movement.rigidbody.velocity, direction);
			float num = Vector3.Dot(PlayerInput.p.inputDirection, direction);
			if (this.speed > 3f && num > 0.85f)
			{
				this.t = base.GetClosestInterpolated(position, closest);
				if (this.t > (float)this.positions.Length - 2.2f)
				{
					return;
				}
				Vector3 position2 = base.GetPosition(this.t);
				if (Mathf.Abs(position.y - position2.y) > 0.2f)
				{
					return;
				}
				Player.movement.ForceModdedState();
				base.enabled = true;
			}
		}
	}

	// Token: 0x0600030D RID: 781 RVA: 0x0002424C File Offset: 0x0002244C
	private void OnEnable()
	{
		Player.movement.isModified = true;
		Player.movement.modJumpRule = (this.jumpOff ? PlayerMovement.ModRule.Cancels : PlayerMovement.ModRule.Locked);
		Player.movement.modGlideRule = PlayerMovement.ModRule.Locked;
		Player.movement.modPrimaryRule = PlayerMovement.ModRule.Allowed;
		Player.movement.modSecondaryRule = PlayerMovement.ModRule.Locked;
		Player.movement.modItemRule = PlayerMovement.ModRule.Locked;
		Player.movement.modCustomMovement = true;
		Player.movement.modCustomMovementScript = this;
		Player.movement.modNoClimbing = true;
		Player.movement.modDisableCollision = true;
		Player.movement.modIgnoreGroundedness = true;
		Player.overrideAnimations.SetContextualAnimations(this.animations);
		Player.footsteps.overrideSettings = true;
		Player.footsteps.overrideHasVisualFootsteps = false;
		Player.footsteps.overrideHasFootstepDust = false;
		Player.movement.recoveringControl = 0f;
		if (this.lockedCamera)
		{
			this.lockedCameraT = base.GetClosestInterpolated(MainCamera.t.position);
			this.UpdateCameraPos();
			this.lockedCameraRotationalVelocity = new Quaternion(0f, 0f, 0f, 0f);
			this.lockedCameraTVelocity = 0f;
			this.camera.transform.rotation = Quaternion.LookRotation(base.GetDirection(this.lockedCameraT));
			this.camera.SetActive(true);
		}
		this.lastT = this.t;
		if (this.surfaceMaterial != null)
		{
			ScrapingSFX s = ScrapingSFX.s;
			if (s == null)
			{
				return;
			}
			s.SetOverride(this.surfaceMaterial.scraping);
		}
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0000463F File Offset: 0x0000283F
	private void OnDisable()
	{
		Player.footsteps.ClearOverride();
		Player.movement.ClampSpeedForABit();
		if (this.lockedCamera)
		{
			this.camera.SetActive(false);
		}
		ScrapingSFX s = ScrapingSFX.s;
		if (s == null)
		{
			return;
		}
		s.ClearOverride();
	}

	// Token: 0x0600030F RID: 783 RVA: 0x000243CC File Offset: 0x000225CC
	public void MovementUpdate(Vector3 input, ref Vector3 position, ref Vector3 velocity, ref Vector3 direction, ref Vector3 up, ref float animationIndex)
	{
		this.lastT = this.t;
		animationIndex = 0f;
		Vector3 position2 = base.GetPosition(this.t);
		direction = base.GetDirection(this.t);
		position = position2;
		this.speed = Mathf.MoveTowards(this.speed, this.minSpeed, Mathf.Max(this.minSpeed, this.speed) * this.friction * Time.deltaTime);
		this.speed += Mathf.Max(0f, direction.y * Physics.gravity.y * Time.deltaTime);
		this.t = base.MoveAlongPath(this.t, this.speed * Time.deltaTime);
		Vector3 position3 = base.GetPosition(this.t);
		velocity = (position3 - position) / Time.fixedDeltaTime;
		if (this.t >= (float)(this.positions.Length - 1))
		{
			Player.movement.ClearMods();
		}
	}

	// Token: 0x06000310 RID: 784 RVA: 0x000244DC File Offset: 0x000226DC
	private void Update()
	{
		if (this.lockedCamera)
		{
			float num = Mathf.Lerp(this.lastT, this.t, (Time.time - Time.fixedTime) / Time.fixedDeltaTime);
			float num2 = base.MoveAlongPath(num, -this.cameraDistance);
			this.lockedCameraT = Mathf.SmoothDamp(this.lockedCameraT, num2, ref this.lockedCameraTVelocity, 0.3f);
			this.UpdateCameraPos();
		}
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00024548 File Offset: 0x00022748
	private void UpdateCameraPos()
	{
		this.camera.transform.position = base.GetPosition(this.lockedCameraT) + this.cameraOffset;
		Vector3 vector = Player.Position + new Vector3(0f, 0.25f, 0f) - this.camera.transform.position;
		if (this.lockedCameraT < 1f)
		{
			vector = Vector3.Slerp(base.GetDirection(0f), vector, this.lockedCameraT);
		}
		this.camera.transform.rotation = QuaternionUtil.SmoothDamp(this.camera.transform.rotation, Quaternion.LookRotation(vector), ref this.lockedCameraRotationalVelocity, 0.3f);
	}

	// Token: 0x06000312 RID: 786 RVA: 0x00004678 File Offset: 0x00002878
	public void Cancel()
	{
		this.lastEnabled = Time.time;
		base.enabled = false;
	}

	public float friction = 0.2f;

	public float minSpeed = 1f;

	private Rigidbody rigidbody;

	private float t;

	private float lastT;

	private Vector3 velocity;

	private float speed;

	private float lastEnabled = -1f;

	public bool jumpOff = true;

	public AnimationSet animations;

	public bool lockedCamera;

	[ConditionalHide("lockedCamera", true)]
	public GameObject camera;

	[ConditionalHide("lockedCamera", true)]
	public float cameraDistance = 1.5f;

	public Vector3 cameraOffset = 0.5f * Vector3.up;

	private float lockedCameraT;

	private float lockedCameraTVelocity;

	private Quaternion lockedCameraRotationalVelocity;

	public SurfaceMaterial surfaceMaterial;

	public AudioSource scrapeSource;
}

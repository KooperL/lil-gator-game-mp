using System;
using UnityEngine;

// Token: 0x020000BB RID: 187
public class Slide : GenericPath, ICustomPlayerMovement
{
	// Token: 0x060002FF RID: 767 RVA: 0x000236F0 File Offset: 0x000218F0
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

	// Token: 0x06000300 RID: 768 RVA: 0x000237F4 File Offset: 0x000219F4
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

	// Token: 0x06000301 RID: 769 RVA: 0x00004553 File Offset: 0x00002753
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

	// Token: 0x06000302 RID: 770 RVA: 0x00023974 File Offset: 0x00021B74
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

	// Token: 0x06000303 RID: 771 RVA: 0x00023A84 File Offset: 0x00021C84
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

	// Token: 0x06000304 RID: 772 RVA: 0x00023AF0 File Offset: 0x00021CF0
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

	// Token: 0x06000305 RID: 773 RVA: 0x0000458C File Offset: 0x0000278C
	public void Cancel()
	{
		this.lastEnabled = Time.time;
		base.enabled = false;
	}

	// Token: 0x04000442 RID: 1090
	public float friction = 0.2f;

	// Token: 0x04000443 RID: 1091
	public float minSpeed = 1f;

	// Token: 0x04000444 RID: 1092
	private Rigidbody rigidbody;

	// Token: 0x04000445 RID: 1093
	private float t;

	// Token: 0x04000446 RID: 1094
	private float lastT;

	// Token: 0x04000447 RID: 1095
	private Vector3 velocity;

	// Token: 0x04000448 RID: 1096
	private float speed;

	// Token: 0x04000449 RID: 1097
	private float lastEnabled = -1f;

	// Token: 0x0400044A RID: 1098
	public bool jumpOff = true;

	// Token: 0x0400044B RID: 1099
	public AnimationSet animations;

	// Token: 0x0400044C RID: 1100
	public bool lockedCamera;

	// Token: 0x0400044D RID: 1101
	[ConditionalHide("lockedCamera", true)]
	public GameObject camera;

	// Token: 0x0400044E RID: 1102
	[ConditionalHide("lockedCamera", true)]
	public float cameraDistance = 1.5f;

	// Token: 0x0400044F RID: 1103
	public Vector3 cameraOffset = 0.5f * Vector3.up;

	// Token: 0x04000450 RID: 1104
	private float lockedCameraT;

	// Token: 0x04000451 RID: 1105
	private float lockedCameraTVelocity;

	// Token: 0x04000452 RID: 1106
	private Quaternion lockedCameraRotationalVelocity;

	// Token: 0x04000453 RID: 1107
	public SurfaceMaterial surfaceMaterial;

	// Token: 0x04000454 RID: 1108
	public AudioSource scrapeSource;
}

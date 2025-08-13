using System;
using Cinemachine;
using UnityEngine;

// Token: 0x0200027D RID: 637
public class PlayerOrbitCamera : MonoBehaviour
{
	// Token: 0x1700013A RID: 314
	// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0000BA2E File Offset: 0x00009C2E
	public float CameraSmoothing
	{
		get
		{
			return this.cameraSmoothing * PlayerOrbitCamera.cameraSmoothingFactor;
		}
	}

	// Token: 0x1700013B RID: 315
	// (get) Token: 0x06000C73 RID: 3187 RVA: 0x00045E9C File Offset: 0x0004409C
	private Vector3 CenterPoint
	{
		get
		{
			if (Player.movement.isRagdolling)
			{
				return this.ragdollCenter.position + 0.5f * this.centerPoint;
			}
			if (!Game.ignoreDialogueDepth && DialogueManager.d.HasFocusPoint)
			{
				return DialogueManager.d.DialogueFocusPoint;
			}
			return Player.transform.TransformPoint((this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static) ? this.centerPoint : ((this.cameraMode == PlayerOrbitCamera.CameraMode.Forward) ? this.c_centerPoint : this.b_centerPoint));
		}
	}

	// Token: 0x1700013C RID: 316
	// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0000BA3C File Offset: 0x00009C3C
	private Vector3 CameraOffset
	{
		get
		{
			return this.cameraOffset + Vector3.right * this.currentLean;
		}
	}

	// Token: 0x1700013D RID: 317
	// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00045F30 File Offset: 0x00044130
	public float DistanceMultiplier
	{
		get
		{
			if (Player.itemManager.IsAiming)
			{
				return this.aimDistanceMultiplier;
			}
			float num = this.distanceMultiplier;
			if (Game.State != GameState.Dialogue)
			{
				num = PlayerOrbitCamera.gameplayDistanceMultiplier;
			}
			if (PlayerCameraDistanceZone.activeZones.Count > 0)
			{
				int num2 = -1000;
				foreach (PlayerCameraDistanceZone playerCameraDistanceZone in PlayerCameraDistanceZone.activeZones)
				{
					if (playerCameraDistanceZone.priority > num2)
					{
						num2 = playerCameraDistanceZone.priority;
						num = Mathf.Min(num, playerCameraDistanceZone.distanceMultiplier);
					}
				}
			}
			this.mostRecentDistanceMultiplier = num;
			return num;
		}
	}

	// Token: 0x1700013E RID: 318
	// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0000BA59 File Offset: 0x00009C59
	public bool IsAutoCameraActive
	{
		get
		{
			return Time.time > this.autoCameraActiveTime;
		}
	}

	// Token: 0x06000C77 RID: 3191 RVA: 0x0000BA68 File Offset: 0x00009C68
	public void ForceAutoCamera()
	{
		this.autoCameraActiveTime = -1f;
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x00045FDC File Offset: 0x000441DC
	public void SetCameraMode(PlayerOrbitCamera.CameraMode cameraMode)
	{
		if (CameraCollidePlayer.c != null)
		{
			switch (cameraMode)
			{
			case PlayerOrbitCamera.CameraMode.Off:
				CameraCollidePlayer.c.SetLockMode(CameraCollidePlayer.LockMode.None);
				break;
			case PlayerOrbitCamera.CameraMode.Forward:
				CameraCollidePlayer.c.SetLockMode(CameraCollidePlayer.LockMode.LockedOn);
				break;
			case PlayerOrbitCamera.CameraMode.Backward:
				CameraCollidePlayer.c.SetLockMode(CameraCollidePlayer.LockMode.LockedOff);
				break;
			case PlayerOrbitCamera.CameraMode.Static:
				CameraCollidePlayer.c.SetLockMode(CameraCollidePlayer.LockMode.None);
				this.GetOrientationFromCamera();
				break;
			}
		}
		this.cameraMode = cameraMode;
	}

	// Token: 0x06000C79 RID: 3193 RVA: 0x0000BA75 File Offset: 0x00009C75
	private void OnEnable()
	{
		PlayerOrbitCamera.active = this;
	}

	// Token: 0x06000C7A RID: 3194 RVA: 0x00046050 File Offset: 0x00044250
	private void Start()
	{
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
		this.virtualCameraNoise = this.virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		this.virtualCameraLens = this.virtualCamera.m_Lens;
		this.originalFov = this.virtualCameraLens.FieldOfView;
		this.noiseAmplitude = this.virtualCameraNoise.m_AmplitudeGain;
		this.playerInput = Player.input;
		this.playerMovement = Player.movement;
		this.playerTransform = Player.transform;
		this.mainCamera = MainCamera.t;
		this.camera = MainCamera.c;
		this.brain = this.mainCamera.GetComponent<CinemachineBrain>();
		this.GetOrientationFromCamera();
		if (this.persistentRotation)
		{
			this.smoothRotation.y = (this.rotation.y = GameData.g.ReadFloat(this.rotationKey, this.rotation.y));
		}
		this.smoothedCenterPoint = base.transform.parent.position + this.centerPoint;
		this.framesSinceTrigger = 30;
		this.stepsSinceStart = 0;
		this.adaptedDistance = this.DistanceFromFocalPoint();
		this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x0000BA7D File Offset: 0x00009C7D
	private void FixedUpdate()
	{
		this.framesSinceTrigger++;
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x0000BA8D File Offset: 0x00009C8D
	public void ReCenterCamera(bool justEnabled = false)
	{
		if (justEnabled)
		{
			this.autoCameraActiveTime = Time.time;
		}
		this.reCenterCameraTime = Time.time + 0.5f;
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x0000BAAE File Offset: 0x00009CAE
	public void CancelReCenterCamera()
	{
		this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
		this.reCenterCameraTime = -1f;
	}

	// Token: 0x06000C7E RID: 3198 RVA: 0x00046188 File Offset: 0x00044388
	private void LateUpdate()
	{
		if ((this.brain.ActiveVirtualCamera != null && !(this.brain.ActiveVirtualCamera.VirtualCameraGameObject != base.gameObject)) || Time.timeSinceLevelLoad <= 1f)
		{
			this.stepsSinceStart++;
		}
		bool flag = false;
		if (this.brain.IsBlending && this.cameraMode == PlayerOrbitCamera.CameraMode.Off)
		{
			CinemachineBlend activeBlend = this.brain.ActiveBlend;
			if (activeBlend.CamA.VirtualCameraGameObject == base.gameObject)
			{
				if (this.smoothedCenterPointVelocity.sqrMagnitude > 0.01f)
				{
					float magnitude = this.smoothedCenterPointVelocity.magnitude;
					float num = magnitude * magnitude * this.smoothedCenterPointDrag;
					this.smoothedCenterPointVelocity += num * -this.smoothedCenterPointVelocity.normalized * Time.unscaledDeltaTime;
					this.smoothedCenterPoint += Time.unscaledDeltaTime * this.smoothedCenterPointVelocity;
					base.transform.position += Time.unscaledDeltaTime * this.smoothedCenterPointVelocity;
				}
			}
			else if (!(activeBlend.CamB.VirtualCameraGameObject == base.gameObject))
			{
				this.GetOrientationFromCamera();
			}
		}
		else if (this.brain.ActiveVirtualCamera != null)
		{
			if (this.brain.ActiveVirtualCamera.VirtualCameraGameObject == base.gameObject || this.cameraMode != PlayerOrbitCamera.CameraMode.Off)
			{
				flag = true;
			}
			else
			{
				this.GetOrientationFromCamera();
			}
		}
		if (flag)
		{
			if (this.CenterPoint.IsNaN())
			{
				return;
			}
			if (Game.State == GameState.Dialogue || Player.movement.isRagdolling || this.cameraMode != PlayerOrbitCamera.CameraMode.Off)
			{
				this.leanDesire = 0f;
			}
			else if (Player.itemManager.IsAiming && this.cameraMode == PlayerOrbitCamera.CameraMode.Off)
			{
				this.leanDesire = -1f;
			}
			else if (Game.State == GameState.Play)
			{
				if (this.framesSinceTrigger < 3 || !this.recovered)
				{
					this.leanDesire = 0f;
				}
				else
				{
					Vector3 vector = Player.rigidbody.velocity;
					if (vector.sqrMagnitude > 2f)
					{
						float x = base.transform.InverseTransformVector(vector).x;
						if (Mathf.Abs(x) > 1f)
						{
							if (Mathf.Sign(x) != Mathf.Sign(this.leanDesire))
							{
								this.leanDesire = Mathf.MoveTowards(this.leanDesire, this.leanAmount * Mathf.Sign(x), 4f * this.leanPushSpeed * Time.unscaledDeltaTime);
							}
							else
							{
								this.leanDesire = Mathf.MoveTowards(this.leanDesire, this.leanAmount * Mathf.Sign(x), this.leanPushSpeed * Time.unscaledDeltaTime);
							}
						}
						else
						{
							this.leanDesire = Mathf.MoveTowards(this.leanDesire, 0f, 4f * this.leanPushSpeed * Time.unscaledDeltaTime);
						}
					}
				}
			}
			this.currentLean = Mathf.SmoothDamp(this.currentLean, this.leanDesire, ref this.currentLeanVelocity, this.leanTransitionTime, float.PositiveInfinity, Time.unscaledDeltaTime);
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Off)
			{
				this.currentLean = 0f;
			}
			if (this.hasControl < 1f)
			{
				if (this.framesSinceTrigger < 2)
				{
					this.hasControl = 1f;
				}
				else
				{
					this.hasControl = Mathf.MoveTowards(this.hasControl, 1f, 1f * Time.unscaledDeltaTime);
				}
				this.virtualCameraNoise.m_AmplitudeGain = Mathf.Lerp(0f, this.noiseAmplitude, this.hasControl);
				this.virtualCameraLens.FieldOfView = Mathf.SmoothStep(this.brainFov, this.originalFov, this.hasControl);
				this.virtualCamera.m_Lens = this.virtualCameraLens;
			}
			this.smoothIsDialogue = Mathf.MoveTowards(this.smoothIsDialogue, (Game.State == GameState.Dialogue) ? 1f : 0f, 2f * Time.unscaledDeltaTime);
			this.lookAxis = this.playerInput.lookAxis;
			if (this.lookAxis.sqrMagnitude > 0.02f)
			{
				if (CameraFocalPoint.HasActive())
				{
					this.autoCameraActiveTime = Time.time + 0.5f * this.autoCameraInputDelay;
				}
				else if (Time.time < this.reCenterCameraTime)
				{
					this.autoCameraActiveTime = Time.time + 1f;
				}
				else
				{
					this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
				}
			}
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Backward)
			{
				this.lookAxis.y = this.lookAxis.y * -1f;
			}
			if (this.autoCameraActiveTime > Time.time)
			{
				Vector2 vector2 = this.speed * this.lookAxis * this.hasControl;
				vector2 *= Settings.mouseSensitivity;
				if (Settings.mouseInvertHorizontal)
				{
					vector2.x *= -1f;
				}
				if (Settings.mouseInvertVertical)
				{
					vector2.y *= -1f;
				}
				this.rotation.x = this.rotation.x + vector2.y;
				this.rotation.y = this.rotation.y + vector2.x;
				this.rotation.x = Mathf.Clamp(this.rotation.x, this.LowAngle, this.HighAngle);
				if (this.CameraSmoothing > 0f)
				{
					this.smoothRotation = Vector3.SmoothDamp(this.smoothRotation, this.rotation, ref this.cameraSmoothingVelocity, this.CameraSmoothing * 1f, float.PositiveInfinity, Time.unscaledDeltaTime);
				}
				else
				{
					this.smoothRotation = this.rotation;
				}
			}
			bool flag2 = true;
			if (this.autoCameraActiveTime > Time.time)
			{
				flag2 = false;
			}
			if (Player.movement == null)
			{
				flag2 = false;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Off)
			{
				flag2 = false;
			}
			if (Player.itemManager.IsAiming)
			{
				flag2 = false;
			}
			bool flag3 = Game.HasControl && Time.time < this.reCenterCameraTime && !Player.movement.isRagdolling;
			if (this.smoothForwardDirection.activeSelf != flag3)
			{
				this.smoothForwardDirection.SetActive(flag3);
			}
			if (flag2)
			{
				float num2 = this.autoCameraSmoothing;
				Vector3 vector3 = this.smoothRotation;
				Vector3 vector4;
				if (CameraFocalPoint.GetActive(out vector4) && !flag3)
				{
					flag3 = false;
					vector3 = Quaternion.LookRotation(vector4 - Player.Position).eulerAngles;
				}
				else
				{
					Vector3 vector5;
					if (Player.movement.isRagdolling)
					{
						vector5 = Player.movement.ragdollController.rigidbodies[0].velocity;
					}
					else
					{
						vector5 = Player.movement.velocity;
					}
					if (Player.movement.IsClimbing)
					{
						Vector3 forward = Player.transform.forward;
						forward.y = 0f;
						forward.Normalize();
						vector3 = Quaternion.LookRotation(forward).eulerAngles;
						num2 = this.autoCameraClimbingSmoothing;
					}
					else if (Player.movement.isRagdolling || !flag3)
					{
						if (Player.movement.JustJumped)
						{
							vector5.y = 0f;
						}
						else
						{
							vector5.y = Mathf.MoveTowards(vector5.y, 0f, 3f);
						}
						if (vector5.sqrMagnitude > this.autoCameraPullThreshold * this.autoCameraPullThreshold)
						{
							Vector3 normalized = vector5.normalized;
							normalized.y += -0.25f;
							vector3 = Quaternion.LookRotation(normalized).eulerAngles;
						}
						else if (Mathf.Abs(vector3.x) > 20f)
						{
							vector3.x = Mathf.Sign(vector3.x) * 20f;
						}
					}
					else
					{
						float num3 = Mathf.InverseLerp(this.autoCameraPullThreshold, 5f, vector5.magnitude);
						Vector3 forward2 = this.smoothForwardDirection.transform.forward;
						forward2.y = Mathf.Lerp(forward2.y, vector5.normalized.y, num3);
						vector3 = Quaternion.LookRotation(forward2).eulerAngles;
						vector3.x += 10f;
						if (Player.movement.isSledding)
						{
							num2 = this.autoCameraSleddingSmoothing;
						}
					}
				}
				if (vector3.x > 180f)
				{
					vector3.x -= 360f;
				}
				vector3.x = Mathf.Clamp(vector3.x, this.LowAngle, this.HighAngle);
				float num4 = num2;
				float num5 = num2;
				if (flag3)
				{
					num4 = 1f;
				}
				if (flag3)
				{
					num5 = 0.5f;
				}
				this.smoothRotation.y = MathUtils.SmoothDampAngle(this.smoothRotation.y, vector3.y, ref this.cameraSmoothingVelocity.y, num5, 1000f, Time.unscaledDeltaTime);
				this.smoothRotation.x = MathUtils.SmoothDamp(this.smoothRotation.x, vector3.x, ref this.cameraSmoothingVelocity.x, num4, float.PositiveInfinity, Time.unscaledDeltaTime);
				this.rotation = this.smoothRotation;
			}
			else if (Player.movement != null && Player.movement.velocity.sqrMagnitude < 0.25f && Time.time > this.reCenterCameraTime)
			{
				this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
			}
			this.smoothRotation.z = 0f;
			Quaternion quaternion = Quaternion.Euler(this.smoothRotation);
			float num6 = this.DistanceFromFocalPoint();
			Vector3 vector6 = base.transform.rotation * this.CameraOffset;
			Vector3 vector7 = this.CenterPoint;
			if (this.playerMovement.IsInWater && this.cameraMode == PlayerOrbitCamera.CameraMode.Off)
			{
				vector7.y = Mathf.Max(new float[]
				{
					this.playerMovement.waterHeight + this.sphereCastRadius + 0.001f,
					vector7.y,
					1f
				});
			}
			RaycastHit raycastHit;
			if (Game.State == GameState.Dialogue && Physics.SphereCast(vector7 + Vector3.up, this.sphereCastRadius, Vector3.down, ref raycastHit, 1f, this.recoveryLayerMask, 2))
			{
				vector7 += (1f - raycastHit.distance) * Vector3.up;
			}
			if ((this.smoothedCenterPoint - vector7).sqrMagnitude > 400f)
			{
				this.smoothedCenterPoint = vector7;
			}
			else
			{
				this.smoothedCenterPoint = Vector3.SmoothDamp(this.smoothedCenterPoint, vector7, ref this.smoothedCenterPointVelocity, Mathf.Lerp(this.transitionTime, (this.cameraMode == PlayerOrbitCamera.CameraMode.Off) ? this.centerPointSmoothing : this.c_centerPointSmoothing, this.hasControl), float.PositiveInfinity, Time.unscaledDeltaTime);
			}
			Vector3 vector8 = this.smoothedCenterPoint;
			if (vector6 != Vector3.zero)
			{
				vector8 = this.smoothedCenterPoint + vector6;
			}
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Backward)
			{
				base.transform.rotation = quaternion * Quaternion.Euler(this.backwardsCameraRotation);
			}
			else
			{
				base.transform.rotation = quaternion;
			}
			if ((this.framesSinceTrigger < 3 || !this.recovered) && this.cameraMode != PlayerOrbitCamera.CameraMode.Backward)
			{
				this.recovered = false;
				Vector3 vector9 = vector8 + quaternion * (num6 * Vector3.back);
				float num7 = 1f;
				float num8 = 1f;
				if (this.framesSinceTrigger < 3)
				{
					Vector3 vector10 = vector9 - this.smoothedCenterPoint;
					float magnitude2 = vector10.magnitude;
					if (Physics.SphereCast(this.smoothedCenterPoint, this.sphereCastRadius, vector10, ref PlayerOrbitCamera.raycastHit, magnitude2, this.recoveryLayerMask, 2))
					{
						num7 = PlayerOrbitCamera.raycastHit.distance / magnitude2;
					}
				}
				if (this.adaptedT > num7)
				{
					this.adaptedT = num7;
					this.recoveryVelocity = 0f;
				}
				else
				{
					this.adaptedT = Mathf.SmoothDamp(this.adaptedT, num7, ref this.recoveryVelocity, this.recoveryTime, float.PositiveInfinity, Time.unscaledDeltaTime);
					if (Mathf.Approximately(this.adaptedT, num8))
					{
						this.recovered = true;
					}
				}
				base.transform.position = Vector3.Lerp(this.smoothedCenterPoint, vector9, this.adaptedT);
			}
			else
			{
				this.adaptedT = 1f;
				this.adaptedDistance = num6;
				Vector3 vector11 = vector8 + quaternion * (num6 * Vector3.back);
				base.transform.position = vector11;
			}
		}
		else
		{
			if (CameraFocalPoint.HasActive())
			{
				this.autoCameraActiveTime = Time.time;
			}
			else
			{
				this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
			}
			this.currentLean = 0f;
			this.currentLeanVelocity = 0f;
			this.virtualCameraNoise.m_AmplitudeGain = 0f;
		}
		if (this.persistentRotation && Game.HasControl)
		{
			GameData.g.Write(this.rotationKey, this.rotation.y);
		}
		float num9 = Mathf.InverseLerp(this.LowAngle, this.HighAngle, -this.smoothRotation.x);
		Player.animator.SetFloat(this._AimAngle, num9);
		if (this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
		{
			base.transform.position = this.mainCamera.position;
		}
		this.lastFramePosition = base.transform.position;
	}

	// Token: 0x06000C7F RID: 3199 RVA: 0x0000BACD File Offset: 0x00009CCD
	public Quaternion GetRotation()
	{
		return Quaternion.Euler(this.smoothRotation);
	}

	// Token: 0x06000C80 RID: 3200 RVA: 0x00046EDC File Offset: 0x000450DC
	public void GetOrientationFromCamera()
	{
		if (this.mainCamera == null)
		{
			return;
		}
		base.transform.position = this.mainCamera.position;
		base.transform.rotation = this.mainCamera.rotation;
		this.rotation = base.transform.rotation.eulerAngles;
		while (this.rotation.x > 180f)
		{
			this.rotation.x = this.rotation.x - 360f;
		}
		this.rotation.x = Mathf.Clamp(this.rotation.x, this.LowAngle, this.HighAngle);
		this.smoothRotation = this.rotation;
		this.smoothedCenterPoint = this.mainCamera.position + this.mainCamera.forward * this.DistanceFromFocalPoint();
		this.smoothedCenterPoint -= base.transform.rotation * this.CameraOffset;
		Vector3 vector = base.transform.position - this.lastFramePosition;
		if (vector.sqrMagnitude < 1f)
		{
			this.smoothedCenterPointVelocity = 1f / Time.unscaledDeltaTime * vector;
		}
		LensSettings lens = this.brain.CurrentCameraState.Lens;
		this.virtualCameraLens.FieldOfView = (this.brainFov = lens.FieldOfView);
		this.virtualCamera.m_Lens = this.virtualCameraLens;
		this.cameraSmoothingVelocity = Vector3.zero;
		this.hasControl = 0f;
	}

	// Token: 0x1700013F RID: 319
	// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0000BADA File Offset: 0x00009CDA
	private float HighAngle
	{
		get
		{
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
			{
				return this.highAngle;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Forward)
			{
				return this.b_highAngle;
			}
			return this.c_highAngle;
		}
	}

	// Token: 0x17000140 RID: 320
	// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0000BB0A File Offset: 0x00009D0A
	private float LowAngle
	{
		get
		{
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
			{
				return this.lowAngle;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Forward)
			{
				return this.b_lowAngle;
			}
			return this.c_lowAngle;
		}
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0000BB3A File Offset: 0x00009D3A
	private float LowDistance
	{
		get
		{
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
			{
				return this.lowDistance;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Forward)
			{
				return this.b_lowDistance;
			}
			return this.c_lowDistance;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0000BB6A File Offset: 0x00009D6A
	private float MiddleDistance
	{
		get
		{
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
			{
				return this.middleDistance;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Forward)
			{
				return this.b_middleDistance;
			}
			return this.c_middleDistance;
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0000BB9A File Offset: 0x00009D9A
	private float HighDistance
	{
		get
		{
			if (this.cameraMode == PlayerOrbitCamera.CameraMode.Off || this.cameraMode == PlayerOrbitCamera.CameraMode.Static)
			{
				return this.highDistance;
			}
			if (this.cameraMode != PlayerOrbitCamera.CameraMode.Forward)
			{
				return this.b_highDistance;
			}
			return this.c_highDistance;
		}
	}

	// Token: 0x06000C86 RID: 3206 RVA: 0x00047080 File Offset: 0x00045280
	private float DistanceFromFocalPoint()
	{
		float num = this.HighAngle;
		float num2 = this.LowAngle;
		float num3 = this.LowDistance;
		float num4 = this.MiddleDistance;
		float num5 = this.HighDistance;
		float num6 = this.DistanceMultiplier;
		this.smoothDistanceMultiplier = Mathf.SmoothDamp(this.smoothDistanceMultiplier, num6, ref this.distanceMultiplierVel, (num6 > this.smoothDistanceMultiplier) ? 1f : 0.5f, float.PositiveInfinity, Time.unscaledDeltaTime);
		if (this.smoothRotation.x > 0f)
		{
			return this.smoothDistanceMultiplier * Mathf.Lerp(num4, num5, Mathf.InverseLerp(0f, num, this.smoothRotation.x));
		}
		return this.smoothDistanceMultiplier * Mathf.Lerp(num4, num3, Mathf.InverseLerp(0f, num2, this.smoothRotation.x));
	}

	// Token: 0x06000C87 RID: 3207 RVA: 0x0000BBCA File Offset: 0x00009DCA
	private void OnTriggerEnter(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0000BBCA File Offset: 0x00009DCA
	private void OnTriggerStay(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0000BBD3 File Offset: 0x00009DD3
	public void ForceRecovery()
	{
		this.framesSinceTrigger = 0;
		this.recovered = false;
	}

	// Token: 0x06000C8A RID: 3210 RVA: 0x0000BBE3 File Offset: 0x00009DE3
	public void ResetPosition()
	{
		this.smoothedCenterPoint = this.CenterPoint;
		this.smoothedCenterPointVelocity = Vector3.zero;
	}

	// Token: 0x0400105B RID: 4187
	private static RaycastHit raycastHit;

	// Token: 0x0400105C RID: 4188
	public static float gameplayDistanceMultiplier = 1f;

	// Token: 0x0400105D RID: 4189
	public static PlayerOrbitCamera active;

	// Token: 0x0400105E RID: 4190
	public static float cameraSmoothingFactor;

	// Token: 0x0400105F RID: 4191
	private PlayerInput playerInput;

	// Token: 0x04001060 RID: 4192
	private PlayerMovement playerMovement;

	// Token: 0x04001061 RID: 4193
	private Transform playerTransform;

	// Token: 0x04001062 RID: 4194
	public bool persistentRotation;

	// Token: 0x04001063 RID: 4195
	[ConditionalHide("persistentRotation", true)]
	public string rotationKey = "CameraRotation";

	// Token: 0x04001064 RID: 4196
	[Space]
	public Vector2 lookAxis;

	// Token: 0x04001065 RID: 4197
	public Vector2 speed;

	// Token: 0x04001066 RID: 4198
	public float cameraSmoothing;

	// Token: 0x04001067 RID: 4199
	private Vector3 cameraSmoothingVelocity;

	// Token: 0x04001068 RID: 4200
	private Vector3 smoothRotation;

	// Token: 0x04001069 RID: 4201
	private CinemachineVirtualCamera virtualCamera;

	// Token: 0x0400106A RID: 4202
	private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

	// Token: 0x0400106B RID: 4203
	private LensSettings virtualCameraLens;

	// Token: 0x0400106C RID: 4204
	public Transform ragdollCenter;

	// Token: 0x0400106D RID: 4205
	public Vector3 centerPoint = Vector3.up * 0.6f;

	// Token: 0x0400106E RID: 4206
	public Vector3 c_centerPoint = Vector3.up * 0.6f;

	// Token: 0x0400106F RID: 4207
	public Vector3 b_centerPoint = Vector3.up * 0.6f;

	// Token: 0x04001070 RID: 4208
	public Vector3 cameraOffset = Vector3.zero;

	// Token: 0x04001071 RID: 4209
	public Vector3 aimCameraOffset = Vector3.zero;

	// Token: 0x04001072 RID: 4210
	public float leanAmount = 0.75f;

	// Token: 0x04001073 RID: 4211
	public float leanPushSpeed = 0.5f;

	// Token: 0x04001074 RID: 4212
	private float currentLean;

	// Token: 0x04001075 RID: 4213
	private float leanDesire;

	// Token: 0x04001076 RID: 4214
	private float currentLeanVelocity;

	// Token: 0x04001077 RID: 4215
	public float leanTransitionTime = 0.5f;

	// Token: 0x04001078 RID: 4216
	private Vector3 smoothedCenterPoint;

	// Token: 0x04001079 RID: 4217
	private Vector3 smoothedCenterPointVelocity;

	// Token: 0x0400107A RID: 4218
	public float centerPointSmoothing = 0.1f;

	// Token: 0x0400107B RID: 4219
	public float c_centerPointSmoothing = 0.025f;

	// Token: 0x0400107C RID: 4220
	public float smoothedCenterPointDrag = 1f;

	// Token: 0x0400107D RID: 4221
	private Vector3 rotation;

	// Token: 0x0400107E RID: 4222
	public float lowAngle;

	// Token: 0x0400107F RID: 4223
	public float highAngle;

	// Token: 0x04001080 RID: 4224
	public float lowDistance;

	// Token: 0x04001081 RID: 4225
	public float middleDistance;

	// Token: 0x04001082 RID: 4226
	public float highDistance;

	// Token: 0x04001083 RID: 4227
	public float c_lowAngle;

	// Token: 0x04001084 RID: 4228
	public float c_highAngle;

	// Token: 0x04001085 RID: 4229
	public float c_lowDistance;

	// Token: 0x04001086 RID: 4230
	public float c_middleDistance;

	// Token: 0x04001087 RID: 4231
	public float c_highDistance;

	// Token: 0x04001088 RID: 4232
	public float b_lowAngle;

	// Token: 0x04001089 RID: 4233
	public float b_highAngle;

	// Token: 0x0400108A RID: 4234
	public float b_lowDistance;

	// Token: 0x0400108B RID: 4235
	public float b_middleDistance;

	// Token: 0x0400108C RID: 4236
	public float b_highDistance;

	// Token: 0x0400108D RID: 4237
	public float distanceMultiplier = 1f;

	// Token: 0x0400108E RID: 4238
	public float aimDistanceMultiplier = 0.5f;

	// Token: 0x0400108F RID: 4239
	public float mostRecentDistanceMultiplier = 1f;

	// Token: 0x04001090 RID: 4240
	private float smoothDistanceMultiplier = 0.75f;

	// Token: 0x04001091 RID: 4241
	private float distanceMultiplierVel;

	// Token: 0x04001092 RID: 4242
	private float smoothIsDialogue;

	// Token: 0x04001093 RID: 4243
	private Transform mainCamera;

	// Token: 0x04001094 RID: 4244
	private CinemachineBrain brain;

	// Token: 0x04001095 RID: 4245
	[Header("Collision Detection")]
	public float sphereCastRadius = 0.25f;

	// Token: 0x04001096 RID: 4246
	public LayerMask recoveryLayerMask;

	// Token: 0x04001097 RID: 4247
	private int framesSinceTrigger = 5;

	// Token: 0x04001098 RID: 4248
	private float adaptedDistance;

	// Token: 0x04001099 RID: 4249
	private float adaptedT;

	// Token: 0x0400109A RID: 4250
	private bool recovered = true;

	// Token: 0x0400109B RID: 4251
	public float recoveryTime = 0.3f;

	// Token: 0x0400109C RID: 4252
	private float recoveryVelocity;

	// Token: 0x0400109D RID: 4253
	[Header("Feelers")]
	public float feelerAngle = 30f;

	// Token: 0x0400109E RID: 4254
	public int feelerCount = 5;

	// Token: 0x0400109F RID: 4255
	[Header("Auto Camera")]
	public float autoCameraInputDelay = 2f;

	// Token: 0x040010A0 RID: 4256
	private float autoCameraActiveTime = -1f;

	// Token: 0x040010A1 RID: 4257
	private float reCenterCameraTime = -1f;

	// Token: 0x040010A2 RID: 4258
	public float autoCameraSmoothing = 5f;

	// Token: 0x040010A3 RID: 4259
	public float autoCameraPullThreshold = 1f;

	// Token: 0x040010A4 RID: 4260
	public float autoCameraClimbingSmoothing = 1.5f;

	// Token: 0x040010A5 RID: 4261
	public float autoCameraSleddingSmoothing = 0.25f;

	// Token: 0x040010A6 RID: 4262
	public GameObject smoothForwardDirection;

	// Token: 0x040010A7 RID: 4263
	[Header("Transition Handling")]
	public float transitionTime = 1f;

	// Token: 0x040010A8 RID: 4264
	private float hasControl = 1f;

	// Token: 0x040010A9 RID: 4265
	private float originalFov;

	// Token: 0x040010AA RID: 4266
	private float brainFov;

	// Token: 0x040010AB RID: 4267
	private float noiseAmplitude;

	// Token: 0x040010AC RID: 4268
	private int stepsSinceStart;

	// Token: 0x040010AD RID: 4269
	private Vector3 lastFramePosition;

	// Token: 0x040010AE RID: 4270
	private Vector3 velocity;

	// Token: 0x040010AF RID: 4271
	private const float playerHideDistance = 0.7f;

	// Token: 0x040010B0 RID: 4272
	private Camera camera;

	// Token: 0x040010B1 RID: 4273
	public LayerMask defaultMask;

	// Token: 0x040010B2 RID: 4274
	public LayerMask playerExcludedMask;

	// Token: 0x040010B3 RID: 4275
	private bool isPlayerHidden;

	// Token: 0x040010B4 RID: 4276
	public PlayerOrbitCamera.CameraMode cameraMode;

	// Token: 0x040010B5 RID: 4277
	public Vector3 backwardsCameraRotation;

	// Token: 0x040010B6 RID: 4278
	private readonly int _AimAngle = Animator.StringToHash("AimAngle");

	// Token: 0x0200027E RID: 638
	public enum CameraMode
	{
		// Token: 0x040010B8 RID: 4280
		Off,
		// Token: 0x040010B9 RID: 4281
		Forward,
		// Token: 0x040010BA RID: 4282
		Backward,
		// Token: 0x040010BB RID: 4283
		Static
	}
}

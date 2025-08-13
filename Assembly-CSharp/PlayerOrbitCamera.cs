using System;
using Cinemachine;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class PlayerOrbitCamera : MonoBehaviour
{
	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000ABD RID: 2749 RVA: 0x00034C76 File Offset: 0x00032E76
	public float CameraSmoothing
	{
		get
		{
			return this.cameraSmoothing * PlayerOrbitCamera.cameraSmoothingFactor;
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00034C84 File Offset: 0x00032E84
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

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00034D16 File Offset: 0x00032F16
	private Vector3 CameraOffset
	{
		get
		{
			return this.cameraOffset + Vector3.right * this.currentLean;
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00034D34 File Offset: 0x00032F34
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

	// Token: 0x170000AC RID: 172
	// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x00034DE0 File Offset: 0x00032FE0
	public bool IsAutoCameraActive
	{
		get
		{
			return Time.time > this.autoCameraActiveTime;
		}
	}

	// Token: 0x06000AC2 RID: 2754 RVA: 0x00034DEF File Offset: 0x00032FEF
	public void ForceAutoCamera()
	{
		this.autoCameraActiveTime = -1f;
	}

	// Token: 0x06000AC3 RID: 2755 RVA: 0x00034DFC File Offset: 0x00032FFC
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

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00034E6D File Offset: 0x0003306D
	private void OnEnable()
	{
		PlayerOrbitCamera.active = this;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00034E78 File Offset: 0x00033078
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

	// Token: 0x06000AC6 RID: 2758 RVA: 0x00034FAD File Offset: 0x000331AD
	private void FixedUpdate()
	{
		this.framesSinceTrigger++;
	}

	// Token: 0x06000AC7 RID: 2759 RVA: 0x00034FBD File Offset: 0x000331BD
	public void ReCenterCamera(bool justEnabled = false)
	{
		if (justEnabled)
		{
			this.autoCameraActiveTime = Time.time;
		}
		this.reCenterCameraTime = Time.time + 0.5f;
	}

	// Token: 0x06000AC8 RID: 2760 RVA: 0x00034FDE File Offset: 0x000331DE
	public void CancelReCenterCamera()
	{
		this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
		this.reCenterCameraTime = -1f;
	}

	// Token: 0x06000AC9 RID: 2761 RVA: 0x00035000 File Offset: 0x00033200
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
			if (Game.State == GameState.Dialogue && Physics.SphereCast(vector7 + Vector3.up, this.sphereCastRadius, Vector3.down, out raycastHit, 1f, this.recoveryLayerMask, QueryTriggerInteraction.Collide))
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
					if (Physics.SphereCast(this.smoothedCenterPoint, this.sphereCastRadius, vector10, out PlayerOrbitCamera.raycastHit, magnitude2, this.recoveryLayerMask, QueryTriggerInteraction.Collide))
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

	// Token: 0x06000ACA RID: 2762 RVA: 0x00035D54 File Offset: 0x00033F54
	public Quaternion GetRotation()
	{
		return Quaternion.Euler(this.smoothRotation);
	}

	// Token: 0x06000ACB RID: 2763 RVA: 0x00035D64 File Offset: 0x00033F64
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

	// Token: 0x170000AD RID: 173
	// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00035F06 File Offset: 0x00034106
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

	// Token: 0x170000AE RID: 174
	// (get) Token: 0x06000ACD RID: 2765 RVA: 0x00035F36 File Offset: 0x00034136
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

	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00035F66 File Offset: 0x00034166
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

	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00035F96 File Offset: 0x00034196
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

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00035FC6 File Offset: 0x000341C6
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

	// Token: 0x06000AD1 RID: 2769 RVA: 0x00035FF8 File Offset: 0x000341F8
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

	// Token: 0x06000AD2 RID: 2770 RVA: 0x000360C6 File Offset: 0x000342C6
	private void OnTriggerEnter(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000AD3 RID: 2771 RVA: 0x000360CF File Offset: 0x000342CF
	private void OnTriggerStay(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000AD4 RID: 2772 RVA: 0x000360D8 File Offset: 0x000342D8
	public void ForceRecovery()
	{
		this.framesSinceTrigger = 0;
		this.recovered = false;
	}

	// Token: 0x06000AD5 RID: 2773 RVA: 0x000360E8 File Offset: 0x000342E8
	public void ResetPosition()
	{
		this.smoothedCenterPoint = this.CenterPoint;
		this.smoothedCenterPointVelocity = Vector3.zero;
	}

	// Token: 0x04000E0A RID: 3594
	private static RaycastHit raycastHit;

	// Token: 0x04000E0B RID: 3595
	public static float gameplayDistanceMultiplier = 1f;

	// Token: 0x04000E0C RID: 3596
	public static PlayerOrbitCamera active;

	// Token: 0x04000E0D RID: 3597
	public static float cameraSmoothingFactor;

	// Token: 0x04000E0E RID: 3598
	private PlayerInput playerInput;

	// Token: 0x04000E0F RID: 3599
	private PlayerMovement playerMovement;

	// Token: 0x04000E10 RID: 3600
	private Transform playerTransform;

	// Token: 0x04000E11 RID: 3601
	public bool persistentRotation;

	// Token: 0x04000E12 RID: 3602
	[ConditionalHide("persistentRotation", true)]
	public string rotationKey = "CameraRotation";

	// Token: 0x04000E13 RID: 3603
	[Space]
	public Vector2 lookAxis;

	// Token: 0x04000E14 RID: 3604
	public Vector2 speed;

	// Token: 0x04000E15 RID: 3605
	public float cameraSmoothing;

	// Token: 0x04000E16 RID: 3606
	private Vector3 cameraSmoothingVelocity;

	// Token: 0x04000E17 RID: 3607
	private Vector3 smoothRotation;

	// Token: 0x04000E18 RID: 3608
	private CinemachineVirtualCamera virtualCamera;

	// Token: 0x04000E19 RID: 3609
	private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

	// Token: 0x04000E1A RID: 3610
	private LensSettings virtualCameraLens;

	// Token: 0x04000E1B RID: 3611
	public Transform ragdollCenter;

	// Token: 0x04000E1C RID: 3612
	public Vector3 centerPoint = Vector3.up * 0.6f;

	// Token: 0x04000E1D RID: 3613
	public Vector3 c_centerPoint = Vector3.up * 0.6f;

	// Token: 0x04000E1E RID: 3614
	public Vector3 b_centerPoint = Vector3.up * 0.6f;

	// Token: 0x04000E1F RID: 3615
	public Vector3 cameraOffset = Vector3.zero;

	// Token: 0x04000E20 RID: 3616
	public Vector3 aimCameraOffset = Vector3.zero;

	// Token: 0x04000E21 RID: 3617
	public float leanAmount = 0.75f;

	// Token: 0x04000E22 RID: 3618
	public float leanPushSpeed = 0.5f;

	// Token: 0x04000E23 RID: 3619
	private float currentLean;

	// Token: 0x04000E24 RID: 3620
	private float leanDesire;

	// Token: 0x04000E25 RID: 3621
	private float currentLeanVelocity;

	// Token: 0x04000E26 RID: 3622
	public float leanTransitionTime = 0.5f;

	// Token: 0x04000E27 RID: 3623
	private Vector3 smoothedCenterPoint;

	// Token: 0x04000E28 RID: 3624
	private Vector3 smoothedCenterPointVelocity;

	// Token: 0x04000E29 RID: 3625
	public float centerPointSmoothing = 0.1f;

	// Token: 0x04000E2A RID: 3626
	public float c_centerPointSmoothing = 0.025f;

	// Token: 0x04000E2B RID: 3627
	public float smoothedCenterPointDrag = 1f;

	// Token: 0x04000E2C RID: 3628
	private Vector3 rotation;

	// Token: 0x04000E2D RID: 3629
	public float lowAngle;

	// Token: 0x04000E2E RID: 3630
	public float highAngle;

	// Token: 0x04000E2F RID: 3631
	public float lowDistance;

	// Token: 0x04000E30 RID: 3632
	public float middleDistance;

	// Token: 0x04000E31 RID: 3633
	public float highDistance;

	// Token: 0x04000E32 RID: 3634
	public float c_lowAngle;

	// Token: 0x04000E33 RID: 3635
	public float c_highAngle;

	// Token: 0x04000E34 RID: 3636
	public float c_lowDistance;

	// Token: 0x04000E35 RID: 3637
	public float c_middleDistance;

	// Token: 0x04000E36 RID: 3638
	public float c_highDistance;

	// Token: 0x04000E37 RID: 3639
	public float b_lowAngle;

	// Token: 0x04000E38 RID: 3640
	public float b_highAngle;

	// Token: 0x04000E39 RID: 3641
	public float b_lowDistance;

	// Token: 0x04000E3A RID: 3642
	public float b_middleDistance;

	// Token: 0x04000E3B RID: 3643
	public float b_highDistance;

	// Token: 0x04000E3C RID: 3644
	public float distanceMultiplier = 1f;

	// Token: 0x04000E3D RID: 3645
	public float aimDistanceMultiplier = 0.5f;

	// Token: 0x04000E3E RID: 3646
	public float mostRecentDistanceMultiplier = 1f;

	// Token: 0x04000E3F RID: 3647
	private float smoothDistanceMultiplier = 0.75f;

	// Token: 0x04000E40 RID: 3648
	private float distanceMultiplierVel;

	// Token: 0x04000E41 RID: 3649
	private float smoothIsDialogue;

	// Token: 0x04000E42 RID: 3650
	private Transform mainCamera;

	// Token: 0x04000E43 RID: 3651
	private CinemachineBrain brain;

	// Token: 0x04000E44 RID: 3652
	[Header("Collision Detection")]
	public float sphereCastRadius = 0.25f;

	// Token: 0x04000E45 RID: 3653
	public LayerMask recoveryLayerMask;

	// Token: 0x04000E46 RID: 3654
	private int framesSinceTrigger = 5;

	// Token: 0x04000E47 RID: 3655
	private float adaptedDistance;

	// Token: 0x04000E48 RID: 3656
	private float adaptedT;

	// Token: 0x04000E49 RID: 3657
	private bool recovered = true;

	// Token: 0x04000E4A RID: 3658
	public float recoveryTime = 0.3f;

	// Token: 0x04000E4B RID: 3659
	private float recoveryVelocity;

	// Token: 0x04000E4C RID: 3660
	[Header("Feelers")]
	public float feelerAngle = 30f;

	// Token: 0x04000E4D RID: 3661
	public int feelerCount = 5;

	// Token: 0x04000E4E RID: 3662
	[Header("Auto Camera")]
	public float autoCameraInputDelay = 2f;

	// Token: 0x04000E4F RID: 3663
	private float autoCameraActiveTime = -1f;

	// Token: 0x04000E50 RID: 3664
	private float reCenterCameraTime = -1f;

	// Token: 0x04000E51 RID: 3665
	public float autoCameraSmoothing = 5f;

	// Token: 0x04000E52 RID: 3666
	public float autoCameraPullThreshold = 1f;

	// Token: 0x04000E53 RID: 3667
	public float autoCameraClimbingSmoothing = 1.5f;

	// Token: 0x04000E54 RID: 3668
	public float autoCameraSleddingSmoothing = 0.25f;

	// Token: 0x04000E55 RID: 3669
	public GameObject smoothForwardDirection;

	// Token: 0x04000E56 RID: 3670
	[Header("Transition Handling")]
	public float transitionTime = 1f;

	// Token: 0x04000E57 RID: 3671
	private float hasControl = 1f;

	// Token: 0x04000E58 RID: 3672
	private float originalFov;

	// Token: 0x04000E59 RID: 3673
	private float brainFov;

	// Token: 0x04000E5A RID: 3674
	private float noiseAmplitude;

	// Token: 0x04000E5B RID: 3675
	private int stepsSinceStart;

	// Token: 0x04000E5C RID: 3676
	private Vector3 lastFramePosition;

	// Token: 0x04000E5D RID: 3677
	private Vector3 velocity;

	// Token: 0x04000E5E RID: 3678
	private const float playerHideDistance = 0.7f;

	// Token: 0x04000E5F RID: 3679
	private Camera camera;

	// Token: 0x04000E60 RID: 3680
	public LayerMask defaultMask;

	// Token: 0x04000E61 RID: 3681
	public LayerMask playerExcludedMask;

	// Token: 0x04000E62 RID: 3682
	private bool isPlayerHidden;

	// Token: 0x04000E63 RID: 3683
	public PlayerOrbitCamera.CameraMode cameraMode;

	// Token: 0x04000E64 RID: 3684
	public Vector3 backwardsCameraRotation;

	// Token: 0x04000E65 RID: 3685
	private readonly int _AimAngle = Animator.StringToHash("AimAngle");

	// Token: 0x020003EA RID: 1002
	public enum CameraMode
	{
		// Token: 0x04001C7E RID: 7294
		Off,
		// Token: 0x04001C7F RID: 7295
		Forward,
		// Token: 0x04001C80 RID: 7296
		Backward,
		// Token: 0x04001C81 RID: 7297
		Static
	}
}

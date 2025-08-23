using System;
using Cinemachine;
using UnityEngine;

public class PlayerOrbitCamera : MonoBehaviour
{
	// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0000BD40 File Offset: 0x00009F40
	public float CameraSmoothing
	{
		get
		{
			return this.cameraSmoothing * PlayerOrbitCamera.cameraSmoothingFactor;
		}
	}

	// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00047CEC File Offset: 0x00045EEC
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

	// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0000BD4E File Offset: 0x00009F4E
	private Vector3 CameraOffset
	{
		get
		{
			return this.cameraOffset + Vector3.right * this.currentLean;
		}
	}

	// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x00047D80 File Offset: 0x00045F80
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

	// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0000BD6B File Offset: 0x00009F6B
	public bool IsAutoCameraActive
	{
		get
		{
			return Time.time > this.autoCameraActiveTime;
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x0000BD7A File Offset: 0x00009F7A
	public void ForceAutoCamera()
	{
		this.autoCameraActiveTime = -1f;
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x00047E2C File Offset: 0x0004602C
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

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0000BD87 File Offset: 0x00009F87
	private void OnEnable()
	{
		PlayerOrbitCamera.active = this;
	}

	// Token: 0x06000CC7 RID: 3271 RVA: 0x00047EA0 File Offset: 0x000460A0
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

	// Token: 0x06000CC8 RID: 3272 RVA: 0x0000BD8F File Offset: 0x00009F8F
	private void FixedUpdate()
	{
		this.framesSinceTrigger++;
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x0000BD9F File Offset: 0x00009F9F
	public void ReCenterCamera(bool justEnabled = false)
	{
		if (justEnabled)
		{
			this.autoCameraActiveTime = Time.time;
		}
		this.reCenterCameraTime = Time.time + 0.5f;
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x0000BDC0 File Offset: 0x00009FC0
	public void CancelReCenterCamera()
	{
		this.autoCameraActiveTime = Time.time + this.autoCameraInputDelay;
		this.reCenterCameraTime = -1f;
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00047FD8 File Offset: 0x000461D8
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

	// Token: 0x06000CCC RID: 3276 RVA: 0x0000BDDF File Offset: 0x00009FDF
	public Quaternion GetRotation()
	{
		return Quaternion.Euler(this.smoothRotation);
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x00048D2C File Offset: 0x00046F2C
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

	// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0000BDEC File Offset: 0x00009FEC
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

	// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0000BE1C File Offset: 0x0000A01C
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

	// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0000BE4C File Offset: 0x0000A04C
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

	// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0000BE7C File Offset: 0x0000A07C
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

	// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0000BEAC File Offset: 0x0000A0AC
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

	// Token: 0x06000CD3 RID: 3283 RVA: 0x00048ED0 File Offset: 0x000470D0
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

	// Token: 0x06000CD4 RID: 3284 RVA: 0x0000BEDC File Offset: 0x0000A0DC
	private void OnTriggerEnter(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x0000BEDC File Offset: 0x0000A0DC
	private void OnTriggerStay(Collider other)
	{
		this.framesSinceTrigger = 0;
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
	public void ForceRecovery()
	{
		this.framesSinceTrigger = 0;
		this.recovered = false;
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
	public void ResetPosition()
	{
		this.smoothedCenterPoint = this.CenterPoint;
		this.smoothedCenterPointVelocity = Vector3.zero;
	}

	private static RaycastHit raycastHit;

	public static float gameplayDistanceMultiplier = 1f;

	public static PlayerOrbitCamera active;

	public static float cameraSmoothingFactor;

	private PlayerInput playerInput;

	private PlayerMovement playerMovement;

	private Transform playerTransform;

	public bool persistentRotation;

	[ConditionalHide("persistentRotation", true)]
	public string rotationKey = "CameraRotation";

	[Space]
	public Vector2 lookAxis;

	public Vector2 speed;

	public float cameraSmoothing;

	private Vector3 cameraSmoothingVelocity;

	private Vector3 smoothRotation;

	private CinemachineVirtualCamera virtualCamera;

	private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

	private LensSettings virtualCameraLens;

	public Transform ragdollCenter;

	public Vector3 centerPoint = Vector3.up * 0.6f;

	public Vector3 c_centerPoint = Vector3.up * 0.6f;

	public Vector3 b_centerPoint = Vector3.up * 0.6f;

	public Vector3 cameraOffset = Vector3.zero;

	public Vector3 aimCameraOffset = Vector3.zero;

	public float leanAmount = 0.75f;

	public float leanPushSpeed = 0.5f;

	private float currentLean;

	private float leanDesire;

	private float currentLeanVelocity;

	public float leanTransitionTime = 0.5f;

	private Vector3 smoothedCenterPoint;

	private Vector3 smoothedCenterPointVelocity;

	public float centerPointSmoothing = 0.1f;

	public float c_centerPointSmoothing = 0.025f;

	public float smoothedCenterPointDrag = 1f;

	private Vector3 rotation;

	public float lowAngle;

	public float highAngle;

	public float lowDistance;

	public float middleDistance;

	public float highDistance;

	public float c_lowAngle;

	public float c_highAngle;

	public float c_lowDistance;

	public float c_middleDistance;

	public float c_highDistance;

	public float b_lowAngle;

	public float b_highAngle;

	public float b_lowDistance;

	public float b_middleDistance;

	public float b_highDistance;

	public float distanceMultiplier = 1f;

	public float aimDistanceMultiplier = 0.5f;

	public float mostRecentDistanceMultiplier = 1f;

	private float smoothDistanceMultiplier = 0.75f;

	private float distanceMultiplierVel;

	private float smoothIsDialogue;

	private Transform mainCamera;

	private CinemachineBrain brain;

	[Header("Collision Detection")]
	public float sphereCastRadius = 0.25f;

	public LayerMask recoveryLayerMask;

	private int framesSinceTrigger = 5;

	private float adaptedDistance;

	private float adaptedT;

	private bool recovered = true;

	public float recoveryTime = 0.3f;

	private float recoveryVelocity;

	[Header("Feelers")]
	public float feelerAngle = 30f;

	public int feelerCount = 5;

	[Header("Auto Camera")]
	public float autoCameraInputDelay = 2f;

	private float autoCameraActiveTime = -1f;

	private float reCenterCameraTime = -1f;

	public float autoCameraSmoothing = 5f;

	public float autoCameraPullThreshold = 1f;

	public float autoCameraClimbingSmoothing = 1.5f;

	public float autoCameraSleddingSmoothing = 0.25f;

	public GameObject smoothForwardDirection;

	[Header("Transition Handling")]
	public float transitionTime = 1f;

	private float hasControl = 1f;

	private float originalFov;

	private float brainFov;

	private float noiseAmplitude;

	private int stepsSinceStart;

	private Vector3 lastFramePosition;

	private Vector3 velocity;

	private const float playerHideDistance = 0.7f;

	private Camera camera;

	public LayerMask defaultMask;

	public LayerMask playerExcludedMask;

	private bool isPlayerHidden;

	public PlayerOrbitCamera.CameraMode cameraMode;

	public Vector3 backwardsCameraRotation;

	private readonly int _AimAngle = Animator.StringToHash("AimAngle");

	public enum CameraMode
	{
		Off,
		Forward,
		Backward,
		Static
	}
}

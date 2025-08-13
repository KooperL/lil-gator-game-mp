using System;
using Cinemachine;
using Rewired;
using UnityEngine;

// Token: 0x02000015 RID: 21
public class EditorlikeCamera : MonoBehaviour
{
	// Token: 0x0600003B RID: 59 RVA: 0x000022E4 File Offset: 0x000004E4
	private void Awake()
	{
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0001791C File Offset: 0x00015B1C
	private void OnEnable()
	{
		base.transform.ApplyTransform(MainCamera.t);
		this.position = base.transform.position;
		this.rotation = base.transform.rotation;
		this.lookInputVelocity = Vector2.zero;
		this.positionSmoothVelocity = Vector2.zero;
		this.velocity = Vector3.zero;
		this.rePlayer = ReInput.players.GetPlayer(0);
		if (this.allowMovement)
		{
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), 0, ReInput.mapping.GetActionId("DebugCameraHorizontal"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveForward), 0, ReInput.mapping.GetActionId("DebugCameraForward"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveVertical), 0, ReInput.mapping.GetActionId("DebugCameraVertical"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSpeedChange), 0, ReInput.mapping.GetActionId("DebugCameraSpeed"));
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnLookVertical), 0, ReInput.mapping.GetActionId("Look Vertical"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnLookHorizontal), 0, ReInput.mapping.GetActionId("Look Horizontal"));
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00017A88 File Offset: 0x00015C88
	private void OnDisable()
	{
		if (this.allowMovement)
		{
			this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnSpeedChange));
			this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveVertical));
			this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveForward));
			this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal));
		}
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnLookHorizontal));
		this.rePlayer.RemoveInputEventDelegate(new Action<InputActionEventData>(this.OnLookVertical));
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00017B28 File Offset: 0x00015D28
	private void Update()
	{
		if (this.lookSmoothing == 0f)
		{
			this.lookInputSmooth = this.lookInput;
		}
		else
		{
			this.lookInputSmooth = Vector2.SmoothDamp(this.lookInputSmooth, this.lookInput, ref this.lookInputVelocity, this.lookSmoothing);
		}
		Vector3 vector = Settings.mouseSensitivity * new Vector3(this.lookInputSmooth.y * this.lookSensitivity.y, this.lookInputSmooth.x * this.lookSensitivity.x, 0f);
		Vector3 vector2 = this.rotation.eulerAngles;
		vector2 += Time.deltaTime * vector;
		this.rotation = Quaternion.Euler(vector2);
		base.transform.rotation = this.rotation;
		this.speedExp += this.speedAdjust * this.speedAdjustRate * Time.deltaTime;
		float num = Mathf.Pow(this.speed, this.speedExp);
		Vector3 vector3 = (base.transform.rotation * this.moveInput).normalized * num;
		float num2 = Mathf.Max(Mathf.Pow(this.acceleration, this.speedExp), this.velocity.magnitude * this.acceleration / this.speed);
		this.velocity = Vector3.MoveTowards(this.velocity, vector3, Time.deltaTime * num2);
		this.position += Time.deltaTime * this.velocity;
		if (this.positionSmoothing == 0f)
		{
			base.transform.position = this.position;
		}
		else
		{
			base.transform.position = Vector3.SmoothDamp(base.transform.position, this.position, ref this.positionSmoothVelocity, this.positionSmoothing);
		}
		if (base.transform.up.y < 0f)
		{
			this.rotation = (base.transform.rotation = Quaternion.identity);
		}
	}

	// Token: 0x0600003F RID: 63 RVA: 0x000022F2 File Offset: 0x000004F2
	private void OnLookHorizontal(InputActionEventData obj)
	{
		this.lookInput.x = obj.GetAxis();
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00002306 File Offset: 0x00000506
	private void OnLookVertical(InputActionEventData obj)
	{
		this.lookInput.y = obj.GetAxis();
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000231A File Offset: 0x0000051A
	private void OnSpeedChange(InputActionEventData obj)
	{
		this.speedAdjust = obj.GetAxis();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00002329 File Offset: 0x00000529
	private void OnMoveVertical(InputActionEventData obj)
	{
		this.moveInput.y = obj.GetAxis();
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000233D File Offset: 0x0000053D
	private void OnMoveForward(InputActionEventData obj)
	{
		this.moveInput.z = obj.GetAxis();
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002351 File Offset: 0x00000551
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		this.moveInput.x = obj.GetAxis();
	}

	// Token: 0x04000046 RID: 70
	private CinemachineVirtualCamera virtualCamera;

	// Token: 0x04000047 RID: 71
	public Vector2 lookSensitivity;

	// Token: 0x04000048 RID: 72
	public float acceleration = 10f;

	// Token: 0x04000049 RID: 73
	public float speed = 20f;

	// Token: 0x0400004A RID: 74
	public float modifier = 5f;

	// Token: 0x0400004B RID: 75
	public float speedExp = 1f;

	// Token: 0x0400004C RID: 76
	private float speedAdjust;

	// Token: 0x0400004D RID: 77
	public float speedAdjustRate = 0.1f;

	// Token: 0x0400004E RID: 78
	public float zoomSpeed = 2f;

	// Token: 0x0400004F RID: 79
	private Vector3 velocity;

	// Token: 0x04000050 RID: 80
	private bool modifierDown;

	// Token: 0x04000051 RID: 81
	public float lookSmoothing = 0.5f;

	// Token: 0x04000052 RID: 82
	private Vector3 moveInput;

	// Token: 0x04000053 RID: 83
	private Vector2 lookInput;

	// Token: 0x04000054 RID: 84
	private Vector2 lookInputSmooth;

	// Token: 0x04000055 RID: 85
	private Vector2 lookInputVelocity;

	// Token: 0x04000056 RID: 86
	private float verticalInput;

	// Token: 0x04000057 RID: 87
	public float positionSmoothing = 0.5f;

	// Token: 0x04000058 RID: 88
	private Vector3 positionSmoothVelocity;

	// Token: 0x04000059 RID: 89
	private Vector3 position;

	// Token: 0x0400005A RID: 90
	private Quaternion rotation;

	// Token: 0x0400005B RID: 91
	private Player rePlayer;

	// Token: 0x0400005C RID: 92
	public bool allowMovement = true;
}

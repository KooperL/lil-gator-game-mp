using System;
using Cinemachine;
using Rewired;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class EditorlikeCamera : MonoBehaviour
{
	// Token: 0x06000042 RID: 66 RVA: 0x00002E36 File Offset: 0x00001036
	private void Awake()
	{
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00002E44 File Offset: 0x00001044
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
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveHorizontal), UpdateLoopType.Update, ReInput.mapping.GetActionId("DebugCameraHorizontal"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveForward), UpdateLoopType.Update, ReInput.mapping.GetActionId("DebugCameraForward"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnMoveVertical), UpdateLoopType.Update, ReInput.mapping.GetActionId("DebugCameraVertical"));
			this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnSpeedChange), UpdateLoopType.Update, ReInput.mapping.GetActionId("DebugCameraSpeed"));
		}
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnLookVertical), UpdateLoopType.Update, ReInput.mapping.GetActionId("Look Vertical"));
		this.rePlayer.AddInputEventDelegate(new Action<InputActionEventData>(this.OnLookHorizontal), UpdateLoopType.Update, ReInput.mapping.GetActionId("Look Horizontal"));
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002FB0 File Offset: 0x000011B0
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

	// Token: 0x06000045 RID: 69 RVA: 0x00003050 File Offset: 0x00001250
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

	// Token: 0x06000046 RID: 70 RVA: 0x0000325C File Offset: 0x0000145C
	private void OnLookHorizontal(InputActionEventData obj)
	{
		this.lookInput.x = obj.GetAxis();
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003270 File Offset: 0x00001470
	private void OnLookVertical(InputActionEventData obj)
	{
		this.lookInput.y = obj.GetAxis();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x00003284 File Offset: 0x00001484
	private void OnSpeedChange(InputActionEventData obj)
	{
		this.speedAdjust = obj.GetAxis();
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00003293 File Offset: 0x00001493
	private void OnMoveVertical(InputActionEventData obj)
	{
		this.moveInput.y = obj.GetAxis();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000032A7 File Offset: 0x000014A7
	private void OnMoveForward(InputActionEventData obj)
	{
		this.moveInput.z = obj.GetAxis();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000032BB File Offset: 0x000014BB
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		this.moveInput.x = obj.GetAxis();
	}

	// Token: 0x0400005C RID: 92
	private CinemachineVirtualCamera virtualCamera;

	// Token: 0x0400005D RID: 93
	public Vector2 lookSensitivity;

	// Token: 0x0400005E RID: 94
	public float acceleration = 10f;

	// Token: 0x0400005F RID: 95
	public float speed = 20f;

	// Token: 0x04000060 RID: 96
	public float modifier = 5f;

	// Token: 0x04000061 RID: 97
	public float speedExp = 1f;

	// Token: 0x04000062 RID: 98
	private float speedAdjust;

	// Token: 0x04000063 RID: 99
	public float speedAdjustRate = 0.1f;

	// Token: 0x04000064 RID: 100
	public float zoomSpeed = 2f;

	// Token: 0x04000065 RID: 101
	private Vector3 velocity;

	// Token: 0x04000066 RID: 102
	private bool modifierDown;

	// Token: 0x04000067 RID: 103
	public float lookSmoothing = 0.5f;

	// Token: 0x04000068 RID: 104
	private Vector3 moveInput;

	// Token: 0x04000069 RID: 105
	private Vector2 lookInput;

	// Token: 0x0400006A RID: 106
	private Vector2 lookInputSmooth;

	// Token: 0x0400006B RID: 107
	private Vector2 lookInputVelocity;

	// Token: 0x0400006C RID: 108
	private float verticalInput;

	// Token: 0x0400006D RID: 109
	public float positionSmoothing = 0.5f;

	// Token: 0x0400006E RID: 110
	private Vector3 positionSmoothVelocity;

	// Token: 0x0400006F RID: 111
	private Vector3 position;

	// Token: 0x04000070 RID: 112
	private Quaternion rotation;

	// Token: 0x04000071 RID: 113
	private global::Rewired.Player rePlayer;

	// Token: 0x04000072 RID: 114
	public bool allowMovement = true;
}

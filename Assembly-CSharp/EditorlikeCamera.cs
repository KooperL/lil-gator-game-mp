using System;
using Cinemachine;
using Rewired;
using UnityEngine;

public class EditorlikeCamera : MonoBehaviour
{
	// Token: 0x06000043 RID: 67 RVA: 0x00002348 File Offset: 0x00000548
	private void Awake()
	{
		this.virtualCamera = base.GetComponent<CinemachineVirtualCamera>();
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0001817C File Offset: 0x0001637C
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

	// Token: 0x06000045 RID: 69 RVA: 0x000182E8 File Offset: 0x000164E8
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

	// Token: 0x06000046 RID: 70 RVA: 0x00018388 File Offset: 0x00016588
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

	// Token: 0x06000047 RID: 71 RVA: 0x00002356 File Offset: 0x00000556
	private void OnLookHorizontal(InputActionEventData obj)
	{
		this.lookInput.x = obj.GetAxis();
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000236A File Offset: 0x0000056A
	private void OnLookVertical(InputActionEventData obj)
	{
		this.lookInput.y = obj.GetAxis();
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000237E File Offset: 0x0000057E
	private void OnSpeedChange(InputActionEventData obj)
	{
		this.speedAdjust = obj.GetAxis();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000238D File Offset: 0x0000058D
	private void OnMoveVertical(InputActionEventData obj)
	{
		this.moveInput.y = obj.GetAxis();
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000023A1 File Offset: 0x000005A1
	private void OnMoveForward(InputActionEventData obj)
	{
		this.moveInput.z = obj.GetAxis();
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000023B5 File Offset: 0x000005B5
	private void OnMoveHorizontal(InputActionEventData obj)
	{
		this.moveInput.x = obj.GetAxis();
	}

	private CinemachineVirtualCamera virtualCamera;

	public Vector2 lookSensitivity;

	public float acceleration = 10f;

	public float speed = 20f;

	public float modifier = 5f;

	public float speedExp = 1f;

	private float speedAdjust;

	public float speedAdjustRate = 0.1f;

	public float zoomSpeed = 2f;

	private Vector3 velocity;

	private bool modifierDown;

	public float lookSmoothing = 0.5f;

	private Vector3 moveInput;

	private Vector2 lookInput;

	private Vector2 lookInputSmooth;

	private Vector2 lookInputVelocity;

	private float verticalInput;

	public float positionSmoothing = 0.5f;

	private Vector3 positionSmoothVelocity;

	private Vector3 position;

	private Quaternion rotation;

	private Player rePlayer;

	public bool allowMovement = true;
}

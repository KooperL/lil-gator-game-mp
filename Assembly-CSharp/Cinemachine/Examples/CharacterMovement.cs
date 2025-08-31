using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	[AddComponentMenu("")]
	public class CharacterMovement : MonoBehaviour
	{
		// Token: 0x06001024 RID: 4132 RVA: 0x0004D477 File Offset: 0x0004B677
		private void Start()
		{
			this.anim = base.GetComponent<Animator>();
			this.mainCamera = Camera.main;
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0004D490 File Offset: 0x0004B690
		private void FixedUpdate()
		{
			this.input.x = Input.GetAxis("Horizontal");
			this.input.y = Input.GetAxis("Vertical");
			if (this.useCharacterForward)
			{
				this.speed = Mathf.Abs(this.input.x) + this.input.y;
			}
			else
			{
				this.speed = Mathf.Abs(this.input.x) + Mathf.Abs(this.input.y);
			}
			this.speed = Mathf.Clamp(this.speed, 0f, 1f);
			this.speed = Mathf.SmoothDamp(this.anim.GetFloat("Speed"), this.speed, ref this.velocity, 0.1f);
			this.anim.SetFloat("Speed", this.speed);
			if (this.input.y < 0f && this.useCharacterForward)
			{
				this.direction = this.input.y;
			}
			else
			{
				this.direction = 0f;
			}
			this.anim.SetFloat("Direction", this.direction);
			this.isSprinting = (Input.GetKey(this.sprintJoystick) || Input.GetKey(this.sprintKeyboard)) && this.input != Vector2.zero && this.direction >= 0f;
			this.anim.SetBool("isSprinting", this.isSprinting);
			this.UpdateTargetDirection();
			if (this.input != Vector2.zero && this.targetDirection.magnitude > 0.1f)
			{
				Vector3 normalized = this.targetDirection.normalized;
				this.freeRotation = Quaternion.LookRotation(normalized, base.transform.up);
				float num = this.freeRotation.eulerAngles.y - base.transform.eulerAngles.y;
				float num2 = base.transform.eulerAngles.y;
				if (num < 0f || num > 0f)
				{
					num2 = this.freeRotation.eulerAngles.y;
				}
				Vector3 vector = new Vector3(0f, num2, 0f);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(vector), this.turnSpeed * this.turnSpeedMultiplier * Time.deltaTime);
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0004D710 File Offset: 0x0004B910
		public virtual void UpdateTargetDirection()
		{
			if (!this.useCharacterForward)
			{
				this.turnSpeedMultiplier = 1f;
				Vector3 vector = this.mainCamera.transform.TransformDirection(Vector3.forward);
				vector.y = 0f;
				Vector3 vector2 = this.mainCamera.transform.TransformDirection(Vector3.right);
				this.targetDirection = this.input.x * vector2 + this.input.y * vector;
				return;
			}
			this.turnSpeedMultiplier = 0.2f;
			Vector3 vector3 = base.transform.TransformDirection(Vector3.forward);
			vector3.y = 0f;
			Vector3 vector4 = base.transform.TransformDirection(Vector3.right);
			this.targetDirection = this.input.x * vector4 + Mathf.Abs(this.input.y) * vector3;
		}

		public bool useCharacterForward;

		public bool lockToCameraForward;

		public float turnSpeed = 10f;

		public KeyCode sprintJoystick = KeyCode.JoystickButton2;

		public KeyCode sprintKeyboard = KeyCode.Space;

		private float turnSpeedMultiplier;

		private float speed;

		private float direction;

		private bool isSprinting;

		private Animator anim;

		private Vector3 targetDirection;

		private Vector2 input;

		private Quaternion freeRotation;

		private Camera mainCamera;

		private float velocity;
	}
}

using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002F5 RID: 757
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

		// Token: 0x04001523 RID: 5411
		public bool useCharacterForward;

		// Token: 0x04001524 RID: 5412
		public bool lockToCameraForward;

		// Token: 0x04001525 RID: 5413
		public float turnSpeed = 10f;

		// Token: 0x04001526 RID: 5414
		public KeyCode sprintJoystick = KeyCode.JoystickButton2;

		// Token: 0x04001527 RID: 5415
		public KeyCode sprintKeyboard = KeyCode.Space;

		// Token: 0x04001528 RID: 5416
		private float turnSpeedMultiplier;

		// Token: 0x04001529 RID: 5417
		private float speed;

		// Token: 0x0400152A RID: 5418
		private float direction;

		// Token: 0x0400152B RID: 5419
		private bool isSprinting;

		// Token: 0x0400152C RID: 5420
		private Animator anim;

		// Token: 0x0400152D RID: 5421
		private Vector3 targetDirection;

		// Token: 0x0400152E RID: 5422
		private Vector2 input;

		// Token: 0x0400152F RID: 5423
		private Quaternion freeRotation;

		// Token: 0x04001530 RID: 5424
		private Camera mainCamera;

		// Token: 0x04001531 RID: 5425
		private float velocity;
	}
}

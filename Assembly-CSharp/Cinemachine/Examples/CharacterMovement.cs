using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003E8 RID: 1000
	[AddComponentMenu("")]
	public class CharacterMovement : MonoBehaviour
	{
		// Token: 0x06001351 RID: 4945 RVA: 0x000106A9 File Offset: 0x0000E8A9
		private void Start()
		{
			this.anim = base.GetComponent<Animator>();
			this.mainCamera = Camera.main;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0005E4E8 File Offset: 0x0005C6E8
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
				Vector3 vector;
				vector..ctor(0f, num2, 0f);
				base.transform.rotation = Quaternion.Slerp(base.transform.rotation, Quaternion.Euler(vector), this.turnSpeed * this.turnSpeedMultiplier * Time.deltaTime);
			}
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0005E768 File Offset: 0x0005C968
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

		// Token: 0x040018E6 RID: 6374
		public bool useCharacterForward;

		// Token: 0x040018E7 RID: 6375
		public bool lockToCameraForward;

		// Token: 0x040018E8 RID: 6376
		public float turnSpeed = 10f;

		// Token: 0x040018E9 RID: 6377
		public KeyCode sprintJoystick = 332;

		// Token: 0x040018EA RID: 6378
		public KeyCode sprintKeyboard = 32;

		// Token: 0x040018EB RID: 6379
		private float turnSpeedMultiplier;

		// Token: 0x040018EC RID: 6380
		private float speed;

		// Token: 0x040018ED RID: 6381
		private float direction;

		// Token: 0x040018EE RID: 6382
		private bool isSprinting;

		// Token: 0x040018EF RID: 6383
		private Animator anim;

		// Token: 0x040018F0 RID: 6384
		private Vector3 targetDirection;

		// Token: 0x040018F1 RID: 6385
		private Vector2 input;

		// Token: 0x040018F2 RID: 6386
		private Quaternion freeRotation;

		// Token: 0x040018F3 RID: 6387
		private Camera mainCamera;

		// Token: 0x040018F4 RID: 6388
		private float velocity;
	}
}

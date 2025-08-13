using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020002F6 RID: 758
	[AddComponentMenu("")]
	public class CharacterMovement2D : MonoBehaviour
	{
		// Token: 0x06001028 RID: 4136 RVA: 0x0004D827 File Offset: 0x0004BA27
		private void Start()
		{
			this.anim = base.GetComponent<Animator>();
			this.rigbody = base.GetComponent<Rigidbody>();
			this.targetrot = base.transform.rotation;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0004D854 File Offset: 0x0004BA54
		private void FixedUpdate()
		{
			this.input.x = Input.GetAxis("Horizontal");
			if ((this.input.x < 0f && !this.headingleft) || (this.input.x > 0f && this.headingleft))
			{
				if (this.input.x < 0f)
				{
					this.targetrot = Quaternion.Euler(0f, 270f, 0f);
				}
				if (this.input.x > 0f)
				{
					this.targetrot = Quaternion.Euler(0f, 90f, 0f);
				}
				this.headingleft = !this.headingleft;
			}
			base.transform.rotation = Quaternion.Lerp(base.transform.rotation, this.targetrot, Time.deltaTime * 20f);
			this.speed = Mathf.Abs(this.input.x);
			this.speed = Mathf.SmoothDamp(this.anim.GetFloat("Speed"), this.speed, ref this.velocity, 0.1f);
			this.anim.SetFloat("Speed", this.speed);
			if ((Input.GetKeyDown(this.sprintJoystick) || Input.GetKeyDown(this.sprintKeyboard)) && this.input != Vector2.zero)
			{
				this.isSprinting = true;
			}
			if (Input.GetKeyUp(this.sprintJoystick) || Input.GetKeyUp(this.sprintKeyboard) || this.input == Vector2.zero)
			{
				this.isSprinting = false;
			}
			this.anim.SetBool("isSprinting", this.isSprinting);
			if ((Input.GetKeyDown(this.jumpJoystick) || Input.GetKeyDown(this.jumpKeyboard)) && this.isGrounded())
			{
				this.rigbody.velocity = new Vector3(this.input.x, this.jumpVelocity, 0f);
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0004DA59 File Offset: 0x0004BC59
		public bool isGrounded()
		{
			return !this.checkGroundForJump || Physics.Raycast(base.transform.position, Vector3.down, this.groundTolerance);
		}

		// Token: 0x04001532 RID: 5426
		public KeyCode sprintJoystick = KeyCode.JoystickButton2;

		// Token: 0x04001533 RID: 5427
		public KeyCode jumpJoystick = KeyCode.JoystickButton0;

		// Token: 0x04001534 RID: 5428
		public KeyCode sprintKeyboard = KeyCode.LeftShift;

		// Token: 0x04001535 RID: 5429
		public KeyCode jumpKeyboard = KeyCode.Space;

		// Token: 0x04001536 RID: 5430
		public float jumpVelocity = 7f;

		// Token: 0x04001537 RID: 5431
		public float groundTolerance = 0.2f;

		// Token: 0x04001538 RID: 5432
		public bool checkGroundForJump = true;

		// Token: 0x04001539 RID: 5433
		private float speed;

		// Token: 0x0400153A RID: 5434
		private bool isSprinting;

		// Token: 0x0400153B RID: 5435
		private Animator anim;

		// Token: 0x0400153C RID: 5436
		private Vector2 input;

		// Token: 0x0400153D RID: 5437
		private float velocity;

		// Token: 0x0400153E RID: 5438
		private bool headingleft;

		// Token: 0x0400153F RID: 5439
		private Quaternion targetrot;

		// Token: 0x04001540 RID: 5440
		private Rigidbody rigbody;
	}
}

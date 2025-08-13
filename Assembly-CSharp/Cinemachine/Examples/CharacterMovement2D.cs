using System;
using UnityEngine;

namespace Cinemachine.Examples
{
	// Token: 0x020003E9 RID: 1001
	[AddComponentMenu("")]
	public class CharacterMovement2D : MonoBehaviour
	{
		// Token: 0x06001355 RID: 4949 RVA: 0x000106E8 File Offset: 0x0000E8E8
		private void Start()
		{
			this.anim = base.GetComponent<Animator>();
			this.rigbody = base.GetComponent<Rigidbody>();
			this.targetrot = base.transform.rotation;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005E85C File Offset: 0x0005CA5C
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

		// Token: 0x06001357 RID: 4951 RVA: 0x00010713 File Offset: 0x0000E913
		public bool isGrounded()
		{
			return !this.checkGroundForJump || Physics.Raycast(base.transform.position, Vector3.down, this.groundTolerance);
		}

		// Token: 0x040018F5 RID: 6389
		public KeyCode sprintJoystick = 332;

		// Token: 0x040018F6 RID: 6390
		public KeyCode jumpJoystick = 330;

		// Token: 0x040018F7 RID: 6391
		public KeyCode sprintKeyboard = 304;

		// Token: 0x040018F8 RID: 6392
		public KeyCode jumpKeyboard = 32;

		// Token: 0x040018F9 RID: 6393
		public float jumpVelocity = 7f;

		// Token: 0x040018FA RID: 6394
		public float groundTolerance = 0.2f;

		// Token: 0x040018FB RID: 6395
		public bool checkGroundForJump = true;

		// Token: 0x040018FC RID: 6396
		private float speed;

		// Token: 0x040018FD RID: 6397
		private bool isSprinting;

		// Token: 0x040018FE RID: 6398
		private Animator anim;

		// Token: 0x040018FF RID: 6399
		private Vector2 input;

		// Token: 0x04001900 RID: 6400
		private float velocity;

		// Token: 0x04001901 RID: 6401
		private bool headingleft;

		// Token: 0x04001902 RID: 6402
		private Quaternion targetrot;

		// Token: 0x04001903 RID: 6403
		private Rigidbody rigbody;
	}
}

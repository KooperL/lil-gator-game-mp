using System;
using Cinemachine.Utility;
using UnityEngine;

// Token: 0x02000050 RID: 80
public class PlayerMove : MonoBehaviour
{
	// Token: 0x06000131 RID: 305 RVA: 0x0000765C File Offset: 0x0000585C
	private void Update()
	{
		Vector3 vector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		if (vector.magnitude > 0f)
		{
			Vector3 vector2 = (this.worldDirection ? Vector3.forward : (base.transform.position - Camera.main.transform.position));
			vector2.y = 0f;
			vector2 = vector2.normalized;
			if (vector2.magnitude > 0.001f)
			{
				vector = Quaternion.LookRotation(vector2, Vector3.up) * vector;
				if (vector.magnitude > 0.001f)
				{
					base.transform.position += vector * (this.speed * Time.deltaTime);
					if (this.rotatePlayer)
					{
						float num = Damper.Damp(1f, this.rotationDamping, Time.deltaTime);
						Quaternion quaternion = Quaternion.LookRotation(vector.normalized, Vector3.up);
						base.transform.rotation = Quaternion.Slerp(base.transform.rotation, quaternion, num);
					}
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Space) && this.spaceAction != null)
		{
			this.spaceAction();
		}
		if (Input.GetKeyDown(KeyCode.Return) && this.enterAction != null)
		{
			this.enterAction();
		}
	}

	// Token: 0x040001A9 RID: 425
	public float speed = 5f;

	// Token: 0x040001AA RID: 426
	public bool worldDirection;

	// Token: 0x040001AB RID: 427
	public bool rotatePlayer = true;

	// Token: 0x040001AC RID: 428
	public float rotationDamping = 0.5f;

	// Token: 0x040001AD RID: 429
	public Action spaceAction;

	// Token: 0x040001AE RID: 430
	public Action enterAction;
}

using System;
using Cinemachine.Utility;
using UnityEngine;

// Token: 0x02000069 RID: 105
public class PlayerMove : MonoBehaviour
{
	// Token: 0x06000156 RID: 342 RVA: 0x0001BC08 File Offset: 0x00019E08
	private void Update()
	{
		Vector3 vector;
		vector..ctor(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
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
		if (Input.GetKeyDown(32) && this.spaceAction != null)
		{
			this.spaceAction();
		}
		if (Input.GetKeyDown(13) && this.enterAction != null)
		{
			this.enterAction();
		}
	}

	// Token: 0x0400020D RID: 525
	public float speed = 5f;

	// Token: 0x0400020E RID: 526
	public bool worldDirection;

	// Token: 0x0400020F RID: 527
	public bool rotatePlayer = true;

	// Token: 0x04000210 RID: 528
	public float rotationDamping = 0.5f;

	// Token: 0x04000211 RID: 529
	public Action spaceAction;

	// Token: 0x04000212 RID: 530
	public Action enterAction;
}

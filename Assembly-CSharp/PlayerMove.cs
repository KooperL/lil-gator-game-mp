using System;
using Cinemachine.Utility;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// Token: 0x0600015E RID: 350 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
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

	public float speed = 5f;

	public bool worldDirection;

	public bool rotatePlayer = true;

	public float rotationDamping = 0.5f;

	public Action spaceAction;

	public Action enterAction;
}

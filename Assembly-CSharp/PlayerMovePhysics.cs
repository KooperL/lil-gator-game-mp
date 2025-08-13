using System;
using UnityEngine;

// Token: 0x0200006B RID: 107
public class PlayerMovePhysics : MonoBehaviour
{
	// Token: 0x0600015A RID: 346 RVA: 0x000032A1 File Offset: 0x000014A1
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000032AF File Offset: 0x000014AF
	private void OnEnable()
	{
		base.transform.position += new Vector3(10f, 0f, 0f);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
	private void FixedUpdate()
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
					this.rb.AddForce(this.speed * vector);
					if (this.rotatePlayer)
					{
						base.transform.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
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

	// Token: 0x04000217 RID: 535
	public float speed = 5f;

	// Token: 0x04000218 RID: 536
	public bool worldDirection = true;

	// Token: 0x04000219 RID: 537
	public bool rotatePlayer = true;

	// Token: 0x0400021A RID: 538
	public Action spaceAction;

	// Token: 0x0400021B RID: 539
	public Action enterAction;

	// Token: 0x0400021C RID: 540
	private Rigidbody rb;
}

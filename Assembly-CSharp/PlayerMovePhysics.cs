using System;
using UnityEngine;

public class PlayerMovePhysics : MonoBehaviour
{
	// Token: 0x06000135 RID: 309 RVA: 0x0000798A File Offset: 0x00005B8A
	private void Start()
	{
		this.rb = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00007998 File Offset: 0x00005B98
	private void OnEnable()
	{
		base.transform.position += new Vector3(10f, 0f, 0f);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000079C4 File Offset: 0x00005BC4
	private void FixedUpdate()
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
					this.rb.AddForce(this.speed * vector);
					if (this.rotatePlayer)
					{
						base.transform.rotation = Quaternion.LookRotation(vector.normalized, Vector3.up);
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

	public bool worldDirection = true;

	public bool rotatePlayer = true;

	public Action spaceAction;

	public Action enterAction;

	private Rigidbody rb;
}

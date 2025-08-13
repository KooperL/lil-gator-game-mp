using System;
using UnityEngine;

// Token: 0x0200013D RID: 317
public class ControllableRigidbody : MonoBehaviour
{
	// Token: 0x060005EF RID: 1519 RVA: 0x00006383 File Offset: 0x00004583
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00006391 File Offset: 0x00004591
	private void Start()
	{
		this.input = Player.input;
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0000639E File Offset: 0x0000459E
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.force * this.input.smoothedInputDirection);
	}

	// Token: 0x040007F6 RID: 2038
	private Rigidbody rigidbody;

	// Token: 0x040007F7 RID: 2039
	private Transform camera;

	// Token: 0x040007F8 RID: 2040
	private PlayerInput input;

	// Token: 0x040007F9 RID: 2041
	public float force = 10f;
}

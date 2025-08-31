using System;
using UnityEngine;

public class ControllableRigidbody : MonoBehaviour
{
	// Token: 0x060004E3 RID: 1251 RVA: 0x0001A729 File Offset: 0x00018929
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060004E4 RID: 1252 RVA: 0x0001A737 File Offset: 0x00018937
	private void Start()
	{
		this.input = Player.input;
	}

	// Token: 0x060004E5 RID: 1253 RVA: 0x0001A744 File Offset: 0x00018944
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.force * this.input.smoothedInputDirection);
	}

	private Rigidbody rigidbody;

	private Transform camera;

	private PlayerInput input;

	public float force = 10f;
}

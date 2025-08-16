using System;
using UnityEngine;

public class ControllableRigidbody : MonoBehaviour
{
	// Token: 0x06000629 RID: 1577 RVA: 0x00006649 File Offset: 0x00004849
	private void Awake()
	{
		this.rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x00006657 File Offset: 0x00004857
	private void Start()
	{
		this.input = Player.input;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00006664 File Offset: 0x00004864
	private void FixedUpdate()
	{
		this.rigidbody.AddForce(this.force * this.input.smoothedInputDirection);
	}

	private Rigidbody rigidbody;

	private Transform camera;

	private PlayerInput input;

	public float force = 10f;
}

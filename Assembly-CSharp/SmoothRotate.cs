using System;
using UnityEngine;

public class SmoothRotate : MonoBehaviour
{
	// Token: 0x0600069C RID: 1692 RVA: 0x00006C83 File Offset: 0x00004E83
	private void Awake()
	{
		this.initialRotation = base.transform.localRotation;
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x00006C96 File Offset: 0x00004E96
	private void OnEnable()
	{
		if (this.resetRotation)
		{
			base.transform.localRotation = this.initialRotation;
		}
		this.t = 0f;
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00031FA4 File Offset: 0x000301A4
	private void Update()
	{
		if (this.t < 1f)
		{
			this.t = Mathf.MoveTowards(this.t, 1f, Time.deltaTime / this.initialTime);
		}
		base.transform.Rotate(Vector3.Lerp(this.initialSpeed, this.regularSpeed, this.t) * Time.deltaTime);
	}

	public Vector3 initialSpeed;

	public float initialTime = 1f;

	public Vector3 regularSpeed;

	private float t;

	public bool resetRotation = true;

	private Quaternion initialRotation;
}

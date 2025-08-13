using System;
using UnityEngine;

// Token: 0x02000103 RID: 259
public class SmoothRotate : MonoBehaviour
{
	// Token: 0x06000550 RID: 1360 RVA: 0x0001C493 File Offset: 0x0001A693
	private void Awake()
	{
		this.initialRotation = base.transform.localRotation;
	}

	// Token: 0x06000551 RID: 1361 RVA: 0x0001C4A6 File Offset: 0x0001A6A6
	private void OnEnable()
	{
		if (this.resetRotation)
		{
			base.transform.localRotation = this.initialRotation;
		}
		this.t = 0f;
	}

	// Token: 0x06000552 RID: 1362 RVA: 0x0001C4CC File Offset: 0x0001A6CC
	private void Update()
	{
		if (this.t < 1f)
		{
			this.t = Mathf.MoveTowards(this.t, 1f, Time.deltaTime / this.initialTime);
		}
		base.transform.Rotate(Vector3.Lerp(this.initialSpeed, this.regularSpeed, this.t) * Time.deltaTime);
	}

	// Token: 0x0400074C RID: 1868
	public Vector3 initialSpeed;

	// Token: 0x0400074D RID: 1869
	public float initialTime = 1f;

	// Token: 0x0400074E RID: 1870
	public Vector3 regularSpeed;

	// Token: 0x0400074F RID: 1871
	private float t;

	// Token: 0x04000750 RID: 1872
	public bool resetRotation = true;

	// Token: 0x04000751 RID: 1873
	private Quaternion initialRotation;
}

using System;
using UnityEngine;

// Token: 0x02000156 RID: 342
public class SmoothRotate : MonoBehaviour
{
	// Token: 0x06000662 RID: 1634 RVA: 0x000069BD File Offset: 0x00004BBD
	private void Awake()
	{
		this.initialRotation = base.transform.localRotation;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x000069D0 File Offset: 0x00004BD0
	private void OnEnable()
	{
		if (this.resetRotation)
		{
			base.transform.localRotation = this.initialRotation;
		}
		this.t = 0f;
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00030A24 File Offset: 0x0002EC24
	private void Update()
	{
		if (this.t < 1f)
		{
			this.t = Mathf.MoveTowards(this.t, 1f, Time.deltaTime / this.initialTime);
		}
		base.transform.Rotate(Vector3.Lerp(this.initialSpeed, this.regularSpeed, this.t) * Time.deltaTime);
	}

	// Token: 0x04000893 RID: 2195
	public Vector3 initialSpeed;

	// Token: 0x04000894 RID: 2196
	public float initialTime = 1f;

	// Token: 0x04000895 RID: 2197
	public Vector3 regularSpeed;

	// Token: 0x04000896 RID: 2198
	private float t;

	// Token: 0x04000897 RID: 2199
	public bool resetRotation = true;

	// Token: 0x04000898 RID: 2200
	private Quaternion initialRotation;
}

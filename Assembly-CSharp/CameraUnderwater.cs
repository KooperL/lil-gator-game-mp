using System;
using UnityEngine;

// Token: 0x020000B3 RID: 179
public class CameraUnderwater : MonoBehaviour
{
	// Token: 0x0600029B RID: 667 RVA: 0x0000420C File Offset: 0x0000240C
	private void Start()
	{
		this.shaderVariables = ShaderVariables.s;
		base.enabled = false;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x00004220 File Offset: 0x00002420
	private void FixedUpdate()
	{
		if (this.stepsSinceWater > 3)
		{
			this.underwaterEffects.SetActive(false);
			base.enabled = false;
		}
		this.stepsSinceWater++;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0000424C File Offset: 0x0000244C
	private void OnTriggerEnter(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000424C File Offset: 0x0000244C
	private void OnTriggerStay(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00020518 File Offset: 0x0001E718
	private void ProcessTrigger(Collider other)
	{
		Water water;
		if (other.TryGetComponent<Water>(ref water) && base.transform.position.y > water.GetWaterPlaneHeight(base.transform.position))
		{
			return;
		}
		if (!base.enabled)
		{
			base.enabled = true;
			this.underwaterEffects.SetActive(true);
		}
		this.stepsSinceWater = 0;
	}

	// Token: 0x040003A4 RID: 932
	private ShaderVariables shaderVariables;

	// Token: 0x040003A5 RID: 933
	private int stepsSinceWater = 100;

	// Token: 0x040003A6 RID: 934
	public GameObject underwaterEffects;
}

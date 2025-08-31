using System;
using UnityEngine;

public class CameraUnderwater : MonoBehaviour
{
	// Token: 0x06000251 RID: 593 RVA: 0x0000C6FC File Offset: 0x0000A8FC
	private void Start()
	{
		this.shaderVariables = ShaderVariables.s;
		base.enabled = false;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000C710 File Offset: 0x0000A910
	private void FixedUpdate()
	{
		if (this.stepsSinceWater > 3)
		{
			this.underwaterEffects.SetActive(false);
			base.enabled = false;
		}
		this.stepsSinceWater++;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x0000C73C File Offset: 0x0000A93C
	private void OnTriggerEnter(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000C745 File Offset: 0x0000A945
	private void OnTriggerStay(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x06000255 RID: 597 RVA: 0x0000C750 File Offset: 0x0000A950
	private void ProcessTrigger(Collider other)
	{
		Water water;
		if (other.TryGetComponent<Water>(out water) && base.transform.position.y > water.GetWaterPlaneHeight(base.transform.position))
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

	private ShaderVariables shaderVariables;

	private int stepsSinceWater = 100;

	public GameObject underwaterEffects;
}

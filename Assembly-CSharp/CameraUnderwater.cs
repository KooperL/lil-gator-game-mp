using System;
using UnityEngine;

public class CameraUnderwater : MonoBehaviour
{
	// Token: 0x060002A8 RID: 680 RVA: 0x000042F8 File Offset: 0x000024F8
	private void Start()
	{
		this.shaderVariables = ShaderVariables.s;
		base.enabled = false;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000430C File Offset: 0x0000250C
	private void FixedUpdate()
	{
		if (this.stepsSinceWater > 3)
		{
			this.underwaterEffects.SetActive(false);
			base.enabled = false;
		}
		this.stepsSinceWater++;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00004338 File Offset: 0x00002538
	private void OnTriggerEnter(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00004338 File Offset: 0x00002538
	private void OnTriggerStay(Collider other)
	{
		this.ProcessTrigger(other);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00020F4C File Offset: 0x0001F14C
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

using System;
using Cinemachine;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class ActivateOnKeypress : MonoBehaviour
{
	// Token: 0x06000153 RID: 339 RVA: 0x0000322E File Offset: 0x0000142E
	private void Start()
	{
		this.vcam = base.GetComponent<CinemachineVirtualCameraBase>();
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0001BB70 File Offset: 0x00019D70
	private void Update()
	{
		if (this.vcam != null)
		{
			if (Input.GetKey(this.ActivationKey))
			{
				if (!this.boosted)
				{
					this.vcam.Priority += this.PriorityBoostAmount;
					this.boosted = true;
				}
			}
			else if (this.boosted)
			{
				this.vcam.Priority -= this.PriorityBoostAmount;
				this.boosted = false;
			}
		}
		if (this.Reticle != null)
		{
			this.Reticle.SetActive(this.boosted);
		}
	}

	// Token: 0x04000208 RID: 520
	public KeyCode ActivationKey = 306;

	// Token: 0x04000209 RID: 521
	public int PriorityBoostAmount = 10;

	// Token: 0x0400020A RID: 522
	public GameObject Reticle;

	// Token: 0x0400020B RID: 523
	private CinemachineVirtualCameraBase vcam;

	// Token: 0x0400020C RID: 524
	private bool boosted;
}

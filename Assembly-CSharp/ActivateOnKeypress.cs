using System;
using Cinemachine;
using UnityEngine;

// Token: 0x0200004F RID: 79
public class ActivateOnKeypress : MonoBehaviour
{
	// Token: 0x0600012E RID: 302 RVA: 0x00007597 File Offset: 0x00005797
	private void Start()
	{
		this.vcam = base.GetComponent<CinemachineVirtualCameraBase>();
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000075A8 File Offset: 0x000057A8
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

	// Token: 0x040001A4 RID: 420
	public KeyCode ActivationKey = KeyCode.LeftControl;

	// Token: 0x040001A5 RID: 421
	public int PriorityBoostAmount = 10;

	// Token: 0x040001A6 RID: 422
	public GameObject Reticle;

	// Token: 0x040001A7 RID: 423
	private CinemachineVirtualCameraBase vcam;

	// Token: 0x040001A8 RID: 424
	private bool boosted;
}

using System;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class SpeakingActorCameras : MonoBehaviour
{
	// Token: 0x060008EA RID: 2282 RVA: 0x00008AF8 File Offset: 0x00006CF8
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x00038748 File Offset: 0x00036948
	private void OnDisable()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(false);
		}
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00008AF8 File Offset: 0x00006CF8
	public void Update()
	{
		this.UpdateState();
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x00038778 File Offset: 0x00036978
	private void UpdateState()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(i == DialogueManager.currentlySpeakingActorIndex);
		}
	}

	// Token: 0x04000B85 RID: 2949
	public GameObject[] cameras;
}

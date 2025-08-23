using System;
using UnityEngine;

public class SpeakingActorCameras : MonoBehaviour
{
	// Token: 0x0600092B RID: 2347 RVA: 0x00008E2B File Offset: 0x0000702B
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0003A380 File Offset: 0x00038580
	private void OnDisable()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(false);
		}
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00008E2B File Offset: 0x0000702B
	public void Update()
	{
		this.UpdateState();
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0003A3B0 File Offset: 0x000385B0
	private void UpdateState()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(i == DialogueManager.currentlySpeakingActorIndex);
		}
	}

	public GameObject[] cameras;
}

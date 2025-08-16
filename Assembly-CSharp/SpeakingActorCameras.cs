using System;
using UnityEngine;

public class SpeakingActorCameras : MonoBehaviour
{
	// Token: 0x0600092A RID: 2346 RVA: 0x00008E0C File Offset: 0x0000700C
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00039ED8 File Offset: 0x000380D8
	private void OnDisable()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(false);
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00008E0C File Offset: 0x0000700C
	public void Update()
	{
		this.UpdateState();
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00039F08 File Offset: 0x00038108
	private void UpdateState()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(i == DialogueManager.currentlySpeakingActorIndex);
		}
	}

	public GameObject[] cameras;
}

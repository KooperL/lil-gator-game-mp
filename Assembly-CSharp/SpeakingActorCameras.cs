using System;
using UnityEngine;

public class SpeakingActorCameras : MonoBehaviour
{
	// Token: 0x0600092A RID: 2346 RVA: 0x00008E21 File Offset: 0x00007021
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003A0B8 File Offset: 0x000382B8
	private void OnDisable()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(false);
		}
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00008E21 File Offset: 0x00007021
	public void Update()
	{
		this.UpdateState();
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0003A0E8 File Offset: 0x000382E8
	private void UpdateState()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(i == DialogueManager.currentlySpeakingActorIndex);
		}
	}

	public GameObject[] cameras;
}

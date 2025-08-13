using System;
using UnityEngine;

// Token: 0x0200016E RID: 366
public class SpeakingActorCameras : MonoBehaviour
{
	// Token: 0x0600078B RID: 1931 RVA: 0x00025383 File Offset: 0x00023583
	private void OnEnable()
	{
		this.UpdateState();
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x0002538C File Offset: 0x0002358C
	private void OnDisable()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(false);
		}
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x000253BA File Offset: 0x000235BA
	public void Update()
	{
		this.UpdateState();
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x000253C4 File Offset: 0x000235C4
	private void UpdateState()
	{
		for (int i = 0; i < this.cameras.Length; i++)
		{
			this.cameras[i].SetActive(i == DialogueManager.currentlySpeakingActorIndex);
		}
	}

	// Token: 0x040009B7 RID: 2487
	public GameObject[] cameras;
}

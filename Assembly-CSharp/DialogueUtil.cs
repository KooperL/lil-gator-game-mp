using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000D2 RID: 210
public static class DialogueUtil
{
	// Token: 0x06000478 RID: 1144 RVA: 0x00019048 File Offset: 0x00017248
	public static IEnumerator RunWithCues(IEnumerator dialogueEnumerator, DialogueUtil.CueData[] cueData, GameObject camera = null, bool precueFirstCue = false, ICueable cueable = null)
	{
		Coroutine dialogueCoroutine = CoroutineUtil.Start(dialogueEnumerator);
		int num;
		for (int i = 0; i < cueData.Length; i = num + 1)
		{
			yield return DialogueManager.d.waitUntilCue;
			DialogueManager.d.cue = false;
			if (i > 0)
			{
				cueData[i - 1].Uncue();
			}
			if (!precueFirstCue || i != 0)
			{
				cueData[i].Cue(ref camera);
			}
			num = i;
		}
		if (cueable != null)
		{
			cueable.SetCamera(camera);
		}
		yield return dialogueCoroutine;
		yield break;
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x00019074 File Offset: 0x00017274
	public static void StopLastCue(DialogueUtil.CueData[] cueData, GameObject camera = null)
	{
		if (cueData.Length != 0)
		{
			cueData[cueData.Length - 1].Uncue();
			if (camera != null)
			{
				camera.SetActive(false);
			}
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x0001909C File Offset: 0x0001729C
	public static DialogueUtil.CueData[] UpdateCues(MultilingualTextDocument document, string dialogue, DialogueUtil.CueData[] cueData)
	{
		if (document == null)
		{
			return cueData;
		}
		DialogueChunk dialogueChunk = document.FetchChunk(dialogue);
		if (dialogueChunk == null)
		{
			return cueData;
		}
		List<int> list = new List<int>();
		List<string> list2 = new List<string>();
		for (int i = 0; i < dialogueChunk.lines.Length; i++)
		{
			DialogueLine dialogueLine = dialogueChunk.lines[i];
			if (dialogueLine.cue)
			{
				list.Add(i);
				list2.Add(dialogueLine.descriptor);
			}
		}
		if (cueData.Length != list2.Count)
		{
			DialogueUtil.CueData[] array = new DialogueUtil.CueData[list2.Count];
			List<DialogueUtil.CueData> list3 = new List<DialogueUtil.CueData>(cueData);
			for (int j = 0; j < array.Length; j++)
			{
				bool flag = false;
				DialogueUtil.CueData cueData2 = default(DialogueUtil.CueData);
				foreach (DialogueUtil.CueData cueData3 in list3)
				{
					if (cueData3.name == list2[j])
					{
						flag = true;
						array[j] = cueData3;
						cueData2 = cueData3;
						break;
					}
				}
				if (flag)
				{
					list3.Remove(cueData2);
					array[j].name = "foundmatch";
				}
			}
			for (int k = 0; k < array.Length; k++)
			{
				if (!(array[k].name != ""))
				{
					bool flag2 = false;
					DialogueUtil.CueData cueData4 = default(DialogueUtil.CueData);
					foreach (DialogueUtil.CueData cueData5 in list3)
					{
						if (cueData5.lineIndex == list[k])
						{
							flag2 = true;
							array[k] = cueData5;
							cueData4 = cueData5;
							break;
						}
					}
					if (flag2)
					{
						list3.Remove(cueData4);
						array[k].name = "foundmatch";
					}
				}
			}
			int num = 0;
			while (num < array.Length && list3.Count != 0)
			{
				if (!(array[num].name != ""))
				{
					DialogueUtil.CueData cueData6 = list3[0];
					list3.Remove(cueData6);
				}
				num++;
			}
			cueData = array;
		}
		for (int l = 0; l < cueData.Length; l++)
		{
			if (l < list2.Count)
			{
				cueData[l].name = list2[l];
				cueData[l].lineIndex = list[l];
			}
			else
			{
				cueData[l].name = "Not enough Q's";
			}
		}
		return cueData;
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00019340 File Offset: 0x00017540
	public static bool CheckForPrequeue(MultilingualTextDocument document, string dialogue, DialogueUtil.CueData firstCue)
	{
		if (firstCue.lineIndex == 0)
		{
			return true;
		}
		DialogueChunk dialogueChunk = document.FetchChunk(dialogue);
		for (int i = 0; i < firstCue.lineIndex; i++)
		{
			if (dialogueChunk.lines[i].HasText())
			{
				return false;
			}
			if (dialogueChunk.lines[i].waitTime > 0f)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x020003A2 RID: 930
	[Serializable]
	public struct CueData
	{
		// Token: 0x06001915 RID: 6421 RVA: 0x0006B8B9 File Offset: 0x00069AB9
		public void Clear()
		{
			this.name = "";
			this.onCue = new UnityEvent();
			this.cueObjects = new GameObject[0];
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x0006B8E0 File Offset: 0x00069AE0
		public void Cue(ref GameObject currentCamera)
		{
			this.onCue.Invoke();
			GameObject[] array = this.cueObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(true);
			}
			if (this.setCamera)
			{
				if (currentCamera != null)
				{
					currentCamera.SetActive(false);
				}
				currentCamera = this.camera;
				if (currentCamera != null)
				{
					currentCamera.SetActive(true);
				}
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0006B94C File Offset: 0x00069B4C
		public void Uncue()
		{
			GameObject[] array = this.cueObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}

		// Token: 0x04001B3C RID: 6972
		[HideInInspector]
		public string name;

		// Token: 0x04001B3D RID: 6973
		[HideInInspector]
		public int lineIndex;

		// Token: 0x04001B3E RID: 6974
		public UnityEvent onCue;

		// Token: 0x04001B3F RID: 6975
		public GameObject[] cueObjects;

		// Token: 0x04001B40 RID: 6976
		public bool setCamera;

		// Token: 0x04001B41 RID: 6977
		[ConditionalHide("setCamera", true)]
		public GameObject camera;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200011C RID: 284
public static class DialogueUtil
{
	// Token: 0x06000560 RID: 1376 RVA: 0x00005D97 File Offset: 0x00003F97
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

	// Token: 0x06000561 RID: 1377 RVA: 0x00005DC3 File Offset: 0x00003FC3
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

	// Token: 0x06000562 RID: 1378 RVA: 0x0002DA94 File Offset: 0x0002BC94
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

	// Token: 0x06000563 RID: 1379 RVA: 0x0002DD38 File Offset: 0x0002BF38
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

	// Token: 0x0200011D RID: 285
	[Serializable]
	public struct CueData
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00005DE9 File Offset: 0x00003FE9
		public void Clear()
		{
			this.name = "";
			this.onCue = new UnityEvent();
			this.cueObjects = new GameObject[0];
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002DD98 File Offset: 0x0002BF98
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

		// Token: 0x06000566 RID: 1382 RVA: 0x0002DE04 File Offset: 0x0002C004
		public void Uncue()
		{
			GameObject[] array = this.cueObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}

		// Token: 0x04000765 RID: 1893
		[HideInInspector]
		public string name;

		// Token: 0x04000766 RID: 1894
		[HideInInspector]
		public int lineIndex;

		// Token: 0x04000767 RID: 1895
		public UnityEvent onCue;

		// Token: 0x04000768 RID: 1896
		public GameObject[] cueObjects;

		// Token: 0x04000769 RID: 1897
		public bool setCamera;

		// Token: 0x0400076A RID: 1898
		[ConditionalHide("setCamera", true)]
		public GameObject camera;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class DialogueUtil
{
	// Token: 0x0600059A RID: 1434 RVA: 0x0000605D File Offset: 0x0000425D
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

	// Token: 0x0600059B RID: 1435 RVA: 0x00006089 File Offset: 0x00004289
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

	// Token: 0x0600059C RID: 1436 RVA: 0x0002F1A4 File Offset: 0x0002D3A4
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

	// Token: 0x0600059D RID: 1437 RVA: 0x0002F448 File Offset: 0x0002D648
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

	[Serializable]
	public struct CueData
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x000060AF File Offset: 0x000042AF
		public void Clear()
		{
			this.name = "";
			this.onCue = new UnityEvent();
			this.cueObjects = new GameObject[0];
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
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

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002F514 File Offset: 0x0002D714
		public void Uncue()
		{
			GameObject[] array = this.cueObjects;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].SetActive(false);
			}
		}

		[HideInInspector]
		public string name;

		[HideInInspector]
		public int lineIndex;

		public UnityEvent onCue;

		public GameObject[] cueObjects;

		public bool setCamera;

		[ConditionalHide("setCamera", true)]
		public GameObject camera;
	}
}

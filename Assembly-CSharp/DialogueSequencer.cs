using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000133 RID: 307
[AddComponentMenu("Dialogue Sequence/Sequencer", 0)]
public class DialogueSequencer : MonoBehaviour
{
	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00006152 File Offset: 0x00004352
	public static bool IsInSequence
	{
		get
		{
			return DialogueSequencer.depth > 0;
		}
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0000615C File Offset: 0x0000435C
	public void JustStartSequence()
	{
		this.StartSequence();
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x0002E914 File Offset: 0x0002CB14
	[ContextMenu("Start Sequence")]
	public Coroutine StartSequence()
	{
		if (this.runningSequence)
		{
			return null;
		}
		if (this.saveBeforeSequence)
		{
			GameData.g.WriteToDisk(true);
		}
		if (this.sequence == null || this.sequence.Length == 0)
		{
			this.sequence = base.GetComponents<DialogueSequence>();
		}
		return CoroutineUtil.Start(this.RunSequence());
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x00006165 File Offset: 0x00004365
	protected virtual IEnumerator RunSequence()
	{
		this.runningSequence = true;
		DialogueSequencer.depth++;
		if (DialogueSequencer.depth > 1 && this.waitForOtherSequencesToFinish)
		{
			yield return new WaitUntil(() => DialogueSequencer.depth == 1);
		}
		if (this.waitUntilReady && !DialogueManager.d.IsPlayerReady)
		{
			yield return base.StartCoroutine(DialogueManager.d.WaitForPlayer());
		}
		Game.DialogueDepth++;
		this.beforeSequence.Invoke();
		int j;
		if (this.sequenceObjects != null)
		{
			GameObject[] array = this.sequenceObjects;
			for (j = 0; j < array.Length; j++)
			{
				array[j].SetActive(true);
			}
		}
		yield return null;
		if (this.sequence.Length != 0 && this.sequence[0].fade)
		{
			yield return Blackout.FadeIn();
			this.fading = Blackout.FadeOut();
		}
		for (int i = 0; i < this.sequence.Length; i = j + 1)
		{
			DialogueSequence ds = this.sequence[i];
			bool willFade = i < this.sequence.Length - 1 && this.sequence[i + 1].fade;
			ds.Activate();
			if (this.fading != null)
			{
				yield return this.fading;
				this.fading = null;
			}
			YieldInstruction yieldInstruction = ds.Run();
			if (yieldInstruction != null)
			{
				yield return yieldInstruction;
			}
			if (willFade)
			{
				yield return Blackout.FadeIn();
			}
			ds.Deactivate();
			if (willFade)
			{
				this.fading = Blackout.FadeOut();
			}
			ds = null;
			j = i;
		}
		if (this.sequenceObjects != null)
		{
			GameObject[] array = this.sequenceObjects;
			for (j = 0; j < array.Length; j++)
			{
				array[j].SetActive(false);
			}
		}
		this.afterSequence.Invoke();
		if (this.chainedSequence != null)
		{
			yield return this.chainedSequence.StartSequence();
		}
		this.afterChainedSequence.Invoke();
		if (this.runningSequence)
		{
			DialogueSequencer.depth--;
			Game.DialogueDepth--;
			this.runningSequence = false;
			this.asControlRegained.Invoke();
		}
		yield break;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00006174 File Offset: 0x00004374
	private void OnDisable()
	{
		if (this.runningSequence)
		{
			Game.DialogueDepth--;
			DialogueSequencer.depth--;
			this.runningSequence = false;
		}
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x00006174 File Offset: 0x00004374
	private void OnDestroy()
	{
		if (this.runningSequence)
		{
			Game.DialogueDepth--;
			DialogueSequencer.depth--;
			this.runningSequence = false;
		}
	}

	// Token: 0x040007BE RID: 1982
	private static int depth;

	// Token: 0x040007BF RID: 1983
	private DialogueSequence[] sequence;

	// Token: 0x040007C0 RID: 1984
	public bool saveBeforeSequence;

	// Token: 0x040007C1 RID: 1985
	public UnityEvent beforeSequence;

	// Token: 0x040007C2 RID: 1986
	public UnityEvent afterSequence;

	// Token: 0x040007C3 RID: 1987
	public GameObject[] sequenceObjects;

	// Token: 0x040007C4 RID: 1988
	public DialogueSequencer chainedSequence;

	// Token: 0x040007C5 RID: 1989
	public UnityEvent afterChainedSequence;

	// Token: 0x040007C6 RID: 1990
	public UnityEvent asControlRegained;

	// Token: 0x040007C7 RID: 1991
	private bool runningSequence;

	// Token: 0x040007C8 RID: 1992
	public bool waitUntilReady;

	// Token: 0x040007C9 RID: 1993
	public bool waitForOtherSequencesToFinish;

	// Token: 0x040007CA RID: 1994
	private Coroutine fading;
}

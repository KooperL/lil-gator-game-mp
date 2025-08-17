using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Sequencer", 0)]
public class DialogueSequencer : MonoBehaviour
{
	// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00006418 File Offset: 0x00004618
	public static bool IsInSequence
	{
		get
		{
			return DialogueSequencer.depth > 0;
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00006422 File Offset: 0x00004622
	public void JustStartSequence()
	{
		this.StartSequence();
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00030010 File Offset: 0x0002E210
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

	// Token: 0x060005F5 RID: 1525 RVA: 0x0000642B File Offset: 0x0000462B
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

	// Token: 0x060005F6 RID: 1526 RVA: 0x0000643A File Offset: 0x0000463A
	private void OnDisable()
	{
		if (this.runningSequence)
		{
			Game.DialogueDepth--;
			DialogueSequencer.depth--;
			this.runningSequence = false;
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x0000643A File Offset: 0x0000463A
	private void OnDestroy()
	{
		if (this.runningSequence)
		{
			Game.DialogueDepth--;
			DialogueSequencer.depth--;
			this.runningSequence = false;
		}
	}

	private static int depth;

	private DialogueSequence[] sequence;

	public bool saveBeforeSequence;

	public UnityEvent beforeSequence;

	public UnityEvent afterSequence;

	public GameObject[] sequenceObjects;

	public DialogueSequencer chainedSequence;

	public UnityEvent afterChainedSequence;

	public UnityEvent asControlRegained;

	private bool runningSequence;

	public bool waitUntilReady;

	public bool waitForOtherSequencesToFinish;

	private Coroutine fading;
}

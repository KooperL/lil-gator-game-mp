using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Dialogue Sequence/Sequencer", 0)]
public class DialogueSequencer : MonoBehaviour
{
	// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00019E35 File Offset: 0x00018035
	public static bool IsInSequence
	{
		get
		{
			return DialogueSequencer.depth > 0;
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00019E3F File Offset: 0x0001803F
	public void JustStartSequence()
	{
		this.StartSequence();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00019E48 File Offset: 0x00018048
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

	// Token: 0x060004B8 RID: 1208 RVA: 0x00019E9A File Offset: 0x0001809A
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

	// Token: 0x060004B9 RID: 1209 RVA: 0x00019EA9 File Offset: 0x000180A9
	private void OnDisable()
	{
		if (this.runningSequence)
		{
			Game.DialogueDepth--;
			DialogueSequencer.depth--;
			this.runningSequence = false;
		}
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00019ED2 File Offset: 0x000180D2
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

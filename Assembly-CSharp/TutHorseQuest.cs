using System;
using System.Collections;
using UnityEngine;

public class TutHorseQuest : MonoBehaviour
{
	// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0000D00C File Offset: 0x0000B20C
	private string StateID
	{
		get
		{
			return "TutHorseState";
		}
	}

	// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0000D013 File Offset: 0x0000B213
	// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0000D026 File Offset: 0x0000B226
	private int State
	{
		get
		{
			return GameData.g.ReadInt(this.StateID, 0);
		}
		set
		{
			GameData.g.Write(this.StateID, value);
		}
	}

	// Token: 0x06000EE2 RID: 3810 RVA: 0x0004E498 File Offset: 0x0004C698
	private void Start()
	{
		int state = this.State;
		this.trigger1.SetActive(state == 2);
		this.trigger2.SetActive(state == 4);
		this.trigger3.SetActive(state == 6);
	}

	// Token: 0x06000EE3 RID: 3811 RVA: 0x0000D039 File Offset: 0x0000B239
	public void Interact()
	{
		base.StartCoroutine(this.InteractC());
	}

	// Token: 0x06000EE4 RID: 3812 RVA: 0x0000D048 File Offset: 0x0000B248
	private IEnumerator InteractC()
	{
		Game.DialogueDepth++;
		int state = this.State;
		switch (state)
		{
		case 0:
		case 1:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest1a", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera1.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest1b", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera1.SetActive(false);
			this.trigger1.SetActive(true);
			state = 2;
			break;
		case 2:
			this.camera1.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest2", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera1.SetActive(false);
			break;
		case 3:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest3a", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera2.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest3b", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera2.SetActive(false);
			this.trigger2.SetActive(true);
			state = 4;
			break;
		case 4:
			this.camera2.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest4", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera2.SetActive(false);
			break;
		case 5:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest5a", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera3.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest5b", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera3.SetActive(false);
			this.trigger3.SetActive(true);
			state = 6;
			break;
		case 6:
			this.camera3.SetActive(true);
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest6", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			this.camera3.SetActive(false);
			break;
		case 7:
		case 8:
			yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest8", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
			break;
		default:
			yield return null;
			break;
		}
		this.State = state;
		Game.DialogueDepth--;
		yield break;
	}

	// Token: 0x06000EE5 RID: 3813 RVA: 0x0000D057 File Offset: 0x0000B257
	public void Trigger1()
	{
		this.trigger1.SetActive(false);
		this.State = 3;
		DialogueManager.d.Bubble("Tutorial_HorseQuest2_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000EE6 RID: 3814 RVA: 0x0000D08A File Offset: 0x0000B28A
	private IEnumerator Trigger1C()
	{
		this.trigger1.SetActive(false);
		this.scene1.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest2_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene1.SetActive(false);
		this.State = 3;
		yield break;
	}

	// Token: 0x06000EE7 RID: 3815 RVA: 0x0000D099 File Offset: 0x0000B299
	public void Trigger2()
	{
		this.trigger2.SetActive(false);
		this.State = 5;
		DialogueManager.d.Bubble("Tutorial_HorseQuest4_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000EE8 RID: 3816 RVA: 0x0000D0CC File Offset: 0x0000B2CC
	private IEnumerator Trigger2C()
	{
		this.trigger2.SetActive(false);
		this.scene2.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest4_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene2.SetActive(false);
		this.State = 5;
		yield break;
	}

	// Token: 0x06000EE9 RID: 3817 RVA: 0x0000D0DB File Offset: 0x0000B2DB
	public void Trigger3()
	{
		base.StartCoroutine(this.Trigger3C());
	}

	// Token: 0x06000EEA RID: 3818 RVA: 0x0000D0EA File Offset: 0x0000B2EA
	private IEnumerator Trigger3C()
	{
		Game.DialogueDepth++;
		this.horse.SetActive(false);
		this.trigger3.SetActive(false);
		this.scene3a.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest7a", null, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene3b.SetActive(true);
		this.scene3a.SetActive(false);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest7b", this.cliffActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest7c", this.cliffActors, DialogueManager.DialogueBoxBackground.Standard, true));
		yield return ItemManager.i.UnlockItem("Hat1");
		yield return Blackout.FadeIn();
		this.scene3b.SetActive(false);
		this.horse.SetActive(true);
		yield return Blackout.FadeOut();
		this.State = 8;
		Game.DialogueDepth--;
		yield break;
	}

	public DialogueActor[] actors;

	public GameObject horse;

	public GameObject camera1;

	public GameObject trigger1;

	public GameObject scene1;

	public GameObject camera2;

	public GameObject trigger2;

	public GameObject scene2;

	public GameObject camera3;

	public GameObject trigger3;

	public GameObject scene3a;

	public GameObject scene3b;

	public DialogueActor[] cliffActors;
}

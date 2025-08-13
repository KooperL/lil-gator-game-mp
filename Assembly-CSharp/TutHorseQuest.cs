using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class TutHorseQuest : MonoBehaviour
{
	// Token: 0x170001A8 RID: 424
	// (get) Token: 0x06000E93 RID: 3731 RVA: 0x0000CCFA File Offset: 0x0000AEFA
	private string StateID
	{
		get
		{
			return "TutHorseState";
		}
	}

	// Token: 0x170001A9 RID: 425
	// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0000CD01 File Offset: 0x0000AF01
	// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0000CD14 File Offset: 0x0000AF14
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

	// Token: 0x06000E96 RID: 3734 RVA: 0x0004C934 File Offset: 0x0004AB34
	private void Start()
	{
		int state = this.State;
		this.trigger1.SetActive(state == 2);
		this.trigger2.SetActive(state == 4);
		this.trigger3.SetActive(state == 6);
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x0000CD27 File Offset: 0x0000AF27
	public void Interact()
	{
		base.StartCoroutine(this.InteractC());
	}

	// Token: 0x06000E98 RID: 3736 RVA: 0x0000CD36 File Offset: 0x0000AF36
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

	// Token: 0x06000E99 RID: 3737 RVA: 0x0000CD45 File Offset: 0x0000AF45
	public void Trigger1()
	{
		this.trigger1.SetActive(false);
		this.State = 3;
		DialogueManager.d.Bubble("Tutorial_HorseQuest2_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x0000CD78 File Offset: 0x0000AF78
	private IEnumerator Trigger1C()
	{
		this.trigger1.SetActive(false);
		this.scene1.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest2_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene1.SetActive(false);
		this.State = 3;
		yield break;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x0000CD87 File Offset: 0x0000AF87
	public void Trigger2()
	{
		this.trigger2.SetActive(false);
		this.State = 5;
		DialogueManager.d.Bubble("Tutorial_HorseQuest4_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x0000CDBA File Offset: 0x0000AFBA
	private IEnumerator Trigger2C()
	{
		this.trigger2.SetActive(false);
		this.scene2.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest4_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene2.SetActive(false);
		this.State = 5;
		yield break;
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x0000CDC9 File Offset: 0x0000AFC9
	public void Trigger3()
	{
		base.StartCoroutine(this.Trigger3C());
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x0000CDD8 File Offset: 0x0000AFD8
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

	// Token: 0x040012B3 RID: 4787
	public DialogueActor[] actors;

	// Token: 0x040012B4 RID: 4788
	public GameObject horse;

	// Token: 0x040012B5 RID: 4789
	public GameObject camera1;

	// Token: 0x040012B6 RID: 4790
	public GameObject trigger1;

	// Token: 0x040012B7 RID: 4791
	public GameObject scene1;

	// Token: 0x040012B8 RID: 4792
	public GameObject camera2;

	// Token: 0x040012B9 RID: 4793
	public GameObject trigger2;

	// Token: 0x040012BA RID: 4794
	public GameObject scene2;

	// Token: 0x040012BB RID: 4795
	public GameObject camera3;

	// Token: 0x040012BC RID: 4796
	public GameObject trigger3;

	// Token: 0x040012BD RID: 4797
	public GameObject scene3a;

	// Token: 0x040012BE RID: 4798
	public GameObject scene3b;

	// Token: 0x040012BF RID: 4799
	public DialogueActor[] cliffActors;
}

using System;
using System.Collections;
using UnityEngine;

public class TutHorseQuest : MonoBehaviour
{
	// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00039667 File Offset: 0x00037867
	private string StateID
	{
		get
		{
			return "TutHorseState";
		}
	}

	// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0003966E File Offset: 0x0003786E
	// (set) Token: 0x06000C02 RID: 3074 RVA: 0x00039681 File Offset: 0x00037881
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

	// Token: 0x06000C03 RID: 3075 RVA: 0x00039694 File Offset: 0x00037894
	private void Start()
	{
		int state = this.State;
		this.trigger1.SetActive(state == 2);
		this.trigger2.SetActive(state == 4);
		this.trigger3.SetActive(state == 6);
	}

	// Token: 0x06000C04 RID: 3076 RVA: 0x000396D5 File Offset: 0x000378D5
	public void Interact()
	{
		base.StartCoroutine(this.InteractC());
	}

	// Token: 0x06000C05 RID: 3077 RVA: 0x000396E4 File Offset: 0x000378E4
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

	// Token: 0x06000C06 RID: 3078 RVA: 0x000396F3 File Offset: 0x000378F3
	public void Trigger1()
	{
		this.trigger1.SetActive(false);
		this.State = 3;
		DialogueManager.d.Bubble("Tutorial_HorseQuest2_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00039726 File Offset: 0x00037926
	private IEnumerator Trigger1C()
	{
		this.trigger1.SetActive(false);
		this.scene1.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest2_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene1.SetActive(false);
		this.State = 3;
		yield break;
	}

	// Token: 0x06000C08 RID: 3080 RVA: 0x00039735 File Offset: 0x00037935
	public void Trigger2()
	{
		this.trigger2.SetActive(false);
		this.State = 5;
		DialogueManager.d.Bubble("Tutorial_HorseQuest4_Trigger", this.actors, 0f, false, true, true);
	}

	// Token: 0x06000C09 RID: 3081 RVA: 0x00039768 File Offset: 0x00037968
	private IEnumerator Trigger2C()
	{
		this.trigger2.SetActive(false);
		this.scene2.SetActive(true);
		yield return base.StartCoroutine(DialogueManager.d.LoadChunk("Tutorial_HorseQuest4_Trigger", this.actors, DialogueManager.DialogueBoxBackground.Standard, true));
		this.scene2.SetActive(false);
		this.State = 5;
		yield break;
	}

	// Token: 0x06000C0A RID: 3082 RVA: 0x00039777 File Offset: 0x00037977
	public void Trigger3()
	{
		base.StartCoroutine(this.Trigger3C());
	}

	// Token: 0x06000C0B RID: 3083 RVA: 0x00039786 File Offset: 0x00037986
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingUpgradeStation : MonoBehaviour, Interaction
{
	// Token: 0x0600022B RID: 555 RVA: 0x0000BF68 File Offset: 0x0000A168
	public static void UpdateAllActive()
	{
		foreach (BuildingUpgradeStation buildingUpgradeStation in BuildingUpgradeStation.upgradedStations)
		{
			buildingUpgradeStation.UpdateActive();
		}
	}

	// (get) Token: 0x0600022C RID: 556 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
	// (set) Token: 0x0600022D RID: 557 RVA: 0x0000BFCB File Offset: 0x0000A1CB
	public bool IsUpgraded
	{
		get
		{
			return GameData.g.ReadBool(this.id, false);
		}
		private set
		{
			GameData.g.Write(this.id, value);
		}
	}

	// (get) Token: 0x0600022E RID: 558 RVA: 0x0000BFDE File Offset: 0x0000A1DE
	// (set) Token: 0x0600022F RID: 559 RVA: 0x0000C014 File Offset: 0x0000A214
	public bool HasPrompted
	{
		get
		{
			if (string.IsNullOrEmpty(this.promptedID))
			{
				this.promptedID = this.id + "_Prompted";
			}
			return GameData.g.ReadBool(this.promptedID, false);
		}
		private set
		{
			if (string.IsNullOrEmpty(this.promptedID))
			{
				this.promptedID = this.id + "_Prompted";
			}
			GameData.g.Write(this.promptedID, value);
		}
	}

	// Token: 0x06000230 RID: 560 RVA: 0x0000C04A File Offset: 0x0000A24A
	private void Awake()
	{
		this.waitUpgradeStep = new WaitForSeconds(0.4f);
		this.waitUpgradePost = new WaitForSeconds(1f);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x0000C06C File Offset: 0x0000A26C
	private void OnValidate()
	{
		if (this.uiCurrency == null || (this.upgradeCurrency != null && this.uiCurrency.itemResource != this.upgradeCurrency))
		{
			foreach (UIItemResource uiitemResource in Object.FindObjectsOfType<UIItemResource>())
			{
				if (uiitemResource.itemResource == this.upgradeCurrency)
				{
					this.uiCurrency = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x06000232 RID: 562 RVA: 0x0000C0E0 File Offset: 0x0000A2E0
	[ContextMenu("Get Objects")]
	public void GetObjects()
	{
		if (this.upgradeParents == null)
		{
			return;
		}
		List<GameObject> list = new List<GameObject>();
		foreach (Transform transform in this.upgradeParents)
		{
			int num = 0;
			foreach (object obj in transform)
			{
				Transform transform2 = (Transform)obj;
				list.Add(transform2.gameObject);
				num++;
			}
		}
		this.upgradeObjects = list.ToArray();
	}

	// Token: 0x06000233 RID: 563 RVA: 0x0000C17C File Offset: 0x0000A37C
	public void Interact()
	{
		CoroutineUtil.Start(this.RunInteraction());
	}

	// Token: 0x06000234 RID: 564 RVA: 0x0000C18A File Offset: 0x0000A38A
	public void SetActive(bool isActive)
	{
		base.gameObject.SetActive(isActive);
		this.UpdateState();
		if (this.IsUpgraded)
		{
			BuildingUpgradeStation.upgradedStations.Add(this);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
	[ContextMenu("Update State")]
	public void UpdateState()
	{
		bool isUpgraded = this.IsUpgraded;
		GameObject[] array = this.upgradeObjects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].SetActive(isUpgraded);
		}
		this.UpdateCharacters(isUpgraded, base.gameObject.activeSelf);
		DependentDecoration.ActivateAll();
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000C20C File Offset: 0x0000A40C
	private void UpdateCharacters(bool isUpgraded, bool isUnlocked)
	{
		bool flag = true;
		if (this.actors.Length != 0)
		{
			flag = this.actors[0].ignoreUnlock || this.actors[0].profile.IsUnlocked;
		}
		if (this.preUnlockCharacter != null)
		{
			this.preUnlockCharacter.SetActive(flag && !isUnlocked && !isUpgraded);
		}
		if (this.preUpdateCharacter != null)
		{
			this.preUpdateCharacter.SetActive(flag && !isUpgraded && (this.preUnlockCharacter == null || isUnlocked));
		}
		if (this.postUpgradeCharacter != null)
		{
			this.postUpgradeCharacter.SetActive(flag && isUpgraded);
		}
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000C2BB File Offset: 0x0000A4BB
	public IEnumerator RunUnlockSequence()
	{
		this.UpdateCharacters(false, true);
		this.previewCanvas.SetActive(false);
		base.gameObject.SetActive(true);
		yield return this.unlockSequence.StartSequence();
		yield break;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000C2CA File Offset: 0x0000A4CA
	private IEnumerator RunInteraction()
	{
		Game.DialogueDepth++;
		this.UpdateState();
		if (this.HasPrompted)
		{
			if (!base.gameObject.activeSelf)
			{
				if (this.IsUpgraded)
				{
					yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.completeRepeat), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				}
				else
				{
					yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.notReady), (this.preUnlockCharacter != null) ? this.preUnlockActors : this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				}
				Game.DialogueDepth--;
				yield break;
			}
			if (!string.IsNullOrEmpty(this.repeatPrompt))
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.repeatPrompt), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
		}
		else
		{
			if (!string.IsNullOrEmpty(this.firstPrompt))
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.firstPrompt), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			this.HasPrompted = true;
			if (!base.gameObject.activeSelf)
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.notReady), (this.preUnlockCharacter != null) ? this.preUnlockActors : this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
				Game.DialogueDepth--;
				yield break;
			}
			if (!string.IsNullOrEmpty(this.lezgo))
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.lezgo), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
		}
		this.previewCanvas.SetActive(true);
		this.previewCamera.SetActive(true);
		if (this.cost > 0)
		{
			this.uiCurrency.SetPrice(this.cost);
			this.upgradeCurrency.ForceShow = true;
		}
		yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.upgradePrompt), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
		int optionChosen = DialogueManager.optionChosen;
		if (optionChosen != 0)
		{
			if (optionChosen == 1)
			{
				this.uiCurrency.ClearPrice();
			}
		}
		else if (this.upgradeCurrency.Amount >= this.cost)
		{
			this.uiCurrency.ClearPrice();
			this.upgradeCurrency.Amount -= this.cost;
			yield return CoroutineUtil.Start(this.UpgradeAnimation());
			if (!string.IsNullOrEmpty(this.complete))
			{
				yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.complete), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			}
			this.onUpgraded.Invoke();
		}
		else
		{
			yield return CoroutineUtil.Start(DialogueManager.d.LoadChunk(this.document.FetchChunk(this.notEnough), this.actors, DialogueManager.DialogueBoxBackground.Standard, true, true, false, false));
			this.uiCurrency.ClearPrice();
		}
		this.previewCamera.SetActive(false);
		this.upgradeCurrency.ForceShow = false;
		Game.DialogueDepth--;
		if (this.IsUpgraded)
		{
			BuildingUpgradeStation.upgradedStations.Add(this);
			if (this.backToGameCamera != null)
			{
				this.bulletinBoard.SetActive(false);
				this.backToGameCamera.SetActive(true);
				yield return null;
				this.backToGameCamera.SetActive(false);
			}
			base.gameObject.SetActive(false);
		}
		yield break;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000C2D9 File Offset: 0x0000A4D9
	private IEnumerator UpgradeAnimation()
	{
		yield return null;
		this.upgradeEffectCamera.SetActive(true);
		this.onStartUpgrade.Invoke();
		int l;
		for (int i = 0; i < 4; i = l + 1)
		{
			for (int j = 0; j < this.upgradeObjects.Length; j++)
			{
				int num = Mathf.FloorToInt((float)(2 * Random.Range(0, i + 1)));
				if (num > 0)
				{
					EffectsManager.e.Dust(this.upgradeObjects[j].transform.position, num, Vector3.zero, 3f);
				}
			}
			for (int k = 0; k < this.smokePuffObjects.Length; k++)
			{
				int num2 = Mathf.FloorToInt((float)(2 * Random.Range(0, i + 1)));
				if (num2 > 0)
				{
					EffectsManager.e.Dust(this.smokePuffObjects[k].transform.position, num2, Vector3.zero, 3f);
				}
			}
			yield return this.waitUpgradeStep;
			l = i;
		}
		this.IsUpgraded = true;
		this.UpdateState();
		foreach (GameObject gameObject in this.upgradeObjects)
		{
			EffectsManager.e.Dust(gameObject.transform.position, 2, Vector3.zero, 6f);
		}
		foreach (GameObject gameObject2 in this.smokePuffObjects)
		{
			EffectsManager.e.Dust(gameObject2.transform.position, 2, Vector3.zero, 6f);
		}
		this.upgradeEffectCamera.SetActive(false);
		this.previewCanvas.SetActive(false);
		yield return this.waitUpgradePost;
		yield break;
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000C2E8 File Offset: 0x0000A4E8
	public void UpdateActive()
	{
		foreach (GameObject gameObject in this.upgradeObjects)
		{
			if (gameObject != null)
			{
				gameObject.SetActive(true);
			}
		}
	}

	public static List<BuildingUpgradeStation> upgradedStations = new List<BuildingUpgradeStation>();

	public GameObject previewCamera;

	public GameObject previewCanvas;

	public GameObject upgradeEffectCamera;

	public UnityEvent onStartUpgrade;

	public GameObject bulletinBoard;

	public GameObject backToGameCamera;

	public int cost;

	public ItemResource upgradeCurrency;

	public UIItemResource uiCurrency;

	public Transform[] upgradeParents;

	public GameObject[] upgradeObjects;

	public GameObject[] smokePuffObjects;

	public UnityEvent onUpgraded;

	private WaitForSeconds waitUpgradeStep;

	private WaitForSeconds waitUpgradePost;

	public MultilingualTextDocument document;

	[ChunkLookup("document")]
	public string firstPrompt;

	[ChunkLookup("document")]
	public string notReady;

	[ChunkLookup("document")]
	public string lezgo;

	[ChunkLookup("document")]
	public string repeatPrompt;

	[ChunkLookup("document")]
	public string upgradePrompt;

	[ChunkLookup("document")]
	public string notEnough;

	[ChunkLookup("document")]
	public string complete;

	[ChunkLookup("document")]
	public string completeRepeat;

	public DialogueActor[] preUnlockActors;

	public DialogueActor[] actors;

	public DialogueActor[] upgradedActors;

	public GameObject preUnlockCharacter;

	public GameObject preUpdateCharacter;

	public GameObject postUpgradeCharacter;

	public DialogueSequencer unlockSequence;

	public string id;

	private string promptedID;
}

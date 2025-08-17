using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingUpgradeStation : MonoBehaviour, Interaction
{
	// Token: 0x06000270 RID: 624 RVA: 0x00020294 File Offset: 0x0001E494
	public static void UpdateAllActive()
	{
		foreach (BuildingUpgradeStation buildingUpgradeStation in BuildingUpgradeStation.upgradedStations)
		{
			buildingUpgradeStation.UpdateActive();
		}
	}

	// (get) Token: 0x06000271 RID: 625 RVA: 0x000040C1 File Offset: 0x000022C1
	// (set) Token: 0x06000272 RID: 626 RVA: 0x000040D4 File Offset: 0x000022D4
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

	// (get) Token: 0x06000273 RID: 627 RVA: 0x000040E7 File Offset: 0x000022E7
	// (set) Token: 0x06000274 RID: 628 RVA: 0x0000411D File Offset: 0x0000231D
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

	// Token: 0x06000275 RID: 629 RVA: 0x00004153 File Offset: 0x00002353
	private void Awake()
	{
		this.waitUpgradeStep = new WaitForSeconds(0.4f);
		this.waitUpgradePost = new WaitForSeconds(1f);
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000202E4 File Offset: 0x0001E4E4
	private void OnValidate()
	{
		if (this.uiCurrency == null || (this.upgradeCurrency != null && this.uiCurrency.itemResource != this.upgradeCurrency))
		{
			foreach (UIItemResource uiitemResource in global::UnityEngine.Object.FindObjectsOfType<UIItemResource>())
			{
				if (uiitemResource.itemResource == this.upgradeCurrency)
				{
					this.uiCurrency = uiitemResource;
					return;
				}
			}
		}
	}

	// Token: 0x06000277 RID: 631 RVA: 0x00020358 File Offset: 0x0001E558
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

	// Token: 0x06000278 RID: 632 RVA: 0x00004175 File Offset: 0x00002375
	public void Interact()
	{
		CoroutineUtil.Start(this.RunInteraction());
	}

	// Token: 0x06000279 RID: 633 RVA: 0x00004183 File Offset: 0x00002383
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

	// Token: 0x0600027A RID: 634 RVA: 0x000203F4 File Offset: 0x0001E5F4
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

	// Token: 0x0600027B RID: 635 RVA: 0x00020440 File Offset: 0x0001E640
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

	// Token: 0x0600027C RID: 636 RVA: 0x000041B6 File Offset: 0x000023B6
	public IEnumerator RunUnlockSequence()
	{
		this.UpdateCharacters(false, true);
		this.previewCanvas.SetActive(false);
		base.gameObject.SetActive(true);
		yield return this.unlockSequence.StartSequence();
		yield break;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x000041C5 File Offset: 0x000023C5
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

	// Token: 0x0600027E RID: 638 RVA: 0x000041D4 File Offset: 0x000023D4
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
				int num = Mathf.FloorToInt((float)(2 * global::UnityEngine.Random.Range(0, i + 1)));
				if (num > 0)
				{
					EffectsManager.e.Dust(this.upgradeObjects[j].transform.position, num, Vector3.zero, 3f);
				}
			}
			for (int k = 0; k < this.smokePuffObjects.Length; k++)
			{
				int num2 = Mathf.FloorToInt((float)(2 * global::UnityEngine.Random.Range(0, i + 1)));
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

	// Token: 0x0600027F RID: 639 RVA: 0x000204F0 File Offset: 0x0001E6F0
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000A9 RID: 169
public class BuildingUpgradeStation : MonoBehaviour, Interaction
{
	// Token: 0x06000263 RID: 611 RVA: 0x0001F83C File Offset: 0x0001DA3C
	public static void UpdateAllActive()
	{
		foreach (BuildingUpgradeStation buildingUpgradeStation in BuildingUpgradeStation.upgradedStations)
		{
			buildingUpgradeStation.UpdateActive();
		}
	}

	// Token: 0x17000027 RID: 39
	// (get) Token: 0x06000264 RID: 612 RVA: 0x00003FD5 File Offset: 0x000021D5
	// (set) Token: 0x06000265 RID: 613 RVA: 0x00003FE8 File Offset: 0x000021E8
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

	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000266 RID: 614 RVA: 0x00003FFB File Offset: 0x000021FB
	// (set) Token: 0x06000267 RID: 615 RVA: 0x00004031 File Offset: 0x00002231
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

	// Token: 0x06000268 RID: 616 RVA: 0x00004067 File Offset: 0x00002267
	private void Awake()
	{
		this.waitUpgradeStep = new WaitForSeconds(0.4f);
		this.waitUpgradePost = new WaitForSeconds(1f);
	}

	// Token: 0x06000269 RID: 617 RVA: 0x0001F88C File Offset: 0x0001DA8C
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

	// Token: 0x0600026A RID: 618 RVA: 0x0001F900 File Offset: 0x0001DB00
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

	// Token: 0x0600026B RID: 619 RVA: 0x00004089 File Offset: 0x00002289
	public void Interact()
	{
		CoroutineUtil.Start(this.RunInteraction());
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00004097 File Offset: 0x00002297
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

	// Token: 0x0600026D RID: 621 RVA: 0x0001F99C File Offset: 0x0001DB9C
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

	// Token: 0x0600026E RID: 622 RVA: 0x0001F9E8 File Offset: 0x0001DBE8
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

	// Token: 0x0600026F RID: 623 RVA: 0x000040CA File Offset: 0x000022CA
	public IEnumerator RunUnlockSequence()
	{
		this.UpdateCharacters(false, true);
		this.previewCanvas.SetActive(false);
		base.gameObject.SetActive(true);
		yield return this.unlockSequence.StartSequence();
		yield break;
	}

	// Token: 0x06000270 RID: 624 RVA: 0x000040D9 File Offset: 0x000022D9
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

	// Token: 0x06000271 RID: 625 RVA: 0x000040E8 File Offset: 0x000022E8
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

	// Token: 0x06000272 RID: 626 RVA: 0x0001FA98 File Offset: 0x0001DC98
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

	// Token: 0x04000369 RID: 873
	public static List<BuildingUpgradeStation> upgradedStations = new List<BuildingUpgradeStation>();

	// Token: 0x0400036A RID: 874
	public GameObject previewCamera;

	// Token: 0x0400036B RID: 875
	public GameObject previewCanvas;

	// Token: 0x0400036C RID: 876
	public GameObject upgradeEffectCamera;

	// Token: 0x0400036D RID: 877
	public UnityEvent onStartUpgrade;

	// Token: 0x0400036E RID: 878
	public GameObject bulletinBoard;

	// Token: 0x0400036F RID: 879
	public GameObject backToGameCamera;

	// Token: 0x04000370 RID: 880
	public int cost;

	// Token: 0x04000371 RID: 881
	public ItemResource upgradeCurrency;

	// Token: 0x04000372 RID: 882
	public UIItemResource uiCurrency;

	// Token: 0x04000373 RID: 883
	public Transform[] upgradeParents;

	// Token: 0x04000374 RID: 884
	public GameObject[] upgradeObjects;

	// Token: 0x04000375 RID: 885
	public GameObject[] smokePuffObjects;

	// Token: 0x04000376 RID: 886
	public UnityEvent onUpgraded;

	// Token: 0x04000377 RID: 887
	private WaitForSeconds waitUpgradeStep;

	// Token: 0x04000378 RID: 888
	private WaitForSeconds waitUpgradePost;

	// Token: 0x04000379 RID: 889
	public MultilingualTextDocument document;

	// Token: 0x0400037A RID: 890
	[ChunkLookup("document")]
	public string firstPrompt;

	// Token: 0x0400037B RID: 891
	[ChunkLookup("document")]
	public string notReady;

	// Token: 0x0400037C RID: 892
	[ChunkLookup("document")]
	public string lezgo;

	// Token: 0x0400037D RID: 893
	[ChunkLookup("document")]
	public string repeatPrompt;

	// Token: 0x0400037E RID: 894
	[ChunkLookup("document")]
	public string upgradePrompt;

	// Token: 0x0400037F RID: 895
	[ChunkLookup("document")]
	public string notEnough;

	// Token: 0x04000380 RID: 896
	[ChunkLookup("document")]
	public string complete;

	// Token: 0x04000381 RID: 897
	[ChunkLookup("document")]
	public string completeRepeat;

	// Token: 0x04000382 RID: 898
	public DialogueActor[] preUnlockActors;

	// Token: 0x04000383 RID: 899
	public DialogueActor[] actors;

	// Token: 0x04000384 RID: 900
	public DialogueActor[] upgradedActors;

	// Token: 0x04000385 RID: 901
	public GameObject preUnlockCharacter;

	// Token: 0x04000386 RID: 902
	public GameObject preUpdateCharacter;

	// Token: 0x04000387 RID: 903
	public GameObject postUpgradeCharacter;

	// Token: 0x04000388 RID: 904
	public DialogueSequencer unlockSequence;

	// Token: 0x04000389 RID: 905
	public string id;

	// Token: 0x0400038A RID: 906
	private string promptedID;
}

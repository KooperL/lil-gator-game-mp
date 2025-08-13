using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020003B1 RID: 945
public class UIItemResource : MonoBehaviour
{
	// Token: 0x060011EE RID: 4590 RVA: 0x000594C8 File Offset: 0x000576C8
	private void Awake()
	{
		if (this.itemResource != null && !this.listenerAdded)
		{
			this.itemResource.onAmountChanged.AddListener(new UnityAction<int>(this.AmountChanged));
			this.listenerAdded = true;
		}
		if (this.collectSound == null)
		{
			this.collectSound = this.itemResource.collectSoundEffect.GetComponent<AudioSourceVariance>();
		}
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0000F47E File Offset: 0x0000D67E
	private void OnEnable()
	{
		if (Time.time > this.deltaTransferTime)
		{
			this.deltaValue = 0f;
		}
		this.currentValue = this.itemResource.Amount - (int)this.deltaValue;
		this.UpdateDisplay();
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x00059534 File Offset: 0x00057734
	private void Start()
	{
		if (this.hide && this.startHidden)
		{
			this.boxTransform.anchoredPosition = new Vector2(this.hiddenPosition, this.boxTransform.anchoredPosition.y);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0000F4B7 File Offset: 0x0000D6B7
	private void OnDisable()
	{
		this.deltaValue = 0f;
		this.currentValue = this.itemResource.Amount;
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0000F4D5 File Offset: 0x0000D6D5
	private void OnDestroy()
	{
		if (this.itemResource != null)
		{
			this.itemResource.onAmountChanged.RemoveListener(new UnityAction<int>(this.AmountChanged));
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x00059584 File Offset: 0x00057784
	private void LateUpdate()
	{
		if (this.deltaValue != 0f)
		{
			if (this.skipDelta)
			{
				this.deltaTransferTime = -1f;
				this.deltaValue = 0f;
			}
			if (this.deltaTransferTime < Time.time)
			{
				this.deltaValue = Mathf.MoveTowards(this.deltaValue, 0f, this.deltaTransferSpeed * Time.deltaTime);
				this.UpdateDisplay();
			}
			this.hideTime = Time.time + this.hideDelay;
		}
		if (!this.DisplayChanges)
		{
			this.hideTime = Time.time;
		}
		if (this.pullout != null)
		{
			if (this.deltaTransferTime < Time.time)
			{
				if (this.pullout.gameObject.activeSelf)
				{
					float num = Mathf.SmoothDamp(this.pullout.anchoredPosition.x, this.pulloutHiddenX, ref this.pulloutVelocity, 0.25f);
					this.pullout.anchoredPosition = new Vector2(num, this.pullout.anchoredPosition.y);
					if (num == this.pulloutHiddenX)
					{
						this.pullout.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				this.pullout.gameObject.SetActive(true);
				float num2 = Mathf.SmoothDamp(this.pullout.anchoredPosition.x, this.pulloutVisibleX, ref this.pulloutVelocity, 0.1f);
				this.pullout.anchoredPosition = new Vector2(num2, this.pullout.anchoredPosition.y);
			}
		}
		bool flag = this.itemResource.ForceShow || this.hideTime < 0f || Time.time < this.hideTime;
		float num3 = this.boxTransform.anchoredPosition.x;
		if (flag)
		{
			num3 = Mathf.SmoothDamp(num3, 0f, ref this.velocity, 0.1f);
		}
		else
		{
			num3 = Mathf.SmoothDamp(num3, this.hiddenPosition, ref this.velocity, 0.25f);
		}
		this.boxTransform.anchoredPosition = new Vector2(num3, this.boxTransform.anchoredPosition.y);
		if (num3 == this.hiddenPosition)
		{
			base.gameObject.SetActive(false);
			return;
		}
	}

	// Token: 0x060011F4 RID: 4596 RVA: 0x000597B0 File Offset: 0x000579B0
	public void SetItemResource(ItemResource newResource)
	{
		if (newResource == this.itemResource)
		{
			return;
		}
		if (this.itemResource != null)
		{
			this.itemResource.onAmountChanged.RemoveListener(new UnityAction<int>(this.AmountChanged));
		}
		if (newResource != null)
		{
			newResource.onAmountChanged.AddListener(new UnityAction<int>(this.AmountChanged));
		}
		this.itemResource = newResource;
		this.UpdateDisplay();
	}

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0000F501 File Offset: 0x0000D701
	private bool DisplayChanges
	{
		get
		{
			return Game.State == GameState.Play || Game.State == GameState.Dialogue || this.showOutsideGameplay || this.itemResource.ForceShow;
		}
	}

	// Token: 0x060011F6 RID: 4598 RVA: 0x00059824 File Offset: 0x00057A24
	public void AmountChanged(int amount)
	{
		if (!this.DisplayChanges)
		{
			this.currentValue = amount;
			return;
		}
		if (!this.itemResource.lastChangeHidden && amount != this.currentValue)
		{
			this.deltaValue = (float)(amount - this.currentValue);
			if (this.deltaValue > 0f)
			{
				if (Time.time > this.soundBudgetResetTime)
				{
					this.soundBudgetResetTime = Time.time + this.soundBudgetFrame;
					this.soundBudget = this.soundsPerFrame;
				}
				if (this.soundBudget > 0)
				{
					this.soundBudget--;
					this.collectSound.Play();
				}
			}
			this.deltaTransferTime = Time.time + this.deltaTransferDelay;
		}
		if (this.skipDelta)
		{
			this.deltaTransferTime = -1f;
			this.deltaValue = 0f;
		}
		this.UpdateDisplay();
	}

	// Token: 0x060011F7 RID: 4599 RVA: 0x000598FC File Offset: 0x00057AFC
	public void UpdateDisplay()
	{
		if (this.itemResource == null)
		{
			return;
		}
		this.hideTime = Time.time + this.hideDelay;
		this.currentValue = this.itemResource.Amount - Mathf.FloorToInt(this.deltaValue);
		if (this.bold)
		{
			this.text.text = string.Format("<b>{0}</b>", this.currentValue.ToString("0"));
		}
		else
		{
			this.text.text = this.currentValue.ToString("0");
		}
		if (this.image != null)
		{
			this.image.sprite = this.itemResource.sprite;
		}
		if (this.deltaValue == 0f)
		{
			this.deltaText.text = "";
		}
		else if (this.bold)
		{
			this.deltaText.text = string.Format("<b>{0}</b>", Mathf.FloorToInt(this.deltaValue).ToString("+#;-#;0"));
		}
		else
		{
			this.deltaText.text = string.Format("{0}", Mathf.FloorToInt(this.deltaValue).ToString("+#;-#;0"));
		}
		if (this.background != null)
		{
			this.background.color = this.itemResource.color;
		}
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060011F8 RID: 4600 RVA: 0x00059A78 File Offset: 0x00057C78
	public void SetPrice(int price)
	{
		price = Mathf.Abs(price);
		if (this.bold)
		{
			this.priceText.text = string.Format("<b>{0}</b>", Mathf.FloorToInt((float)price).ToString("0"));
		}
		else
		{
			this.priceText.text = string.Format("{0}", Mathf.FloorToInt((float)price).ToString("0"));
		}
		this.pricetag.SetActive(true);
	}

	// Token: 0x060011F9 RID: 4601 RVA: 0x0000F527 File Offset: 0x0000D727
	public void ClearPrice()
	{
		this.pricetag.SetActive(false);
	}

	// Token: 0x04001729 RID: 5929
	public ItemResource itemResource;

	// Token: 0x0400172A RID: 5930
	public bool showOutsideGameplay;

	// Token: 0x0400172B RID: 5931
	public Text text;

	// Token: 0x0400172C RID: 5932
	public Text deltaText;

	// Token: 0x0400172D RID: 5933
	public Image image;

	// Token: 0x0400172E RID: 5934
	public Image background;

	// Token: 0x0400172F RID: 5935
	public GameObject pricetag;

	// Token: 0x04001730 RID: 5936
	public Text priceText;

	// Token: 0x04001731 RID: 5937
	public bool hide = true;

	// Token: 0x04001732 RID: 5938
	public bool startHidden;

	// Token: 0x04001733 RID: 5939
	public float hideDelay = 5f;

	// Token: 0x04001734 RID: 5940
	private float hideTime = -1f;

	// Token: 0x04001735 RID: 5941
	public float hiddenPosition;

	// Token: 0x04001736 RID: 5942
	private float velocity;

	// Token: 0x04001737 RID: 5943
	public RectTransform boxTransform;

	// Token: 0x04001738 RID: 5944
	public int currentValue;

	// Token: 0x04001739 RID: 5945
	public float deltaValue;

	// Token: 0x0400173A RID: 5946
	private float deltaTransferTime = -1f;

	// Token: 0x0400173B RID: 5947
	public float deltaTransferDelay = 2f;

	// Token: 0x0400173C RID: 5948
	public float deltaTransferSpeed = 30f;

	// Token: 0x0400173D RID: 5949
	private AudioSourceVariance collectSound;

	// Token: 0x0400173E RID: 5950
	public RectTransform pullout;

	// Token: 0x0400173F RID: 5951
	public float pulloutHiddenX;

	// Token: 0x04001740 RID: 5952
	public float pulloutVisibleX;

	// Token: 0x04001741 RID: 5953
	private float pulloutVelocity;

	// Token: 0x04001742 RID: 5954
	public bool bold;

	// Token: 0x04001743 RID: 5955
	private int soundsPerFrame = 3;

	// Token: 0x04001744 RID: 5956
	private int soundBudget;

	// Token: 0x04001745 RID: 5957
	private float soundBudgetResetTime = -1f;

	// Token: 0x04001746 RID: 5958
	private float soundBudgetFrame = 0.05f;

	// Token: 0x04001747 RID: 5959
	private bool listenerAdded;

	// Token: 0x04001748 RID: 5960
	public bool skipDelta;
}

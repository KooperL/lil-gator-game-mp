using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIItemResource : MonoBehaviour
{
	// Token: 0x0600124E RID: 4686 RVA: 0x0005B468 File Offset: 0x00059668
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

	// Token: 0x0600124F RID: 4687 RVA: 0x0000F871 File Offset: 0x0000DA71
	private void OnEnable()
	{
		if (Time.time > this.deltaTransferTime)
		{
			this.deltaValue = 0f;
		}
		this.currentValue = this.itemResource.Amount - (int)this.deltaValue;
		this.UpdateDisplay();
	}

	// Token: 0x06001250 RID: 4688 RVA: 0x0005B4D4 File Offset: 0x000596D4
	private void Start()
	{
		if (this.hide && this.startHidden)
		{
			this.boxTransform.anchoredPosition = new Vector2(this.hiddenPosition, this.boxTransform.anchoredPosition.y);
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001251 RID: 4689 RVA: 0x0000F8AA File Offset: 0x0000DAAA
	private void OnDisable()
	{
		this.deltaValue = 0f;
		this.currentValue = this.itemResource.Amount;
	}

	// Token: 0x06001252 RID: 4690 RVA: 0x0000F8C8 File Offset: 0x0000DAC8
	private void OnDestroy()
	{
		if (this.itemResource != null)
		{
			this.itemResource.onAmountChanged.RemoveListener(new UnityAction<int>(this.AmountChanged));
		}
	}

	// Token: 0x06001253 RID: 4691 RVA: 0x0005B524 File Offset: 0x00059724
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

	// Token: 0x06001254 RID: 4692 RVA: 0x0005B750 File Offset: 0x00059950
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

	// (get) Token: 0x06001255 RID: 4693 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
	private bool DisplayChanges
	{
		get
		{
			return Game.State == GameState.Play || Game.State == GameState.Dialogue || this.showOutsideGameplay || this.itemResource.ForceShow;
		}
	}

	// Token: 0x06001256 RID: 4694 RVA: 0x0005B7C4 File Offset: 0x000599C4
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

	// Token: 0x06001257 RID: 4695 RVA: 0x0005B89C File Offset: 0x00059A9C
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

	// Token: 0x06001258 RID: 4696 RVA: 0x0005BA18 File Offset: 0x00059C18
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

	// Token: 0x06001259 RID: 4697 RVA: 0x0000F91A File Offset: 0x0000DB1A
	public void ClearPrice()
	{
		this.pricetag.SetActive(false);
	}

	public ItemResource itemResource;

	public bool showOutsideGameplay;

	public Text text;

	public Text deltaText;

	public Image image;

	public Image background;

	public GameObject pricetag;

	public Text priceText;

	public bool hide = true;

	public bool startHidden;

	public float hideDelay = 5f;

	private float hideTime = -1f;

	public float hiddenPosition;

	private float velocity;

	public RectTransform boxTransform;

	public int currentValue;

	public float deltaValue;

	private float deltaTransferTime = -1f;

	public float deltaTransferDelay = 2f;

	public float deltaTransferSpeed = 30f;

	private AudioSourceVariance collectSound;

	public RectTransform pullout;

	public float pulloutHiddenX;

	public float pulloutVisibleX;

	private float pulloutVelocity;

	public bool bold;

	private int soundsPerFrame = 3;

	private int soundBudget;

	private float soundBudgetResetTime = -1f;

	private float soundBudgetFrame = 0.05f;

	private bool listenerAdded;

	public bool skipDelta;
}

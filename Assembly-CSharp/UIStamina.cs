using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStamina : MonoBehaviour
{
	// Token: 0x06000FB4 RID: 4020 RVA: 0x0004B26D File Offset: 0x0004946D
	private void Awake()
	{
	}

	// Token: 0x06000FB5 RID: 4021 RVA: 0x0004B26F File Offset: 0x0004946F
	private void Start()
	{
		if (Player.movement == null)
		{
			this.currentStamina = 0f;
			return;
		}
		this.currentStamina = Player.movement.Stamina;
		this.shadowStamina = this.currentStamina;
		this.UpdateStamina();
	}

	// Token: 0x06000FB6 RID: 4022 RVA: 0x0004B2AC File Offset: 0x000494AC
	private void OnEnable()
	{
		UIStamina.u = this;
	}

	// Token: 0x06000FB7 RID: 4023 RVA: 0x0004B2B4 File Offset: 0x000494B4
	private void LateUpdate()
	{
		if (Player.movement == null)
		{
			return;
		}
		if (this.isDisplaying && (ItemManager.HasInfiniteStamina || !Game.HasControl || (this.hideWhenFull && Player.movement.Stamina == Player.movement.maxStamina && Time.time > this.deactivateTime)))
		{
			Image[] array = this.shadowImages;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			array = this.braceletImages;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			this.isDisplaying = false;
			this.currentStamina = (this.shadowStamina = Player.movement.Stamina);
			return;
		}
		if (!this.isDisplaying && Game.HasControl && (!this.hideWhenFull || Player.movement.Stamina != this.currentStamina))
		{
			this.isDisplaying = true;
			this.currentStamina = (this.shadowStamina = Player.movement.Stamina);
			this.UpdateStamina();
		}
		if (this.isDisplaying && (Player.movement.Stamina != this.currentStamina || this.shadowStamina != this.currentStamina))
		{
			float num = Player.movement.Stamina;
			if (this.shadowVelocity < -0.05f)
			{
				num -= this.projectionAmount;
			}
			this.currentStamina = Mathf.SmoothDamp(this.currentStamina, num, ref this.staminaVelocity, (this.currentStamina > num) ? 0.05f : 0.2f);
			this.shadowStamina = Mathf.SmoothDamp(this.shadowStamina, Player.movement.Stamina, ref this.shadowVelocity, (this.shadowStamina - Player.movement.Stamina < 0.1f) ? 0.05f : 0.2f);
			this.UpdateStamina();
			if (Player.movement.Stamina < Player.movement.maxStamina - 0.05f)
			{
				this.deactivateTime = Time.time + this.activeTime;
			}
		}
	}

	// Token: 0x06000FB8 RID: 4024 RVA: 0x0004B4B8 File Offset: 0x000496B8
	private void UpdateStamina()
	{
		for (int i = 0; i < this.braceletImages.Length; i++)
		{
			if (this.shadowStamina <= this.currentStamina || this.currentStamina >= (float)(i + 1))
			{
				this.shadowImages[i].enabled = false;
			}
			else if (this.shadowStamina >= (float)(i + 1))
			{
				this.shadowImages[i].enabled = true;
				this.shadowImages[i].fillAmount = 1f;
			}
			else
			{
				this.shadowImages[i].enabled = true;
				this.shadowImages[i].fillAmount = this.shadowStamina - (float)i;
			}
			if (this.currentStamina <= (float)i)
			{
				this.braceletImages[i].enabled = false;
			}
			else if (this.currentStamina >= (float)(i + 1))
			{
				this.braceletImages[i].enabled = true;
				this.braceletImages[i].fillAmount = 1f;
			}
			else
			{
				this.braceletImages[i].enabled = true;
				this.braceletImages[i].fillAmount = this.currentStamina - (float)i;
			}
		}
	}

	private RectTransform rectTransform;

	public static UIStamina u;

	public float currentStamina;

	public float shadowStamina;

	private float staminaVelocity;

	private float shadowVelocity;

	public float maxStamina = 1f;

	public float projectionAmount = 0.2f;

	public Image[] braceletImages;

	public Image[] shadowImages;

	public Vector2 activePosition;

	private Vector2 lockedPosition;

	public float activeTime = 1f;

	private float deactivateTime;

	private Vector2 anchorVelocity;

	private bool isDisplaying = true;

	public bool hideWhenFull = true;
}

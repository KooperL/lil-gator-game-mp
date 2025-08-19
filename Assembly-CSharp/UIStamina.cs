using System;
using UnityEngine;
using UnityEngine.UI;

public class UIStamina : MonoBehaviour
{
	// Token: 0x06001340 RID: 4928 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x06001341 RID: 4929 RVA: 0x000103B0 File Offset: 0x0000E5B0
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

	// Token: 0x06001342 RID: 4930 RVA: 0x000103ED File Offset: 0x0000E5ED
	private void OnEnable()
	{
		UIStamina.u = this;
	}

	// Token: 0x06001343 RID: 4931 RVA: 0x0005EA5C File Offset: 0x0005CC5C
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

	// Token: 0x06001344 RID: 4932 RVA: 0x0005EC60 File Offset: 0x0005CE60
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

using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020003D5 RID: 981
public class UIStamina : MonoBehaviour
{
	// Token: 0x060012E0 RID: 4832 RVA: 0x00002229 File Offset: 0x00000429
	private void Awake()
	{
	}

	// Token: 0x060012E1 RID: 4833 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
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

	// Token: 0x060012E2 RID: 4834 RVA: 0x0000FFE6 File Offset: 0x0000E1E6
	private void OnEnable()
	{
		UIStamina.u = this;
	}

	// Token: 0x060012E3 RID: 4835 RVA: 0x0005CA58 File Offset: 0x0005AC58
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

	// Token: 0x060012E4 RID: 4836 RVA: 0x0005CC5C File Offset: 0x0005AE5C
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

	// Token: 0x04001852 RID: 6226
	private RectTransform rectTransform;

	// Token: 0x04001853 RID: 6227
	public static UIStamina u;

	// Token: 0x04001854 RID: 6228
	public float currentStamina;

	// Token: 0x04001855 RID: 6229
	public float shadowStamina;

	// Token: 0x04001856 RID: 6230
	private float staminaVelocity;

	// Token: 0x04001857 RID: 6231
	private float shadowVelocity;

	// Token: 0x04001858 RID: 6232
	public float maxStamina = 1f;

	// Token: 0x04001859 RID: 6233
	public float projectionAmount = 0.2f;

	// Token: 0x0400185A RID: 6234
	public Image[] braceletImages;

	// Token: 0x0400185B RID: 6235
	public Image[] shadowImages;

	// Token: 0x0400185C RID: 6236
	public Vector2 activePosition;

	// Token: 0x0400185D RID: 6237
	private Vector2 lockedPosition;

	// Token: 0x0400185E RID: 6238
	public float activeTime = 1f;

	// Token: 0x0400185F RID: 6239
	private float deactivateTime;

	// Token: 0x04001860 RID: 6240
	private Vector2 anchorVelocity;

	// Token: 0x04001861 RID: 6241
	private bool isDisplaying = true;

	// Token: 0x04001862 RID: 6242
	public bool hideWhenFull = true;
}

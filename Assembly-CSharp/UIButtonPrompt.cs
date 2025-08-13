using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200039C RID: 924
public class UIButtonPrompt : MonoBehaviour
{
	// Token: 0x0600118D RID: 4493 RVA: 0x00057D0C File Offset: 0x00055F0C
	private void Awake()
	{
		this.rePlayer = ReInput.players.GetPlayer(0);
		if (this.allowSkip)
		{
			this.waitUntilTriggered = new WaitUntil(() => this.triggered || DebugButtons.IsSkipHeld);
			return;
		}
		this.waitUntilTriggered = new WaitUntil(() => this.triggered);
	}

	// Token: 0x0600118E RID: 4494 RVA: 0x0000F02D File Offset: 0x0000D22D
	private void OnEnable()
	{
		this.triggered = false;
		this.startTime = Time.time;
		PlayerInteract.interactButtonPriority = base.gameObject;
	}

	// Token: 0x0600118F RID: 4495 RVA: 0x00004EBF File Offset: 0x000030BF
	private void OnDisable()
	{
		if (PlayerInteract.interactButtonPriority == base.gameObject)
		{
			PlayerInteract.interactButtonPriority = null;
		}
	}

	// Token: 0x06001190 RID: 4496 RVA: 0x00057D64 File Offset: 0x00055F64
	private void Update()
	{
		if (this.rePlayer != null && (this.rePlayer.GetButtonDown("Interact") || (Game.State == GameState.Dialogue && (this.rePlayer.GetButtonDown("Interact Dialogue") || this.rePlayer.GetButtonDown("UISubmit"))) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && this.rePlayer.GetButtonDown("Jump")) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && this.rePlayer.GetButtonDown("Primary"))))
		{
			this.triggered = true;
		}
	}

	// Token: 0x040016A1 RID: 5793
	public float timeLimit;

	// Token: 0x040016A2 RID: 5794
	private float startTime;

	// Token: 0x040016A3 RID: 5795
	public Image backgroundImage;

	// Token: 0x040016A4 RID: 5796
	public Sprite[] backgroundSprites;

	// Token: 0x040016A5 RID: 5797
	public WaitUntil waitUntilTriggered;

	// Token: 0x040016A6 RID: 5798
	public bool triggered;

	// Token: 0x040016A7 RID: 5799
	public bool allowAlternateInputs = true;

	// Token: 0x040016A8 RID: 5800
	public bool allowSkip = true;

	// Token: 0x040016A9 RID: 5801
	private Player rePlayer;
}

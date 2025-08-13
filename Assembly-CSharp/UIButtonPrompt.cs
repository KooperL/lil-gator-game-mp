using System;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002BA RID: 698
public class UIButtonPrompt : MonoBehaviour
{
	// Token: 0x06000EB5 RID: 3765 RVA: 0x000466F4 File Offset: 0x000448F4
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

	// Token: 0x06000EB6 RID: 3766 RVA: 0x00046749 File Offset: 0x00044949
	private void OnEnable()
	{
		this.triggered = false;
		this.startTime = Time.time;
		PlayerInteract.interactButtonPriority = base.gameObject;
	}

	// Token: 0x06000EB7 RID: 3767 RVA: 0x00046768 File Offset: 0x00044968
	private void OnDisable()
	{
		if (PlayerInteract.interactButtonPriority == base.gameObject)
		{
			PlayerInteract.interactButtonPriority = null;
		}
	}

	// Token: 0x06000EB8 RID: 3768 RVA: 0x00046784 File Offset: 0x00044984
	private void Update()
	{
		if (this.rePlayer != null && (this.rePlayer.GetButtonDown("Interact") || (Game.State == GameState.Dialogue && (this.rePlayer.GetButtonDown("Interact Dialogue") || this.rePlayer.GetButtonDown("UISubmit"))) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Jump && this.rePlayer.GetButtonDown("Jump")) || (PlayerInput.interactMapping == PlayerInput.InteractMapping.Primary && this.rePlayer.GetButtonDown("Primary"))))
		{
			this.triggered = true;
		}
	}

	// Token: 0x0400132B RID: 4907
	public float timeLimit;

	// Token: 0x0400132C RID: 4908
	private float startTime;

	// Token: 0x0400132D RID: 4909
	public Image backgroundImage;

	// Token: 0x0400132E RID: 4910
	public Sprite[] backgroundSprites;

	// Token: 0x0400132F RID: 4911
	public WaitUntil waitUntilTriggered;

	// Token: 0x04001330 RID: 4912
	public bool triggered;

	// Token: 0x04001331 RID: 4913
	public bool allowAlternateInputs = true;

	// Token: 0x04001332 RID: 4914
	public bool allowSkip = true;

	// Token: 0x04001333 RID: 4915
	private global::Rewired.Player rePlayer;
}

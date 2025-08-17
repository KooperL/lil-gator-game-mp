using System;

public class LogicStateSubmerge : LogicState
{
	// Token: 0x06000928 RID: 2344 RVA: 0x00008DEC File Offset: 0x00006FEC
	private void FixedUpdate()
	{
		if (Player.movement.IsSwimming)
		{
			this.swimmingCounter++;
		}
		else
		{
			this.swimmingCounter = 0;
		}
		if (this.swimmingCounter > 10)
		{
			base.LogicCompleted();
		}
	}

	private int swimmingCounter;
}

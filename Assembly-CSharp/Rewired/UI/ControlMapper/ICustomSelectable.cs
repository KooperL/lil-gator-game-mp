using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rewired.UI.ControlMapper
{
	public interface ICustomSelectable : ICancelHandler, IEventSystemHandler
	{
		// (get) Token: 0x06001B0C RID: 6924
		// (set) Token: 0x06001B0D RID: 6925
		Sprite disabledHighlightedSprite { get; set; }

		// (get) Token: 0x06001B0E RID: 6926
		// (set) Token: 0x06001B0F RID: 6927
		Color disabledHighlightedColor { get; set; }

		// (get) Token: 0x06001B10 RID: 6928
		// (set) Token: 0x06001B11 RID: 6929
		string disabledHighlightedTrigger { get; set; }

		// (get) Token: 0x06001B12 RID: 6930
		// (set) Token: 0x06001B13 RID: 6931
		bool autoNavUp { get; set; }

		// (get) Token: 0x06001B14 RID: 6932
		// (set) Token: 0x06001B15 RID: 6933
		bool autoNavDown { get; set; }

		// (get) Token: 0x06001B16 RID: 6934
		// (set) Token: 0x06001B17 RID: 6935
		bool autoNavLeft { get; set; }

		// (get) Token: 0x06001B18 RID: 6936
		// (set) Token: 0x06001B19 RID: 6937
		bool autoNavRight { get; set; }

		// (add) Token: 0x06001B1A RID: 6938
		// (remove) Token: 0x06001B1B RID: 6939
		event UnityAction CancelEvent;
	}
}

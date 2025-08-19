using System;
using System.Collections.Generic;
using System.Text;
using Rewired.Integration.UnityUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rewired.Demos
{
	[AddComponentMenu("")]
	public sealed class PlayerPointerEventHandlerExample : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IPointerClickHandler, IScrollHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		// Token: 0x06001DC5 RID: 7621 RVA: 0x00016BF9 File Offset: 0x00014DF9
		private void Log(string o)
		{
			this.log.Add(o);
			if (this.log.Count > 10)
			{
				this.log.RemoveAt(0);
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000751B0 File Offset: 0x000733B0
		private void Update()
		{
			if (this.text != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in this.log)
				{
					stringBuilder.AppendLine(text);
				}
				this.text.text = stringBuilder.ToString();
			}
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0007522C File Offset: 0x0007342C
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerEnter:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x0007529C File Offset: 0x0007349C
		public void OnPointerExit(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerExit:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0007530C File Offset: 0x0007350C
		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerUp:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x00075398 File Offset: 0x00073598
		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerDown:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00075424 File Offset: 0x00073624
		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnPointerClick:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000754B0 File Offset: 0x000736B0
		public void OnScroll(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnScroll:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData)
				}));
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00075520 File Offset: 0x00073720
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnBeginDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000755AC File Offset: 0x000737AC
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x00075638 File Offset: 0x00073838
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData is PlayerPointerEventData)
			{
				PlayerPointerEventData playerPointerEventData = (PlayerPointerEventData)eventData;
				this.Log(string.Concat(new string[]
				{
					"OnEndDrag:  Player = ",
					playerPointerEventData.playerId.ToString(),
					", Pointer Index = ",
					playerPointerEventData.inputSourceIndex.ToString(),
					", Source = ",
					PlayerPointerEventHandlerExample.GetSourceName(playerPointerEventData),
					", Button Index = ",
					playerPointerEventData.buttonIndex.ToString()
				}));
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000756C4 File Offset: 0x000738C4
		private static string GetSourceName(PlayerPointerEventData playerEventData)
		{
			if (playerEventData.sourceType == PointerEventType.Mouse)
			{
				if (playerEventData.mouseSource is Behaviour)
				{
					return (playerEventData.mouseSource as Behaviour).name;
				}
			}
			else if (playerEventData.sourceType == PointerEventType.Touch && playerEventData.touchSource is Behaviour)
			{
				return (playerEventData.touchSource as Behaviour).name;
			}
			return null;
		}

		public Text text;

		private const int logLength = 10;

		private List<string> log = new List<string>();
	}
}

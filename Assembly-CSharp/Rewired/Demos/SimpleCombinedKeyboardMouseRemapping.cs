using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.Demos
{
	// Token: 0x020004A7 RID: 1191
	[AddComponentMenu("")]
	public class SimpleCombinedKeyboardMouseRemapping : MonoBehaviour
	{
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001D96 RID: 7574 RVA: 0x00016A0A File Offset: 0x00014C0A
		private Player player
		{
			get
			{
				return ReInput.players.GetPlayer(0);
			}
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x00073CE8 File Offset: 0x00071EE8
		private void OnEnable()
		{
			if (!ReInput.isReady)
			{
				return;
			}
			this.inputMapper_keyboard.options.timeout = 5f;
			this.inputMapper_mouse.options.timeout = 5f;
			this.inputMapper_mouse.options.ignoreMouseXAxis = true;
			this.inputMapper_mouse.options.ignoreMouseYAxis = true;
			this.inputMapper_keyboard.options.allowButtonsOnFullAxisAssignment = false;
			this.inputMapper_mouse.options.allowButtonsOnFullAxisAssignment = false;
			this.inputMapper_keyboard.InputMappedEvent += this.OnInputMapped;
			this.inputMapper_keyboard.StoppedEvent += this.OnStopped;
			this.inputMapper_mouse.InputMappedEvent += this.OnInputMapped;
			this.inputMapper_mouse.StoppedEvent += this.OnStopped;
			this.InitializeUI();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x00016A17 File Offset: 0x00014C17
		private void OnDisable()
		{
			this.inputMapper_keyboard.Stop();
			this.inputMapper_mouse.Stop();
			this.inputMapper_keyboard.RemoveAllEventListeners();
			this.inputMapper_mouse.RemoveAllEventListeners();
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x00073DD0 File Offset: 0x00071FD0
		private void RedrawUI()
		{
			this.controllerNameUIText.text = "Keyboard/Mouse";
			for (int i = 0; i < this.rows.Count; i++)
			{
				SimpleCombinedKeyboardMouseRemapping.Row row = this.rows[i];
				InputAction action = this.rows[i].action;
				string text = string.Empty;
				int actionElementMapId = -1;
				for (int j = 0; j < 2; j++)
				{
					ControllerType controllerType = ((j == 0) ? 0 : 1);
					foreach (ActionElementMap actionElementMap in this.player.controllers.maps.GetMap(controllerType, 0, "Default", "Default").ElementMapsWithAction(action.id))
					{
						if (actionElementMap.ShowInField(row.actionRange))
						{
							text = actionElementMap.elementIdentifierName;
							actionElementMapId = actionElementMap.id;
							break;
						}
					}
					if (actionElementMapId >= 0)
					{
						break;
					}
				}
				row.text.text = text;
				row.button.onClick.RemoveAllListeners();
				int index = i;
				row.button.onClick.AddListener(delegate
				{
					this.OnInputFieldClicked(index, actionElementMapId);
				});
			}
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00073F3C File Offset: 0x0007213C
		private void ClearUI()
		{
			this.controllerNameUIText.text = string.Empty;
			for (int i = 0; i < this.rows.Count; i++)
			{
				this.rows[i].text.text = string.Empty;
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00073F8C File Offset: 0x0007218C
		private void InitializeUI()
		{
			foreach (object obj in this.actionGroupTransform)
			{
				Object.Destroy(((Transform)obj).gameObject);
			}
			foreach (object obj2 in this.fieldGroupTransform)
			{
				Object.Destroy(((Transform)obj2).gameObject);
			}
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory("Default"))
			{
				if (inputAction.type == null)
				{
					this.CreateUIRow(inputAction, 0, inputAction.descriptiveName);
					this.CreateUIRow(inputAction, 1, (!string.IsNullOrEmpty(inputAction.positiveDescriptiveName)) ? inputAction.positiveDescriptiveName : (inputAction.descriptiveName + " +"));
					this.CreateUIRow(inputAction, 2, (!string.IsNullOrEmpty(inputAction.negativeDescriptiveName)) ? inputAction.negativeDescriptiveName : (inputAction.descriptiveName + " -"));
				}
				else if (inputAction.type == 1)
				{
					this.CreateUIRow(inputAction, 1, inputAction.descriptiveName);
				}
			}
			this.RedrawUI();
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x00074104 File Offset: 0x00072304
		private void CreateUIRow(InputAction action, AxisRange actionRange, string label)
		{
			GameObject gameObject = Object.Instantiate<GameObject>(this.textPrefab);
			gameObject.transform.SetParent(this.actionGroupTransform);
			gameObject.transform.SetAsLastSibling();
			gameObject.GetComponent<Text>().text = label;
			GameObject gameObject2 = Object.Instantiate<GameObject>(this.buttonPrefab);
			gameObject2.transform.SetParent(this.fieldGroupTransform);
			gameObject2.transform.SetAsLastSibling();
			this.rows.Add(new SimpleCombinedKeyboardMouseRemapping.Row
			{
				action = action,
				actionRange = actionRange,
				button = gameObject2.GetComponent<Button>(),
				text = gameObject2.GetComponentInChildren<Text>()
			});
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x000741A4 File Offset: 0x000723A4
		private void OnInputFieldClicked(int index, int actionElementMapToReplaceId)
		{
			if (index < 0 || index >= this.rows.Count)
			{
				return;
			}
			ControllerMap map = this.player.controllers.maps.GetMap(0, 0, "Default", "Default");
			ControllerMap map2 = this.player.controllers.maps.GetMap(1, 0, "Default", "Default");
			ControllerMap controllerMap;
			if (map.ContainsElementMap(actionElementMapToReplaceId))
			{
				controllerMap = map;
			}
			else if (map2.ContainsElementMap(actionElementMapToReplaceId))
			{
				controllerMap = map2;
			}
			else
			{
				controllerMap = null;
			}
			this._replaceTargetMapping = new SimpleCombinedKeyboardMouseRemapping.TargetMapping
			{
				actionElementMapId = actionElementMapToReplaceId,
				controllerMap = controllerMap
			};
			base.StartCoroutine(this.StartListeningDelayed(index, map, map2, actionElementMapToReplaceId));
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x00016A45 File Offset: 0x00014C45
		private IEnumerator StartListeningDelayed(int index, ControllerMap keyboardMap, ControllerMap mouseMap, int actionElementMapToReplaceId)
		{
			yield return new WaitForSeconds(0.1f);
			this.inputMapper_keyboard.Start(new InputMapper.Context
			{
				actionId = this.rows[index].action.id,
				controllerMap = keyboardMap,
				actionRange = this.rows[index].actionRange,
				actionElementMapToReplace = keyboardMap.GetElementMap(actionElementMapToReplaceId)
			});
			this.inputMapper_mouse.Start(new InputMapper.Context
			{
				actionId = this.rows[index].action.id,
				controllerMap = mouseMap,
				actionRange = this.rows[index].actionRange,
				actionElementMapToReplace = mouseMap.GetElementMap(actionElementMapToReplaceId)
			});
			this.player.controllers.maps.SetMapsEnabled(false, "UI");
			this.statusUIText.text = "Listening...";
			yield break;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x00074254 File Offset: 0x00072454
		private void OnInputMapped(InputMapper.InputMappedEventData data)
		{
			this.inputMapper_keyboard.Stop();
			this.inputMapper_mouse.Stop();
			if (this._replaceTargetMapping.controllerMap != null && data.actionElementMap.controllerMap != this._replaceTargetMapping.controllerMap)
			{
				this._replaceTargetMapping.controllerMap.DeleteElementMap(this._replaceTargetMapping.actionElementMapId);
			}
			this.RedrawUI();
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x00016A71 File Offset: 0x00014C71
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.statusUIText.text = string.Empty;
			this.player.controllers.maps.SetMapsEnabled(true, "UI");
		}

		// Token: 0x04001EE7 RID: 7911
		private const string category = "Default";

		// Token: 0x04001EE8 RID: 7912
		private const string layout = "Default";

		// Token: 0x04001EE9 RID: 7913
		private const string uiCategory = "UI";

		// Token: 0x04001EEA RID: 7914
		private InputMapper inputMapper_keyboard = new InputMapper();

		// Token: 0x04001EEB RID: 7915
		private InputMapper inputMapper_mouse = new InputMapper();

		// Token: 0x04001EEC RID: 7916
		public GameObject buttonPrefab;

		// Token: 0x04001EED RID: 7917
		public GameObject textPrefab;

		// Token: 0x04001EEE RID: 7918
		public RectTransform fieldGroupTransform;

		// Token: 0x04001EEF RID: 7919
		public RectTransform actionGroupTransform;

		// Token: 0x04001EF0 RID: 7920
		public Text controllerNameUIText;

		// Token: 0x04001EF1 RID: 7921
		public Text statusUIText;

		// Token: 0x04001EF2 RID: 7922
		private List<SimpleCombinedKeyboardMouseRemapping.Row> rows = new List<SimpleCombinedKeyboardMouseRemapping.Row>();

		// Token: 0x04001EF3 RID: 7923
		private SimpleCombinedKeyboardMouseRemapping.TargetMapping _replaceTargetMapping;

		// Token: 0x020004A8 RID: 1192
		private class Row
		{
			// Token: 0x04001EF4 RID: 7924
			public InputAction action;

			// Token: 0x04001EF5 RID: 7925
			public AxisRange actionRange;

			// Token: 0x04001EF6 RID: 7926
			public Button button;

			// Token: 0x04001EF7 RID: 7927
			public Text text;
		}

		// Token: 0x020004A9 RID: 1193
		private struct TargetMapping
		{
			// Token: 0x04001EF8 RID: 7928
			public ControllerMap controllerMap;

			// Token: 0x04001EF9 RID: 7929
			public int actionElementMapId;
		}
	}
}

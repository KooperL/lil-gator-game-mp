using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000336 RID: 822
	[AddComponentMenu("")]
	public class ControlRemappingDemo1 : MonoBehaviour
	{
		// Token: 0x060016F5 RID: 5877 RVA: 0x0005FF81 File Offset: 0x0005E181
		private void Awake()
		{
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			this.Initialize();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005FFC0 File Offset: 0x0005E1C0
		private void OnEnable()
		{
			this.Subscribe();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005FFC8 File Offset: 0x0005E1C8
		private void OnDisable()
		{
			this.Unsubscribe();
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x0005FFD0 File Offset: 0x0005E1D0
		private void Initialize()
		{
			this.dialog = new ControlRemappingDemo1.DialogHelper();
			this.actionQueue = new Queue<ControlRemappingDemo1.QueueEntry>();
			this.selectedController = new ControlRemappingDemo1.ControllerSelection();
			ReInput.ControllerConnectedEvent += this.JoystickConnected;
			ReInput.ControllerPreDisconnectEvent += this.JoystickPreDisconnect;
			ReInput.ControllerDisconnectedEvent += this.JoystickDisconnected;
			this.ResetAll();
			this.initialized = true;
			ReInput.userDataStore.Load();
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x060016F9 RID: 5881 RVA: 0x00060058 File Offset: 0x0005E258
		private void Setup()
		{
			if (this.setupFinished)
			{
				return;
			}
			this.style_wordWrap = new GUIStyle(GUI.skin.label);
			this.style_wordWrap.wordWrap = true;
			this.style_centeredBox = new GUIStyle(GUI.skin.box);
			this.style_centeredBox.alignment = TextAnchor.MiddleCenter;
			this.setupFinished = true;
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x000600B7 File Offset: 0x0005E2B7
		private void Subscribe()
		{
			this.Unsubscribe();
			this.inputMapper.ConflictFoundEvent += this.OnConflictFound;
			this.inputMapper.StoppedEvent += this.OnStopped;
		}

		// Token: 0x060016FB RID: 5883 RVA: 0x000600ED File Offset: 0x0005E2ED
		private void Unsubscribe()
		{
			this.inputMapper.RemoveAllEventListeners();
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x000600FC File Offset: 0x0005E2FC
		public void OnGUI()
		{
			if (!this.initialized)
			{
				return;
			}
			this.Setup();
			this.HandleMenuControl();
			if (!this.showMenu)
			{
				this.DrawInitialScreen();
				return;
			}
			this.SetGUIStateStart();
			this.ProcessQueue();
			this.DrawPage();
			this.ShowDialog();
			this.SetGUIStateEnd();
			this.busy = false;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00060154 File Offset: 0x0005E354
		private void HandleMenuControl()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (Event.current.type == EventType.Layout && ReInput.players.GetSystemPlayer().GetButtonDown("Menu"))
			{
				if (this.showMenu)
				{
					ReInput.userDataStore.Save();
					this.Close();
					return;
				}
				this.Open();
			}
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x000601B1 File Offset: 0x0005E3B1
		private void Close()
		{
			this.ClearWorkingVars();
			this.showMenu = false;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x000601C0 File Offset: 0x0005E3C0
		private void Open()
		{
			this.showMenu = true;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x000601CC File Offset: 0x0005E3CC
		private void DrawInitialScreen()
		{
			ActionElementMap firstElementMapWithAction = ReInput.players.GetSystemPlayer().controllers.maps.GetFirstElementMapWithAction("Menu", true);
			GUIContent guicontent;
			if (firstElementMapWithAction != null)
			{
				guicontent = new GUIContent("Press " + firstElementMapWithAction.elementIdentifierName + " to open the menu.");
			}
			else
			{
				guicontent = new GUIContent("There is no element assigned to open the menu!");
			}
			GUILayout.BeginArea(this.GetScreenCenteredRect(300f, 50f));
			GUILayout.Box(guicontent, this.style_centeredBox, new GUILayoutOption[]
			{
				GUILayout.ExpandHeight(true),
				GUILayout.ExpandWidth(true)
			});
			GUILayout.EndArea();
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00060264 File Offset: 0x0005E464
		private void DrawPage()
		{
			if (GUI.enabled != this.pageGUIState)
			{
				GUI.enabled = this.pageGUIState;
			}
			GUILayout.BeginArea(new Rect(((float)Screen.width - (float)Screen.width * 0.9f) * 0.5f, ((float)Screen.height - (float)Screen.height * 0.9f) * 0.5f, (float)Screen.width * 0.9f, (float)Screen.height * 0.9f));
			this.DrawPlayerSelector();
			this.DrawJoystickSelector();
			this.DrawMouseAssignment();
			this.DrawControllerSelector();
			this.DrawCalibrateButton();
			this.DrawMapCategories();
			this.actionScrollPos = GUILayout.BeginScrollView(this.actionScrollPos, Array.Empty<GUILayoutOption>());
			this.DrawCategoryActions();
			GUILayout.EndScrollView();
			GUILayout.EndArea();
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00060328 File Offset: 0x0005E528
		private void DrawPlayerSelector()
		{
			if (ReInput.players.allPlayerCount == 0)
			{
				GUILayout.Label("There are no players.", Array.Empty<GUILayoutOption>());
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Players:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			foreach (Player player in ReInput.players.GetPlayers(true))
			{
				if (this.selectedPlayer == null)
				{
					this.selectedPlayer = player;
				}
				bool flag = player == this.selectedPlayer;
				bool flag2 = GUILayout.Toggle(flag, (player.descriptiveName != string.Empty) ? player.descriptiveName : player.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (flag2 != flag && flag2)
				{
					this.selectedPlayer = player;
					this.selectedController.Clear();
					this.selectedMapCategoryId = -1;
				}
			}
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0006043C File Offset: 0x0005E63C
		private void DrawMouseAssignment()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Mouse:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool flag = this.selectedPlayer != null && this.selectedPlayer.controllers.hasMouse;
			bool flag2 = GUILayout.Toggle(flag, "Assign Mouse", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
			if (flag2 != flag)
			{
				if (flag2)
				{
					this.selectedPlayer.controllers.hasMouse = true;
					using (IEnumerator<Player> enumerator = ReInput.players.Players.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Player player = enumerator.Current;
							if (player != this.selectedPlayer)
							{
								player.controllers.hasMouse = false;
							}
						}
						goto IL_00E9;
					}
				}
				this.selectedPlayer.controllers.hasMouse = false;
			}
			IL_00E9:
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x00060558 File Offset: 0x0005E758
		private void DrawJoystickSelector()
		{
			bool enabled = GUI.enabled;
			if (this.selectedPlayer == null)
			{
				GUI.enabled = false;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Assign Joysticks:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool flag = this.selectedPlayer == null || this.selectedPlayer.controllers.joystickCount == 0;
			bool flag2 = GUILayout.Toggle(flag, "None", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
			if (flag2 != flag)
			{
				this.selectedPlayer.controllers.ClearControllersOfType(ControllerType.Joystick);
				this.ControllerSelectionChanged();
			}
			if (this.selectedPlayer != null)
			{
				foreach (Joystick joystick in ReInput.controllers.Joysticks)
				{
					flag = this.selectedPlayer.controllers.ContainsController(joystick);
					flag2 = GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
					if (flag2 != flag)
					{
						this.EnqueueAction(new ControlRemappingDemo1.JoystickAssignmentChange(this.selectedPlayer.id, joystick.id, flag2));
					}
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x000606B4 File Offset: 0x0005E8B4
		private void DrawControllerSelector()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Controller to Map:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			if (!this.selectedController.hasSelection)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			bool flag = this.selectedController.type == ControllerType.Keyboard;
			if (GUILayout.Toggle(flag, "Keyboard", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
			{
				this.selectedController.Set(0, ControllerType.Keyboard);
				this.ControllerSelectionChanged();
			}
			if (!this.selectedPlayer.controllers.hasMouse)
			{
				GUI.enabled = false;
			}
			flag = this.selectedController.type == ControllerType.Mouse;
			if (GUILayout.Toggle(flag, "Mouse", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
			{
				this.selectedController.Set(0, ControllerType.Mouse);
				this.ControllerSelectionChanged();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			foreach (Joystick joystick in this.selectedPlayer.controllers.Joysticks)
			{
				flag = this.selectedController.type == ControllerType.Joystick && this.selectedController.id == joystick.id;
				if (GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
				{
					this.selectedController.Set(joystick.id, ControllerType.Joystick);
					this.ControllerSelectionChanged();
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x00060888 File Offset: 0x0005EA88
		private void DrawCalibrateButton()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(10f);
			Controller controller = (this.selectedController.hasSelection ? this.selectedPlayer.controllers.GetController(this.selectedController.type, this.selectedController.id) : null);
			if (controller == null || this.selectedController.type != ControllerType.Joystick)
			{
				GUI.enabled = false;
				GUILayout.Button("Select a controller to calibrate", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else if (GUILayout.Button("Calibrate " + controller.name, new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
			{
				Joystick joystick = controller as Joystick;
				if (joystick != null)
				{
					CalibrationMap calibrationMap = joystick.calibrationMap;
					if (calibrationMap != null)
					{
						this.EnqueueAction(new ControlRemappingDemo1.Calibration(this.selectedPlayer, joystick, calibrationMap));
					}
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x00060984 File Offset: 0x0005EB84
		private void DrawMapCategories()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (!this.selectedController.hasSelection)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(15f);
			GUILayout.Label("Categories:", Array.Empty<GUILayoutOption>());
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			foreach (InputMapCategory inputMapCategory in ReInput.mapping.UserAssignableMapCategories)
			{
				if (!this.selectedPlayer.controllers.maps.ContainsMapInCategory(this.selectedController.type, inputMapCategory.id))
				{
					GUI.enabled = false;
				}
				else if (this.selectedMapCategoryId < 0)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				bool flag = inputMapCategory.id == this.selectedMapCategoryId;
				if (GUILayout.Toggle(flag, (inputMapCategory.descriptiveName != string.Empty) ? inputMapCategory.descriptiveName : inputMapCategory.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
				{
					this.selectedMapCategoryId = inputMapCategory.id;
					this.selectedMap = this.selectedPlayer.controllers.maps.GetFirstMapInCategory(this.selectedController.type, this.selectedController.id, inputMapCategory.id);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x00060B58 File Offset: 0x0005ED58
		private void DrawCategoryActions()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			if (this.selectedMapCategoryId < 0)
			{
				return;
			}
			bool enabled = GUI.enabled;
			if (this.selectedMap == null)
			{
				return;
			}
			GUILayout.Space(15f);
			GUILayout.Label("Actions:", Array.Empty<GUILayoutOption>());
			InputMapCategory mapCategory = ReInput.mapping.GetMapCategory(this.selectedMapCategoryId);
			if (mapCategory == null)
			{
				return;
			}
			InputCategory actionCategory = ReInput.mapping.GetActionCategory(mapCategory.name);
			if (actionCategory == null)
			{
				return;
			}
			float num = 150f;
			foreach (InputAction inputAction in ReInput.mapping.ActionsInCategory(actionCategory.id))
			{
				string text = ((inputAction.descriptiveName != string.Empty) ? inputAction.descriptiveName : inputAction.name);
				if (inputAction.type == InputActionType.Button)
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap in this.selectedMap.AllMaps)
					{
						if (actionElementMap.actionId == inputAction.id)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap);
						}
					}
					GUILayout.EndHorizontal();
				}
				else if (inputAction.type == InputActionType.Axis)
				{
					if (this.selectedController.type != ControllerType.Keyboard)
					{
						GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
						GUILayout.Label(text, new GUILayoutOption[] { GUILayout.Width(num) });
						this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap);
						foreach (ActionElementMap actionElementMap2 in this.selectedMap.AllMaps)
						{
							if (actionElementMap2.actionId == inputAction.id && actionElementMap2.elementType != ControllerElementType.Button && actionElementMap2.axisType != AxisType.Split)
							{
								this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Full, this.selectedController, this.selectedMap, actionElementMap2);
								this.DrawInvertButton(this.selectedPlayer.id, inputAction, Pole.Positive, this.selectedController, this.selectedMap, actionElementMap2);
							}
						}
						GUILayout.EndHorizontal();
					}
					string text2 = ((inputAction.positiveDescriptiveName != string.Empty) ? inputAction.positiveDescriptiveName : (inputAction.descriptiveName + " +"));
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text2, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap3 in this.selectedMap.AllMaps)
					{
						if (actionElementMap3.actionId == inputAction.id && actionElementMap3.axisContribution == Pole.Positive && actionElementMap3.axisType != AxisType.Normal)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Positive, this.selectedController, this.selectedMap, actionElementMap3);
						}
					}
					GUILayout.EndHorizontal();
					string text3 = ((inputAction.negativeDescriptiveName != string.Empty) ? inputAction.negativeDescriptiveName : (inputAction.descriptiveName + " -"));
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text3, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap4 in this.selectedMap.AllMaps)
					{
						if (actionElementMap4.actionId == inputAction.id && actionElementMap4.axisContribution == Pole.Negative && actionElementMap4.axisType != AxisType.Normal)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, AxisRange.Negative, this.selectedController, this.selectedMap, actionElementMap4);
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0006104C File Offset: 0x0005F24C
		private void DrawActionAssignmentButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			if (GUILayout.Button(elementMap.elementIdentifierName, new GUILayoutOption[]
			{
				GUILayout.ExpandWidth(false),
				GUILayout.MinWidth(30f)
			}))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap,
					actionElementMapToReplace = elementMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove, context));
				this.startListening = true;
			}
			GUILayout.Space(4f);
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x000610CC File Offset: 0x0005F2CC
		private void DrawInvertButton(int playerId, InputAction action, Pole actionAxisContribution, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap, ActionElementMap elementMap)
		{
			bool invert = elementMap.invert;
			bool flag = GUILayout.Toggle(invert, "Invert", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
			if (flag != invert)
			{
				elementMap.invert = flag;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x00061114 File Offset: 0x0005F314
		private void DrawAddActionMapButton(int playerId, InputAction action, AxisRange actionRange, ControlRemappingDemo1.ControllerSelection controller, ControllerMap controllerMap)
		{
			if (GUILayout.Button("Add...", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
			{
				InputMapper.Context context = new InputMapper.Context
				{
					actionId = action.id,
					actionRange = actionRange,
					controllerMap = controllerMap
				};
				this.EnqueueAction(new ControlRemappingDemo1.ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType.Add, context));
				this.startListening = true;
			}
			GUILayout.Space(10f);
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0006117B File Offset: 0x0005F37B
		private void ShowDialog()
		{
			this.dialog.Update();
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x00061188 File Offset: 0x0005F388
		private void DrawModalWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x000611F4 File Offset: 0x0005F3F4
		private void DrawModalWindow_OkayOnly(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Okay");
			GUILayout.EndHorizontal();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00061250 File Offset: 0x0005F450
		private void DrawElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = this.actionQueue.Peek() as ControlRemappingDemo1.ElementAssignmentChange;
			if (elementAssignmentChange == null)
			{
				this.dialog.Cancel();
				return;
			}
			float num;
			if (!this.dialog.busy)
			{
				if (this.startListening && this.inputMapper.status == InputMapper.Status.Idle)
				{
					this.inputMapper.Start(elementAssignmentChange.context);
					this.startListening = false;
				}
				if (this.conflictFoundEventData != null)
				{
					this.dialog.Confirm();
					return;
				}
				num = this.inputMapper.timeRemaining;
				if (num == 0f)
				{
					this.dialog.Cancel();
					return;
				}
			}
			else
			{
				num = this.inputMapper.options.timeout;
			}
			GUILayout.Label("Assignment will be canceled in " + ((int)Mathf.Ceil(num)).ToString() + "...", this.style_wordWrap, Array.Empty<GUILayoutOption>());
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0006135C File Offset: 0x0005F55C
		private void DrawElementAssignmentProtectedConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x000613E8 File Offset: 0x0005F5E8
		private void DrawElementAssignmentNormalConflictWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (!(this.actionQueue.Peek() is ControlRemappingDemo1.ElementAssignmentChange))
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Confirm, "Replace");
			GUILayout.FlexibleSpace();
			this.dialog.DrawConfirmButton(ControlRemappingDemo1.UserResponse.Custom1, "Add");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton();
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00061488 File Offset: 0x0005F688
		private void DrawReassignOrRemoveElementAssignmentWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			this.dialog.DrawConfirmButton("Reassign");
			GUILayout.FlexibleSpace();
			this.dialog.DrawCancelButton("Remove");
			GUILayout.EndHorizontal();
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x000614F8 File Offset: 0x0005F6F8
		private void DrawFallbackJoystickIdentificationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.FallbackJoystickIdentification fallbackJoystickIdentification = this.actionQueue.Peek() as ControlRemappingDemo1.FallbackJoystickIdentification;
			if (fallbackJoystickIdentification == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.Label("Press any button or axis on \"" + fallbackJoystickIdentification.joystickName + "\" now.", this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.FlexibleSpace();
			if (GUILayout.Button("Skip", Array.Empty<GUILayoutOption>()))
			{
				this.dialog.Cancel();
				return;
			}
			if (this.dialog.busy)
			{
				return;
			}
			if (!ReInput.controllers.SetUnityJoystickIdFromAnyButtonOrAxisPress(fallbackJoystickIdentification.joystickId, 0.8f, false))
			{
				return;
			}
			this.dialog.Confirm();
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x000615C8 File Offset: 0x0005F7C8
		private void DrawCalibrationWindow(string title, string message)
		{
			if (!this.dialog.enabled)
			{
				return;
			}
			ControlRemappingDemo1.Calibration calibration = this.actionQueue.Peek() as ControlRemappingDemo1.Calibration;
			if (calibration == null)
			{
				this.dialog.Cancel();
				return;
			}
			GUILayout.Space(5f);
			GUILayout.Label(message, this.style_wordWrap, Array.Empty<GUILayoutOption>());
			GUILayout.Space(20f);
			GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
			bool enabled = GUI.enabled;
			GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(200f) });
			this.calibrateScrollPos = GUILayout.BeginScrollView(this.calibrateScrollPos, Array.Empty<GUILayoutOption>());
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			IList<ControllerElementIdentifier> axisElementIdentifiers = calibration.joystick.AxisElementIdentifiers;
			for (int i = 0; i < axisElementIdentifiers.Count; i++)
			{
				ControllerElementIdentifier controllerElementIdentifier = axisElementIdentifiers[i];
				bool flag = calibration.selectedElementIdentifierId == controllerElementIdentifier.id;
				bool flag2 = GUILayout.Toggle(flag, controllerElementIdentifier.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (flag != flag2)
				{
					calibration.selectedElementIdentifierId = controllerElementIdentifier.id;
				}
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			GUILayout.EndScrollView();
			GUILayout.EndVertical();
			GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(200f) });
			if (calibration.selectedElementIdentifierId >= 0)
			{
				float axisRawById = calibration.joystick.GetAxisRawById(calibration.selectedElementIdentifierId);
				GUILayout.Label("Raw Value: " + axisRawById.ToString(), Array.Empty<GUILayoutOption>());
				int axisIndexById = calibration.joystick.GetAxisIndexById(calibration.selectedElementIdentifierId);
				AxisCalibration axis = calibration.calibrationMap.GetAxis(axisIndexById);
				GUILayout.Label("Calibrated Value: " + calibration.joystick.GetAxisById(calibration.selectedElementIdentifierId).ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Zero: " + axis.calibratedZero.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Min: " + axis.calibratedMin.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Max: " + axis.calibratedMax.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Label("Dead Zone: " + axis.deadZone.ToString(), Array.Empty<GUILayoutOption>());
				GUILayout.Space(15f);
				bool flag3 = GUILayout.Toggle(axis.enabled, "Enabled", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (axis.enabled != flag3)
				{
					axis.enabled = flag3;
				}
				GUILayout.Space(10f);
				bool flag4 = GUILayout.Toggle(calibration.recording, "Record Min/Max", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (flag4 != calibration.recording)
				{
					if (flag4)
					{
						axis.calibratedMax = 0f;
						axis.calibratedMin = 0f;
					}
					calibration.recording = flag4;
				}
				if (calibration.recording)
				{
					axis.calibratedMin = Mathf.Min(new float[] { axis.calibratedMin, axisRawById, axis.calibratedMin });
					axis.calibratedMax = Mathf.Max(new float[] { axis.calibratedMax, axisRawById, axis.calibratedMax });
					GUI.enabled = false;
				}
				if (GUILayout.Button("Set Zero", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
				{
					axis.calibratedZero = axisRawById;
				}
				if (GUILayout.Button("Set Dead Zone", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
				{
					axis.deadZone = axisRawById;
				}
				bool flag5 = GUILayout.Toggle(axis.invert, "Invert", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) });
				if (axis.invert != flag5)
				{
					axis.invert = flag5;
				}
				GUILayout.Space(10f);
				if (GUILayout.Button("Reset", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }))
				{
					axis.Reset();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}
			else
			{
				GUILayout.Label("Select an axis to begin.", Array.Empty<GUILayoutOption>());
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
			GUILayout.FlexibleSpace();
			if (calibration.recording)
			{
				GUI.enabled = false;
			}
			if (GUILayout.Button("Close", Array.Empty<GUILayoutOption>()))
			{
				this.calibrateScrollPos = default(Vector2);
				this.dialog.Confirm();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00061A68 File Offset: 0x0005FC68
		private void DialogResultCallback(int queueActionId, ControlRemappingDemo1.UserResponse response)
		{
			foreach (ControlRemappingDemo1.QueueEntry queueEntry in this.actionQueue)
			{
				if (queueEntry.id == queueActionId)
				{
					if (response != ControlRemappingDemo1.UserResponse.Cancel)
					{
						queueEntry.Confirm(response);
						break;
					}
					queueEntry.Cancel();
					break;
				}
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x00061AD4 File Offset: 0x0005FCD4
		private Rect GetScreenCenteredRect(float width, float height)
		{
			return new Rect((float)Screen.width * 0.5f - width * 0.5f, (float)((double)Screen.height * 0.5 - (double)(height * 0.5f)), width, height);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00061B0C File Offset: 0x0005FD0C
		private void EnqueueAction(ControlRemappingDemo1.QueueEntry entry)
		{
			if (entry == null)
			{
				return;
			}
			this.busy = true;
			GUI.enabled = false;
			this.actionQueue.Enqueue(entry);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00061B2C File Offset: 0x0005FD2C
		private void ProcessQueue()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (this.busy || this.actionQueue.Count == 0)
			{
				return;
			}
			while (this.actionQueue.Count > 0)
			{
				ControlRemappingDemo1.QueueEntry queueEntry = this.actionQueue.Peek();
				bool flag = false;
				switch (queueEntry.queueActionType)
				{
				case ControlRemappingDemo1.QueueActionType.JoystickAssignment:
					flag = this.ProcessJoystickAssignmentChange((ControlRemappingDemo1.JoystickAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.ElementAssignment:
					flag = this.ProcessElementAssignmentChange((ControlRemappingDemo1.ElementAssignmentChange)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification:
					flag = this.ProcessFallbackJoystickIdentification((ControlRemappingDemo1.FallbackJoystickIdentification)queueEntry);
					break;
				case ControlRemappingDemo1.QueueActionType.Calibrate:
					flag = this.ProcessCalibration((ControlRemappingDemo1.Calibration)queueEntry);
					break;
				}
				if (!flag)
				{
					break;
				}
				this.actionQueue.Dequeue();
			}
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00061BE8 File Offset: 0x0005FDE8
		private bool ProcessJoystickAssignmentChange(ControlRemappingDemo1.JoystickAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			Player player = ReInput.players.GetPlayer(entry.playerId);
			if (player == null)
			{
				return true;
			}
			if (!entry.assign)
			{
				player.controllers.RemoveController(ControllerType.Joystick, entry.joystickId);
				this.ControllerSelectionChanged();
				return true;
			}
			if (player.controllers.ContainsController(ControllerType.Joystick, entry.joystickId))
			{
				return true;
			}
			if (!ReInput.controllers.IsJoystickAssigned(entry.joystickId) || entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				player.controllers.AddController(ControllerType.Joystick, entry.joystickId, true);
				this.ControllerSelectionChanged();
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Reassignment",
				message = "This joystick is already assigned to another player. Do you want to reassign this joystick to " + player.descriptiveName + "?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00061D00 File Offset: 0x0005FF00
		private bool ProcessElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			switch (entry.changeType)
			{
			case ControlRemappingDemo1.ElementAssignmentChangeType.Add:
			case ControlRemappingDemo1.ElementAssignmentChangeType.Replace:
				return this.ProcessAddOrReplaceElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.Remove:
				return this.ProcessRemoveElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ReassignOrRemove:
				return this.ProcessRemoveOrReassignElementAssignment(entry);
			case ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck:
				return this.ProcessElementAssignmentConflictCheck(entry);
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00061D58 File Offset: 0x0005FF58
		private bool ProcessRemoveOrReassignElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Remove;
				this.actionQueue.Enqueue(elementAssignmentChange);
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange2 = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange2.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.Replace;
				this.actionQueue.Enqueue(elementAssignmentChange2);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
			{
				title = "Reassign or Remove",
				message = "Do you want to reassign or remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawReassignOrRemoveElementAssignmentWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00061E2C File Offset: 0x0006002C
		private bool ProcessRemoveElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				entry.context.controllerMap.DeleteElementMap(entry.context.actionElementMapToReplace.id);
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.DeleteAssignmentConfirmation, new ControlRemappingDemo1.WindowProperties
			{
				title = "Remove Assignment",
				message = "Are you sure you want to remove this assignment?",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawModalWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00061EEC File Offset: 0x000600EC
		private bool ProcessAddOrReplaceElementAssignment(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (entry.state != ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				string text;
				if (entry.context.controllerMap.controllerType == ControllerType.Keyboard)
				{
					if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Command, Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					else
					{
						text = "Press any key to assign it to this action. You may also use the modifier keys Control, Alt, and Shift. If you wish to assign a modifier key itself to this action, press and hold the key for 1 second.";
					}
					if (Application.isEditor)
					{
						text += "\n\nNOTE: Some modifier key combinations will not work in the Unity Editor, but they will work in a game build.";
					}
				}
				else if (entry.context.controllerMap.controllerType == ControllerType.Mouse)
				{
					text = "Press any mouse button or axis to assign it to this action.\n\nTo assign mouse movement axes, move the mouse quickly in the direction you want mapped to the action. Slow movements will be ignored.";
				}
				else
				{
					text = "Press any button or axis to assign it to this action.";
				}
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assign",
					message = text,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
				return false;
			}
			if (Event.current.type != EventType.Layout)
			{
				return false;
			}
			if (this.conflictFoundEventData != null)
			{
				ControlRemappingDemo1.ElementAssignmentChange elementAssignmentChange = new ControlRemappingDemo1.ElementAssignmentChange(entry);
				elementAssignmentChange.changeType = ControlRemappingDemo1.ElementAssignmentChangeType.ConflictCheck;
				this.actionQueue.Enqueue(elementAssignmentChange);
			}
			return true;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0006201C File Offset: 0x0006021C
		private bool ProcessElementAssignmentConflictCheck(ControlRemappingDemo1.ElementAssignmentChange entry)
		{
			if (entry.context.controllerMap == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				this.inputMapper.Stop();
				return true;
			}
			if (this.conflictFoundEventData == null)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				if (entry.response == ControlRemappingDemo1.UserResponse.Confirm)
				{
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Replace);
				}
				else
				{
					if (entry.response != ControlRemappingDemo1.UserResponse.Custom1)
					{
						throw new NotImplementedException();
					}
					this.conflictFoundEventData.responseCallback(InputMapper.ConflictResponse.Add);
				}
				return true;
			}
			if (this.conflictFoundEventData.isProtected)
			{
				string text = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use and is protected from reassignment. You cannot remove the protected assignment, but you can still assign the action to this element. If you do so, the element will trigger multiple actions when activated.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = text,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentProtectedConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			else
			{
				string text2 = this.conflictFoundEventData.assignment.elementDisplayName + " is already in use. You may replace the other conflicting assignments, add this assignment anyway which will leave multiple actions assigned to this element, or cancel this assignment.";
				this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.AssignElement, new ControlRemappingDemo1.WindowProperties
				{
					title = "Assignment Conflict",
					message = text2,
					rect = this.GetScreenCenteredRect(250f, 200f),
					windowDrawDelegate = new Action<string, string>(this.DrawElementAssignmentNormalConflictWindow)
				}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			}
			return false;
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x000621B8 File Offset: 0x000603B8
		private bool ProcessFallbackJoystickIdentification(ControlRemappingDemo1.FallbackJoystickIdentification entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Joystick Identification Required",
				message = "A joystick has been attached or removed. You will need to identify each joystick by pressing a button on the controller listed below:",
				rect = this.GetScreenCenteredRect(250f, 200f),
				windowDrawDelegate = new Action<string, string>(this.DrawFallbackJoystickIdentificationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback), 1f);
			return false;
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0006224C File Offset: 0x0006044C
		private bool ProcessCalibration(ControlRemappingDemo1.Calibration entry)
		{
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Canceled)
			{
				return true;
			}
			if (entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				return true;
			}
			this.dialog.StartModal(entry.id, ControlRemappingDemo1.DialogHelper.DialogType.JoystickConflict, new ControlRemappingDemo1.WindowProperties
			{
				title = "Calibrate Controller",
				message = "Select an axis to calibrate on the " + entry.joystick.name + ".",
				rect = this.GetScreenCenteredRect(450f, 480f),
				windowDrawDelegate = new Action<string, string>(this.DrawCalibrationWindow)
			}, new Action<int, ControlRemappingDemo1.UserResponse>(this.DialogResultCallback));
			return false;
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x000622EE File Offset: 0x000604EE
		private void PlayerSelectionChanged()
		{
			this.ClearControllerSelection();
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000622F6 File Offset: 0x000604F6
		private void ControllerSelectionChanged()
		{
			this.ClearMapSelection();
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000622FE File Offset: 0x000604FE
		private void ClearControllerSelection()
		{
			this.selectedController.Clear();
			this.ClearMapSelection();
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x00062311 File Offset: 0x00060511
		private void ClearMapSelection()
		{
			this.selectedMapCategoryId = -1;
			this.selectedMap = null;
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x00062321 File Offset: 0x00060521
		private void ResetAll()
		{
			this.ClearWorkingVars();
			this.initialized = false;
			this.showMenu = false;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x00062338 File Offset: 0x00060538
		private void ClearWorkingVars()
		{
			this.selectedPlayer = null;
			this.ClearMapSelection();
			this.selectedController.Clear();
			this.actionScrollPos = default(Vector2);
			this.dialog.FullReset();
			this.actionQueue.Clear();
			this.busy = false;
			this.startListening = false;
			this.conflictFoundEventData = null;
			this.inputMapper.Stop();
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000623A0 File Offset: 0x000605A0
		private void SetGUIStateStart()
		{
			this.guiState = true;
			if (this.busy)
			{
				this.guiState = false;
			}
			this.pageGUIState = this.guiState && !this.busy && !this.dialog.enabled && !this.dialog.busy;
			if (GUI.enabled != this.guiState)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0006240F File Offset: 0x0006060F
		private void SetGUIStateEnd()
		{
			this.guiState = true;
			if (!GUI.enabled)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0006242C File Offset: 0x0006062C
		private void JoystickConnected(ControllerStatusChangedEventArgs args)
		{
			if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
			{
				using (IEnumerator<Player> enumerator = ReInput.players.AllPlayers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Player player = enumerator.Current;
						if (player.controllers.ContainsController(args.controllerType, args.controllerId))
						{
							ReInput.userDataStore.LoadControllerData(player.id, args.controllerType, args.controllerId);
						}
					}
					goto IL_0090;
				}
			}
			ReInput.userDataStore.LoadControllerData(args.controllerType, args.controllerId);
			IL_0090:
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000624E8 File Offset: 0x000606E8
		private void JoystickPreDisconnect(ControllerStatusChangedEventArgs args)
		{
			if (this.selectedController.hasSelection && args.controllerType == this.selectedController.type && args.controllerId == this.selectedController.id)
			{
				this.ClearControllerSelection();
			}
			if (this.showMenu)
			{
				if (ReInput.controllers.IsControllerAssigned(args.controllerType, args.controllerId))
				{
					using (IEnumerator<Player> enumerator = ReInput.players.AllPlayers.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Player player = enumerator.Current;
							if (player.controllers.ContainsController(args.controllerType, args.controllerId))
							{
								ReInput.userDataStore.SaveControllerData(player.id, args.controllerType, args.controllerId);
							}
						}
						return;
					}
				}
				ReInput.userDataStore.SaveControllerData(args.controllerType, args.controllerId);
			}
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000625DC File Offset: 0x000607DC
		private void JoystickDisconnected(ControllerStatusChangedEventArgs args)
		{
			if (this.showMenu)
			{
				this.ClearWorkingVars();
			}
			if (ReInput.unityJoystickIdentificationRequired)
			{
				this.IdentifyAllJoysticks();
			}
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000625F9 File Offset: 0x000607F9
		private void OnConflictFound(InputMapper.ConflictFoundEventData data)
		{
			this.conflictFoundEventData = data;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x00062602 File Offset: 0x00060802
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.conflictFoundEventData = null;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0006260C File Offset: 0x0006080C
		public void IdentifyAllJoysticks()
		{
			if (ReInput.controllers.joystickCount == 0)
			{
				return;
			}
			this.ClearWorkingVars();
			this.Open();
			foreach (Joystick joystick in ReInput.controllers.Joysticks)
			{
				this.actionQueue.Enqueue(new ControlRemappingDemo1.FallbackJoystickIdentification(joystick.id, joystick.name));
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0006268C File Offset: 0x0006088C
		protected void CheckRecompile()
		{
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0006268E File Offset: 0x0006088E
		private void RecompileWindow(int windowId)
		{
		}

		// Token: 0x040018F7 RID: 6391
		private const float defaultModalWidth = 250f;

		// Token: 0x040018F8 RID: 6392
		private const float defaultModalHeight = 200f;

		// Token: 0x040018F9 RID: 6393
		private const float assignmentTimeout = 5f;

		// Token: 0x040018FA RID: 6394
		private ControlRemappingDemo1.DialogHelper dialog;

		// Token: 0x040018FB RID: 6395
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x040018FC RID: 6396
		private InputMapper.ConflictFoundEventData conflictFoundEventData;

		// Token: 0x040018FD RID: 6397
		private bool guiState;

		// Token: 0x040018FE RID: 6398
		private bool busy;

		// Token: 0x040018FF RID: 6399
		private bool pageGUIState;

		// Token: 0x04001900 RID: 6400
		private Player selectedPlayer;

		// Token: 0x04001901 RID: 6401
		private int selectedMapCategoryId;

		// Token: 0x04001902 RID: 6402
		private ControlRemappingDemo1.ControllerSelection selectedController;

		// Token: 0x04001903 RID: 6403
		private ControllerMap selectedMap;

		// Token: 0x04001904 RID: 6404
		private bool showMenu;

		// Token: 0x04001905 RID: 6405
		private bool startListening;

		// Token: 0x04001906 RID: 6406
		private Vector2 actionScrollPos;

		// Token: 0x04001907 RID: 6407
		private Vector2 calibrateScrollPos;

		// Token: 0x04001908 RID: 6408
		private Queue<ControlRemappingDemo1.QueueEntry> actionQueue;

		// Token: 0x04001909 RID: 6409
		private bool setupFinished;

		// Token: 0x0400190A RID: 6410
		[NonSerialized]
		private bool initialized;

		// Token: 0x0400190B RID: 6411
		private bool isCompiling;

		// Token: 0x0400190C RID: 6412
		private GUIStyle style_wordWrap;

		// Token: 0x0400190D RID: 6413
		private GUIStyle style_centeredBox;

		// Token: 0x020004A0 RID: 1184
		private class ControllerSelection
		{
			// Token: 0x06001DB7 RID: 7607 RVA: 0x00078969 File Offset: 0x00076B69
			public ControllerSelection()
			{
				this.Clear();
			}

			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00078977 File Offset: 0x00076B77
			// (set) Token: 0x06001DB9 RID: 7609 RVA: 0x0007897F File Offset: 0x00076B7F
			public int id
			{
				get
				{
					return this._id;
				}
				set
				{
					this._idPrev = this._id;
					this._id = value;
				}
			}

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001DBA RID: 7610 RVA: 0x00078994 File Offset: 0x00076B94
			// (set) Token: 0x06001DBB RID: 7611 RVA: 0x0007899C File Offset: 0x00076B9C
			public ControllerType type
			{
				get
				{
					return this._type;
				}
				set
				{
					this._typePrev = this._type;
					this._type = value;
				}
			}

			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001DBC RID: 7612 RVA: 0x000789B1 File Offset: 0x00076BB1
			public int idPrev
			{
				get
				{
					return this._idPrev;
				}
			}

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001DBD RID: 7613 RVA: 0x000789B9 File Offset: 0x00076BB9
			public ControllerType typePrev
			{
				get
				{
					return this._typePrev;
				}
			}

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001DBE RID: 7614 RVA: 0x000789C1 File Offset: 0x00076BC1
			public bool hasSelection
			{
				get
				{
					return this._id >= 0;
				}
			}

			// Token: 0x06001DBF RID: 7615 RVA: 0x000789CF File Offset: 0x00076BCF
			public void Set(int id, ControllerType type)
			{
				this.id = id;
				this.type = type;
			}

			// Token: 0x06001DC0 RID: 7616 RVA: 0x000789DF File Offset: 0x00076BDF
			public void Clear()
			{
				this._id = -1;
				this._idPrev = -1;
				this._type = ControllerType.Joystick;
				this._typePrev = ControllerType.Joystick;
			}

			// Token: 0x04001F3F RID: 7999
			private int _id;

			// Token: 0x04001F40 RID: 8000
			private int _idPrev;

			// Token: 0x04001F41 RID: 8001
			private ControllerType _type;

			// Token: 0x04001F42 RID: 8002
			private ControllerType _typePrev;
		}

		// Token: 0x020004A1 RID: 1185
		private class DialogHelper
		{
			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x000789FD File Offset: 0x00076BFD
			private float busyTimer
			{
				get
				{
					if (!this._busyTimerRunning)
					{
						return 0f;
					}
					return this._busyTime - Time.realtimeSinceStartup;
				}
			}

			// Token: 0x17000614 RID: 1556
			// (get) Token: 0x06001DC2 RID: 7618 RVA: 0x00078A19 File Offset: 0x00076C19
			// (set) Token: 0x06001DC3 RID: 7619 RVA: 0x00078A21 File Offset: 0x00076C21
			public bool enabled
			{
				get
				{
					return this._enabled;
				}
				set
				{
					if (!value)
					{
						this._enabled = value;
						this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
						this.StateChanged(0.1f);
						return;
					}
					if (this._type == ControlRemappingDemo1.DialogHelper.DialogType.None)
					{
						return;
					}
					this.StateChanged(0.25f);
				}
			}

			// Token: 0x17000615 RID: 1557
			// (get) Token: 0x06001DC4 RID: 7620 RVA: 0x00078A54 File Offset: 0x00076C54
			// (set) Token: 0x06001DC5 RID: 7621 RVA: 0x00078A66 File Offset: 0x00076C66
			public ControlRemappingDemo1.DialogHelper.DialogType type
			{
				get
				{
					if (!this._enabled)
					{
						return ControlRemappingDemo1.DialogHelper.DialogType.None;
					}
					return this._type;
				}
				set
				{
					if (value == ControlRemappingDemo1.DialogHelper.DialogType.None)
					{
						this._enabled = false;
						this.StateChanged(0.1f);
					}
					else
					{
						this._enabled = true;
						this.StateChanged(0.25f);
					}
					this._type = value;
				}
			}

			// Token: 0x17000616 RID: 1558
			// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x00078A98 File Offset: 0x00076C98
			public bool busy
			{
				get
				{
					return this._busyTimerRunning;
				}
			}

			// Token: 0x06001DC7 RID: 7623 RVA: 0x00078AA0 File Offset: 0x00076CA0
			public DialogHelper()
			{
				this.drawWindowDelegate = new Action<int>(this.DrawWindow);
				this.drawWindowFunction = new GUI.WindowFunction(this.drawWindowDelegate.Invoke);
			}

			// Token: 0x06001DC8 RID: 7624 RVA: 0x00078AD1 File Offset: 0x00076CD1
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback)
			{
				this.StartModal(queueActionId, type, windowProperties, resultCallback, -1f);
			}

			// Token: 0x06001DC9 RID: 7625 RVA: 0x00078AE3 File Offset: 0x00076CE3
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback, float openBusyDelay)
			{
				this.currentActionId = queueActionId;
				this.windowProperties = windowProperties;
				this.type = type;
				this.resultCallback = resultCallback;
				if (openBusyDelay >= 0f)
				{
					this.StateChanged(openBusyDelay);
				}
			}

			// Token: 0x06001DCA RID: 7626 RVA: 0x00078B13 File Offset: 0x00076D13
			public void Update()
			{
				this.Draw();
				this.UpdateTimers();
			}

			// Token: 0x06001DCB RID: 7627 RVA: 0x00078B24 File Offset: 0x00076D24
			public void Draw()
			{
				if (!this._enabled)
				{
					return;
				}
				bool enabled = GUI.enabled;
				GUI.enabled = true;
				GUILayout.Window(this.windowProperties.windowId, this.windowProperties.rect, this.drawWindowFunction, this.windowProperties.title, Array.Empty<GUILayoutOption>());
				GUI.FocusWindow(this.windowProperties.windowId);
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001DCC RID: 7628 RVA: 0x00078B96 File Offset: 0x00076D96
			public void DrawConfirmButton()
			{
				this.DrawConfirmButton("Confirm");
			}

			// Token: 0x06001DCD RID: 7629 RVA: 0x00078BA4 File Offset: 0x00076DA4
			public void DrawConfirmButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001DCE RID: 7630 RVA: 0x00078BE7 File Offset: 0x00076DE7
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response)
			{
				this.DrawConfirmButton(response, "Confirm");
			}

			// Token: 0x06001DCF RID: 7631 RVA: 0x00078BF8 File Offset: 0x00076DF8
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response, string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Confirm(response);
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001DD0 RID: 7632 RVA: 0x00078C3B File Offset: 0x00076E3B
			public void DrawCancelButton()
			{
				this.DrawCancelButton("Cancel");
			}

			// Token: 0x06001DD1 RID: 7633 RVA: 0x00078C48 File Offset: 0x00076E48
			public void DrawCancelButton(string title)
			{
				bool enabled = GUI.enabled;
				if (this.busy)
				{
					GUI.enabled = false;
				}
				if (GUILayout.Button(title, Array.Empty<GUILayoutOption>()))
				{
					this.Cancel();
				}
				if (GUI.enabled != enabled)
				{
					GUI.enabled = enabled;
				}
			}

			// Token: 0x06001DD2 RID: 7634 RVA: 0x00078C8A File Offset: 0x00076E8A
			public void Confirm()
			{
				this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
			}

			// Token: 0x06001DD3 RID: 7635 RVA: 0x00078C93 File Offset: 0x00076E93
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.resultCallback(this.currentActionId, response);
				this.Close();
			}

			// Token: 0x06001DD4 RID: 7636 RVA: 0x00078CAD File Offset: 0x00076EAD
			public void Cancel()
			{
				this.resultCallback(this.currentActionId, ControlRemappingDemo1.UserResponse.Cancel);
				this.Close();
			}

			// Token: 0x06001DD5 RID: 7637 RVA: 0x00078CC7 File Offset: 0x00076EC7
			private void DrawWindow(int windowId)
			{
				this.windowProperties.windowDrawDelegate(this.windowProperties.title, this.windowProperties.message);
			}

			// Token: 0x06001DD6 RID: 7638 RVA: 0x00078CEF File Offset: 0x00076EEF
			private void UpdateTimers()
			{
				if (this._busyTimerRunning && this.busyTimer <= 0f)
				{
					this._busyTimerRunning = false;
				}
			}

			// Token: 0x06001DD7 RID: 7639 RVA: 0x00078D0D File Offset: 0x00076F0D
			private void StartBusyTimer(float time)
			{
				this._busyTime = time + Time.realtimeSinceStartup;
				this._busyTimerRunning = true;
			}

			// Token: 0x06001DD8 RID: 7640 RVA: 0x00078D23 File Offset: 0x00076F23
			private void Close()
			{
				this.Reset();
				this.StateChanged(0.1f);
			}

			// Token: 0x06001DD9 RID: 7641 RVA: 0x00078D36 File Offset: 0x00076F36
			private void StateChanged(float delay)
			{
				this.StartBusyTimer(delay);
			}

			// Token: 0x06001DDA RID: 7642 RVA: 0x00078D3F File Offset: 0x00076F3F
			private void Reset()
			{
				this._enabled = false;
				this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
				this.currentActionId = -1;
				this.resultCallback = null;
			}

			// Token: 0x06001DDB RID: 7643 RVA: 0x00078D5D File Offset: 0x00076F5D
			private void ResetTimers()
			{
				this._busyTimerRunning = false;
			}

			// Token: 0x06001DDC RID: 7644 RVA: 0x00078D66 File Offset: 0x00076F66
			public void FullReset()
			{
				this.Reset();
				this.ResetTimers();
			}

			// Token: 0x04001F43 RID: 8003
			private const float openBusyDelay = 0.25f;

			// Token: 0x04001F44 RID: 8004
			private const float closeBusyDelay = 0.1f;

			// Token: 0x04001F45 RID: 8005
			private ControlRemappingDemo1.DialogHelper.DialogType _type;

			// Token: 0x04001F46 RID: 8006
			private bool _enabled;

			// Token: 0x04001F47 RID: 8007
			private float _busyTime;

			// Token: 0x04001F48 RID: 8008
			private bool _busyTimerRunning;

			// Token: 0x04001F49 RID: 8009
			private Action<int> drawWindowDelegate;

			// Token: 0x04001F4A RID: 8010
			private GUI.WindowFunction drawWindowFunction;

			// Token: 0x04001F4B RID: 8011
			private ControlRemappingDemo1.WindowProperties windowProperties;

			// Token: 0x04001F4C RID: 8012
			private int currentActionId;

			// Token: 0x04001F4D RID: 8013
			private Action<int, ControlRemappingDemo1.UserResponse> resultCallback;

			// Token: 0x020004CD RID: 1229
			public enum DialogType
			{
				// Token: 0x04001FDD RID: 8157
				None,
				// Token: 0x04001FDE RID: 8158
				JoystickConflict,
				// Token: 0x04001FDF RID: 8159
				ElementConflict,
				// Token: 0x04001FE0 RID: 8160
				KeyConflict,
				// Token: 0x04001FE1 RID: 8161
				DeleteAssignmentConfirmation = 10,
				// Token: 0x04001FE2 RID: 8162
				AssignElement
			}
		}

		// Token: 0x020004A2 RID: 1186
		private abstract class QueueEntry
		{
			// Token: 0x17000617 RID: 1559
			// (get) Token: 0x06001DDD RID: 7645 RVA: 0x00078D74 File Offset: 0x00076F74
			// (set) Token: 0x06001DDE RID: 7646 RVA: 0x00078D7C File Offset: 0x00076F7C
			public int id { get; protected set; }

			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00078D85 File Offset: 0x00076F85
			// (set) Token: 0x06001DE0 RID: 7648 RVA: 0x00078D8D File Offset: 0x00076F8D
			public ControlRemappingDemo1.QueueActionType queueActionType { get; protected set; }

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x00078D96 File Offset: 0x00076F96
			// (set) Token: 0x06001DE2 RID: 7650 RVA: 0x00078D9E File Offset: 0x00076F9E
			public ControlRemappingDemo1.QueueEntry.State state { get; protected set; }

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00078DA7 File Offset: 0x00076FA7
			// (set) Token: 0x06001DE4 RID: 7652 RVA: 0x00078DAF File Offset: 0x00076FAF
			public ControlRemappingDemo1.UserResponse response { get; protected set; }

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00078DB8 File Offset: 0x00076FB8
			protected static int nextId
			{
				get
				{
					int num = ControlRemappingDemo1.QueueEntry.uidCounter;
					ControlRemappingDemo1.QueueEntry.uidCounter++;
					return num;
				}
			}

			// Token: 0x06001DE6 RID: 7654 RVA: 0x00078DCB File Offset: 0x00076FCB
			public QueueEntry(ControlRemappingDemo1.QueueActionType queueActionType)
			{
				this.id = ControlRemappingDemo1.QueueEntry.nextId;
				this.queueActionType = queueActionType;
			}

			// Token: 0x06001DE7 RID: 7655 RVA: 0x00078DE5 File Offset: 0x00076FE5
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Confirmed;
				this.response = response;
			}

			// Token: 0x06001DE8 RID: 7656 RVA: 0x00078DF5 File Offset: 0x00076FF5
			public void Cancel()
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Canceled;
			}

			// Token: 0x04001F52 RID: 8018
			private static int uidCounter;

			// Token: 0x020004CE RID: 1230
			public enum State
			{
				// Token: 0x04001FE4 RID: 8164
				Waiting,
				// Token: 0x04001FE5 RID: 8165
				Confirmed,
				// Token: 0x04001FE6 RID: 8166
				Canceled
			}
		}

		// Token: 0x020004A3 RID: 1187
		private class JoystickAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x00078DFE File Offset: 0x00076FFE
			// (set) Token: 0x06001DEA RID: 7658 RVA: 0x00078E06 File Offset: 0x00077006
			public int playerId { get; private set; }

			// Token: 0x1700061D RID: 1565
			// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00078E0F File Offset: 0x0007700F
			// (set) Token: 0x06001DEC RID: 7660 RVA: 0x00078E17 File Offset: 0x00077017
			public int joystickId { get; private set; }

			// Token: 0x1700061E RID: 1566
			// (get) Token: 0x06001DED RID: 7661 RVA: 0x00078E20 File Offset: 0x00077020
			// (set) Token: 0x06001DEE RID: 7662 RVA: 0x00078E28 File Offset: 0x00077028
			public bool assign { get; private set; }

			// Token: 0x06001DEF RID: 7663 RVA: 0x00078E31 File Offset: 0x00077031
			public JoystickAssignmentChange(int newPlayerId, int joystickId, bool assign)
				: base(ControlRemappingDemo1.QueueActionType.JoystickAssignment)
			{
				this.playerId = newPlayerId;
				this.joystickId = joystickId;
				this.assign = assign;
			}
		}

		// Token: 0x020004A4 RID: 1188
		private class ElementAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x1700061F RID: 1567
			// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00078E4F File Offset: 0x0007704F
			// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x00078E57 File Offset: 0x00077057
			public ControlRemappingDemo1.ElementAssignmentChangeType changeType { get; set; }

			// Token: 0x17000620 RID: 1568
			// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00078E60 File Offset: 0x00077060
			// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x00078E68 File Offset: 0x00077068
			public InputMapper.Context context { get; private set; }

			// Token: 0x06001DF4 RID: 7668 RVA: 0x00078E71 File Offset: 0x00077071
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType changeType, InputMapper.Context context)
				: base(ControlRemappingDemo1.QueueActionType.ElementAssignment)
			{
				this.changeType = changeType;
				this.context = context;
			}

			// Token: 0x06001DF5 RID: 7669 RVA: 0x00078E88 File Offset: 0x00077088
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange other)
				: this(other.changeType, other.context.Clone())
			{
			}
		}

		// Token: 0x020004A5 RID: 1189
		private class FallbackJoystickIdentification : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x17000621 RID: 1569
			// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x00078EA1 File Offset: 0x000770A1
			// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x00078EA9 File Offset: 0x000770A9
			public int joystickId { get; private set; }

			// Token: 0x17000622 RID: 1570
			// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x00078EB2 File Offset: 0x000770B2
			// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x00078EBA File Offset: 0x000770BA
			public string joystickName { get; private set; }

			// Token: 0x06001DFA RID: 7674 RVA: 0x00078EC3 File Offset: 0x000770C3
			public FallbackJoystickIdentification(int joystickId, string joystickName)
				: base(ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification)
			{
				this.joystickId = joystickId;
				this.joystickName = joystickName;
			}
		}

		// Token: 0x020004A6 RID: 1190
		private class Calibration : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x17000623 RID: 1571
			// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00078EDA File Offset: 0x000770DA
			// (set) Token: 0x06001DFC RID: 7676 RVA: 0x00078EE2 File Offset: 0x000770E2
			public Player player { get; private set; }

			// Token: 0x17000624 RID: 1572
			// (get) Token: 0x06001DFD RID: 7677 RVA: 0x00078EEB File Offset: 0x000770EB
			// (set) Token: 0x06001DFE RID: 7678 RVA: 0x00078EF3 File Offset: 0x000770F3
			public ControllerType controllerType { get; private set; }

			// Token: 0x17000625 RID: 1573
			// (get) Token: 0x06001DFF RID: 7679 RVA: 0x00078EFC File Offset: 0x000770FC
			// (set) Token: 0x06001E00 RID: 7680 RVA: 0x00078F04 File Offset: 0x00077104
			public Joystick joystick { get; private set; }

			// Token: 0x17000626 RID: 1574
			// (get) Token: 0x06001E01 RID: 7681 RVA: 0x00078F0D File Offset: 0x0007710D
			// (set) Token: 0x06001E02 RID: 7682 RVA: 0x00078F15 File Offset: 0x00077115
			public CalibrationMap calibrationMap { get; private set; }

			// Token: 0x06001E03 RID: 7683 RVA: 0x00078F1E File Offset: 0x0007711E
			public Calibration(Player player, Joystick joystick, CalibrationMap calibrationMap)
				: base(ControlRemappingDemo1.QueueActionType.Calibrate)
			{
				this.player = player;
				this.joystick = joystick;
				this.calibrationMap = calibrationMap;
				this.selectedElementIdentifierId = -1;
			}

			// Token: 0x04001F5E RID: 8030
			public int selectedElementIdentifierId;

			// Token: 0x04001F5F RID: 8031
			public bool recording;
		}

		// Token: 0x020004A7 RID: 1191
		private struct WindowProperties
		{
			// Token: 0x04001F60 RID: 8032
			public int windowId;

			// Token: 0x04001F61 RID: 8033
			public Rect rect;

			// Token: 0x04001F62 RID: 8034
			public Action<string, string> windowDrawDelegate;

			// Token: 0x04001F63 RID: 8035
			public string title;

			// Token: 0x04001F64 RID: 8036
			public string message;
		}

		// Token: 0x020004A8 RID: 1192
		private enum QueueActionType
		{
			// Token: 0x04001F66 RID: 8038
			None,
			// Token: 0x04001F67 RID: 8039
			JoystickAssignment,
			// Token: 0x04001F68 RID: 8040
			ElementAssignment,
			// Token: 0x04001F69 RID: 8041
			FallbackJoystickIdentification,
			// Token: 0x04001F6A RID: 8042
			Calibrate
		}

		// Token: 0x020004A9 RID: 1193
		private enum ElementAssignmentChangeType
		{
			// Token: 0x04001F6C RID: 8044
			Add,
			// Token: 0x04001F6D RID: 8045
			Replace,
			// Token: 0x04001F6E RID: 8046
			Remove,
			// Token: 0x04001F6F RID: 8047
			ReassignOrRemove,
			// Token: 0x04001F70 RID: 8048
			ConflictCheck
		}

		// Token: 0x020004AA RID: 1194
		public enum UserResponse
		{
			// Token: 0x04001F72 RID: 8050
			Confirm,
			// Token: 0x04001F73 RID: 8051
			Cancel,
			// Token: 0x04001F74 RID: 8052
			Custom1,
			// Token: 0x04001F75 RID: 8053
			Custom2
		}
	}
}

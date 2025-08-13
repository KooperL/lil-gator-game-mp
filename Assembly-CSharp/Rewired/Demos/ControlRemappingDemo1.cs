using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rewired.Demos
{
	// Token: 0x02000486 RID: 1158
	[AddComponentMenu("")]
	public class ControlRemappingDemo1 : MonoBehaviour
	{
		// Token: 0x06001C90 RID: 7312 RVA: 0x00015E03 File Offset: 0x00014003
		private void Awake()
		{
			this.inputMapper.options.timeout = 5f;
			this.inputMapper.options.ignoreMouseXAxis = true;
			this.inputMapper.options.ignoreMouseYAxis = true;
			this.Initialize();
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x00015E42 File Offset: 0x00014042
		private void OnEnable()
		{
			this.Subscribe();
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00015E4A File Offset: 0x0001404A
		private void OnDisable()
		{
			this.Unsubscribe();
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x0006F85C File Offset: 0x0006DA5C
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

		// Token: 0x06001C94 RID: 7316 RVA: 0x0006F8E4 File Offset: 0x0006DAE4
		private void Setup()
		{
			if (this.setupFinished)
			{
				return;
			}
			this.style_wordWrap = new GUIStyle(GUI.skin.label);
			this.style_wordWrap.wordWrap = true;
			this.style_centeredBox = new GUIStyle(GUI.skin.box);
			this.style_centeredBox.alignment = 4;
			this.setupFinished = true;
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00015E52 File Offset: 0x00014052
		private void Subscribe()
		{
			this.Unsubscribe();
			this.inputMapper.ConflictFoundEvent += this.OnConflictFound;
			this.inputMapper.StoppedEvent += this.OnStopped;
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00015E88 File Offset: 0x00014088
		private void Unsubscribe()
		{
			this.inputMapper.RemoveAllEventListeners();
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x0006F944 File Offset: 0x0006DB44
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

		// Token: 0x06001C98 RID: 7320 RVA: 0x0006F99C File Offset: 0x0006DB9C
		private void HandleMenuControl()
		{
			if (this.dialog.enabled)
			{
				return;
			}
			if (Event.current.type == 8 && ReInput.players.GetSystemPlayer().GetButtonDown("Menu"))
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

		// Token: 0x06001C99 RID: 7321 RVA: 0x00015E95 File Offset: 0x00014095
		private void Close()
		{
			this.ClearWorkingVars();
			this.showMenu = false;
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00015EA4 File Offset: 0x000140A4
		private void Open()
		{
			this.showMenu = true;
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0006F9FC File Offset: 0x0006DBFC
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

		// Token: 0x06001C9C RID: 7324 RVA: 0x0006FA94 File Offset: 0x0006DC94
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

		// Token: 0x06001C9D RID: 7325 RVA: 0x0006FB58 File Offset: 0x0006DD58
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

		// Token: 0x06001C9E RID: 7326 RVA: 0x0006FC6C File Offset: 0x0006DE6C
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

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006FD88 File Offset: 0x0006DF88
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
				this.selectedPlayer.controllers.ClearControllersOfType(2);
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

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0006FEE4 File Offset: 0x0006E0E4
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
				this.selectedController.Set(0, 0);
				this.ControllerSelectionChanged();
			}
			bool flag = this.selectedController.type == 0;
			if (GUILayout.Toggle(flag, "Keyboard", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
			{
				this.selectedController.Set(0, 0);
				this.ControllerSelectionChanged();
			}
			if (!this.selectedPlayer.controllers.hasMouse)
			{
				GUI.enabled = false;
			}
			flag = this.selectedController.type == 1;
			if (GUILayout.Toggle(flag, "Mouse", "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
			{
				this.selectedController.Set(0, 1);
				this.ControllerSelectionChanged();
			}
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
			foreach (Joystick joystick in this.selectedPlayer.controllers.Joysticks)
			{
				flag = this.selectedController.type == 2 && this.selectedController.id == joystick.id;
				if (GUILayout.Toggle(flag, joystick.name, "Button", new GUILayoutOption[] { GUILayout.ExpandWidth(false) }) != flag)
				{
					this.selectedController.Set(joystick.id, 2);
					this.ControllerSelectionChanged();
				}
			}
			GUILayout.EndHorizontal();
			if (GUI.enabled != enabled)
			{
				GUI.enabled = enabled;
			}
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x000700B8 File Offset: 0x0006E2B8
		private void DrawCalibrateButton()
		{
			if (this.selectedPlayer == null)
			{
				return;
			}
			bool enabled = GUI.enabled;
			GUILayout.Space(10f);
			Controller controller = (this.selectedController.hasSelection ? this.selectedPlayer.controllers.GetController(this.selectedController.type, this.selectedController.id) : null);
			if (controller == null || this.selectedController.type != 2)
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

		// Token: 0x06001CA2 RID: 7330 RVA: 0x000701B4 File Offset: 0x0006E3B4
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

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00070388 File Offset: 0x0006E588
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
				if (inputAction.type == 1)
				{
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, 1, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap in this.selectedMap.AllMaps)
					{
						if (actionElementMap.actionId == inputAction.id)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, 1, this.selectedController, this.selectedMap, actionElementMap);
						}
					}
					GUILayout.EndHorizontal();
				}
				else if (inputAction.type == null)
				{
					if (this.selectedController.type != null)
					{
						GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
						GUILayout.Label(text, new GUILayoutOption[] { GUILayout.Width(num) });
						this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, 0, this.selectedController, this.selectedMap);
						foreach (ActionElementMap actionElementMap2 in this.selectedMap.AllMaps)
						{
							if (actionElementMap2.actionId == inputAction.id && actionElementMap2.elementType != 1 && actionElementMap2.axisType != 2)
							{
								this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, 0, this.selectedController, this.selectedMap, actionElementMap2);
								this.DrawInvertButton(this.selectedPlayer.id, inputAction, 0, this.selectedController, this.selectedMap, actionElementMap2);
							}
						}
						GUILayout.EndHorizontal();
					}
					string text2 = ((inputAction.positiveDescriptiveName != string.Empty) ? inputAction.positiveDescriptiveName : (inputAction.descriptiveName + " +"));
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text2, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, 1, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap3 in this.selectedMap.AllMaps)
					{
						if (actionElementMap3.actionId == inputAction.id && actionElementMap3.axisContribution == null && actionElementMap3.axisType != 1)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, 1, this.selectedController, this.selectedMap, actionElementMap3);
						}
					}
					GUILayout.EndHorizontal();
					string text3 = ((inputAction.negativeDescriptiveName != string.Empty) ? inputAction.negativeDescriptiveName : (inputAction.descriptiveName + " -"));
					GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
					GUILayout.Label(text3, new GUILayoutOption[] { GUILayout.Width(num) });
					this.DrawAddActionMapButton(this.selectedPlayer.id, inputAction, 2, this.selectedController, this.selectedMap);
					foreach (ActionElementMap actionElementMap4 in this.selectedMap.AllMaps)
					{
						if (actionElementMap4.actionId == inputAction.id && actionElementMap4.axisContribution == 1 && actionElementMap4.axisType != 1)
						{
							this.DrawActionAssignmentButton(this.selectedPlayer.id, inputAction, 2, this.selectedController, this.selectedMap, actionElementMap4);
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

		// Token: 0x06001CA4 RID: 7332 RVA: 0x0007087C File Offset: 0x0006EA7C
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

		// Token: 0x06001CA5 RID: 7333 RVA: 0x000708FC File Offset: 0x0006EAFC
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

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00070944 File Offset: 0x0006EB44
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

		// Token: 0x06001CA7 RID: 7335 RVA: 0x00015EAD File Offset: 0x000140AD
		private void ShowDialog()
		{
			this.dialog.Update();
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000709AC File Offset: 0x0006EBAC
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

		// Token: 0x06001CA9 RID: 7337 RVA: 0x00070A18 File Offset: 0x0006EC18
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

		// Token: 0x06001CAA RID: 7338 RVA: 0x00070A74 File Offset: 0x0006EC74
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
				if (this.startListening && this.inputMapper.status == null)
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

		// Token: 0x06001CAB RID: 7339 RVA: 0x00070B80 File Offset: 0x0006ED80
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

		// Token: 0x06001CAC RID: 7340 RVA: 0x00070C0C File Offset: 0x0006EE0C
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

		// Token: 0x06001CAD RID: 7341 RVA: 0x00070CAC File Offset: 0x0006EEAC
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

		// Token: 0x06001CAE RID: 7342 RVA: 0x00070D1C File Offset: 0x0006EF1C
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

		// Token: 0x06001CAF RID: 7343 RVA: 0x00070DEC File Offset: 0x0006EFEC
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

		// Token: 0x06001CB0 RID: 7344 RVA: 0x0007128C File Offset: 0x0006F48C
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

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00015EBA File Offset: 0x000140BA
		private Rect GetScreenCenteredRect(float width, float height)
		{
			return new Rect((float)Screen.width * 0.5f - width * 0.5f, (float)((double)Screen.height * 0.5 - (double)(height * 0.5f)), width, height);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00015EF2 File Offset: 0x000140F2
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

		// Token: 0x06001CB3 RID: 7347 RVA: 0x000712F8 File Offset: 0x0006F4F8
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

		// Token: 0x06001CB4 RID: 7348 RVA: 0x000713B4 File Offset: 0x0006F5B4
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
				player.controllers.RemoveController(2, entry.joystickId);
				this.ControllerSelectionChanged();
				return true;
			}
			if (player.controllers.ContainsController(2, entry.joystickId))
			{
				return true;
			}
			if (!ReInput.controllers.IsJoystickAssigned(entry.joystickId) || entry.state == ControlRemappingDemo1.QueueEntry.State.Confirmed)
			{
				player.controllers.AddController(2, entry.joystickId, true);
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

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000714CC File Offset: 0x0006F6CC
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

		// Token: 0x06001CB6 RID: 7350 RVA: 0x00071524 File Offset: 0x0006F724
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

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000715F8 File Offset: 0x0006F7F8
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

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000716B8 File Offset: 0x0006F8B8
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
				if (entry.context.controllerMap.controllerType == null)
				{
					if (Application.platform == null || Application.platform == 1)
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
				else if (entry.context.controllerMap.controllerType == 1)
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
			if (Event.current.type != 8)
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

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000717E8 File Offset: 0x0006F9E8
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
					this.conflictFoundEventData.responseCallback(1);
				}
				else
				{
					if (entry.response != ControlRemappingDemo1.UserResponse.Custom1)
					{
						throw new NotImplementedException();
					}
					this.conflictFoundEventData.responseCallback(2);
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

		// Token: 0x06001CBA RID: 7354 RVA: 0x00071984 File Offset: 0x0006FB84
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

		// Token: 0x06001CBB RID: 7355 RVA: 0x00071A18 File Offset: 0x0006FC18
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

		// Token: 0x06001CBC RID: 7356 RVA: 0x00015F11 File Offset: 0x00014111
		private void PlayerSelectionChanged()
		{
			this.ClearControllerSelection();
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x00015F19 File Offset: 0x00014119
		private void ControllerSelectionChanged()
		{
			this.ClearMapSelection();
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x00015F21 File Offset: 0x00014121
		private void ClearControllerSelection()
		{
			this.selectedController.Clear();
			this.ClearMapSelection();
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x00015F34 File Offset: 0x00014134
		private void ClearMapSelection()
		{
			this.selectedMapCategoryId = -1;
			this.selectedMap = null;
		}

		// Token: 0x06001CC0 RID: 7360 RVA: 0x00015F44 File Offset: 0x00014144
		private void ResetAll()
		{
			this.ClearWorkingVars();
			this.initialized = false;
			this.showMenu = false;
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00071ABC File Offset: 0x0006FCBC
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

		// Token: 0x06001CC2 RID: 7362 RVA: 0x00071B24 File Offset: 0x0006FD24
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

		// Token: 0x06001CC3 RID: 7363 RVA: 0x00015F5A File Offset: 0x0001415A
		private void SetGUIStateEnd()
		{
			this.guiState = true;
			if (!GUI.enabled)
			{
				GUI.enabled = this.guiState;
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00071B94 File Offset: 0x0006FD94
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

		// Token: 0x06001CC5 RID: 7365 RVA: 0x00071C50 File Offset: 0x0006FE50
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

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00015F75 File Offset: 0x00014175
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

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00015F92 File Offset: 0x00014192
		private void OnConflictFound(InputMapper.ConflictFoundEventData data)
		{
			this.conflictFoundEventData = data;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x00015F9B File Offset: 0x0001419B
		private void OnStopped(InputMapper.StoppedEventData data)
		{
			this.conflictFoundEventData = null;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x00071D44 File Offset: 0x0006FF44
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

		// Token: 0x06001CCA RID: 7370 RVA: 0x00002229 File Offset: 0x00000429
		protected void CheckRecompile()
		{
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00002229 File Offset: 0x00000429
		private void RecompileWindow(int windowId)
		{
		}

		// Token: 0x04001E27 RID: 7719
		private const float defaultModalWidth = 250f;

		// Token: 0x04001E28 RID: 7720
		private const float defaultModalHeight = 200f;

		// Token: 0x04001E29 RID: 7721
		private const float assignmentTimeout = 5f;

		// Token: 0x04001E2A RID: 7722
		private ControlRemappingDemo1.DialogHelper dialog;

		// Token: 0x04001E2B RID: 7723
		private InputMapper inputMapper = new InputMapper();

		// Token: 0x04001E2C RID: 7724
		private InputMapper.ConflictFoundEventData conflictFoundEventData;

		// Token: 0x04001E2D RID: 7725
		private bool guiState;

		// Token: 0x04001E2E RID: 7726
		private bool busy;

		// Token: 0x04001E2F RID: 7727
		private bool pageGUIState;

		// Token: 0x04001E30 RID: 7728
		private Player selectedPlayer;

		// Token: 0x04001E31 RID: 7729
		private int selectedMapCategoryId;

		// Token: 0x04001E32 RID: 7730
		private ControlRemappingDemo1.ControllerSelection selectedController;

		// Token: 0x04001E33 RID: 7731
		private ControllerMap selectedMap;

		// Token: 0x04001E34 RID: 7732
		private bool showMenu;

		// Token: 0x04001E35 RID: 7733
		private bool startListening;

		// Token: 0x04001E36 RID: 7734
		private Vector2 actionScrollPos;

		// Token: 0x04001E37 RID: 7735
		private Vector2 calibrateScrollPos;

		// Token: 0x04001E38 RID: 7736
		private Queue<ControlRemappingDemo1.QueueEntry> actionQueue;

		// Token: 0x04001E39 RID: 7737
		private bool setupFinished;

		// Token: 0x04001E3A RID: 7738
		[NonSerialized]
		private bool initialized;

		// Token: 0x04001E3B RID: 7739
		private bool isCompiling;

		// Token: 0x04001E3C RID: 7740
		private GUIStyle style_wordWrap;

		// Token: 0x04001E3D RID: 7741
		private GUIStyle style_centeredBox;

		// Token: 0x02000487 RID: 1159
		private class ControllerSelection
		{
			// Token: 0x06001CCD RID: 7373 RVA: 0x00015FB7 File Offset: 0x000141B7
			public ControllerSelection()
			{
				this.Clear();
			}

			// Token: 0x170005FB RID: 1531
			// (get) Token: 0x06001CCE RID: 7374 RVA: 0x00015FC5 File Offset: 0x000141C5
			// (set) Token: 0x06001CCF RID: 7375 RVA: 0x00015FCD File Offset: 0x000141CD
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

			// Token: 0x170005FC RID: 1532
			// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00015FE2 File Offset: 0x000141E2
			// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00015FEA File Offset: 0x000141EA
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

			// Token: 0x170005FD RID: 1533
			// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00015FFF File Offset: 0x000141FF
			public int idPrev
			{
				get
				{
					return this._idPrev;
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00016007 File Offset: 0x00014207
			public ControllerType typePrev
			{
				get
				{
					return this._typePrev;
				}
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x06001CD4 RID: 7380 RVA: 0x0001600F File Offset: 0x0001420F
			public bool hasSelection
			{
				get
				{
					return this._id >= 0;
				}
			}

			// Token: 0x06001CD5 RID: 7381 RVA: 0x0001601D File Offset: 0x0001421D
			public void Set(int id, ControllerType type)
			{
				this.id = id;
				this.type = type;
			}

			// Token: 0x06001CD6 RID: 7382 RVA: 0x0001602D File Offset: 0x0001422D
			public void Clear()
			{
				this._id = -1;
				this._idPrev = -1;
				this._type = 2;
				this._typePrev = 2;
			}

			// Token: 0x04001E3E RID: 7742
			private int _id;

			// Token: 0x04001E3F RID: 7743
			private int _idPrev;

			// Token: 0x04001E40 RID: 7744
			private ControllerType _type;

			// Token: 0x04001E41 RID: 7745
			private ControllerType _typePrev;
		}

		// Token: 0x02000488 RID: 1160
		private class DialogHelper
		{
			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x0001604B File Offset: 0x0001424B
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

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00016067 File Offset: 0x00014267
			// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x0001606F File Offset: 0x0001426F
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

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000160A2 File Offset: 0x000142A2
			// (set) Token: 0x06001CDB RID: 7387 RVA: 0x000160B4 File Offset: 0x000142B4
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

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000160E6 File Offset: 0x000142E6
			public bool busy
			{
				get
				{
					return this._busyTimerRunning;
				}
			}

			// Token: 0x06001CDD RID: 7389 RVA: 0x000160EE File Offset: 0x000142EE
			public DialogHelper()
			{
				this.drawWindowDelegate = new Action<int>(this.DrawWindow);
				this.drawWindowFunction = new GUI.WindowFunction(this.drawWindowDelegate.Invoke);
			}

			// Token: 0x06001CDE RID: 7390 RVA: 0x0001611F File Offset: 0x0001431F
			public void StartModal(int queueActionId, ControlRemappingDemo1.DialogHelper.DialogType type, ControlRemappingDemo1.WindowProperties windowProperties, Action<int, ControlRemappingDemo1.UserResponse> resultCallback)
			{
				this.StartModal(queueActionId, type, windowProperties, resultCallback, -1f);
			}

			// Token: 0x06001CDF RID: 7391 RVA: 0x00016131 File Offset: 0x00014331
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

			// Token: 0x06001CE0 RID: 7392 RVA: 0x00016161 File Offset: 0x00014361
			public void Update()
			{
				this.Draw();
				this.UpdateTimers();
			}

			// Token: 0x06001CE1 RID: 7393 RVA: 0x00071DC4 File Offset: 0x0006FFC4
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

			// Token: 0x06001CE2 RID: 7394 RVA: 0x0001616F File Offset: 0x0001436F
			public void DrawConfirmButton()
			{
				this.DrawConfirmButton("Confirm");
			}

			// Token: 0x06001CE3 RID: 7395 RVA: 0x00071E38 File Offset: 0x00070038
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

			// Token: 0x06001CE4 RID: 7396 RVA: 0x0001617C File Offset: 0x0001437C
			public void DrawConfirmButton(ControlRemappingDemo1.UserResponse response)
			{
				this.DrawConfirmButton(response, "Confirm");
			}

			// Token: 0x06001CE5 RID: 7397 RVA: 0x00071E7C File Offset: 0x0007007C
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

			// Token: 0x06001CE6 RID: 7398 RVA: 0x0001618A File Offset: 0x0001438A
			public void DrawCancelButton()
			{
				this.DrawCancelButton("Cancel");
			}

			// Token: 0x06001CE7 RID: 7399 RVA: 0x00071EC0 File Offset: 0x000700C0
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

			// Token: 0x06001CE8 RID: 7400 RVA: 0x00016197 File Offset: 0x00014397
			public void Confirm()
			{
				this.Confirm(ControlRemappingDemo1.UserResponse.Confirm);
			}

			// Token: 0x06001CE9 RID: 7401 RVA: 0x000161A0 File Offset: 0x000143A0
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.resultCallback(this.currentActionId, response);
				this.Close();
			}

			// Token: 0x06001CEA RID: 7402 RVA: 0x000161BA File Offset: 0x000143BA
			public void Cancel()
			{
				this.resultCallback(this.currentActionId, ControlRemappingDemo1.UserResponse.Cancel);
				this.Close();
			}

			// Token: 0x06001CEB RID: 7403 RVA: 0x000161D4 File Offset: 0x000143D4
			private void DrawWindow(int windowId)
			{
				this.windowProperties.windowDrawDelegate(this.windowProperties.title, this.windowProperties.message);
			}

			// Token: 0x06001CEC RID: 7404 RVA: 0x000161FC File Offset: 0x000143FC
			private void UpdateTimers()
			{
				if (this._busyTimerRunning && this.busyTimer <= 0f)
				{
					this._busyTimerRunning = false;
				}
			}

			// Token: 0x06001CED RID: 7405 RVA: 0x0001621A File Offset: 0x0001441A
			private void StartBusyTimer(float time)
			{
				this._busyTime = time + Time.realtimeSinceStartup;
				this._busyTimerRunning = true;
			}

			// Token: 0x06001CEE RID: 7406 RVA: 0x00016230 File Offset: 0x00014430
			private void Close()
			{
				this.Reset();
				this.StateChanged(0.1f);
			}

			// Token: 0x06001CEF RID: 7407 RVA: 0x00016243 File Offset: 0x00014443
			private void StateChanged(float delay)
			{
				this.StartBusyTimer(delay);
			}

			// Token: 0x06001CF0 RID: 7408 RVA: 0x0001624C File Offset: 0x0001444C
			private void Reset()
			{
				this._enabled = false;
				this._type = ControlRemappingDemo1.DialogHelper.DialogType.None;
				this.currentActionId = -1;
				this.resultCallback = null;
			}

			// Token: 0x06001CF1 RID: 7409 RVA: 0x0001626A File Offset: 0x0001446A
			private void ResetTimers()
			{
				this._busyTimerRunning = false;
			}

			// Token: 0x06001CF2 RID: 7410 RVA: 0x00016273 File Offset: 0x00014473
			public void FullReset()
			{
				this.Reset();
				this.ResetTimers();
			}

			// Token: 0x04001E42 RID: 7746
			private const float openBusyDelay = 0.25f;

			// Token: 0x04001E43 RID: 7747
			private const float closeBusyDelay = 0.1f;

			// Token: 0x04001E44 RID: 7748
			private ControlRemappingDemo1.DialogHelper.DialogType _type;

			// Token: 0x04001E45 RID: 7749
			private bool _enabled;

			// Token: 0x04001E46 RID: 7750
			private float _busyTime;

			// Token: 0x04001E47 RID: 7751
			private bool _busyTimerRunning;

			// Token: 0x04001E48 RID: 7752
			private Action<int> drawWindowDelegate;

			// Token: 0x04001E49 RID: 7753
			private GUI.WindowFunction drawWindowFunction;

			// Token: 0x04001E4A RID: 7754
			private ControlRemappingDemo1.WindowProperties windowProperties;

			// Token: 0x04001E4B RID: 7755
			private int currentActionId;

			// Token: 0x04001E4C RID: 7756
			private Action<int, ControlRemappingDemo1.UserResponse> resultCallback;

			// Token: 0x02000489 RID: 1161
			public enum DialogType
			{
				// Token: 0x04001E4E RID: 7758
				None,
				// Token: 0x04001E4F RID: 7759
				JoystickConflict,
				// Token: 0x04001E50 RID: 7760
				ElementConflict,
				// Token: 0x04001E51 RID: 7761
				KeyConflict,
				// Token: 0x04001E52 RID: 7762
				DeleteAssignmentConfirmation = 10,
				// Token: 0x04001E53 RID: 7763
				AssignElement
			}
		}

		// Token: 0x0200048A RID: 1162
		private abstract class QueueEntry
		{
			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00016281 File Offset: 0x00014481
			// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x00016289 File Offset: 0x00014489
			public int id { get; protected set; }

			// Token: 0x17000605 RID: 1541
			// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x00016292 File Offset: 0x00014492
			// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x0001629A File Offset: 0x0001449A
			public ControlRemappingDemo1.QueueActionType queueActionType { get; protected set; }

			// Token: 0x17000606 RID: 1542
			// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000162A3 File Offset: 0x000144A3
			// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x000162AB File Offset: 0x000144AB
			public ControlRemappingDemo1.QueueEntry.State state { get; protected set; }

			// Token: 0x17000607 RID: 1543
			// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x000162B4 File Offset: 0x000144B4
			// (set) Token: 0x06001CFA RID: 7418 RVA: 0x000162BC File Offset: 0x000144BC
			public ControlRemappingDemo1.UserResponse response { get; protected set; }

			// Token: 0x17000608 RID: 1544
			// (get) Token: 0x06001CFB RID: 7419 RVA: 0x000162C5 File Offset: 0x000144C5
			protected static int nextId
			{
				get
				{
					int num = ControlRemappingDemo1.QueueEntry.uidCounter;
					ControlRemappingDemo1.QueueEntry.uidCounter++;
					return num;
				}
			}

			// Token: 0x06001CFC RID: 7420 RVA: 0x000162D8 File Offset: 0x000144D8
			public QueueEntry(ControlRemappingDemo1.QueueActionType queueActionType)
			{
				this.id = ControlRemappingDemo1.QueueEntry.nextId;
				this.queueActionType = queueActionType;
			}

			// Token: 0x06001CFD RID: 7421 RVA: 0x000162F2 File Offset: 0x000144F2
			public void Confirm(ControlRemappingDemo1.UserResponse response)
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Confirmed;
				this.response = response;
			}

			// Token: 0x06001CFE RID: 7422 RVA: 0x00016302 File Offset: 0x00014502
			public void Cancel()
			{
				this.state = ControlRemappingDemo1.QueueEntry.State.Canceled;
			}

			// Token: 0x04001E58 RID: 7768
			private static int uidCounter;

			// Token: 0x0200048B RID: 1163
			public enum State
			{
				// Token: 0x04001E5A RID: 7770
				Waiting,
				// Token: 0x04001E5B RID: 7771
				Confirmed,
				// Token: 0x04001E5C RID: 7772
				Canceled
			}
		}

		// Token: 0x0200048C RID: 1164
		private class JoystickAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x17000609 RID: 1545
			// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0001630B File Offset: 0x0001450B
			// (set) Token: 0x06001D00 RID: 7424 RVA: 0x00016313 File Offset: 0x00014513
			public int playerId { get; private set; }

			// Token: 0x1700060A RID: 1546
			// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0001631C File Offset: 0x0001451C
			// (set) Token: 0x06001D02 RID: 7426 RVA: 0x00016324 File Offset: 0x00014524
			public int joystickId { get; private set; }

			// Token: 0x1700060B RID: 1547
			// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0001632D File Offset: 0x0001452D
			// (set) Token: 0x06001D04 RID: 7428 RVA: 0x00016335 File Offset: 0x00014535
			public bool assign { get; private set; }

			// Token: 0x06001D05 RID: 7429 RVA: 0x0001633E File Offset: 0x0001453E
			public JoystickAssignmentChange(int newPlayerId, int joystickId, bool assign)
				: base(ControlRemappingDemo1.QueueActionType.JoystickAssignment)
			{
				this.playerId = newPlayerId;
				this.joystickId = joystickId;
				this.assign = assign;
			}
		}

		// Token: 0x0200048D RID: 1165
		private class ElementAssignmentChange : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x1700060C RID: 1548
			// (get) Token: 0x06001D06 RID: 7430 RVA: 0x0001635C File Offset: 0x0001455C
			// (set) Token: 0x06001D07 RID: 7431 RVA: 0x00016364 File Offset: 0x00014564
			public ControlRemappingDemo1.ElementAssignmentChangeType changeType { get; set; }

			// Token: 0x1700060D RID: 1549
			// (get) Token: 0x06001D08 RID: 7432 RVA: 0x0001636D File Offset: 0x0001456D
			// (set) Token: 0x06001D09 RID: 7433 RVA: 0x00016375 File Offset: 0x00014575
			public InputMapper.Context context { get; private set; }

			// Token: 0x06001D0A RID: 7434 RVA: 0x0001637E File Offset: 0x0001457E
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChangeType changeType, InputMapper.Context context)
				: base(ControlRemappingDemo1.QueueActionType.ElementAssignment)
			{
				this.changeType = changeType;
				this.context = context;
			}

			// Token: 0x06001D0B RID: 7435 RVA: 0x00016395 File Offset: 0x00014595
			public ElementAssignmentChange(ControlRemappingDemo1.ElementAssignmentChange other)
				: this(other.changeType, other.context.Clone())
			{
			}
		}

		// Token: 0x0200048E RID: 1166
		private class FallbackJoystickIdentification : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x1700060E RID: 1550
			// (get) Token: 0x06001D0C RID: 7436 RVA: 0x000163AE File Offset: 0x000145AE
			// (set) Token: 0x06001D0D RID: 7437 RVA: 0x000163B6 File Offset: 0x000145B6
			public int joystickId { get; private set; }

			// Token: 0x1700060F RID: 1551
			// (get) Token: 0x06001D0E RID: 7438 RVA: 0x000163BF File Offset: 0x000145BF
			// (set) Token: 0x06001D0F RID: 7439 RVA: 0x000163C7 File Offset: 0x000145C7
			public string joystickName { get; private set; }

			// Token: 0x06001D10 RID: 7440 RVA: 0x000163D0 File Offset: 0x000145D0
			public FallbackJoystickIdentification(int joystickId, string joystickName)
				: base(ControlRemappingDemo1.QueueActionType.FallbackJoystickIdentification)
			{
				this.joystickId = joystickId;
				this.joystickName = joystickName;
			}
		}

		// Token: 0x0200048F RID: 1167
		private class Calibration : ControlRemappingDemo1.QueueEntry
		{
			// Token: 0x17000610 RID: 1552
			// (get) Token: 0x06001D11 RID: 7441 RVA: 0x000163E7 File Offset: 0x000145E7
			// (set) Token: 0x06001D12 RID: 7442 RVA: 0x000163EF File Offset: 0x000145EF
			public Player player { get; private set; }

			// Token: 0x17000611 RID: 1553
			// (get) Token: 0x06001D13 RID: 7443 RVA: 0x000163F8 File Offset: 0x000145F8
			// (set) Token: 0x06001D14 RID: 7444 RVA: 0x00016400 File Offset: 0x00014600
			public ControllerType controllerType { get; private set; }

			// Token: 0x17000612 RID: 1554
			// (get) Token: 0x06001D15 RID: 7445 RVA: 0x00016409 File Offset: 0x00014609
			// (set) Token: 0x06001D16 RID: 7446 RVA: 0x00016411 File Offset: 0x00014611
			public Joystick joystick { get; private set; }

			// Token: 0x17000613 RID: 1555
			// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0001641A File Offset: 0x0001461A
			// (set) Token: 0x06001D18 RID: 7448 RVA: 0x00016422 File Offset: 0x00014622
			public CalibrationMap calibrationMap { get; private set; }

			// Token: 0x06001D19 RID: 7449 RVA: 0x0001642B File Offset: 0x0001462B
			public Calibration(Player player, Joystick joystick, CalibrationMap calibrationMap)
				: base(ControlRemappingDemo1.QueueActionType.Calibrate)
			{
				this.player = player;
				this.joystick = joystick;
				this.calibrationMap = calibrationMap;
				this.selectedElementIdentifierId = -1;
			}

			// Token: 0x04001E68 RID: 7784
			public int selectedElementIdentifierId;

			// Token: 0x04001E69 RID: 7785
			public bool recording;
		}

		// Token: 0x02000490 RID: 1168
		private struct WindowProperties
		{
			// Token: 0x04001E6A RID: 7786
			public int windowId;

			// Token: 0x04001E6B RID: 7787
			public Rect rect;

			// Token: 0x04001E6C RID: 7788
			public Action<string, string> windowDrawDelegate;

			// Token: 0x04001E6D RID: 7789
			public string title;

			// Token: 0x04001E6E RID: 7790
			public string message;
		}

		// Token: 0x02000491 RID: 1169
		private enum QueueActionType
		{
			// Token: 0x04001E70 RID: 7792
			None,
			// Token: 0x04001E71 RID: 7793
			JoystickAssignment,
			// Token: 0x04001E72 RID: 7794
			ElementAssignment,
			// Token: 0x04001E73 RID: 7795
			FallbackJoystickIdentification,
			// Token: 0x04001E74 RID: 7796
			Calibrate
		}

		// Token: 0x02000492 RID: 1170
		private enum ElementAssignmentChangeType
		{
			// Token: 0x04001E76 RID: 7798
			Add,
			// Token: 0x04001E77 RID: 7799
			Replace,
			// Token: 0x04001E78 RID: 7800
			Remove,
			// Token: 0x04001E79 RID: 7801
			ReassignOrRemove,
			// Token: 0x04001E7A RID: 7802
			ConflictCheck
		}

		// Token: 0x02000493 RID: 1171
		public enum UserResponse
		{
			// Token: 0x04001E7C RID: 7804
			Confirm,
			// Token: 0x04001E7D RID: 7805
			Cancel,
			// Token: 0x04001E7E RID: 7806
			Custom1,
			// Token: 0x04001E7F RID: 7807
			Custom2
		}
	}
}

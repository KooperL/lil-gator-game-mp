using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Platforms;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rewired
{
	[AddComponentMenu("Rewired/Input Manager")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x000111D9 File Offset: 0x0000F3D9
		protected override void OnInitialized()
		{
			this.SubscribeEvents();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000111E1 File Offset: 0x0000F3E1
		protected override void OnDeinitialized()
		{
			this.UnsubscribeEvents();
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00060BA8 File Offset: 0x0005EDA8
		protected override void DetectPlatform()
		{
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.Net20;
			this.editorPlatform = EditorPlatform.None;
			this.platform = Platform.Unknown;
			this.webplayerPlatform = WebplayerPlatform.None;
			this.isEditor = false;
			if (SystemInfo.deviceName == null)
			{
				string empty = string.Empty;
			}
			if (SystemInfo.deviceModel == null)
			{
				string empty2 = string.Empty;
			}
			this.platform = Platform.Windows;
			this.scriptingBackend = ScriptingBackend.Mono;
			this.scriptingAPILevel = ScriptingAPILevel.NetStandard20;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00002229 File Offset: 0x00000429
		protected override void CheckRecompile()
		{
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000111E9 File Offset: 0x0000F3E9
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x000111F0 File Offset: 0x0000F3F0
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, RegexOptions.IgnoreCase) || Regex.IsMatch(deviceModel, searchPattern, RegexOptions.IgnoreCase);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00011206 File Offset: 0x0000F406
		private void SubscribeEvents()
		{
			this.UnsubscribeEvents();
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0001121F File Offset: 0x0000F41F
		private void UnsubscribeEvents()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00011232 File Offset: 0x0000F432
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			base.OnSceneLoaded();
		}

		private bool ignoreRecompile;
	}
}

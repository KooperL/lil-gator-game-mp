using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Rewired.Utils;
using Rewired.Utils.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rewired
{
	[AddComponentMenu("Rewired/Input Manager")]
	[EditorBrowsable(1)]
	public sealed class InputManager : InputManager_Base
	{
		// Token: 0x060015CB RID: 5579 RVA: 0x000111EE File Offset: 0x0000F3EE
		protected override void OnInitialized()
		{
			this.SubscribeEvents();
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x000111F6 File Offset: 0x0000F3F6
		protected override void OnDeinitialized()
		{
			this.UnsubscribeEvents();
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00060D3C File Offset: 0x0005EF3C
		protected override void DetectPlatform()
		{
			this.scriptingBackend = 0;
			this.scriptingAPILevel = 0;
			this.editorPlatform = 0;
			this.platform = 0;
			this.webplayerPlatform = 0;
			this.isEditor = false;
			if (SystemInfo.deviceName == null)
			{
				string empty = string.Empty;
			}
			if (SystemInfo.deviceModel == null)
			{
				string empty2 = string.Empty;
			}
			this.platform = 1;
			this.scriptingBackend = 0;
			this.scriptingAPILevel = 3;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00002229 File Offset: 0x00000429
		protected override void CheckRecompile()
		{
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000111FE File Offset: 0x0000F3FE
		protected override IExternalTools GetExternalTools()
		{
			return new ExternalTools();
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00011205 File Offset: 0x0000F405
		private bool CheckDeviceName(string searchPattern, string deviceName, string deviceModel)
		{
			return Regex.IsMatch(deviceName, searchPattern, 1) || Regex.IsMatch(deviceModel, searchPattern, 1);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0001121B File Offset: 0x0000F41B
		private void SubscribeEvents()
		{
			this.UnsubscribeEvents();
			SceneManager.sceneLoaded += this.OnSceneLoaded;
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00011234 File Offset: 0x0000F434
		private void UnsubscribeEvents()
		{
			SceneManager.sceneLoaded -= this.OnSceneLoaded;
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00011247 File Offset: 0x0000F447
		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			base.OnSceneLoaded();
		}

		private bool ignoreRecompile;
	}
}

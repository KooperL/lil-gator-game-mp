using System;
using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

// Token: 0x02000073 RID: 115
public class LoadSceneSequence : MonoBehaviour
{
	// Token: 0x06000172 RID: 370 RVA: 0x000033A8 File Offset: 0x000015A8
	public static void LoadScene(int buildIndex, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = false;
		LoadSceneSequence.sceneBuildIndex = buildIndex;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000033BC File Offset: 0x000015BC
	public static void LoadScene(AssetReference sceneAsset, LoadSceneSequence.LoadType loadType = LoadSceneSequence.LoadType.LoadingScreen)
	{
		LoadSceneSequence.isSceneAsset = true;
		LoadSceneSequence.sceneAssetReference = sceneAsset;
		LoadSceneSequence.LoadScene(loadType);
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000033D0 File Offset: 0x000015D0
	public static void StartPreloadScene(AssetReference sceneAsset)
	{
		LoadSceneSequence.hasPreloadedScene = true;
		LoadSceneSequence.preloadedSceneHandle = Addressables.LoadSceneAsync(sceneAsset, 0, false, 100);
		LoadSceneSequence.preloadedScene = sceneAsset;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x000033ED File Offset: 0x000015ED
	private static void LoadScene(LoadSceneSequence.LoadType loadType)
	{
		LoadSceneSequence.loadType = loadType;
		LoadSceneSequence.oldScene = SceneManager.GetActiveScene();
		if (loadType == LoadSceneSequence.LoadType.Fade)
		{
			Object.Instantiate<GameObject>(Prefabs.p.loadingSequenceFade);
			return;
		}
		Object.Instantiate<GameObject>(Prefabs.p.loadingSequence);
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00003424 File Offset: 0x00001624
	private void Start()
	{
		base.StartCoroutine(this.LoadSceneAsync());
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00003433 File Offset: 0x00001633
	public IEnumerator LoadSceneAsync()
	{
		Object.DontDestroyOnLoad(base.gameObject);
		FadeGameVolume.FadeOutGameVolume();
		ReInput.players.GetPlayer(0).controllers.maps.SetAllMapsEnabled(false);
		yield return new WaitForSeconds(1f);
		SpeedrunData.isLoading = true;
		if (LoadSceneSequence.hasPreloadedScene)
		{
			if (LoadSceneSequence.isSceneAsset && LoadSceneSequence.preloadedScene.SubObjectName == LoadSceneSequence.sceneAssetReference.SubObjectName)
			{
				Debug.Log("Using preloaded scene");
			}
			else
			{
				yield return LoadSceneSequence.preloadedSceneHandle;
				yield return LoadSceneSequence.preloadedSceneHandle.Result.ActivateAsync();
				LoadSceneSequence.hasPreloadedScene = false;
			}
		}
		if (LoadSceneSequence.isSceneAsset)
		{
			AsyncOperationHandle<SceneInstance> handle;
			if (LoadSceneSequence.hasPreloadedScene)
			{
				handle = LoadSceneSequence.preloadedSceneHandle;
			}
			else
			{
				handle = Addressables.LoadSceneAsync(LoadSceneSequence.sceneAssetReference, 0, false, 100);
			}
			yield return handle;
			yield return handle.Result.ActivateAsync();
			handle = default(AsyncOperationHandle<SceneInstance>);
		}
		else
		{
			AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(LoadSceneSequence.sceneBuildIndex, 0);
			yield return asyncOperation;
		}
		SpeedrunData.isLoading = false;
		LoadSceneSequence.hasPreloadedScene = false;
		if (this.animator != null)
		{
			this.animator.SetTrigger("LoadFinished");
		}
		FadeGameVolume.FadeInGameVolume();
		yield return new WaitForSeconds(0.5f);
		ReInput.players.GetPlayer(0).controllers.maps.SetAllMapsEnabled(true);
		Object.Destroy(base.gameObject);
		yield break;
	}

	// Token: 0x04000232 RID: 562
	private static Scene oldScene;

	// Token: 0x04000233 RID: 563
	private static bool isSceneAsset;

	// Token: 0x04000234 RID: 564
	private static int sceneBuildIndex;

	// Token: 0x04000235 RID: 565
	private static AssetReference sceneAssetReference;

	// Token: 0x04000236 RID: 566
	private static LoadSceneSequence.LoadType loadType;

	// Token: 0x04000237 RID: 567
	private static bool hasPreloadedScene;

	// Token: 0x04000238 RID: 568
	private static AsyncOperationHandle<SceneInstance> preloadedSceneHandle;

	// Token: 0x04000239 RID: 569
	private static AssetReference preloadedScene;

	// Token: 0x0400023A RID: 570
	public AssetReference mainMenuScene;

	// Token: 0x0400023B RID: 571
	public Animator animator;

	// Token: 0x0400023C RID: 572
	public bool isReadyToShowScene = true;

	// Token: 0x02000074 RID: 116
	public enum LoadType
	{
		// Token: 0x0400023E RID: 574
		LoadingScreen,
		// Token: 0x0400023F RID: 575
		Fade
	}
}

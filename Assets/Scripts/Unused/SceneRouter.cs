using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneRouter
{
	public enum SceneIndex
	{
		WorldMap = 1,
		DistrictEditor = 2,
		DistrictViewer = 3,
		UI = 4,
	}

	public enum RouteType
	{
		None,
		WorldMap,
		CityEditor,
		CityViewer
	}

	public delegate void ActiveSceneChangedEventHandler(RouteType routeType);

	public static event ActiveSceneChangedEventHandler BeforeActiveSceneChangeEvent;
	public static event ActiveSceneChangedEventHandler AfterActiveSceneChangeEvent;

	private static RouteType _previousRoute = RouteType.None;
	private static RouteType _currentRoute = RouteType.None;

	public static RouteType PreviousRoute => _previousRoute;
	public static RouteType CurrentRoute => _currentRoute;

	public static void SwitchToRoute(RouteType route)
	{
		if (_currentRoute == route) return;

		BeforeActiveSceneChangeEvent?.Invoke(_currentRoute);

		SetCurrentRoute(route);
	}

	public static async Task SwitchToRoute(RouteType route, float delay)
	{
		if (_currentRoute == route) return;

		BeforeActiveSceneChangeEvent?.Invoke(_currentRoute);

		await Task.Delay(TimeSpan.FromSeconds(delay));

		SetCurrentRoute(route);
	}

	private static void SetCurrentRoute(RouteType route)
	{
		_previousRoute = _currentRoute;
		_currentRoute = route;

		switch (_currentRoute)
		{
			case RouteType.WorldMap:
				UnloadSceneAsync(SceneIndex.DistrictEditor);
				UnloadSceneAsync(SceneIndex.DistrictViewer);
				LoadSceneAsync(SceneIndex.WorldMap, LoadSceneMode.Additive, true);
				LoadSceneAsync(SceneIndex.UI);
				break;
			case RouteType.CityEditor:
				UnloadSceneAsync(SceneIndex.WorldMap);
				UnloadSceneAsync(SceneIndex.DistrictViewer);
				LoadSceneAsync(SceneIndex.DistrictEditor, LoadSceneMode.Additive, true);
				LoadSceneAsync(SceneIndex.UI);
				break;
			case RouteType.CityViewer:
				UnloadSceneAsync(SceneIndex.WorldMap);
				UnloadSceneAsync(SceneIndex.DistrictEditor);
				LoadSceneAsync(SceneIndex.DistrictViewer, LoadSceneMode.Additive, true);
				LoadSceneAsync(SceneIndex.UI);
				break;
			default:
				break;
		}
	}

	private static void LoadSceneAsync(SceneIndex sceneIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Additive, bool newActiveScene = false)
	{
		if (IsSceneLoaded(sceneIndex)) return;

		var asyncOperation = SceneManager.LoadSceneAsync((int)sceneIndex, loadSceneMode);

		if (newActiveScene)
		{
			asyncOperation.completed += operation =>
			{
				SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex((int)sceneIndex));
				AfterActiveSceneChangeEvent?.Invoke(_currentRoute);
			};
		}

	}

	private static void UnloadSceneAsync(SceneIndex sceneIndex, UnloadSceneOptions unloadSceneOptions = UnloadSceneOptions.None)
	{
		if (!IsSceneLoaded(sceneIndex)) return;

		SceneManager.UnloadSceneAsync((int)sceneIndex, unloadSceneOptions);
	}

	private static bool IsSceneLoaded(SceneIndex sceneIndex)
	{
		return SceneManager.GetSceneByBuildIndex((int)sceneIndex).isLoaded;
	}
}

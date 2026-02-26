using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scene_Type
{
    None, StartScene, WorldScene,BattleScene,LoadingScene,TestScene
}

public class Scene_Manager : Singleton<Scene_Manager>
{
    //委托
    public delegate void Subscribe(Scene_Type type);
    public Subscribe _subscribe;

    private Scene_Type _preScene;
    private Scene_Type _currentScene;
    private Scene_Type _nextScene;

    private AsyncOperation _asyncLoad;
    private void Start()
    {
        _currentScene = Scene_Type.StartScene;
    }
    public void Enter_Scene(Scene_Type sceneType)
    {
        Debug.Log("尝试切换下一个场景:" + sceneType.ToString());

        if(_currentScene == sceneType)
        {
            Debug.Log("已经处在当前场景");
            return;
        }
        _preScene = _currentScene;
        _nextScene = sceneType;
        StartCoroutine(LoadSceneAsync());
    }
    public void Return_PreScene()
    {
        if(_preScene != Scene_Type.None)
        {
            Enter_Scene(_preScene);
        }
    }
    //异步加载场景
    private IEnumerator LoadSceneAsync()
    {
        Debug.Log("进入加载动画");
        yield return SceneManager.LoadSceneAsync(Scene_Type.LoadingScene.ToString(), LoadSceneMode.Additive);
        Debug.Log("尝试卸载场景：" + _currentScene.ToString());
        //卸载旧场景
        yield return SceneManager.UnloadSceneAsync(_currentScene.ToString());
        //加载新场景
        _asyncLoad = SceneManager.LoadSceneAsync(_nextScene.ToString(),LoadSceneMode.Additive);
        //禁止加载操作立即激活场景
        _asyncLoad.allowSceneActivation = false;
        while (!_asyncLoad.isDone)
        {
            yield return null;
            if(_asyncLoad.progress >= 0.9f)
            {
                _asyncLoad.allowSceneActivation = true;
                _currentScene = _nextScene;
            }
        }
        //通知所有订阅者
        _subscribe(_currentScene);

        Debug.Log("退出加载动画");
        yield return SceneManager.UnloadSceneAsync(Scene_Type.LoadingScene.ToString());

    }
}

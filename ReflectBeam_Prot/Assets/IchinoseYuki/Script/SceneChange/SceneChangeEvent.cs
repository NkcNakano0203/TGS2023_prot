// 作成日:02/14 作成者:市瀬
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンチェンジを検知するスクリプト
/// </summary>
public class SceneChangeEvent : MonoBehaviour
{
    private RemoteControl remoteControl;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    /// <summary>
    /// シーンがロードされたときタグを探してオブジェクトがあった時、
    /// リフレクターオブジェクトが入っているスクリプトをGetComponentをする
    /// </summary>
    private void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
       GameObject.FindWithTag("Player").GetComponent<RemoteControl>().CheckObjectName();
    }
}
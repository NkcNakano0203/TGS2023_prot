// 作成日: 作成者:
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        // デリゲート登録
        playerInput.actions["Loadtest_R_Shoulder"].performed += LoadTest;
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void LoadTest(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("TestGameScene");
        // デリゲート登録
        playerInput.actions["Loadtest_R_Shoulder"].performed -= LoadTest;
    }
}
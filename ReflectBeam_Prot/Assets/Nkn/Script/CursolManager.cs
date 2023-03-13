using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursolManager : MonoBehaviour
{
    public void NoCursol()
    {
#if !UNITY_EDITOR
        // カーソルをウィンドウから出さない
        Cursor.lockState  = CursorLockMode.Confined;
        // カーソルを表示しない
        Cursor.visible = false;
#endif
    }
}
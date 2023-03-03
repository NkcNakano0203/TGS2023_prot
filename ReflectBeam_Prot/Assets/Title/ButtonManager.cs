using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class ButtonManager : MonoBehaviour
{
    [SerializeField]PlayerInput playerInput;

    [SerializeField]GameObject[] button;
    int selectnum=0;

    [SerializeField]GameObject arrow;

    [SerializeField]float arrowXPos=10;
    // Start is called before the first frame update
    void Start()
    {

        // デリゲート登録
        playerInput.onActionTriggered += Arrow;
    }

    void Arrow(InputAction.CallbackContext context)
    {
        
        // スティック入力以外か選択変更フラグがfalseの時早期リターン
        //if (context.action.name != "RightStick" ) { return; }

        
        Vector2 rightStickValue = context.ReadValue<Vector2>(); 

        if (rightStickValue.y > 0.5f)
        {
            if (selectnum > 0)
                selectnum--;
        }       
        if (rightStickValue.y < -0.5f)
        {
            if (selectnum < button.Length-1)
                selectnum++;
        }
        
       

    }
    void Update()
    {
        Vector3 arrowpos= button[selectnum].transform.position;
        arrowpos.x += arrowXPos;
        arrow.transform.position = arrowpos;
    }
}

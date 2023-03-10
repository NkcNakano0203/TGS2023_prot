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

        // ÉfÉäÉQÅ[Égìoò^        
        playerInput.actions["Navigate"].performed += RightStickArrow;
    }

    void RightStickArrow(InputAction.CallbackContext context)
    {
        
        Vector2 rightStickValue = context.ReadValue<Vector2>();
        

        if (rightStickValue.y > 0.5f)
        {
            if (selectnum > 0)
                selectnum--;
        }
        if (rightStickValue.y < -0.5)
        {
            if (selectnum < button.Length - 1)
                selectnum++;
        }
        Vector3 arrowpos= button[selectnum].transform.position;
        arrowpos.x += arrowXPos;
        arrow.transform.position = arrowpos;
    }
 
}

// 作成日02/17日金曜日 製作者:市瀬
using UnityEngine;
using UnityEngine.InputSystem;

public class RBLBGimmickSelect : MonoBehaviour
{
    // ギミックを保管する配列
    private GameObject[] gimmicks;
    private GimmickList gimmickList;
    // 現在のギミック番号
    private int currentObjectNumber;
    // 最大オブジェクト数
    private int maxObjectNumber;

    private Transform currentObjectTransform;

    private InputAction leftAction;
    private InputAction rightAction;
    private Transform aimImageTransform;
    private PlayerInput playerInput;

    [SerializeField]
    private Material selectMat;
    [SerializeField]
    private Material gimmickMat;
    [SerializeField]
    private string aimImagePath = "aimImageCanvas_2/aimImage_2";

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        gimmickList = GetComponent<GimmickList>();
        // ギミック取得
        gimmicks = gimmickList.gimmickLists;
        maxObjectNumber = gimmicks.Length;
        // イベント登録
        RightStick_GimmickSelection rightStick_GimmickSelection = GetComponent<RightStick_GimmickSelection>();
        rightStick_GimmickSelection.CurrentObjectNumber += CurrentObjectNumber;
        // デリゲート登録
        playerInput.actions["L_Shoulder"].performed += OnRightBumper;
        playerInput.actions["R_Shoulder"].performed += OnLeftBumper;
        playerInput.actions["R_Trigger"].started += OnRightTrigger;
        playerInput.actions["L_Trigger"].started += OnLeftTrigger;

        // 孫のtransformを取得
        aimImageTransform = GameObject.Find(aimImagePath).gameObject.transform;
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        AimImageMove();
        AimImageRotation();

        rightAction = playerInput.actions["R_Trigger"];
        leftAction = playerInput.actions["L_Trigger"];

        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
    }

    private void Update()
    {
        GimmickFreeRotation();
    }

    /// <summary>
    /// 自由回転時の処理
    /// </summary>
    private void GimmickFreeRotation()
    {
        bool isRightTriggerPressed = rightAction.IsPressed();
        bool isLeftTriggerPressed = leftAction.IsPressed();

        // RかLのトリガーが押されているとき回転させる
        if (isRightTriggerPressed || isLeftTriggerPressed)
        {
            FreeRotation freeRotation = gimmicks[currentObjectNumber].transform.GetChild(0).gameObject.GetComponent<FreeRotation>();
            if (freeRotation)
            {
                freeRotation.RightRotate(isLeftTriggerPressed, isRightTriggerPressed);
                freeRotation.LeftRotate(isLeftTriggerPressed, isRightTriggerPressed);
                AimImageRotation();
            }
        }
    }

    /// <summary>
    /// R1ボタン
    /// </summary>
    private void OnRightBumper(InputAction.CallbackContext context)
    {
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = gimmickMat;

        if (maxObjectNumber - 1 > currentObjectNumber) { currentObjectNumber++; }
        else { currentObjectNumber = 0; }

        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
        AimImageMove();
        AimImageRotation();
    }

    /// <summary>
    /// L1ボタン
    /// </summary>
    private void OnLeftBumper(InputAction.CallbackContext context)
    {
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = gimmickMat;

        if (currentObjectNumber == 0) { currentObjectNumber += maxObjectNumber - 1; }
        else { currentObjectNumber--; }

        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        gimmicks[currentObjectNumber].transform.GetChild(0).transform.GetChild(2).GetComponent<MeshRenderer>().material = selectMat;
        AimImageMove();
        AimImageRotation();
    }

    /// <summary>
    /// R2ボタン
    /// </summary>
    private void OnRightTrigger(InputAction.CallbackContext context)
    {
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        if (gimmicks[currentObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.RightRotate(true, true);
        }
    }

    /// <summary>
    /// L2ボタン
    /// </summary>
    private void OnLeftTrigger(InputAction.CallbackContext context)
    {
        currentObjectTransform = gimmicks[currentObjectNumber].transform.GetChild(0).transform;
        if (gimmicks[currentObjectNumber].transform.GetChild(0).TryGetComponent(out FixedRotation fixedRotation))
        {
            fixedRotation.LeftRotate(true, true);
        }
    }

    /// <summary>
    /// 照準画像の移動メソッド
    /// </summary>
    private void AimImageMove()
    {
        aimImageTransform.position = currentObjectTransform.position;
    }

    /// <summary>
    /// 照準画像の回転メソッド
    /// </summary>
    private void AimImageRotation()
    {
        aimImageTransform.rotation = currentObjectTransform.rotation;
    }

    /// <summary>
    /// 右スティックで選択中オブジェクトを変えたとき何番目にしたかを通知するメソッド
    /// </summary>
    private void CurrentObjectNumber(int number)
    {
        currentObjectNumber = number;
    }

    private void OnDestroy()
    {
        // 解除してあげないとシーンロードした分Missingエラーが出る
        playerInput.actions["L_Shoulder"].performed -= OnRightBumper;
        playerInput.actions["R_Shoulder"].performed -= OnLeftBumper;
        playerInput.actions["R_Trigger"].started -= OnRightTrigger;
        playerInput.actions["L_Trigger"].started -= OnLeftTrigger;
    }
}
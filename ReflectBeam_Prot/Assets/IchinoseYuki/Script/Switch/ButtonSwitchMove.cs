// 作成日:04/19 作成者:市瀬
using UnityEngine;

/// <summary>
/// 押しボタンスイッチの動き
/// </summary>
namespace ButtonSwitch
{
    public class ButtonSwitchMove : MonoBehaviour
    {
        [HideInInspector]
        public string[] direction = new string[] { "x", "y", "-x", "-y" };
        [HideInInspector]
        public int select;

        private Vector3 parentPos;
        [SerializeField, Header("正の数で入力してください。")]
        private Vector3 minPos;
        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private bool active = false;
        private bool isDown;
        private bool onStay;

        [SerializeField,Header("ここに対応するギミックを入れてください")]
        private GameObject SupportedGimmick;

        FixedRotation_2 FR;

        private void Start()
        {
            parentPos = transform.parent.position;
            FR = SupportedGimmick.GetComponent<FixedRotation_2>();
        }

        private void Update()
        {
            switch (direction[select])
            {
                case "x":
                    RightMove();
                    break;
                case "y":
                    UpMove();
                    break;
                case "-x":
                    LeftMove();
                    break;
                case "-y":
                    DownMove();
                    break;
            }
            Return();
        }

        private void RightMove()
        {
            if (active && transform.position.x < parentPos.x + minPos.x)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (!active && transform.position.x > parentPos.x)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (transform.position.x > parentPos.x + minPos.x)
            {
                isDown = true;
            }
        }

        private void LeftMove()
        {
            if (active && transform.position.x > parentPos.x - minPos.x)
            {
                transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (!active && transform.position.x < parentPos.x)
            {
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

            if (transform.position.x < parentPos.x - minPos.x)
            {
                isDown = true;
            }
        }

        private void UpMove()
        {
            if (active && transform.position.y < parentPos.y + minPos.y)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }

            if (!active && transform.position.y > parentPos.y)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }

            if (transform.position.y > parentPos.y + minPos.y)
            {
                isDown = true;
            }
        }

        private void DownMove()
        {
            if (active && transform.position.y > parentPos.y - minPos.y)
            {
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
            }
            else
            {
                active = false;
            }
            if(transform.position.y < parentPos.y - minPos.y)
            {
                isDown = true;
            }
        }

        private void Return()
        {
            if (!active && transform.position.y < parentPos.y)
            {
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
            }
        }

        private void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                active = true;
                FR.RightRotate();
            }
        }

        private void OnCollisionStay(Collision col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                onStay = true;
            }
        }

        private void OnCollisionExit(Collision col)
        {
            if (isDown && col.gameObject.CompareTag("Player"))
            {
                isDown = false;
                active = false;
                onStay = false;
            }
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// 参考サイト https://qiita.com/KNT529/questions/6320c841ddb88f889090
    /// </summary>

    [UnityEditor.CustomEditor(typeof(ButtonSwitchMove))]
    public class EditorButtonManager : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ButtonSwitchMove manager = (ButtonSwitchMove)this.target;
            manager.select = UnityEditor.EditorGUILayout.Popup(
                "どの方向に動かしたいか選択してください。",
                manager.select,
                manager.direction
                );
        }
    }
#endif
}
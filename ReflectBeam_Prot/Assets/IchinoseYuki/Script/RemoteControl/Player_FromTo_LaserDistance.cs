// 作成日:02/22水曜日 作成者:市瀬
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// プレイヤーから一番近いレーザーの距離を求めるスクリプト
/// </summary>
public class Player_FromTo_LaserDistance : MonoBehaviour
{
    [SerializeField]
    private GameObject test;
    private GameObject player;
    private List<float> distanceToLaser;
    private float minDistance;
    private GameObject laserObject;
    private List<GameObject> lasers;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// プレイヤーから一番近いレーザーの距離を求める
    /// </summary>
    public void GimmickDistanceCalculation()
    {
        // リストの初期化
        distanceToLaser = new List<float>();
        lasers = new List<GameObject>();
        // レーザー全取得
        lasers = new List<GameObject>(GameObject.FindGameObjectsWithTag("Laser"));

        if (lasers == null) { return; }
        foreach (GameObject t in lasers)
        {
            // プレイヤーからレーザーの距離を求めてリストに追加
            distanceToLaser.Add(Vector3.SqrMagnitude(t.transform.position - player.transform.position));
        }
        // 最小値を比較
        minDistance = distanceToLaser.Min();
        laserObject = lasers[distanceToLaser.IndexOf(minDistance)];

        IMinLaserToDistance minLaserToDistance = test.GetComponent<IMinLaserToDistance>();
        Debug.Log(minLaserToDistance);

        // プレイヤーからレーザーの最小距離を返す
        test.GetComponent<IMinLaserToDistance>().MinDistanceTolaser(minDistance, laserObject);
    }
}
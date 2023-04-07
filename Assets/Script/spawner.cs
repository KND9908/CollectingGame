using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawner : MonoBehaviour
{
    [SerializeField]
    private float _interval = 3; // 生成間隔
    public float _elapsed; // 経過時間
    [SerializeField]
    private GameObject _original = null; // スポーン時に複製するゲームオブジェクト
    [SerializeField]
    private GameObject _dark = null; // お邪魔アイテム
    private GameObject item = null;

    private int randomcnt = 10;

    private Rigidbody rb;

    private float _spaninterval = 0;

    //マネージャオブジェクトの変数取得
    private GameObject Gamemanager;
    gamemanager gm;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        StartCoroutine("SpawnSpan");
        Gamemanager = GameObject.Find("GameObject");
        gm = Gamemanager.GetComponent<gamemanager>();
    }
    void Update()
    {
        if (!gm.stop)
        {
            _elapsed += Time.deltaTime;

            if (_elapsed > _interval)
            {
                //randの値が特定だった時、rareオブジェクト生成
                _elapsed = 0;
                if (Random.Range(0, randomcnt) == 1)
                {
                    item = Instantiate(_dark);
                }
                else
                {
                    item = Instantiate(_original);
                }
                item.transform.position = this.transform.position;
                //複製オブジェクトはスポナーの子オブジェクトに
                //item.transform.parent = this.transform;
                var rigidbody = item.GetComponent<Rigidbody>();
                var x = Random.Range(-10, 10);
                var y = Random.Range(0, 30);
                var z = this.gameObject.transform.position.z;
                rigidbody.AddForce(x, y, 0, ForceMode.Impulse);

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ghost"))
        {
            rb.AddForce(-100, 0, 0, ForceMode.Impulse);
        }
    }

    IEnumerator SpawnSpan()
    {
        while (true)
        {
            _spaninterval += Time.deltaTime;
            //タイムが経過していくごとにスポーンする弾の量を増やしていく（スパンを短くしていく）
            if (_spaninterval > 0.5f)
            {
                _spaninterval = 0;

                if (_interval > 0.5f)
                {
                    _interval -= 0.3f;
                    if (randomcnt > 2)
                    {
                        randomcnt--;
                    }
                }
                else
                {
                    yield break;
                }
            }

            yield return new WaitForSeconds(0.05f);
            //闇ドロップの発生確率を増加させる

            //ghostがぶつかったら、逆フィーバータイムのような現象を発生させる

            //大体10秒くらいしたら元のモードへ戻す

            //ゲームオーバーになったらブレイク
            // yield break;
        }
    }
}
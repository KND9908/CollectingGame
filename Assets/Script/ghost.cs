using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class ghost : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;

    //動きを生じさせるための感覚
    [SerializeField]
    private float _interval = 10; // 生成間隔
    private float _elapsed; // 経過時間
    private bool _isdo = false;//動作実行中か

    private Vector3 defaultpos;//初期座標への移動用
    private bool invisible = false;
    private Rigidbody rb;
    private Image image;
    private float huyuu;

    [SerializeField]
    private GameObject _dark = null; // baditem
    private GameObject item = null;

    private float bombcount = 0;

    [SerializeField]
    private int waitcount;
    //画像の大きさ調整用
    Vector3 bigsize;

    //画像の色度管理用
    private float invisispeed = 0.1f;
    private float red,green,blue,alpha;

    //マネージャオブジェクトの変数取得
    private GameObject Gamemanager;
    gamemanager gm;
    // Start is called before the first frame update
    void Start()
    {
        Gamemanager = GameObject.Find("GameObject");
        gm = Gamemanager.GetComponent<gamemanager>();

        rb = this.GetComponent<Rigidbody>();
        image = GetComponent<Image>();

        defaultpos = this.gameObject.transform.position;

        waitcount = 0;

        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alpha = GetComponent<Image>().color.a;

        bigsize = this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.stop)
        {
            //特定タイミングになったらランダムに動かす
            _elapsed += Time.deltaTime;

            if (_elapsed > _interval)
            {
                _elapsed = 0;
                if (!_isdo)
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            _isdo = true;
                            this.tag = "NoAttackGhost";
                            this.gameObject.transform.position = new Vector3(0, -100, defaultpos.z - 15);//移動準備
                            gameObject.transform.localScale = new Vector3(bigsize.x * 4, bigsize.x * 4, bigsize.z);   //画像を巨大化、描画するイラストを変更
                            StartCoroutine("BigAppear");
                            break;

                        case 1:
                            _isdo = true;
                            this.tag = "ghost";
                            StartCoroutine("DashAttack");
                            break;
                        case 2:
                            _isdo = true;
                            this.tag = "NoAttackGhost";
                            StartCoroutine("BlackBomb");
                            break;
                    }
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("syanderia")　&& this.tag != "NoAttackGhost")
        {
            //score = score + 100;
            invisible = true;
            rb.velocity = Vector3.zero;
            StartCoroutine("Transparent");
        }
    }

    IEnumerator DashAttack()
    {
        while (true) {
            rb.AddForce(-1 * speed, 0, 0, ForceMode.Force);
            if (invisible)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
    IEnumerator Transparent()
    {
        while(true)
        {
            alpha -= invisispeed;
            GetComponent<Image>().color = new Color(red, green, blue, alpha);
            if (GetComponent<Image>().color.a <= 0)
            {
                alpha = 1.0f;
                this.gameObject.transform.position = defaultpos;
                rb.velocity = Vector3.zero;
                GetComponent<Image>().color = new Color(red, green, blue, alpha);
                _isdo = false;
                invisible = false;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator BigAppear()
    {
        while(true)
        {
            rb.AddForce(0, 30, 0, ForceMode.Force);
            if (this.gameObject.transform.position.y >= -10 && waitcount < 10)
            {
                rb.velocity = Vector3.zero;
                if (Random.Range(0, 5) == 1)
                {
                    waitcount++;
                }
            }
            if (this.gameObject.transform.position.y >= 100)
            {
                rb.velocity = Vector3.zero;
                this.gameObject.transform.position = defaultpos;
                waitcount = 0;
                gameObject.transform.localScale = bigsize;//元の大きさへ
                rb.velocity = Vector3.zero;
                _isdo = false;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }

        //動きが終わったらbigappearfinをtrueにして次の処理へ移行;
    }

    //黒い弾をぱらぱら落としてくる処理　BigAppear処理の後に実行
    IEnumerator BlackBomb()
    {
        while (true)
        {
            huyuu = Mathf.Sin(Time.time * 3);
            transform.position += new Vector3(-3, huyuu, 0f);
            //スポナーとして闇ブロック投下
            bombcount += Time.deltaTime;

            if (bombcount > 0.1f)
            {
                bombcount = 0;
                item = Instantiate(_dark);
                item.transform.position = this.transform.position;
                var rigidbody = item.GetComponent<Rigidbody>();
                var z = this.gameObject.transform.position.z;
                rigidbody.AddForce(0, 1, 0, ForceMode.Impulse);
            }
            if (this.gameObject.transform.position.x <= -100)
            {
                this.gameObject.transform.position = defaultpos;
                rb.velocity = Vector3.zero;
                _isdo = false;
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    //幽霊がシャンデリアへ突撃する前の予備動作
    void kusukusughost()
    {
        //画面端からにゅっと登場

        //画像を笑っている画像へ変更＋ちょっと上下にカタカタ動く感じで

        //その後、シャンデリアへ突撃
    }
}


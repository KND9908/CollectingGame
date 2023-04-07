using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class item : MonoBehaviour
{
    //マネージャオブジェクトの変数取得
    [SerializeField]
    private GameObject Gamemanager;
    gamemanager gm;
    // Start is called before the first frame update
    void Start()
    {
        Gamemanager = GameObject.Find("GameObject");
        gm = Gamemanager.GetComponent<gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.transform.position.y <= -50)//仮定値
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && this.tag == "item")
        {
            gm.Score += 100;
            gm.lifeup(5.0f);
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player") && this.tag == "baditem")
        {
            gm.lifeup(-4.0f);
            Destroy(this.gameObject);
        }

    }
}

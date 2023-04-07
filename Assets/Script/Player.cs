using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float jumpPower = 5.0f;
    private bool jumpflag = false;
    private Rigidbody rb;

    //マネージャオブジェクトの変数取得
    private GameObject Gamemanager;
    gamemanager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Gamemanager = GameObject.Find("GameObject");
        gm = Gamemanager.GetComponent<gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gm.stop)
        KeyCommand();
    }

    void KeyCommand()
    {
        // 左右のキーの入力を取得
        float x = Input.GetAxis("Horizontal");

       
        rb.AddForce(x * speed, 0, 0, ForceMode.Force);

        if (Input.GetKey(KeyCode.Space) && !jumpflag)
        {
            rb.velocity += Vector3.up * jumpPower;
            jumpflag = true;
        }
    }

    // ★★追加
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnTriggerEnter:{collision.gameObject.name}");
       

        if (collision.gameObject.CompareTag("yuka"))
        {
            jumpflag = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("item") || other.gameObject.CompareTag("baditem"))
        {

        }
    }
}

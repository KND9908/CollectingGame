using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spawner : MonoBehaviour
{
    [SerializeField]
    private float _interval = 3; // �����Ԋu
    public float _elapsed; // �o�ߎ���
    [SerializeField]
    private GameObject _original = null; // �X�|�[�����ɕ�������Q�[���I�u�W�F�N�g
    [SerializeField]
    private GameObject _dark = null; // ���ז��A�C�e��
    private GameObject item = null;

    private int randomcnt = 10;

    private Rigidbody rb;

    private float _spaninterval = 0;

    //�}�l�[�W���I�u�W�F�N�g�̕ϐ��擾
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
                //rand�̒l�����肾�������Arare�I�u�W�F�N�g����
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
                //�����I�u�W�F�N�g�̓X�|�i�[�̎q�I�u�W�F�N�g��
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
            //�^�C�����o�߂��Ă������ƂɃX�|�[������e�̗ʂ𑝂₵�Ă����i�X�p����Z�����Ă����j
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
            //�Ńh���b�v�̔����m���𑝉�������

            //ghost���Ԃ�������A�t�t�B�[�o�[�^�C���̂悤�Ȍ��ۂ𔭐�������

            //���10�b���炢�����猳�̃��[�h�֖߂�

            //�Q�[���I�[�o�[�ɂȂ�����u���C�N
            // yield break;
        }
    }
}
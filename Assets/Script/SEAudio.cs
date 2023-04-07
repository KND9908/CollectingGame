using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SEAudio : MonoBehaviour
{
    [SerializeField]
    public AudioClip witch;
    [SerializeField]
    public AudioClip Boy;

    [SerializeField]
    private float _elapsed; // �o�ߎ���
    AudioSource audioSource;

    //�}�l�[�W���I�u�W�F�N�g�̕ϐ��擾
    private GameObject Gamemanager;
    gamemanager gm;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        Gamemanager = GameObject.Find("GameObject");
        gm = Gamemanager.GetComponent<gamemanager>();
    }

    void Update()
    {
        if (!gm.stop)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > 1.0f)
            {
                _elapsed = 0;
                if (Random.Range(0, 30) == 1)
                {
                    audioSource.clip = witch;
                    audioSource.Play();
                }
                else if (Random.Range(0, 30) == 5)
                {
                    audioSource.clip = Boy;
                    audioSource.Play();
                }
            }
        }
    }
}

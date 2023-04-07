using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class gamemanager : MonoBehaviour
{
    [SerializeField]
    public Slider lifeslider;
    [SerializeField]
    float Maxvoltage = 150f;
    float Nowvoltage = 75f;

    [SerializeField]
    private TextMeshProUGUI TxtScore;

    [SerializeField]
    private TextMeshProUGUI TxtStartFinLabel;

    [SerializeField]
    private TextMeshProUGUI TxtScoreBoardLabel;

    [SerializeField]
    GameObject ResultPanel;

    [SerializeField]
    GameObject shadow;

    public float Score = 0;

    public bool stop = true;
    public bool finishflag = false;

    public float countdowntime = 0;
    private int countdownnum = 3;
    private float red, green, blue, alpha;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CountDownStart");
        red = shadow.GetComponent<Image>().color.r;
        green = shadow.GetComponent<Image>().color.g;
        blue = shadow.GetComponent<Image>().color.b;
        alpha = shadow.GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            TxtScore.text = Score.ToString();
            Nowvoltage -= Time.deltaTime * 2;
            if (Nowvoltage <= 0)
            {
                Nowvoltage = 0.1f;
                stop = true;
                finishflag = true;
                TxtStartFinLabel.text = "FINISH!";
                TxtScoreBoardLabel.text = Score.ToString();
                StartCoroutine("Result");
            }
            lifeslider.value = Nowvoltage / Maxvoltage;
            shadow.GetComponent<Image>().color = new Color(red, green, blue, 1.0f - lifeslider.value);
        }
    }

    public void lifeup(float upsize)
    {
        if (Nowvoltage + upsize > Maxvoltage)
        {
            Nowvoltage = Maxvoltage;
        }
        else
        {
            Nowvoltage += upsize;
        }
    }
    IEnumerator CountDownStart()
    {
        while (true)
        {
            countdowntime += Time.deltaTime;
            if (countdowntime > 0.1)
            {
                countdowntime = 0;
                countdownnum--;
            }
            if (countdownnum == 0)
            {
                TxtStartFinLabel.text = "GO!";
                stop = false;
            }
            else if (countdownnum < 0)
            {
                countdowntime = 0;//Finish‚ÅÄ—˜—p
                TxtStartFinLabel.text = "";
                yield break;
            }
            else
            {
                TxtStartFinLabel.text = countdownnum.ToString();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator Result()
    {
        while (true)
        {
            countdowntime += Time.deltaTime;
            if (countdowntime > 0.2f)
            {
                countdowntime = 0.2f;
                TxtStartFinLabel.text = "";
                if (ResultPanel.transform.position.x > 3)
                {
                    ResultPanel.transform.position -= new Vector3(20, 0, 0);
                }
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}
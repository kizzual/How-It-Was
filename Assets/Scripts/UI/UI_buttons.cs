using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_buttons : MonoBehaviour
{
    [SerializeField] private Image progressBar_Image;
    [SerializeField] private Text progressBar_text;
    [SerializeField] private GameObject progress_panel;

    [SerializeField] private Slider _mSlider;
    [SerializeField] private GameObject Ready_panel;
    [SerializeField] private GameObject StartGame_Panel;

    [SerializeField] private Text LevelStartGame_Text;
    [SerializeField] private Text LevelStartGameTimer_Text;


    [SerializeField] private Inventory _inventory;
    [SerializeField] private CreateObjects _createObjects;
    [Header("FinishPanel")]
    [SerializeField] private GameObject finPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private List<Text> lvlFinish_text;
    [SerializeField] private Text winPoints;
    [SerializeField] private Text losePoints;
    [SerializeField] private List<GameObject> Tutorial_panel;
    [SerializeField] private GameObject rememberText;
    [SerializeField] private SoundPlay _soundPlay;
    [SerializeField] private GameObject Grid;
    private float ShowPoints;
    private Coroutine Timer_coroutine;
    [SerializeField] private Text timer_Text;
    [SerializeField] private GameObject timer_panel;
    [SerializeField] private GameObject pointObject;
    public bool timerOn = false;

    [Header("Finish Game")]
    [SerializeField] private GameObject finishGamePanel;
    [SerializeField] private GameObject reset_panel;
    [SerializeField] private Text points_count;

    [SerializeField] private InterAd _adMob;
    [SerializeField] private GameObject okButton;



    void Start()
    {
  /*      StartGame_Panel.SetActive(true);
        rememberText.SetActive(true);
        int leveNumber =  PlayerPrefs.GetInt("level");
        LevelStartGame_Text.text = "LVL " + leveNumber;*/
    }

    void Update()
    {
       
    }
       
    public void StartGamePanel(int lvl)
    {
        if (lvl < 4)
        {
            StartGame_Panel.SetActive(true);
            rememberText.SetActive(true);
            int leveNumber = PlayerPrefs.GetInt("level");
            LevelStartGame_Text.text = "LVL " + leveNumber;

        }
        else if (lvl >= 4)
        {
            StartGame_Panel.SetActive(false);
            Timer_coroutine = StartCoroutine(Timer());
            rememberText.SetActive(true);
            int leveNumber = PlayerPrefs.GetInt("level");
            LevelStartGameTimer_Text.text = "LVL " + leveNumber;

        }
    }
    public void StartGame()
    {
        _inventory.panel.SetActive(true);
        StartGame_Panel.SetActive(false);
        rememberText.SetActive(false);
        // Нажатие на старт Музыка
        _soundPlay.btn_tap.Play();
        _createObjects.InventoryCheck();
        _createObjects.HidePoints();
       

    }
    public void StartReadyCheck()
    {
        _inventory.panel.SetActive(true);
        StartGame_Panel.SetActive(false);
        rememberText.SetActive(false);
        // Нажатие на старт Музыка
        _soundPlay.btn_tap.Play();
        _createObjects.InventoryCheck();
        _createObjects.HidePoints();


        if (_createObjects.level == 4)
        {
            StopCoroutine(Timer_coroutine);
            timer_panel.SetActive(false);
        }

    }
    public void ReadyCheck()
    {
        _inventory.panel.SetActive(false);
        Ready_panel.SetActive(true);
        // Нажатие на старт Музыка
        _soundPlay.btn_tap.Play();
        _createObjects.ReadyButton();

    }
    public void ReadyPanel()
    {

        Ready_panel.SetActive(true);
        _inventory.panel.SetActive(false);
    }
    public void FinishResults(float check)
    {
        if (check >= 60)
        {
            Ready_panel.SetActive(false);
            progress_panel.SetActive(true);
            StartCoroutine(FinishAnimation(true));
            Debug.Log("Win");
        }
        else
        {
            Ready_panel.SetActive(false);
            progress_panel.SetActive(true);
            StartCoroutine(FinishAnimation(false));

            Debug.Log("lose");
        }
    }
 
    public void LevelResult(float percent) 
    {
        int tmp = (int)percent;
     //   progressBar_text.text = "COINCIDENCE   " + tmp.ToString();
    //    _mSlider.value = 100 - tmp;
        _inventory.panel.SetActive(false);
        ShowPoints = (percent / 100f);
        StartCoroutine(CalculatePErcent((int)_mSlider.value, tmp));

    }

    private IEnumerator FinishAnimation(bool status)
    {
        float points;

        if (PlayerPrefs.HasKey("points"))
        {
            points = PlayerPrefs.GetFloat("points");
        }
        else
        {
            points = 0;
        }


        int lvl = PlayerPrefs.GetInt("level");
       
        foreach (var tmp in lvlFinish_text)
        {
            tmp.text = lvl.ToString();
        }
        for (int i = 0; i < _inventory.Cell.Count; i++)
        {
            _inventory.Cell[i].transform.SetParent(Grid.transform);
        }
        yield return new WaitForSeconds(1f);
        
        finPanel.SetActive(true);
        // изменение поинта
        pointObject.SetActive(false);

        if (status)
        {
            _soundPlay.lvl_win.Play();
            lvl -= 1;
            foreach (var tmp in lvlFinish_text)
            {
                tmp.text = "LVL  "+ lvl.ToString();
            }
           
            losePanel.SetActive(false);
            winPanel.SetActive(true);

            var pp = Math.Round(ShowPoints, 2);

            winPoints.text = "+  " + pp.ToString(CultureInfo.InvariantCulture);

        }
        else
        {
            _soundPlay.lvl_lose.Play();

            foreach (var tmp in lvlFinish_text)
            {
                tmp.text = "LVL  " + lvl.ToString();
            }
            losePanel.SetActive(true);
            winPanel.SetActive(false);
            losePoints.text = "+  " + 0.ToString();

        }
        yield return new WaitForSeconds(1.5f);

/*        if (PlayerPrefs.HasKey("no_ads"))
        {
            Debug.Log("have");
        }
        else
        {
            _adMob.ShowAd();
            Debug.Log("!");
        }*/
        yield return new WaitForSeconds(1f);

        okButton.SetActive(true);

    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowRotationTutorсialPanel(int level)
    {
        switch (level)
        {
            case 1:
                {
                    Tutorial_panel[0].SetActive(true);
                break;
                }
            case 2:
                {
                    Tutorial_panel[1].SetActive(true);

                    break;
                }
            case 3:
                {
                    Tutorial_panel[2].SetActive(true);
                    break;
                }
        }
    }
    public void HideTutorialPanel()
    {
        _soundPlay.btn_tap.Play();

        foreach (var tmp  in Tutorial_panel)
        {
            tmp.SetActive(false);
        }
    }
    private IEnumerator CalculatePErcent(int sliderValue, int percent)
    {
        Ready_panel.SetActive(false);
        progress_panel.SetActive(true);
        _mSlider.value = 100;
        progressBar_text.text = "COINCIDENCE   " + 0.ToString() + " %";

        yield return new WaitForSeconds(1f);
        float timer = 1f / percent;
        _soundPlay.calculation.Play();
        if (percent == 0)
        {
            _mSlider.value = 100;
            progressBar_text.text = "COINCIDENCE   " + 0.ToString() +  " %";
        }
        else
        {
            for (int i = 0; i <= percent; i++)
            {
                yield return new WaitForSeconds(timer);


                _mSlider.value = 100 - i;

                progressBar_text.text = "COINCIDENCE   " + i.ToString() + " %";

            }
        }
        
        FinishResults(percent);

    }
    private IEnumerator Timer()
    {
        timer_panel.SetActive(true);
        timer_Text.text = "0:30";
        for (int i = 29; i >= 0; i-- )
        {
            yield return new WaitForSeconds(1);
            timer_Text.text = "00:" + i.ToString();
            if (i == 0)
            {
                _inventory.panel.SetActive(true);
                StartGame_Panel.SetActive(false);
                rememberText.SetActive(false);
                // Нажатие на старт Музыка
                _createObjects.InventoryCheck();
                _createObjects.HidePoints();

            }
        }

    }
    public void FinishGamePanel()
    {

        StartGame_Panel.SetActive(false);
        Ready_panel.SetActive(false);
        finishGamePanel.SetActive(true);

       var tmp = PlayerPrefs.GetFloat("points");
        var pp = Math.Round(tmp, 2);
        points_count.text = pp.ToString(CultureInfo.InvariantCulture);
    }
    public void ResetProgress_panel()
    {
        _soundPlay.btn_tap.Play();
        reset_panel.SetActive(true);

    }
    public void ResetProgress_yes()
    {
        _soundPlay.btn_tap.Play();

        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void ResetProgress_no()
    {
        _soundPlay.btn_tap.Play();
        reset_panel.SetActive(false);

    }

    public void ShareMethod()
    {
        _soundPlay.btn_tap.Play();

    }
}

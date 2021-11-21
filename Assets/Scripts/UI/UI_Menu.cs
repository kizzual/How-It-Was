using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_Menu : MonoBehaviour
{
    [SerializeField] private Text currentLvl_text;
    [SerializeField] private Text nextLvl_text;
    [SerializeField] private Text CurrentMemoryPoint_text;
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject resetPanel;
    [SerializeField] private GameObject currentPoints;
    [SerializeField] private SoundPlay _soundPlay;
    [SerializeField] private InterAd _adMob;
    void Start()
    {

      
   //     PlayerPrefs.DeleteKey("no_ads");
        int lvlNumber = PlayerPrefs.GetInt("level");
        CheckMemoryPOints();
        if (PlayerPrefs.HasKey("level"))
        {
            nextLvl_text.text = "LVL  " + lvlNumber.ToString();
            currentLvl_text.text = "LVL  " + (lvlNumber - 1).ToString();

        }
        else
        {
            nextLvl_text.text = "LVL  1" ;
            currentLvl_text.text = "LVL  0";

        }
        var pp = Math.Round(PlayerPrefs.GetFloat("points"), 2);
        CurrentMemoryPoint_text.text = pp.ToString(CultureInfo.InvariantCulture);
    }

    public void CheckMemoryPOints()
    {

        float tmp;
        if (PlayerPrefs.HasKey("points"))
        {
            tmp = PlayerPrefs.GetFloat("points");
            var pp = Math.Round(tmp, 2);
            Debug.Log(pp);
            CurrentMemoryPoint_text.text = pp.ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            tmp = 0;
            CurrentMemoryPoint_text.text = tmp.ToString(CultureInfo.InvariantCulture);
        }
        float rotate = -125;
        float tt = (250f / 100f) * tmp;
        float q = tt + rotate;
        currentPoints.transform.Rotate(new Vector3(0, 0, q));
    }
    public void ResetPanel()
    {
        _soundPlay.btn_tap.Play();
        resetPanel.SetActive(true);
        MainPanel.SetActive(false);
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

        resetPanel.SetActive(false);
        MainPanel.SetActive(true);
    }
    public void ShareMethod()
    {
        _soundPlay.btn_tap.Play();

    }
    public void NoAdsMethod()
    {
        _soundPlay.btn_tap.Play();

    }
    public void StargGame()
    {
        _soundPlay.btn_tap.Play();
        PlayerPrefs.SetInt("AdMob", 1);
        SceneManager.LoadScene(1);
    }




    public Slider _testSlider;
    public Text _testText;
    public int chang;
    public void saveChan()
    {
        PlayerPrefs.SetInt("level", chang);
        SceneManager.LoadScene(0);
    }
    public void textChang(Slider slider)
    {
        chang = (int)slider.value;
        _testText.text = chang.ToString();
    }
    private void Update()
    {
        textChang(_testSlider);
    }

}

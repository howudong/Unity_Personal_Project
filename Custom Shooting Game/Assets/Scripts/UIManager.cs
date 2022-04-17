using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text remainAmmoText;
    [SerializeField] Text magText;
    [SerializeField] Text healthText;
    [SerializeField] Text waveText;
    [SerializeField] Text remainEnemyText;
    [SerializeField] Text scoreText;
    [SerializeField] Text finalScoreText;
    [SerializeField] Text levelText;
    [SerializeField] Text damageText;
    [SerializeField] Text rpmText;
    [SerializeField] Text speedText;
    [SerializeField] Slider reloadSlider;
    [SerializeField] Slider expSlider;
    [SerializeField] Image gameOverPanel;
    [SerializeField] Image statUpBoard;
    [SerializeField] GameObject[] manualPanels;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIManager>();

            return _instance;
        }
    }
    private static UIManager _instance;


    public void UpdateAmmoText(int magAmmo, int magCapacity, int ammoRemain)
    {
        magText.text = magAmmo + " / " + magCapacity;
        remainAmmoText.text = "" + ammoRemain;
    }
    public void UpdateHealthText(float newHealth)
    {
        if (newHealth <= 0) newHealth = 0;
        healthText.text = newHealth + " / 100";
    }
    public void ActiveReloadSlider(float reloadTime)
    {
        reloadSlider.maxValue = reloadTime;
        reloadSlider.gameObject.SetActive(true);
    }
    public void UpdateWaveText(int wave)
    {
        waveText.text = "Wave : " + wave;
    }
    public void UpdateRemainEnemyText(List<EnemyHealth> enemyList ,int totalEnemy)
    {
        remainEnemyText.text = "Remain : " + enemyList.Count + "/" + totalEnemy;
    }
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score :" + score;
    }
    public void ActiveGameOverPanel(bool check)
    {
        gameOverPanel.gameObject.SetActive(check);
        finalScoreText.text = scoreText.text;
    }
    public void UpdateExpSlider(int newPoint)
    {
        float totalExp = expSlider.value + newPoint;
        if (totalExp >= expSlider.maxValue)
        {
            float remainExp = totalExp - expSlider.maxValue;
            GameManager.instance.LevelUp();
            expSlider.maxValue *= 1.5f;
            expSlider.value = remainExp;
            levelText.text = "Lv." + GameManager.instance.level;
        }
        else expSlider.value = totalExp;
    }
    public void UpdateStatText(float newDamage, float newRPM, float newSpeed)
    {
        damageText.text = "Damage : " + newDamage;
        rpmText.text = "RPM : " + newRPM;
        speedText.text = "Speed : " + newSpeed;
    }
    public void SetActiveStatUpBoard(bool active)
    {
        statUpBoard.gameObject.SetActive(active);
    }
    public void NextManual(int index)
    {
        manualPanels[index].SetActive(false);
        index = ((index + 1) == manualPanels.Length) ? 0 : index + 1;
        manualPanels[index].SetActive(true);
    }
    public void PrevManual(int index)
    {
        manualPanels[index].SetActive(false);
        index = ((index - 1) < 0) ? manualPanels.Length -1  : index - 1;
        manualPanels[index].SetActive(true);
    }
}

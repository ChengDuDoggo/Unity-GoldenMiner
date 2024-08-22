using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldMinerGamePage : MonoBehaviour
{
    public GameObject[] Levels;//所有关卡数组
    public int CountDownTime = 10;//默认10分钟倒计时(单位是分钟)
    public Text TimeText;
    public Text MoneyText;
    private Hook hook;//钩子
    private TimeSpan timeSpan;
    private int totalSeconds = 0;
    private int currentLevelIndex = 0;//当前关卡索引
    public void Start()
    {
        totalSeconds = CountDownTime * 60;
        UpdateGameTime();
        InvokeRepeating(nameof(UpdateGameTime), 1f, 1f);//每秒更新一次
        hook = GetComponentInChildren<Hook>();
        EventManager.instance.GoldMinerGetItemEvent += GetItemEvent;
        LoadLevel();
    }
    public void OnDisable()
    {
        EventManager.instance.GoldMinerGetItemEvent -= GetItemEvent;
    }
    private void LoadLevel()
    {
        foreach (GameObject level in Levels)
        {
            level.SetActive(false);
        }
        if (currentLevelIndex < Levels.Length)
        {
            Levels[currentLevelIndex].SetActive(true);
        }
        else
        {
            Debug.Log("通关啦！");
            currentLevelIndex = 0;
        }
    }
    private void UpdateGameTime()
    {
        totalSeconds--;
        if (totalSeconds < 0)
        {
            totalSeconds = 0;
            //可以在此处添加时间到达后的逻辑，比如游戏结束等
            Debug.Log("Man!~What Can I Say?Mamba Out!~");
        }
        timeSpan = TimeSpan.FromSeconds(totalSeconds);
        string timeText = $"Time:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
        TimeText.text = timeText;
    }
    private void GetItemEvent()
    {
        MoneyText.text = "Money:" + hook.TotleValue + "￥";
        if (Levels[currentLevelIndex].transform.childCount == 0)
        {
            currentLevelIndex++;
            LoadLevel();
        }
    }
}
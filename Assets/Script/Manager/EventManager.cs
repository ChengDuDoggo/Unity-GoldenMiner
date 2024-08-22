using System;

public class EventManager : Singletom<EventManager>
{
    //黄金矿工游戏中抓取物品并得到物品后触发事件
    public event Action GoldMinerGetItemEvent;
    public void CallGoldMinerGetItemEvent()
    {
        GoldMinerGetItemEvent?.Invoke();
    }
}
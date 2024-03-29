using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{
    //string RhythmState { get; set; }
    bool CanUse { get; set; }
    float coolTime { get; set; }
    float maxTime { get; set; }
    bool powerUp { get; set; }

    //인터페이스에서는 가상의 선언만 되고 초기화는 안되기때문에
    void Work(Player player);
}

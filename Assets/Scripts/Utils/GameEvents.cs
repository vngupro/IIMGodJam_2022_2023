using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents
{
    public static ScoreEvent OnAddScore = new ScoreEvent();
}

public class ScoreEvent : UnityEvent<ScoreEventData> { }
public class ScoreEventData
{
    public int baseScore;
    public int scoreMultiplier;

    public ScoreEventData(int _baseScore, int _scoreMultiplier)
    {
        baseScore = _baseScore;
        scoreMultiplier = _scoreMultiplier;
    }
}
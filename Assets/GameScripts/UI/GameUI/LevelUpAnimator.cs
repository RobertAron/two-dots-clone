﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelUpAnimator : MonoBehaviour
{
  [SerializeField] Text levelText;
  [SerializeField] Image expRaidal;
  [SerializeField] Text expText;
  [SerializeField] ParticleSystem ps;
  [SerializeField] Text finalScore;
  // Use this for initialization
  public void LevelUpAnimation(int startingLevel, int startingExp, int expGained)
  {
    StartCoroutine(LevelUpAnimationCoroutine(startingLevel, startingExp, expGained));
  }

  IEnumerator LevelUpAnimationCoroutine(int startingLevel, int startingExp, int expGained)
  {
    float expRemaining = expGained;
    float expCurrent = startingExp;
    int currentLevel = startingLevel;
    float totalExpToLevel = StaticCalcs.experienceToLevel(startingLevel);
    float accPerFrame = expGained / 300f;
    float diffPerFrame = 0;
    float totalExpSoFar = 0;
    SetEXPText(currentLevel, expCurrent, totalExpToLevel);
    yield return new WaitForSeconds(1);
    while (expRemaining > 0)
    {
      diffPerFrame += accPerFrame;
      // Level Up Conditional
      if (expCurrent == totalExpToLevel)
      {
        diffPerFrame = 0;
        currentLevel += 1;
        totalExpToLevel = StaticCalcs.experienceToLevel(currentLevel);
        ps.Play();
        expCurrent = 0;
      }
      float expTillLevelUp = totalExpToLevel - expCurrent;
      float expThisFrame = Mathf.Min(expTillLevelUp, diffPerFrame, expRemaining);
      totalExpSoFar += expThisFrame;
      expRemaining -= expThisFrame;
      expCurrent += expThisFrame;
      SetEXPText(currentLevel, expCurrent, totalExpToLevel);
      finalScore.text = "FINAL SCORE:\n" + Mathf.Floor(totalExpSoFar);
      yield return new WaitForFixedUpdate();
    }
  }

  void SetEXPText(float currentLevel, float expCurrent, float totalExpToLevel)
  {
    expText.text = Math.Floor(expCurrent) + "/" + totalExpToLevel;
    expRaidal.fillAmount = expCurrent / totalExpToLevel;
    levelText.text = currentLevel.ToString();
  }

  [ContextMenu("test burst")]
  void TestBusrt()
  {
    ps.Play();
  }
}

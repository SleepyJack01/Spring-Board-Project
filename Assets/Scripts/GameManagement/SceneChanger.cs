using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator UIAnimator;


    public void StartGame()
    {
        StartCoroutine(SceneChangeSequence("Game Level"));
    }

    public void GameOver()
    {
        StartCoroutine(SceneChangeSequence("Game Over"));
    }

    public void Victory()
    {
        StartCoroutine(SceneChangeSequence("Victory"));
    }

    private IEnumerator SceneChangeSequence(string sceneName)
    {
        UIAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator UIAnimator;


    public void GameOver()
    {
        StartCoroutine(SceneChangeSequence("Game Over"));
    }

    public void Vitory()
    {
        StartCoroutine(SceneChangeSequence("Victory"));
    }

    private IEnumerator SceneChangeSequence(string sceneName)
    {
        UIAnimator.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }
}

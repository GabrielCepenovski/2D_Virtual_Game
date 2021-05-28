using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LoadedScene : MonoBehaviour
{
    public void NextLevel(int index)
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(index);
    }
}

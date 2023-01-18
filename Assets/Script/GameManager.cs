using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver;

    public void gameOver()
    {
        _isGameOver = true;
    }

    public void Update()
    {
        var input = Input.GetKeyDown(KeyCode.R);
        if (_isGameOver && input)
        {
            SceneManager.LoadScene(0);
        }
    }
}

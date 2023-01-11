using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private int _score = 0;
    [SerializeField]
    private int _life = 3;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private TextMeshProUGUI _scoreText;
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private Image _lifeDisplay;
    [SerializeField]
    private Sprite[] _lifeSprite;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;

    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;
        _lifeDisplay.sprite = _lifeSprite[this._life];
    }

    public void incrimentScore()
    {
        this._score++;
    }

    public bool decrimentHealth(int value)
    {
        this._health = _health - value;
        if (_health < 0)
        {
            this._health = 100;
            this._life--;
        }
        if (this._life == 0)
        {
            this._health = 0;
            return false;
        }
        else
            return true;
    }
}

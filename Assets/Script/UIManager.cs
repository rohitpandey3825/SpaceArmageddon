using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Common;

public class UIManager : MonoBehaviour
{
    private bool _isDestroyed = false;
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
    private Slider slider;
    [SerializeField]
    private Sprite[] _lifeSprite;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    [SerializeField]
    private GameManager _gameManager;

    private float time = 0f;
    private float fillAmount = 0f;
    private int powerUpCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;
        StartCoroutine(FillSlider());
    }

    // Update is called once per frame
    void Update()
    {
        _scoreText.text = "Score: " + _score;
        _healthText.text = "Health: " + _health;
        _lifeDisplay.sprite = _lifeSprite[this._life];
    }

    IEnumerator gameOverCorutine()
    {
        var gameOver = true;
        while (true)
        {
            if (this._isDestroyed)
            {
                this._gameOverText.colorGradient = new VertexGradient(CommonExtension.getRandomColor(), CommonExtension.getRandomColor(), CommonExtension.getRandomColor(), CommonExtension.getRandomColor());
                this._gameOverText.gameObject.SetActive(gameOver);
                yield return new WaitForSeconds(0.5f);
                gameOver = !gameOver;
            }
            else
            {
                break;
            }
        }
    }

    private void gameOver()
    {
        this._isDestroyed = true;
        _restartText.gameObject.SetActive(true);
        StartCoroutine(gameOverCorutine());
        _gameManager.gameOver();

    }

    public void incrimentScore()
    {
        this._score++;
    }

    public int decrimentHealth(int value)
    {
        this._health = _health - value;
        if (_health <= 0)
        {
            this._health = 100;
            this._life--;
        }
        if (this._life == 0)
        {
            this._health = 0;
            this.gameOver();
        }
        return this._life;
    }

    public void enablePowerUp(PowerUp powerUps)
    {
        SetSliderColor(powerUps);
        slider.gameObject.SetActive(true);
        powerUpCount++;
    }

    public void DisablePowerUp()
    {
        powerUpCount--;
        if (powerUpCount == 0)
        {
            slider.gameObject.SetActive(false);
        }
    }

    private IEnumerator FillSlider()
    {
        while (true)
        {
            // Enable the slider
            if (time < 10f)
            {
                time += Time.deltaTime;
                fillAmount = time / 10f;
                slider.value = fillAmount;
            }
            yield return null;
        }
    }

    private void SetSliderColor(PowerUp powerUps)
    {
        Color newColor = getSliderColor(powerUps);
        this.time = 0f;
        this.fillAmount = 0f;
        Image[] images = slider.GetComponentsInChildren<Image>();
        foreach (Image image in images)
        {
            if (image.name == "Fill")
            {
                image.color = newColor;
                break;
            }
        }
    }

    private Color getSliderColor(PowerUp powerUps)
    {
        switch (powerUps)
        {
            case PowerUp.Sheild:
                return Color.blue;
            case PowerUp.Speed:
                return Color.red;
            case PowerUp.tripleShot:
                return Color.green;
        }
        return Color.white;
    }

}

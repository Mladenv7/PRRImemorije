using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{

    public const int nrRows = 3;
    public const int nrColumns = 6;
    public const float offsetX = 4.5f;
    public const float offsetY = 4f;

    [SerializeField] private Card mainCard;
    [SerializeField] private Sprite[] images;
    [SerializeField] private TextMesh scoreLabel;
    [SerializeField] private TextMesh triesLabel;
    [SerializeField] private AudioSource source;
    [SerializeField] private List<AudioClip> clips = new List<AudioClip>();

    private int _score = 0;
    private int _tries = 0;
    private Card _firstRevealed;
    private Card _secondRevealed;

    public bool canReveal
    {
        get
        {
            return _secondRevealed == null;
        }
    }

    private void SetCards()
    {
        Vector3 startPosition = mainCard.transform.position;

        int[] cardNumbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8 };
        cardNumbers = ShuffleCards(cardNumbers);

        Card card;

        for (int i = 0; i < nrColumns; i++)
        {
            for (int j = 0; j < nrRows; j++)
            {
                if (i == 0 && j == 0) card = mainCard;
                else card = Instantiate(mainCard);

                int index = j * nrColumns + i;
                int id = cardNumbers[index];
                card.ChangeSprite(id, images[id]);

                float xPos = startPosition.x + i * offsetX;
                float yPos = startPosition.y + j * offsetY;

                card.transform.position = new Vector3(xPos, yPos, startPosition.z);
            }
        }
    }

  

    // Start is called before the first frame update
    private void Start()
    {
        SetCards();
    }

    private int[] ShuffleCards(int[] cardNumbers)
    {
        int[] shuffledNumbers = cardNumbers.Clone() as int[];

        for (int i = 0; i < shuffledNumbers.Length; i++)
        {
            int temp = shuffledNumbers[i];
            int r = Random.Range(0, shuffledNumbers.Length);

            shuffledNumbers[i] = shuffledNumbers[r];
            shuffledNumbers[r] = temp;
        }

        return shuffledNumbers;
    }


    public void PlayMatchedSound()
    {
        source.clip = clips.FirstOrDefault(c => c.name == "match");
        source.Play();
    }

    public void PlayMissedSound()
    {
        source.clip = clips.FirstOrDefault(c => c.name == "missed");
        source.Play();
    }


private IEnumerator CheckCardMatchCorutine()
    {
        if (_firstRevealed.Id == _secondRevealed.Id)
        {
            _score++;
            scoreLabel.text = "Score: " + _score;
            _firstRevealed = null;
            _secondRevealed = null;
            PlayMatchedSound();
        }
        else
        {
            yield return new WaitForSeconds(0.5f); //neefikasno, jer instancira novi obj svaki poziv
            _firstRevealed.FlipDown();
            _secondRevealed.FlipDown();


            //PlayMissedSound();
            _firstRevealed = null;
            _secondRevealed = null;
        }
    }

    public void FlippedCard(Card card)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckCardMatchCorutine());
            triesLabel.text = "Tries: " + ++_tries;
        }
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

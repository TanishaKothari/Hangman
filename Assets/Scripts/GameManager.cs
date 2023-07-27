using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text wordToGuessText;
    public GameObject hangmanVisual;
    public GameObject letterButtonsGroup;
    public Text messageText;
    public Sprite[] hangmanSprites;
    public Button restartButton;

    private string[] wordsToGuess = { "ABRUPTLY","ABSURD","ABYSS","AFFIX","ASKEW","AVENUE","AWKWARD","AXIOM","AZURE","BAGPIPES","BANDWAGON","BANJO","BAYOU","BEEKEEPER","BIKINI","BLITZ","BLIZZARD","BOGGLE","BOOKWORM","BOXCAR","BOXFUL","BUCKAROO","BUFFALO","BUFFOON","BUXOM","BUZZARD","BUZZING","BUZZWORDS","CALIPH","COBWEB","COCKINESS","CROQUET","CRYPT","CURACAO","CYCLE","DAIQUIRI","DIRNDL","DISAVOW","DIZZYING","DUPLEX","DWARVES","EMBEZZLE","EQUIP","ESPIONAGE","EUOUAE","EXODUS","FAKING","FISHHOOK","FIXABLE","FJORD","FLAPJACK","FLOPPING","FLUFFINESS","FLYBY","FOXGLOVE","FRAZZLED","FRIZZLED","FUCHSIA","FUNNY","GABBY","GALAXY","GALVANIZE","GAZEBO","GIAOUR","GIZMO","GLOWWORM","GLYPH","GNARLY","GNOSTIC","GOSSIP","GROGGINESS","HAIKU","HAPHAZARD","HYPHEN","IATROGENIC","ICEBOX","INJURY","IVORY","IVY","JACKPOT","JAUNDICE","JAWBREAKER","JAYWALK","JAZZIEST","JAZZY","JELLY","JIGSAW","JINX","JIUJITSU","JOCKEY","JOGGING","JOKING","JOVIAL","JOYFUL","JUICY","JUKEBOX","JUMBO","KAYAK","KAZOO","KEYHOLE","KHAKI","KILOBYTE","KIOSK","KITSCH","KIWIFRUIT","KLUTZ","KNAPSACK","LARYNX","LENGTHS","LUCKY","LUXURY","LYMPH","MARQUIS","MATRIX","MEGAHERTZ","MICROWAVE","MNEMONIC","MYSTIFY","NAPHTHA","NIGHTCLUB","NOWADAYS","NUMBSKULL","NYMPH","ONYX","OVARY","OXIDIZE","OXYGEN","PAJAMA","PEEKABOO","PHLEGM","PIXEL","PIZAZZ","PNEUMONIA","POLKA","PSHAW","PSYCHE","PUPPY","PUZZLING","QUARTZ","QUEUE","QUIPS","QUIXOTIC","QUIZ","QUIZZES","QUORUM","RAZZMATAZZ","RHUBARB","RHYTHM","RICKSHAW","SCHNAPPS","SCRATCH","SHIV","SNAZZY","SPHINX","SPRITZ","SQUAWK","STAFF","STRENGTH","STRENGTHS","STRETCH","STRONGHOLD","STYMIED","SUBWAY","SWIVEL","SYNDROME","THRIFTLESS","THUMBSCREW","TOPAZ","TRANSCRIPT","TRANSGRESS","TRANSPLANT","TRIPHTHONG","TWELFTH","TWELFTHS","UNKNOWN","UNWORTHY","UNZIP","UPTOWN","VAPORIZE","VIXEN","VODKA","VOODOO","VORTEX","VOYEURISM","WALKWAY","WALTZ","WAVE","WAVY","WAXY","WELLSPRING","WHEEZY","WHISKEY","WHIZZING","WHOMEVER","WIMPY","WITCHCRAFT","WIZARD","WOOZY","WRISTWATCH","WYVERN","XYLOPHONE","YACHTSMAN","YIPPEE","YOKED","YOUTHFUL","YUMMY","ZEPHYR","ZIGZAG","ZIGZAGGING","ZILCH","ZIPPER","ZODIAC","ZOMBIE" };
    private string wordToGuess;
    private string guessedLetters;
    private int incorrectGuesses;

    void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        restartButton.gameObject.SetActive(false);
        letterButtonsGroup.gameObject.SetActive(true);
        wordToGuess = wordsToGuess[Random.Range(0, wordsToGuess.Length)].ToUpper();
        guessedLetters = "";
        incorrectGuesses = 0;
        UpdateWordToGuessText();
        hangmanVisual.GetComponent<SpriteRenderer>().sprite = hangmanSprites[0];
        messageText.text = "";

        Button[] buttons = letterButtonsGroup.GetComponentsInChildren<Button>();
        foreach (Button button in buttons)
        {
            button.image.color = Color.white;
        }
    }

    void UpdateWordToGuessText()
    {
        string displayedWord = "";
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            if (guessedLetters.Contains(wordToGuess[i].ToString()) || !char.IsLetter(wordToGuess[i]))
                displayedWord += wordToGuess[i];
            else
                displayedWord += "_";

            displayedWord += " ";
        }

        wordToGuessText.text = displayedWord;
    }

    void Update()
    {
        // Check for keyboard input if the game is still ongoing (not won or lost)
        if (messageText.text == "")
        {
            foreach (char letter in Input.inputString)
            {
                // Check if the character is a letter
                if (char.IsLetter(letter))
                {
                    // Convert to uppercase to match the wordToGuess format
                    char upperCaseLetter = char.ToUpper(letter);
                    CheckLetter(upperCaseLetter);
                }
            }
        }
    }

    public void CheckLetter(char letter)
    {
        if (!guessedLetters.Contains(letter.ToString()))
        {
            guessedLetters += letter;

            if (!wordToGuess.Contains(letter.ToString()))
            {
                incorrectGuesses++;

                if (incorrectGuesses <= hangmanSprites.Length)
                {
                    hangmanVisual.GetComponent<SpriteRenderer>().sprite = hangmanSprites[incorrectGuesses];
                }
            }

            Button[] buttons = letterButtonsGroup.GetComponentsInChildren<Button>();
            foreach (Button button in buttons)
            {
                if (button.name[0] == letter)
                {
                    button.image.color = new Color(0.47f, 0.48f, 0.49f);
                    break;
                }
            }

            UpdateWordToGuessText();

            if (incorrectGuesses >= 6)
            {
                messageText.text = "Game Over! You Lose.";
                messageText.color = Color.red;
                restartButton.gameObject.SetActive(true);
                letterButtonsGroup.gameObject.SetActive(false);
            }
            else if (!wordToGuessText.text.Contains("_"))
            {
                messageText.text = "You Win!";
                messageText.color = Color.green;
                restartButton.gameObject.SetActive(true);
                letterButtonsGroup.gameObject.SetActive(false);
            }
        }
    }
}
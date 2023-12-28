using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Import TextMeshPro namespace


public class DialogueManager : MonoBehaviour
{
    public Image actorImage;
    public TMP_Text actorName;
    public TMP_Text messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool IsActive = false;    


    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        currentMessages = messages;
        currentActors = actors; 
        activeMessage = 0;
        IsActive = true;
        // Debug.Log("Everything is loaded & your talking!" + messages.Length);
        DisplayMessage();
        backgroundBox.LeanScale(Vector3.one, 0.2f).setEaseInExpo()  ;
    }

    // The talking part.
    void DisplayMessage()
    {
        Message messagetoDisplay = currentMessages[activeMessage];
        messageText.text = messagetoDisplay.message; 
        Actor actorToDisplay = currentActors[messagetoDisplay.actorID];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.Sprite;
        AnimateTextColor();
    }

    public void NextMessage()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        }
        else
        {
            Debug.Log("Stopped talking");
            IsActive = false;
            backgroundBox.LeanScale(Vector3.zero, 0.2f).setEaseInExpo();
        }
    }

    void AnimateTextColor()
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);

    }


    // Start is called before the first frame update
    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsActive == true)
        {
            NextMessage();
        }
    }
}

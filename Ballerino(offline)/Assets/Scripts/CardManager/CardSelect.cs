using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSelect : MonoBehaviour
{
    public Button[] cardButtons;
    public Image[] selectedCardSlots;
    public string playerPrefsKey = "SelectedCards";
    private AbilityStrategy[] selectedCards;
    void Start()
    {
        selectedCards = new AbilityStrategy[selectedCardSlots.Length];
        LoadSelectedCards();

        foreach (Button button in cardButtons)
        {
            button.onClick.AddListener(() => OnCardSelected(button));
        }
        foreach (Image slot in selectedCardSlots)
        {
            slot.GetComponent<Button>().onClick.AddListener(() => OnSlotClicked(slot));
        }
    }

    void OnCardSelected(Button button)                 //kart butonuna tıklandığında butonu kapatır ve rengini yarı saydam yapar
    {
        CardHolder cardHolder = button.GetComponent<CardHolder>();
        if (cardHolder != null && cardHolder.ability != null)
        {
            for (int i = 0; i < selectedCardSlots.Length; i++)
            {
                if (selectedCards[i] == null)
                {
                    selectedCards[i] = cardHolder.ability;
                    selectedCardSlots[i].sprite = cardHolder.ability.cardImage;

                    button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, 0.5f);
                    button.interactable = false;

                    SaveSelectedCards();
                    return;
                }
            }
        }
    }

    void OnSlotClicked(Image slot)             //dolu slota tıklandığında o slotu boşaltır
    {
        int slotIndex = System.Array.IndexOf(selectedCardSlots, slot);

        if (slotIndex >= 0 && slotIndex < selectedCards.Length)
        {
            if (selectedCards[slotIndex] != null)
            {
                foreach (Button button in cardButtons)
                {
                    CardHolder cardHolder = button.GetComponent<CardHolder>();
                    if (cardHolder != null && cardHolder.ability == selectedCards[slotIndex])
                    {
                        button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, 1.0f);
                        button.interactable = true;
                        break;
                    }
                }

                selectedCards[slotIndex] = null;
                slot.sprite = null;
                SaveSelectedCards();
            }
        }
    }
    public void SaveSelectedCards()            //seçilen kartları kayıt eder
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (selectedCards[i] != null)
            {
                PlayerPrefs.SetString(playerPrefsKey + i, selectedCards[i].name);
            }
            else
            {
                PlayerPrefs.DeleteKey(playerPrefsKey + i);
            }
        }
    }
    void LoadSelectedCards()          //seçilen kartları boş slotlara yükler
    {
        for(int i = 0; i < selectedCards.Length;i++)
        {
            string cardName = PlayerPrefs.GetString(playerPrefsKey + i, null);
            if (!string.IsNullOrEmpty(cardName))
            {
                AbilityStrategy card = Resources.Load<AbilityStrategy>(cardName);
                if (card != null)
                {
                    selectedCards[i] = card;
                    selectedCardSlots[i].sprite = card.cardImage;
                    foreach (Button button in cardButtons)
                    {
                        CardHolder cardHolder = button.GetComponent<CardHolder>();
                        if (cardHolder != null && cardHolder.ability == card)
                        {
                            button.image.color = new Color(button.image.color.r, button.image.color.g, button.image.color.b, 0.5f);
                            button.interactable = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}


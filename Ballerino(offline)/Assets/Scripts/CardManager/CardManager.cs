using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    public Image[] cardSlots; 
    private AbilityStrategy[] selectedCards;
    public PlayerControl player;
    [SerializeField] private KeyCode key1 = KeyCode.Alpha1;
    [SerializeField] private KeyCode key2 = KeyCode.Alpha2;
    [SerializeField] private KeyCode key3 = KeyCode.Alpha3;
    public string playerPrefsKey = "SelectedCards";

    private void Start()
    {
        LoadSelectedCards();

        if (selectedCards != null)             //seçili olan kartların imagelerini slotlara yerleştirir
        {
            for (int i = 0; i < cardSlots.Length; i++)
            {
                if (i<selectedCards.Length && selectedCards[i] != null)
                {
                    cardSlots[i].sprite = selectedCards[i].cardImage;
                    cardSlots[i].color = new Color(1f, 1f, 1f, 1f);
                }
                else
                {
                    cardSlots[i].sprite = null;
                    cardSlots[i].color = new Color(1f, 1f, 1f, 0f);
                }
            }
        }
    }

    private void LoadSelectedCards()            //seçili olan kartları slotlara yerleştirir
    {
       selectedCards = new AbilityStrategy[cardSlots.Length];
       for (int i = 0; i < cardSlots.Length; i++)
       {
           string cardName = PlayerPrefs.GetString(playerPrefsKey + i,null);

           if (!string.IsNullOrEmpty(cardName))
           {
               AbilityStrategy card = Resources.Load<AbilityStrategy>(cardName);
               if (card != null)
               {
                   selectedCards[i] = card;
               }
           }
       }
    }
    void Update()
    {
        for (int i = 0; i < selectedCards.Length; i++)        //cooldown süreleri
        {
            if (selectedCards[i] != null)
            {
                selectedCards[i].UpdateCooldown();
                if (selectedCards[i].IsOnCooldown)
                {
                    cardSlots[i].color = new Color(1, 1, 1, 0.5f);
                }
                else
                {
                    cardSlots[i].color = new Color(1, 1, 1, 1);
                }
            }
        }
        if (Input.GetKeyDown(key1))
        {
            UseCard(0);
        }
        else if (Input.GetKeyDown(key2))
        {
            UseCard(1);
        }
        else if (Input.GetKeyDown(key3))
        {
            UseCard(2);
        }
    }
    void UseCard(int slotIndex)              //slotlardaki kartların kullanımı
    {
        if (selectedCards != null && slotIndex < selectedCards.Length && selectedCards[slotIndex] != null)
        {
            if (!selectedCards[slotIndex].IsOnCooldown && !selectedCards[slotIndex].IsEffectActive)
            {
                selectedCards[slotIndex].ApplyEffect(player);
                cardSlots[slotIndex].color = new Color(1, 1, 1, 0.5f);
            }
        }
    }
    public void RemoveAllEffects()          //oyunda gol olduğunda skill etkilerini kaldırır
    {
        for (int i = 0; i < selectedCards.Length; i++)
        {
            if (selectedCards[i] != null && selectedCards[i].IsEffectActive)
            {
                selectedCards[i].RemoveEffect(player);
            }
        }
    }


  
}

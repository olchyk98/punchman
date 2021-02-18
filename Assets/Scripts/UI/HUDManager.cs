using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] private UIHUDController[] myHUDControllers;
        [SerializeField] private HUDAvatar[] myAvatars;

        private TextMeshProUGUI[] myStatTexts = new TextMeshProUGUI[2];

        private void Start()
        {
            myStatTexts[0] = myHUDControllers[0].Stat.GetComponent<TextMeshProUGUI>();
            myStatTexts[1] = myHUDControllers[1].Stat.GetComponent<TextMeshProUGUI>();
        }

        private string DetermineName(string aName)
        {
            return aName.Replace("(Clone)", "");
        }

        private Sprite DeterminePlayerAvatarSprite(string aCharacterName)
        {
            return myAvatars.First(a => a.myName == aCharacterName).mySprite;
        }

        public void SetCharacters(GameObject firstPlayer, GameObject secondPlayer)
        {
            var firstName = DetermineName(firstPlayer.name);
            var secondName = DetermineName(secondPlayer.name);
            myHUDControllers[0].Name.GetComponent<TextMeshProUGUI>().text = firstName;
            myHUDControllers[1].Name.GetComponent<TextMeshProUGUI>().text = secondName;
            myHUDControllers[0].Image.GetComponent<Image>().sprite = DeterminePlayerAvatarSprite(firstName);
            myHUDControllers[1].Image.GetComponent<Image>().sprite = DeterminePlayerAvatarSprite(secondName);

        }

        public void UpdateStat(int playerIndex, string input)
        {
            myStatTexts[playerIndex-1].text = input;
        }
    }
}

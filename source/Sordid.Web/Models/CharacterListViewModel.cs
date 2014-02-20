using Sordid.Core.Model;
using System;

namespace Sordid.Web.Models
{
    public class CharacterListViewModel
    {
        public CharacterListViewModel(Character character)
        {
            Character = character;
            CalculateColors();
        }

        public Character Character { get; private set; }
        public string ForegroundColor { get; private set; }
        public string BackgroundColor { get; private set; }

        private void CalculateColors()
        {
            var hue = GetHueFromRandomHash();
            var oppositeHue = CalcOppositeHue(hue);
            ForegroundColor = string.Format("hsl({0},{1}%,{2}%)", hue, 60, 30);
            BackgroundColor = string.Format("hsl({0},{1}%,{2}%)", oppositeHue, 60, 70);
        }

        private int GetHueFromRandomHash()
        {
            var randomBytes = Convert.FromBase64String(Character.RandomHash);
            var randomInt = BitConverter.ToInt32(randomBytes, 0);
            return randomInt % 361;
        }

        private int CalcOppositeHue(int hue)
        {
            hue += 180;
            if (hue > 360)
                hue -= 360;
            return hue;
        }
    }
}
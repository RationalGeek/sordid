using Sordid.Core.Model;
using System;

namespace Sordid.Web.Models
{
    public class CharacterListViewModel
    {
        public static Random _Random = new Random();

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
            var hue = _Random.Next(0, 361); // TODO: Calculate based on hash of character id or something
            var oppositeHue = CalcOppositeHue(hue);
            ForegroundColor = string.Format("hsl({0},{1}%,{2}%)", hue, 40, 40);
            BackgroundColor = string.Format("hsl({0},{1}%,{2}%)", oppositeHue, 60, 60);
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
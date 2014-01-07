﻿using Sordid.Core.Model;
using System.Threading.Tasks;

namespace Sordid.Core.Interfaces
{
    public interface ICharacterService
    {
        Task<Character> NewCharacter();
        Task<Character> LoadCharacter(int id);
        Task<Character> SaveCharacter(Character character);
    }
}

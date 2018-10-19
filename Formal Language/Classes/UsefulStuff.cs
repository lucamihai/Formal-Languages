using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formal_Language.Classes
{
    class UsefulStuff
    {
        /// <summary>
        /// Returns the index of the first character found.
        /// </summary>
        /// <param name="String">String where the character will be sought.</param>
        /// <param name="character">Character which will be sought.</param>
        /// <returns>Index of the character if it was found, or -1 if it wasn't.</returns>
        public static int GetCharacterIndex(string String, char character)
        {
            for (int index = 0; index < String.Length; index++)
            {
                if (String[index] == character)
                {
                    return index;
                }
            }

            return -1;
        }
    }
}

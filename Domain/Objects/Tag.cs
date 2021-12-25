using System;

namespace Domain.Objects
{
    public class Tag
    {
        public string Word { get; private set; }


        public Tag(string word)
        {
            Word = word;
        }

        public static Tag Create(string word)
        {
            return new Tag(word);
        }
    }
}

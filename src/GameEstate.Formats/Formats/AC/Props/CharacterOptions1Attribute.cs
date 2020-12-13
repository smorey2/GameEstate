using System;

namespace GameEstate.Formats.AC.Props
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CharacterOptions1Attribute : Attribute
    {
        public CharacterOptions1 Option { get; }
        public CharacterOptions1Attribute(CharacterOptions1 option) => Option = option;
    }
}

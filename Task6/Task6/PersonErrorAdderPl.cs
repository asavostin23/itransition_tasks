namespace Task6
{
    public class PersonErrorAdderPl : AbstractPersonErrorAdder
    {
        public PersonErrorAdderPl(float errorLevel) : base(errorLevel)
        {
            symbols = new char[] { 'a', 'ą', 'b', 'с', 'ć', 'd', 'е', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'r', 's', 'ś', 't', 'u', 'w', 'y', 'z', 'ź', 'ż', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        }
    }
}

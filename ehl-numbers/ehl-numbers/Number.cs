namespace ehl_numbers
{
    public class Number
    {
        public int number;
        public string name;

        public override string ToString()
        {
            return $"{number} - {name}";
        }
    }
}

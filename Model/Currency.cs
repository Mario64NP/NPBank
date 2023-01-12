namespace Model
{
    public class Currency
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Currency) 
                return false;

            return ((Currency)obj).ID == ID;
        }
    }
}
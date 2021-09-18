namespace Train_Rezervation.Model
{
    public class RezervationRequest
    {
        public Train Train { get; set; }

        public int RezervationPersonNumber { get; set; }

        public bool Routingtodifferentwagon { get; set; }
    }

    public class Train
    {
        public string Name { get; set; }

        public Wagon[] Wagon { get; set; }
    }

    public class Wagon
    {
        public string Name { get; set; }

        public int Capacity { get; set; }

        public int FullSeatNumber {  get; set; }
    }


}

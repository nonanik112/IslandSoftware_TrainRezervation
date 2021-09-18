namespace Train_Rezervation.Model
{
    public class RezervationResponse
    {
        public bool ReservationAvailable { get; set; }
        public List<InformationDetailed> InformationDetailed { get; set; }
    }

    public class InformationDetailed
    {
        public string WagonName { get; set; }

        public int NumberOfPeople {  get; set; }
    }
}

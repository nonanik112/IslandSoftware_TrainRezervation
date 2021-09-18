using Train_Rezervation.Model;

namespace Train_Rezervation.Services
{
    public interface IRezervationService
    {
       Task<RezervationResponse> GetRezervation(RezervationRequest request);

        public class RezervationService : IRezervationService
        {
            public Task<RezervationResponse> GetRezervation(RezervationRequest request)
            {
                RezervationResponse response = new RezervationResponse();
                response.InformationDetailed = new List<InformationDetailed>();

                var reservationAvailableWagon = request.Train.Wagon.Where(x => x.Capacity * 7 / 10 > x.FullSeatNumber).ToList();
                if (reservationAvailableWagon != null)
                {
                    Wagon wagonwherepeoplecansettle = reservationAvailableWagon.Where(x => x.Capacity * 7 / 10 - x.FullSeatNumber >= request.RezervationPersonNumber).FirstOrDefault();

                    if (!request.Routingtodifferentwagon && wagonwherepeoplecansettle == null)
                        return Task.FromResult(response);

                    if (wagonwherepeoplecansettle != null)
                    {
                        response.ReservationAvailable = true;
                        response.InformationDetailed.Add(new InformationDetailed
                        {
                           NumberOfPeople = request.RezervationPersonNumber,
                           WagonName = wagonwherepeoplecansettle.Name
                        });
                        return Task.FromResult(response);
                    }
                    if (request.Routingtodifferentwagon && wagonwherepeoplecansettle == null)
                    {
                        return Task.FromResult(WagonController(reservationAvailableWagon, request.RezervationPersonNumber, response));

                    }
                    return Task.FromResult(response);
                }
                else
                {
                    response.ReservationAvailable = false;
                    response.InformationDetailed = new List<InformationDetailed>();
                    return Task.FromResult(response);
                }
             }

            private RezervationResponse WagonController(List<Wagon> wagons, int numberOfPeople, RezervationResponse response)
            {
                int placesTaken = 0;
                foreach (var wagon in wagons)
                {
                    int wagonseat = ( wagon.Capacity * 7 / 10)- wagon.FullSeatNumber;
                    if (numberOfPeople < wagonseat)
                    {
                        response.ReservationAvailable = true;
                        response.InformationDetailed.Add(new InformationDetailed
                        {
                            NumberOfPeople = numberOfPeople,
                            WagonName = wagon.Name
                        });
                        return response;
                    }
                    else
                    {
                        for (int i = 0; i < numberOfPeople; i++)
                        {
                            int newwagon =  wagon.FullSeatNumber++;
                            if (newwagon < wagon.Capacity * 7 / 10 )
                            {
                                placesTaken++;
                                if(i < numberOfPeople)
                                    continue;
                            }
                            if (i == numberOfPeople)
                            {
                                response.InformationDetailed.Add(new InformationDetailed
                                {
                                    NumberOfPeople = placesTaken,
                                    WagonName = wagon.Name
                                });
                                break;
                            }

                        }
                        numberOfPeople -= placesTaken;
                        placesTaken = 0;

                    }
                }
                if (numberOfPeople <= 0)
                {
                    response.ReservationAvailable = true;
                    return response;
                }
                else
                {
                    response.InformationDetailed = new List<InformationDetailed>();
                    return response;
                }
            }
        
        
         }
    }
}
using System.Collections.Generic;

namespace Academy2018_.NET_Homework5.Shared.DTOs.Json
{
    public class JsonCrewDto
    {
        public int Id { get; set; }

        public List<JsonPilotDto> Pilot { get; set; }

        public List<JsonStewardessDto> Stewardess { get; set; }
    }
}

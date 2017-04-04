using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public enum Location
    {
        OutlawCamp,
        SheriffsOffice,
        Cemetery,
        Bank,
        Saloon
    };

    public static class LocationProperties
    { 
        public static Vector2[] LocationCoords = new Vector2[Enum.GetValues(typeof(Location)).Length];
        
        public static Location GetLocation(Vector2 position)
        {
            for (int i = 0; i < LocationCoords.Length; ++i)
            {
                if (Vector2.Distance(position, LocationCoords[i]) < 1.0f)
                {
                    return (Location)i;
                }
            }

            return (Location)(-1);
        }
        public static Vector2 GetLocationCoordinates(Location location)
        {
            return LocationCoords[(int)location];
        }
    }
}

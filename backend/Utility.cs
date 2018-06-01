using System;

namespace PharmacyBackend
{
    public static class Utility
    {
        public static double haversine(double lat1, double long1, double lat2, double long2)
        {
            int radius = 6371; // Radius of the Earth in kilometers.

            // Differences between the two coordinates (converted to radians).
            double latDelta = degreesToRadians(lat2 - lat1);
            double longDelta = degreesToRadians(long2 - long1);

            double angle = Math.Sin(latDelta / 2) * Math.Sin(latDelta / 2) + Math.Cos(degreesToRadians(lat1)) * Math.Cos(degreesToRadians(lat2)) * Math.Sin(longDelta / 2) * Math.Sin(longDelta / 2);

            double distance = radius * (2 * Math.Atan2(Math.Sqrt(angle), Math.Sqrt(1 - angle)));

            return distance;
        }

        public static double degreesToRadians(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        public static double kilometersToMiles(double km)
        {
            return (km * 0.62137);
        }
    }
}
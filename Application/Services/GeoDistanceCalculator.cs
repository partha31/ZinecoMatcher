namespace ZinecoMatcher.Application.Services
{
    public static class GeoDistanceCalculator
    {
        // Calculates the distance between two geographical points using the Haversine formula.
        // This method equation implemented using the help of Github Copilot.
        public static double CalculateDistanceInMeters(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371000; // Earth's radius in meters
            double latRad1 = lat1 * Math.PI / 180.0;
            double latRad2 = lat2 * Math.PI / 180.0;
            double deltaLat = (lat2 - lat1) * Math.PI / 180.0;
            double deltaLon = (lon2 - lon1) * Math.PI / 180.0;

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(latRad1) * Math.Cos(latRad2) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }
    }
}

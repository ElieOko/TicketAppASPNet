namespace TicketApp.Services
{
    public static class CalculDuree
    {
        public static TimeSpan? CalculerDuree(string heureDebutString, string? heureFinString)
        {
            if (TimeSpan.TryParse(heureDebutString, out TimeSpan heureDebut) &&
                TimeSpan.TryParse(heureFinString, out TimeSpan heureFin))
            {
                return heureFin - heureDebut;
            }
            return null; 
        }
    }
}

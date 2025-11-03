namespace ToDoDem.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            Filterstring = filterstring ?? "all-all-all";
            string[] filters = Filterstring.Split('-');
            CategoryId = filters[0];
            Due = filters[1];
            StatusId = filters[2];
        }
        public string Filterstring { get;}
        public string CategoryId { get;}
        public string StatusId { get; }
        public string Due { get; }

        public bool HasCategory => CategoryId.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";
        public bool HasDue => Due.ToLower() != "all";

        public static Dictionary<string,string> DueValues = new Dictionary<string, string>
        {
            {"future", "Future"},
            {"today", "Today"},
            {"past", "Past"}
        };

        public bool IsPast => Due.ToLower() == "past";
        public bool IsToday => Due.ToLower() == "today";
        public bool IsFuture => Due.ToLower() == "future";
    }
}

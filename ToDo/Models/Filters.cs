namespace ToDoDem.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            filterString = filterstring ?? "all-all-all";
            string[] filters = filterString.Split('-');
            categoryId = filters[0];
            due = filters[1];
            statusId = filters[2];
        }
        public string filterString { get;}
        public string categoryId { get;}
        public string statusId { get; }
        public string due { get; }

        public bool hasCategory => categoryId.ToLower() != "all";
        public bool hasStatus => statusId.ToLower() != "all";
        public bool hasdue => due.ToLower() != "all";

        public static Dictionary<string,string> DueValues = new Dictionary<string, string>
        {
            {"future", "Future"},
            {"today", "Today"},
            {"past", "Past"}
        };

        public bool isPast => due.ToLower() == "past";
        public bool isToday => due.ToLower() == "today";
        public bool isFuture => due.ToLower() == "future";
    }
}

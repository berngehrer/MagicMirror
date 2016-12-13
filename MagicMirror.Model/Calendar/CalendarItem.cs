using System;

namespace MagicMirror.Model
{
    public class CalendarItem
    {
        public string Uid { get; set; }
        public string Summary { get; set; }
        public string Location { get; set; } 
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override string ToString() => Summary;

        public bool IsFullday()
        {
            var start = Start.ToDayStart();
            var end = Start.AddDays(1).ToDayStart();
            return Start == start && End == end;
        }
    }
}

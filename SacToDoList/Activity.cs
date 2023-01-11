/*
 * Classe per una singola attività nella to-do list. Ogni attività ha un titolo / testo,
 * una data opzionale per quando deve finire, e uno stato, che può essere non finita, in corso, finita.
 */

namespace TheToDoList {
    [Table(nameof(Activity))]
    public class Activity {
        // CAMPI
        private string title;

        // PROPRIETÀ
        [Key]
        public int ActivityId { get; init; }
        [MaxLength(128)]
        public string Title {
            get { return title; }
            set { title = IsTitleValid(value) ? value : throw new ArgumentException("Il titolo provveduto non è valido. Un titolo non può essere nullo, spazio vuoto, o più di 128 caratteri.", nameof(value)); }
        }
        public DateTime? Date { get; set; }
        [Column(TypeName = "nvarchar(12)")]
        public ActivityState State { get; set; } = ActivityState.Unfinished;

        // RELAZIONI
        public List<Tag> Tags { get; set; } = new();

        // COSTRUTTORI
        public Activity() { }

        public Activity(string title, DateTime? date = null, ActivityState state = ActivityState.Unfinished) {
            Title = title;
            Date = date;
            State = state;
        }

        public Activity(int id, string title, DateTime? date = null, ActivityState state = ActivityState.Unfinished)
            : this(title, date, state) {
            ActivityId = id;
        }

        // METODI PUBBLICI
        public static bool IsTitleValid(string title) {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 128;
        }

        public static bool IsDateValid(DateTime date) {
            return date > DateTime.Now;
        }

        public override string ToString() {
            string finalTagsString = "";
            if (Tags != null && Tags.Any()) {
                IEnumerable<string> tagsToStrings = Tags.Select(tag => $"[{tag}]");
                finalTagsString = $"\nTags: {string.Join(" ", tagsToStrings)}";
            }

            return $"[{ActivityId}] {Title}, {(Date != null ? $"{Date}, " : "")}stato: {TranslateState(State)}" +
                finalTagsString;
        }

        public static string TranslateState(ActivityState state) {
            switch (state) {
                case ActivityState.Unfinished: { return "Non finita"; }
                case ActivityState.Ongoing: { return "In corso"; }
                case ActivityState.Finished: { return "Finita"; }
            }
            return "Questa stringa non dovrebbe mai essere visibile. Contatta il developer se sei arrivato qui.";
        }

        public static bool TryParseDate(string data, out DateTime date) {
            return DateTime.TryParseExact(data, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out date)
                   && IsDateValid(date);
        }
    }

    public enum ActivityState {
        Unfinished,
        Ongoing,
        Finished
    }
}
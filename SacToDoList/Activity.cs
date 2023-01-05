/*
 * Classe per una singola attività nella to-do list. Ogni attività ha un titolo / testo,
 * una data opzionale per quando deve finire, e uno stato, che può essere non finita, in corso, finita.
 */

namespace TheToDoList {
    [Table(nameof(Activity))]
    public class Activity {
        // CAMPI
        private string title;
        private DateTime? date;
        private ActivityState state = ActivityState.Unfinished;

        // PROPRIETÀ
        [Key]
        public int ActivityId { get; init; }
        [MaxLength(128)]
        public string Title {
            get { return title; }
            set { title = isTitleValid(value) ? value : throw new ArgumentException("Il titolo provveduto non è valido. Un titolo non può essere nullo, spazio vuoto, o più di 128 caratteri.", nameof(value)); }
        }
        public DateTime? Date { get { return date; } set { date = value; } }
        [Column(TypeName = "nvarchar(12)")]
        public ActivityState State { get => state; set => state = value; }

        // COSTRUTTORI
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
        public static bool isTitleValid(string title) {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 128;
        }

        public static bool isDateValid(DateTime date) {
            return date > DateTime.Now;
        }

        public override string ToString() {
            return $"[{ActivityId}] {Title}, {Date}, stato: {TranslateState(State)}";
        }

        public static string TranslateState(ActivityState state) {
            switch (state) {
                case ActivityState.Unfinished: { return "Non finita"; }
                case ActivityState.Ongoing: { return "In corso"; }
                case ActivityState.Finished: { return "Finita"; }
            }
            return "Questa stringa non dovrebbe mai essere visibile. Contatta il developer se sei arrivato qui.";
        }
    }

    public enum ActivityState {
        Unfinished,
        Ongoing,
        Finished
    }
}
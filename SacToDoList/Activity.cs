namespace TheToDoList {
    [Table(nameof(Activity))]
    public class Activity {
        // CAMPI
        private string title;
        private DateTime? date;
        private bool finished;

        // PROPRIETÀ
        [Key]
        public int ActivityId { get; init; }
        [MaxLength(128)]
        public string Title {
            get { return title; }
            set { title = value ?? throw new ArgumentNullException(nameof(title), "Il titolo dell'attività non può essere nullo."); }
        }
        public DateTime? Date { get { return date; } set { date = value; } }
        public bool Finished { get => finished; set => finished = value; }

        // COSTRUTTORI
#pragma warning disable CS8618
        public Activity(int id, string title, DateTime? date, bool finished = false) {
            ActivityId = id;
            Title = title;
            Date = date;
            Finished = finished;
        }

        public Activity(string title, DateTime? date, bool finished = false)
        {
            Title = title;
            Date = date;
            Finished = finished;
        }
#pragma warning restore CS8618
    }


}
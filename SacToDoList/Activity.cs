namespace TheToDoList {
    public class Activity {
        // CAMPI
        private readonly int _id;
        private string _title;
        private DateTime _date;
        private bool _finished;

        // PROPRIETÀ
        public int ID { get { return _id; } init { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public bool Finished { get => _finished; set => _finished = value; }

        // COSTRUTTORI
#pragma warning disable CS8618
        public Activity(string title, DateTime date, bool finished = false) {
            ID = new Random().Next(5000);
            Title = title ?? throw new ArgumentNullException(nameof(title), "Il titolo dell'attività non può essere nullo.");
            Date = date;
            Finished = finished;
        }
#pragma warning restore CS8618
    }


}
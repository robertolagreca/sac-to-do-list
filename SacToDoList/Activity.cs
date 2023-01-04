namespace TheToDoList {
    public class Activity {
        // CAMPI
        private readonly int _id;
        private string _title;
        private DateTime _date;
        private Employee? _assignedEmployee;
        private bool _finished;

        // PROPRIETÀ
        public int ID { get { return _id; } init { _id = value; } }
        public string Title { get { return _title; } set { _title = value; } }
        public DateTime Date { get { return _date; } set { _date = value; } }
        public Employee? AssignedEmployee { get => _assignedEmployee; set => _assignedEmployee = value; }
        public bool Finished { get => _finished; set => _finished = value; }

        // COSTRUTTORI
#pragma warning disable CS8618
        public Activity(string title, DateTime date, Employee? employee, bool finished = false) {
            ID = new Random().Next(5000);
            Title = title ?? throw new ArgumentNullException(nameof(title), "Il titolo dell'attività non può essere nullo.");
            Date = date;
            AssignedEmployee = employee;
            Finished = finished;
        }
#pragma warning restore CS8618
    }


}
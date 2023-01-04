namespace TheToDoList {
    public class Employee {

        // CAMPI

        private int _id;
        private string _name;
        private string _surname;
        private string _email;

        // PROPRIETÀ
        public string Name { get { return _name; } }
        public string Surname { get { return _surname; } }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public string Email { get { return _email; } }

        // COSTRUTTORI
        public Employee(string name, string surname, string email) {
            _id = new Random().Next(500);
            _name = name ?? throw new ArgumentNullException(nameof(name), "Nome dell'impiegato non può essere nullo.");
            _surname = surname ?? throw new ArgumentNullException(nameof(surname), "Cognome dell'impiegato non può essere nullo.");
            _email = email ?? throw new ArgumentNullException(nameof(email), "Email dell'impiegato non può essere nulla.");
        }
    }
}
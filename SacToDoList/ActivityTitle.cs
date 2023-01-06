namespace TheToDoList {
    [NotMapped]
    public class ActivityTitle {
        // CAMPI
        private readonly string _value;

        // PROPRIETÀ
        public string Value { get { return _value; } init { _value = value; } }

        // COSTRUTTORI
        public ActivityTitle(string value) {
            if (!IsValid(value)) {
                throw new ArgumentException("Titolo di un'attività invalido.", nameof(value));
            }
        }

        // METODI PUBBLICI
        public bool IsValid(string value) {
            return !string.IsNullOrWhiteSpace(value) && value.Length <= 128;
        }

        public static bool TryParse(string candidate, out ActivityTitle activityTitle) {
            activityTitle = null;
            if (string.IsNullOrWhiteSpace(candidate) || candidate.Length > 128)
                return false;

            activityTitle = new ActivityTitle(candidate.Trim().ToUpper());
            return true;
        }

        public override string ToString() {
            return Value;
        }

        public override bool Equals(object obj) {
            var other = obj as ActivityTitle;
            if (other == null)
                return base.Equals(obj);

            return Equals(Value, other.Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public bool Contains(string data) {
            return Value.Contains(data);
        }

        // CONVERSIONI
        public static implicit operator string(ActivityTitle activityTitle) {
            return activityTitle.Value;
        }

        public static implicit operator ActivityTitle(string data) {
            return new ActivityTitle(data);
        }
    }
}
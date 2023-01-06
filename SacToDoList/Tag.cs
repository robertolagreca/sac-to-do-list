namespace TheToDoList {
    //Logica funzionamento sistema tags
    //Gestione attraverso tabella tags con relazione n - n con tabella activity
    //Su classe Activity è stato creato metodo per stampare
    //i campi di activity insieme al campo tag.
    //Sono stati inseriti manulmente dei tags in tabella Tag e create le relazioni nella tabella ActivityTag.
    //La build restituisce eccezione in classe Activity (riga 50) dicendo che la variabile Tags non può essere nulla.
    public class Tag {
        [Key]
        public int Id { get; set; }
        [MaxLength(128)]
        public string Text { get; set; }

        public List<Activity> activities { get; set; }

        public Tag(string text) {
            Text = text;
        }

        public override string ToString() {
            return Text;
        }

        public static implicit operator string(Tag tag) {
            return tag.Text;
        }
    }
}
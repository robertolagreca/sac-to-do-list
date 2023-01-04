// See https://aka.ms/new-console-template for more information

//All 'avvio dell'applicazione il sistema chiede all'utente cosa vuole fare:
//  (con testo, stato e data, se presente)
//2 - aggiungere una nuova attività alla lista
//3- rimuovere un'attività dalla lista
//4- modificare il testo di un'attività inserita precedentemente
//5- modificare lo stato di un'attività inserita precedentemente (se lo stato era
//impostato come "da fare", diventa "fatto" e viceversa)
//6 - aggiungere o modificare una data ad un'attività inserita precedentemente (NB:
//questa operazione si può svolgere solo su attività che sono ancora da fare)
//7 - visualizzare solo le attività ancora da fare, paginate di 3 in 3 (questa è una
//funzionalità desiderabile ma opzionale)
//0 - chiudere il programma

using TheToDoList;

List<Activity> listaDiAttivitaDiTest = new() {
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), new Employee("Thomas", "Youssef", "thomasyoussef.business@protonmail.com"), false),

    new Activity(title: "Vincere MVP nella stagione 2022/2023 dell'NBA", new DateTime(2023, 8, 10), new Employee("Artiom", "Strelnikov", "artiom.str@hotmail.it"), false),
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), new Employee("Roberto", "La Greca", "thomasyoussef.business@protonmail.com"), false),
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), new Employee("Valerio", "Federico Colombo", "thomasyoussef.business@protonmail.com"), false)
};

Console.WriteLine("APPLICAZIONE AVVIATA");
Console.WriteLine("Cosa vuoi fare? Scegli tra le seguenti opzioni.");
List<string> choices = new() {
    "Chiudi il programma",
    "Visualizza la lista delle attività",
    "Aggiungi una nuova attività alla lista",
    "Rimuovi un'attività dalla lista",
    "Modifica il testo di un'attività inserita precedentemente",
    "Modifica lo stato di un'attività inserita precedentemente",
    "Aggiungi o modifica una data ad un'attività inserita precedentemente",
    "Visualizza solo le attività ancora da fare, paginate di 3 in 3"
};

for (int i = 0; i < choices.Count; i++) {
    Console.WriteLine($"[{i}] {choices[i]}");
}

int number_choice;
while (!int.TryParse(Console.ReadLine(), out number_choice) || number_choice < 0 || number_choice >= choices.Count) {
    Console.WriteLine("Non hai inserito un numero valido, riprova");
}

switch (number_choice) {
    case 1:
        Console.Clear();
        foreach (Activity activity in listaDiAttivitaDiTest) {
            Console.WriteLine($"[{activity.ID}] {activity.Title}, {activity.Date}, {activity.AssignedEmployee.FullName}");
        }
        break;
    case 2:
        Console.Clear();

        string InputTitle;
        Console.WriteLine("Inserisca il nome dell'attività:");
        InputTitle = Console.ReadLine();

        bool DateSanification = false;
        DateTime EventDate;
        Console.Write("Inserisca la data dell'evento (gg/mm/yyyy): ");
        do
        {
            string? InputDate = Console.ReadLine();
            if (DateTime.TryParse(InputDate, out DateTime result) == false)
            {
                Console.Write("La data da lei inserita è invalida, inserisca la data nel formato (gg/dd/yyyy): ");
            }
            else
            {
                EventDate = DateTime.Parse(InputDate);
                DateSanification = true;
            }
        } while (DateSanification == false);

        
        // code block
        break;
    case 3:
        // code block
        break;
    case 4:
        // code block
        break;
    case 5:
        // code block
        break;
    case 6:
        // code block
        break;
    case 7:
        // code block
        break;
    default:
        // code block
        break;
}
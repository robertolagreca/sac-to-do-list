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

global using Microsoft.EntityFrameworkCore;
global using System.ComponentModel.DataAnnotations.Schema;
global using System.ComponentModel.DataAnnotations;
global using TheToDoList;

List<Activity> listaDiAttivitaDiTest = new() {
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), false),
    new Activity(title: "Vincere MVP nella stagione 2022/2023 dell'NBA", new DateTime(2023, 8, 10), false),
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), false),
    new Activity(title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), false)
};

Console.WriteLine("APPLICAZIONE AVVIATA");
Console.WriteLine("Cosa vuoi fare? Scegli tra le seguenti opzioni.");
Dictionary<string, Action> choices = new() {
    { "Chiudi il programma", CloseProgram },
    { "Visualizza la lista delle attività", ShowActivities},
    { "Aggiungi una nuova attività alla lista", AddActivity},
    { "Rimuovi un'attività dalla lista", RemoveActivity},
    { "Modifica il testo di un'attività inserita precedentemente", ModifyActivityTitle},
    { "Modifica lo stato di un'attività inserita precedentemente", ModifyActivityState},
    { "Aggiungi o modifica una data ad un'attività inserita precedentemente", ModifyActivityDate},
    { "Visualizza solo le attività ancora da fare, paginate di 3 in 3", ShowUnfinishedActivities}
};

for (int i = 0; i < choices.Count; i++) {
    KeyValuePair<string, Action> kvp = choices.ElementAt(i);
    Console.WriteLine($"[{i}] {kvp.Key}");
}

int number_choice;
while (!int.TryParse(Console.ReadLine(), out number_choice) || number_choice < 0 || number_choice >= choices.Count) {
    Console.WriteLine("Non hai inserito un numero valido, riprova");
}

choices.ElementAt(number_choice).Value.Invoke();

//Definizioni metodi

// METODO 0: Chiudi il programma
void CloseProgram() { Environment.Exit(0); }

// METODO 1: Stampa tutte le attività
void ShowActivities() {
    Console.Clear();
    foreach (Activity activity in listaDiAttivitaDiTest) {
        Console.WriteLine($"[{activity.ActivityId}] {activity.Title}, {activity.Date}");
    }
}

//METODO 2: Aggiunta di una nuova attività alla lista

void AddActivity() {
    Console.Clear();
    string InputTitle;
    Console.WriteLine("Inserisca il nome dell'attività:");
    InputTitle = Console.ReadLine();

    bool DateSanification = false;
    DateTime EventDate = new DateTime();
    Console.Write("Inserisca la data dell'attività (gg/mm/yyyy): ");
    do {
        string? InputDate = Console.ReadLine();
        DateTime result;
        bool success = DateTime.TryParseExact(InputDate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out result);
        if (success == false | result < DateTime.Now) {
            Console.Write("La data da lei inserita è invalida, inserisca la data nel formato (gg/dd/yyyy): ");
        }
        else {
            EventDate = DateTime.Parse(InputDate);
            DateSanification = true;
        }
    } while (DateSanification == false);

    int InputEmployee;
    Console.WriteLine("Inserisca l'id dell'impiegato a cui intende addsegnare l'attività");
    while (!int.TryParse(Console.ReadLine(), out InputEmployee) || InputEmployee < 0) {
        Console.WriteLine("Non hai inserito un numero valido, riprova");
    }

    listaDiAttivitaDiTest.Add(new Activity(title: InputTitle, date: EventDate));
}

// METODO 3: Rimuovi attività
void RemoveActivity() {

}

// METODO 4: Modifica titolo di un'attività
void ModifyActivityTitle() {

}

// METODO 5: Modifica stato dell'attività
void ModifyActivityState() {

}

// METODO 6: Modifica data dell'attività
void ModifyActivityDate() {

}

// METODO 7: Mostra attività ancora da fare, paginate di 3 in 3
void ShowUnfinishedActivities() {

}
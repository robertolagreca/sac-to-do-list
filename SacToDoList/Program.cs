// See https://aka.ms/new-console-template for more information

//Applicazione per la gestione di attività. La schermata permette all'utente 
//di scegliere varie opzioni digitando il numero corrispondente.

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
using Microsoft.EntityFrameworkCore.Diagnostics;

List<Activity> listaDiAttivitaDiTest = new() {
    new Activity(id: 1, title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), ActivityState.Finished),
    new Activity(id: 2, title: "Vincere MVP nella stagione 2022/2023 dell'NBA", new DateTime(2023, 8, 10)),
    new Activity(id: 3, title: "Giocare ad ULTRAKILL", null, ActivityState.Ongoing),
    new Activity(id: 4, title: "Far giocare Outer Wilds al resto della classe", null),
    new Activity(id: 1, title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), ActivityState.Finished),
    new Activity(id: 2, title: "Vincere MVP nella stagione 2022/2023 dell'NBA", new DateTime(2023, 8, 10)),
    new Activity(id: 3, title: "Giocare ad ULTRAKILL", null, ActivityState.Ongoing),
    new Activity(id: 4, title: "Far giocare Outer Wilds al resto della classe", null),
    new Activity(id: 1, title: "Portare fuori il cane di Gianni", new DateTime(2023, 1, 6), ActivityState.Finished),
    new Activity(id: 2, title: "Vincere MVP nella stagione 2022/2023 dell'NBA", new DateTime(2023, 8, 10)),
    new Activity(id: 3, title: "Giocare ad ULTRAKILL", null, ActivityState.Ongoing),
    new Activity(id: 4, title: "Far giocare Outer Wilds al resto della classe", null)

};

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

while (true) {
    Console.Clear();
    Console.WriteLine("Cosa vuoi fare? Scegli tra le seguenti opzioni.");

    for (int i = 0; i < choices.Count; i++) {
        KeyValuePair<string, Action> kvp = choices.ElementAt(i);
        Console.WriteLine($"[{i}] {kvp.Key}");
    }

    int number_choice;
    while (!int.TryParse(Console.ReadLine(), out number_choice) || number_choice < 0 || number_choice >= choices.Count) {
        Console.WriteLine("Non hai inserito un numero valido, riprova");
    }

    choices.ElementAt(number_choice).Value();
}

//Definizioni metodi

// METODO 0: Chiudi il programma
void CloseProgram() { Environment.Exit(0); }

// METODO 1: Stampa tutte le attività
void ShowActivities() {
    Console.Clear();
    foreach (Activity activity in listaDiAttivitaDiTest.OrderBy(a => a.Date)) {
        Console.WriteLine(activity);
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

//METODO 2: Aggiunta di una nuova attività alla lista

void AddActivity() {
    Console.Clear();

    // Prendi titolo dall'utente
    string inputTitle;
    Console.Write("Inserisca il nome dell'attività: ");
    inputTitle = Console.ReadLine();
    while (!Activity.isTitleValid(inputTitle)) {
        Console.Write("Il nome dell'attività non può essere vuoto, riprova: ");
        inputTitle = Console.ReadLine();
    }

    // Prendi data dall'utente
    Console.Write("Inserisca la data dell'attività (gg/mm/yyyy): ");
    string? inputDate = Console.ReadLine();

    DateTime eventDate;
    while (!DateTime.TryParseExact(inputDate, "d/M/yyyy", null, System.Globalization.DateTimeStyles.None, out eventDate)) {
        Console.Write("La data da lei inserita è invalida, inserisca la data nel formato (gg/dd/yyyy): ");
        inputDate = Console.ReadLine();
    }

    listaDiAttivitaDiTest.Add(new Activity(title: inputTitle, date: eventDate, state: ActivityState.Unfinished));

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 3: Rimuovi attività
void RemoveActivity() {
    Console.Clear();

    int idToRemove = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività da rimuovere: ");

    foreach (Activity activity in listaDiAttivitaDiTest) {
        if (activity.ActivityId == idToRemove) {
            listaDiAttivitaDiTest.Remove(activity);
            Console.WriteLine($"Rimossa attività \"{activity.Title}\" con ID {activity.ActivityId}");
            break;
        }
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 4: Modifica titolo di un'attività
void ModifyActivityTitle() {
    Console.Clear();

    // Prendi un Id valido
    int idToModify = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività da rimuovere: ");

    // Se c'è un'attività con 'Id, modifica il titolo
    var activityFound = listaDiAttivitaDiTest.SelectActivityById(idToModify);
    if (activityFound != null) {
        Console.Write($"Trovata un'attività \"{activityFound.Title}\", qual'è il nuovo titolo?: ");
        string newTitle = Console.ReadLine();

        while (!Activity.isTitleValid(newTitle)) {
            Console.Write($"Il nuovo titolo non è valido, riprova: ");
            newTitle = Console.ReadLine().Trim();
        }

        Console.WriteLine($"Aggiornato il titolo da \"{activityFound.Title}\" a \"{newTitle}\"");
        activityFound.Title = newTitle;
    }
    else {
        Console.WriteLine("Non sono state trovate attività con quell'Id.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 5: Modifica stato dell'attività
void ModifyActivityState() {
    Console.Clear();

    int idToChange = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui modificare lo stato: ");

    foreach (Activity activity in listaDiAttivitaDiTest) {
        if (activity.ActivityId == idToChange) {
            Console.WriteLine($"Indichi il nuovo stato dell'attività \"{activity.Title}\":");
            Console.WriteLine("[1] Non finita");
            Console.WriteLine("[2] In corso");
            Console.WriteLine("[3] Finita");
            int ChangeState;
            while (!int.TryParse(Console.ReadLine(), out ChangeState) | ChangeState < 1 | ChangeState > 3) {
                Console.Write("L'opzione scelta non è valida, riprova: ");
            }

            switch (ChangeState) {
                case 1:
                    Console.WriteLine($"L'attività \"{activity.Title}\" avrà come nuovo stato \"Non finita\"");
                    activity.State = ActivityState.Unfinished;
                    break;
                case 2:
                    Console.WriteLine($"L'attività \"{activity.Title}\" avrà come nuovo stato \"In corso\"");
                    activity.State = ActivityState.Ongoing;
                    break;
                case 3:
                    Console.WriteLine($"L'attività \"{activity.Title}\" avrà come nuovo stato \"Finita\"");
                    activity.State = ActivityState.Finished;
                    break;
            }
        }
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 6: Modifica data dell'attività
void ModifyActivityDate() {

    Console.Clear();
    int idToFind = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui modificare la data: ");

    var activityFoundDate = listaDiAttivitaDiTest.SelectActivityById(idToFind);


    if (activityFoundDate != null) {
        Console.Write($"Trovata un'attività \"{activityFoundDate.Title}\", qual'è la nuova data?: [gg/dd/yyyy] ");
        DateTime newDate = DateTime.Parse(Console.ReadLine());

        while (!Activity.isDateValid(newDate)) {
            Console.Write($"La nuova data non è valida, riprova: ");
            newDate = DateTime.Parse(Console.ReadLine());
        }

        Console.WriteLine($"Aggiornata la data di \"{activityFoundDate.Title}\" da \"{activityFoundDate.Date}\" a //\"{newDate}\"");
        activityFoundDate.Date = newDate;
    }
    else {
        Console.WriteLine("Non sono state trovate attività con quell'Id.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 7: Mostra attività ancora da fare, paginate di 3 in 3
void ShowUnfinishedActivities() {
    var unfinishedActivities = listaDiAttivitaDiTest.SelectUnfinishedActivities()
                                                    .OrderBy(a => a.Date);
    int count = 0;
    foreach (var activity in unfinishedActivities) {
        Console.WriteLine(activity);
        count++;
        if (count % 3 == 0) { Console.WriteLine(Environment.NewLine); }
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}
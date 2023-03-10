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

Dictionary<string, Action> choices = new() {
    { "Chiudi il programma", CloseProgram },
    { "Visualizza la lista delle attività", ShowActivities},
    { "Aggiungi una nuova attività alla lista", AddActivity},
    { "Rimuovi un'attività dalla lista", RemoveActivity},
    { "Modifica il testo di un'attività inserita precedentemente", ModifyActivityTitle},
    { "Modifica lo stato di un'attività inserita precedentemente", ModifyActivityState},
    { "Aggiungi o modifica una data ad un'attività inserita precedentemente", ModifyActivityDate},
    { "Visualizza solo le attività ancora da fare, paginate di 3 in 3", ShowUnfinishedActivities},
    { "Mostra tags nel sistema", ShowTags },
    { "Aggiungi una tag al sistema", AddTag },
    { "Rimuovi una tag dal sistema", RemoveTag },
    { "Modifica una tag nel sistema", ModifyTag }
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

// TODO: Tags
// TODO: Impiegato assegnato ad attività
// TODO: Mostra tutte le tag nel database
// TODO: Sistema di logs
// METODO 1: Stampa tutte le attività
void ShowActivities() {
    using var db = new ToDoListContext();

    IEnumerable<Activity> activities = null;

    bool validChoice = false;
    while (validChoice == false) {
        Console.Clear();
        Console.WriteLine("[0] Annulla visualizzazione\n" +
                          "[1] Tutte le attività\n" +
                          "[2] Per titolo\n" +
                          "[3] Per data\n" +
                          "[4] Per stato");
        char choice = Convert.ToChar(Console.ReadLine());
        switch (choice) {
            case '0': { return; }
            case '1': {
                    activities = db.Activities;
                    validChoice = true;
                    break;
                }
            case '2': {
                    string inputTitle = ActivitySelector.ValidTitleFromInput("Inserisci il titolo con cui cercare: ");
                    activities = db.Activities.SelectActivitiesByTitle(inputTitle);
                    validChoice = true;
                    break;
                }
            case '3': {
                    DateTime inputDate = ActivitySelector.ValidDateTimeFromInput("Inserisci la data con cui cercare: ");
                    activities = db.Activities.SelectActivitiesByDate(inputDate);
                    validChoice = true;
                    break;
                }
            case '4': {
                    ActivityState inputState = ActivitySelector.ValidStateFromInput("Inserisci uno stato con cui cercare: ");
                    activities = db.Activities.SelectActivitiesByState(inputState);
                    validChoice = true;
                    break;
                }
            default: {
                    Console.WriteLine("La scelta data non è valida. Riprova.");
                    Console.ReadLine();
                    break;
                }
        }


    }

    Console.Clear();
    if (activities.Any()) {
        foreach (Activity activity in activities.OrderBy(a => a.Date)) {
            db.Entry(activity).Collection(a => a.Tags).Load();
            // Devi dire al database di, data un'attività, caricarne la collezione di tags
            Console.WriteLine(activity);
        }
    }
    else {
        Console.WriteLine("Non sono state trovate attività che soddisfano i criteri di ricercha.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}


//METODO 2: Aggiunta di una nuova attività alla lista

void AddActivity() {
    Console.Clear();

    // Prendi titolo dall'utente
    string inputTitle = ActivitySelector.ValidTitleFromInput("Inserisca il nome dell'attività: ");

    // Prendi data dall'utente
    DateTime inputDate = ActivitySelector.ValidDateTimeFromInput("Inserisca la data dell'attività (gg/mm/yyyy): ");

    using var db = new ToDoListContext();
    db.Activities.Add(new Activity(title: inputTitle, date: inputDate, state: ActivityState.Unfinished));
    Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");


    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 3: Rimuovi attività
void RemoveActivity() {
    using var db = new ToDoListContext();
    Console.Clear();

    int idToRemove = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività da rimuovere: ");

    foreach (Activity activity in db.Activities) {
        if (activity.ActivityId == idToRemove) {
            db.Activities.Remove(activity);
            Console.WriteLine($"Rimossa attività \"{activity.Title}\" con ID {activity.ActivityId}");
            break;
        }
    }

    Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 4: Modifica titolo di un'attività
void ModifyActivityTitle() {
    Console.Clear();
    using var db = new ToDoListContext();

    // Prendi un Id valido
    int idToModify = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui modificare il titolo: ");

    // Se c'è un'attività con 'Id, modifica il titolo
    var activityFound = db.Activities.SelectActivityById(idToModify);
    if (activityFound != null) {
        Console.Write($"Trovata un'attività \"{activityFound.Title}\", qual'è il nuovo titolo?: ");
        string newTitle = Console.ReadLine();

        while (!Activity.IsTitleValid(newTitle)) {
            Console.Write($"Il nuovo titolo non è valido, riprova: ");
            newTitle = Console.ReadLine().Trim();
        }

        Console.WriteLine($"Aggiornato il titolo da \"{activityFound.Title}\" a \"{newTitle}\"");
        activityFound.Title = newTitle;
        Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");
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
    using var db = new ToDoListContext();

    int idToChange = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui modificare lo stato: ");

    Activity? activity = db.Activities.SelectActivityById(idToChange);

    if (activity != null) {
        ActivityState newState = ActivitySelector.ValidStateFromInput($"Indichi il nuovo stato dell'attività \"{activity.Title}\": ");
        Console.WriteLine($"L'attività \"{activity.Title}\" avrà come nuovo stato \"{newState}\"");
        activity.State = newState;
        Console.WriteLine($"Apportato\\i {db.SaveChanges()} cambiamento\\i al database");
    }
    else {
        Console.WriteLine("Non è stata trovata nessuna attività con quell'Id.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 6: Modifica data dell'attività
void ModifyActivityDate() {

    Console.Clear();
    int idToFind = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui modificare la data: ");

    using var db = new ToDoListContext();
    var activityFound = db.Activities.SelectActivityById(idToFind);


    if (activityFound != null) {
        DateTime newDate = ActivitySelector.ValidDateTimeFromInput(prompt: $"Trovata un'attività \"{activityFound.Title}\", qual'è la nuova data?: [gg/dd/yyyy] ");

        Console.WriteLine($"Aggiornata la data di \"{activityFound.Title}\" da \"{activityFound.Date}\" a \"{newDate}\"");
        activityFound.Date = newDate;
        Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");
    }
    else {
        Console.WriteLine("Non sono state trovate attività con quell'Id.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 7: Mostra attività ancora da fare, paginate di 3 in 3
void ShowUnfinishedActivities() {
    Console.Clear();
    Console.WriteLine("Ecco le attività ancora da svolgere: ");
    using var db = new ToDoListContext();

    var unfinishedActivities = db.Activities.SelectUnfinishedActivities()
                                                    .OrderBy(a => a.Date);
    int count = 0;
    foreach (var activity in unfinishedActivities) {
        Console.WriteLine(activity);
        count++;
        if (count % 3 == 0) { Console.WriteLine(Environment.NewLine); }
    }

    if (count == 0) { Console.WriteLine("Tutte le attività sono state completate!"); }
    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 8: Mostra le tags nel database
void ShowTags() {
    Console.Clear();

    using ToDoListContext db = new();
    IEnumerable<Tag> tags = db.Tags.OrderBy(t => t.Id);

    foreach (Tag tag in tags) {
        Console.WriteLine($"[{tag.Id}] {tag}");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}

// METODO 9: Aggiungi una tag al database
void AddTag() {
    Console.Clear();
    using var db = new ToDoListContext();

    int idToAddTag = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id dell'attività a cui aggiungere un tag: ");

    Activity? ActivityToAddTag = db.Activities.SelectActivityById(idToAddTag);

    if (ActivityToAddTag != null) {
        Console.Write("Inserisci la tag da inserire: ");
        Tag newTag = Console.ReadLine().Trim();
        while (!Tag.IsTitleValid(newTag)) {
            Console.Write($"Il nuovo tag non è valido, riprova: ");
            newTag = Console.ReadLine().Trim();
        }
        Console.WriteLine($"Il nuovo tag \"{newTag}\" è stato aggiunto all'attività \"{ActivityToAddTag.Title}\".");
        ActivityToAddTag.Tags.Add(newTag);
        Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");
        
        Console.WriteLine("Premi invio per continuare...");
        Console.ReadLine();
    }
    else {
        Console.WriteLine("Non è stata trovata nessuna attività con quell'Id.");
    }
}

// METODO 10: Rimuovi una tag dal database
void RemoveTag() {
    Console.Clear();
    using var db = new ToDoListContext();

    int idTagToRemove = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id del tag da rimuovere: ");

    foreach (Tag tag in db.Tags) {
        if (tag.Id == idTagToRemove) {
            db.Tags.Remove(tag);
            Console.WriteLine($"Rimosso tag \"{tag.Text}\" con id {tag.Id}");
            break;
        }
    }

    Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();
}


// METODO 11: Modifica una tag nel database
void ModifyTag() {
    Console.Clear();
    using var db = new ToDoListContext();

    // Prendi un Id valido
    int idTagToModify = ActivitySelector.ValidIdFromInput(prompt: "Inserisci l'Id del Tag da modificare: ");

    // Se c'è un'attività con 'Id, modifica il titolo
    var tagFound = db.Tags.SelectTagById(idTagToModify);
    if (tagFound != null)
    {
        Console.Write($"Trovato un tag \"{tagFound.Id}\", inserisci nuovo nome per tag: ");
        string newTag = Console.ReadLine();

        while (!Tag.IsTitleValid(newTag))
        {
            Console.Write($"Il nuovo tag non è valido, riprova: ");
           newTag = Console.ReadLine().Trim();
        }

        Console.WriteLine($"Aggiornato il tag da \"{tagFound.Text}\" a \"{newTag}\"");
        tagFound.Text = newTag;
        Console.WriteLine($"Apportati {db.SaveChanges()} cambiamento\\i al database");
    }
    else
    {
        Console.WriteLine("Non sono state trovate attività con quell'Id.");
    }

    Console.WriteLine("Premi invio per continuare...");
    Console.ReadLine();

}
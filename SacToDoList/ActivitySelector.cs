// Classe responsabile per il selezionare delle attività che seguono un criterio da una collezione

namespace TheToDoList {
    public static class ActivitySelector {
        public static Activity? SelectActivityById(this IEnumerable<Activity> activities, int id) {
            IEnumerable<Activity> activitiesFound = activities.Where(act => act.ActivityId == id);
            return activitiesFound.Any() ? activitiesFound.First() : null;
        }

        public static IEnumerable<Activity> SelectActivitiesByDate(this IEnumerable<Activity> activities, DateTime date) {
            return activities.Where(a => a.Date == date);
        }

        public static IEnumerable<Activity> SelectActivitiesByTitle(this IEnumerable<Activity> activities, string titleToSearch) {
            return activities.Where(a => a.Title.Contains(titleToSearch));
        }

        public static IEnumerable<Activity> SelectActivitiesByState(this IEnumerable<Activity> activities, ActivityState state) {
            return activities.Where(a => a.State == state);
        }

        public static int ValidIdFromInput() {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id)) {
                Console.Write("L'id che hai inserito non è un numero valido, riprova: ");
            }
            return id;
        }

        public static int ValidIdFromInput(string prompt) {
            Console.Write(prompt);
            return ValidIdFromInput();
        }

        public static string ValidTitleFromInput() {
            string inputTitle;
            inputTitle = Console.ReadLine();
            while (!Activity.IsTitleValid(inputTitle)) {
                Console.Write("Il nome dell'attività non può essere vuoto, riprova: ");
                inputTitle = Console.ReadLine();
            }

            return inputTitle;
        }

        public static string ValidTitleFromInput(string prompt) {
            Console.Write(prompt);
            return ValidTitleFromInput();
        }

        public static DateTime ValidDateTimeFromInput() {
            string? inputDate = Console.ReadLine();

            DateTime eventDate;
            while (!Activity.TryParseDate(inputDate, out eventDate)) {
                Console.Write("La data da lei inserita è invalida, inserisca la data nel formato (gg/dd/yyyy): ");
                inputDate = Console.ReadLine();
            }

            return eventDate;
        }

        public static DateTime ValidDateTimeFromInput(string prompt) {
            Console.Write(prompt);
            return ValidDateTimeFromInput();
        }

        public static ActivityState ValidStateFromInput() {
            Console.WriteLine("\n[1] Non finita");
            Console.WriteLine("[2] In corso");
            Console.WriteLine("[3] Finita");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) | choice < 1 | choice > 3) {
                Console.Write("L'opzione scelta non è valida, riprova: ");
            }

            return (ActivityState)choice - 1;
        }

        public static ActivityState ValidStateFromInput(string prompt) {
            Console.WriteLine(prompt);
            return ValidStateFromInput();
        }

        public static IEnumerable<Activity> SelectUnfinishedActivities(this IEnumerable<Activity> activities) {
            return activities.Where(activity => activity.State != ActivityState.Finished);
        }
    }
}
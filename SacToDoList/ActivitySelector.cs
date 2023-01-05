// Classe responsabile per il selezionare delle attività che seguono un criterio da una collezione

namespace TheToDoList {
    public static class ActivitySelector {
        public static Activity? SelectActivityById(this IEnumerable<Activity> activities, int id) {
            IEnumerable<Activity> activitiesFound = activities.Where(act => act.ActivityId == id);
            if (activitiesFound.Any()) {
                return activitiesFound.First();
            } else {
                return null;
            }
        }

        public static int ValidIdFromInput() {
            int id;
            while (!int.TryParse(Console.ReadLine(), out id)) {
                Console.Write("L'id che hai inserito non è un numero valido, riprova: ");
            }
            return id;
        }

        public static int ValidIdFromInput(string prompt)
        {
            Console.Write(prompt);
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("L'id che hai inserito non è un numero valido, riprova: ");
            }
            return id;
        }

        public static IEnumerable<Activity> SelectUnfinishedActivities(this IEnumerable<Activity> activities) {
            return activities.Where(activity => activity.State != ActivityState.Finished);
        }
    }
}
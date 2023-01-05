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
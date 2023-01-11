namespace TheToDoList {
    public static class TagSelector {
        public static Tag? SelectTagById(this IEnumerable<Tag> tags, int id) {
            return tags.Where(t => t.Id == id).FirstOrDefault();
        }

        public static IEnumerable<Tag> SelectTagsByText(this IEnumerable<Tag> tags, string text) {
            return tags.Where(t => t.Text.Contains(text));
        }

    }
}
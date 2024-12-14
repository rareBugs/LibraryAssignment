using Domain.Models;

namespace Database.Databases
{
    public class FakeDatabase
    {
        public List<Book> Books { get; private set; } = new List<Book>();
        public List<Author> Authors { get; private set; } = new List<Author>();

        private static List<User> allUsers = new()
        {
            new User { Id = Guid.NewGuid(), UserName = "admin", Password = "admin" },
            new User { Id = Guid.NewGuid(), UserName = "normie" }
        };

        public List<User> Users
        {
            get => allUsers;
            set => allUsers = value;
        }

        public FakeDatabase()
        {
            Books.AddRange(new List<Book>
            {
                new Book("Hunger", "Hunger (Norwegian: Sult) is a novel by the Norwegian author Knut Hamsun. Published in 1890, it is a psychological drama set in Kristiania (modern-day Oslo)."),
                new Book("Kristin Lavransdatter", "Kristin Lavransdatter is a trilogy by Sigrid Undset about life in medieval Norway. It was published between 1920 and 1922, earning Undset the Nobel Prize in Literature in 1928."),
                new Book("The Ice Palace", "The Ice Palace (Norwegian: Is-slottet) is a novel by Tarjei Vesaas. It is a story of friendship and grief, centered around two girls living in rural Norway."),
                new Book("Naïve. Super.", "Naïve. Super. is a novel by Erlend Loe, originally published in 1996. It follows a young man's existential journey as he seeks meaning in life."),
                new Book("Out Stealing Horses", "Out Stealing Horses (Norwegian: Ut og stjæle hester) is a novel by Per Petterson. The story is set in Norway and explores themes of loss and memory."),
                new Book("Sophie's World", "Sophie's World (Norwegian: Sofies verden) is a novel by Jostein Gaarder. It introduces readers to philosophy through the story of a young girl and her correspondence with a mysterious teacher."),
                new Book("Beatles", "Beatles is a novel by Lars Saabye Christensen. It follows the lives of four teenage boys in Oslo during the 1960s and 1970s, named after members of The Beatles.")
            });

            Authors.AddRange(new List<Author>
            {
                new Author("Knut", "Hamsun"),
                new Author("Sigrid", "Undset"),
                new Author("Tarjei", "Vesaas"),
                new Author("Erlend", "Loe"),
                new Author("Per", "Petterson"),
                new Author("Jostein", "Gaarder"),
                new Author("Lars Saabye", "Christensen")
            });
        }
    }
}



//using Domain.Models;

//namespace Database.Databases
//{
//    public class FakeDatabase
//    {
//        public List<Book> Books = new List<Book>();
//        public List<Author> Authors = new List<Author>();


//        public FakeDatabase()
//        {
//            Books.AddRange(new List<Book>
//        {
//            new Book("Hunger", "Hunger (Norwegian: Sult) is a novel by the Norwegian author Knut Hamsun. Published in 1890, it is a psychological drama set in Kristiania (modern-day Oslo)."),
//            new Book("Kristin Lavransdatter", "Kristin Lavransdatter is a trilogy by Sigrid Undset about life in medieval Norway. It was published between 1920 and 1922, earning Undset the Nobel Prize in Literature in 1928."),
//            new Book("The Ice Palace", "The Ice Palace (Norwegian: Is-slottet) is a novel by Tarjei Vesaas. It is a story of friendship and grief, centered around two girls living in rural Norway."),
//            new Book("Naïve. Super.", "Naïve. Super. is a novel by Erlend Loe, originally published in 1996. It follows a young man's existential journey as he seeks meaning in life."),
//            new Book("Out Stealing Horses", "Out Stealing Horses (Norwegian: Ut og stjæle hester) is a novel by Per Petterson. The story is set in Norway and explores themes of loss and memory."),
//            new Book("Sophie's World", "Sophie's World (Norwegian: Sofies verden) is a novel by Jostein Gaarder. It introduces readers to philosophy through the story of a young girl and her correspondence with a mysterious teacher."),
//            new Book("Beatles", "Beatles is a novel by Lars Saabye Christensen. It follows the lives of four teenage boys in Oslo during the 1960s and 1970s, named after members of The Beatles.")
//        });

//            Authors.AddRange(new List<Author>
//        {
//            new Author("Knut", "Hamsun"),
//            new Author("Sigrid", "Undset"),
//            new Author("Tarjei", "Vesaas"),
//            new Author("Erlend", "Loe"),
//            new Author("Per", "Petterson"),
//            new Author("Jostein", "Gaarder"),
//            new Author("Lars Saabye", "Christensen")
//        });
//        }

//        public List<User> Users
//        {
//            get
//            {
//                return allUsers;
//            }
//            set
//            {
//                allUsers = value;
//            }
//        }
//        private static List<User> allUsers = new(){
//            new User{Id = Guid.NewGuid(), UserName = "admin"},
//            new User{Id = Guid.NewGuid(), UserName = "user"}
//        };
//    }
//}
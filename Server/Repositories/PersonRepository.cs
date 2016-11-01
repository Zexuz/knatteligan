namespace knatteligan.Repositories
{
    public class PersonRepository
    {
        private static PersonRepository _instance;

        internal static PersonRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonRepository();
                }
                return _instance;
            }
        }

    }
}
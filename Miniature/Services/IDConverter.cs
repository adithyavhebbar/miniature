namespace Miniature.Services
{
    public class IDConverter
    {

        private static IDConverter? INSTANCE = null;

        private static Dictionary<char, long> charToIndexTable = new();
        private static List<char> indexToCharTable = new();
        private IDConverter()
        {
            InitializeCharToIndexTable();
            InitiailizeIndexToCharTable();
        }

        private void InitializeCharToIndexTable()
        {
            for (int i = 0; i < 26; i++)
            {
                char c = 'a';

                c = (char)(c + i);
                charToIndexTable.Add(c, i);
            }

            for (int i = 26; i < 52; i++)
            {
                char c = 'A';

                c = (char)(c + (i - 26));
                charToIndexTable.Add(c, i);
            }

            for (int i = 52; i < 62; i++)
            {
                char c = '0';

                c = (char)(c + (i - 52));
                charToIndexTable.Add(c, i);
            }
        }

        private void InitiailizeIndexToCharTable()
        {
            for (int i = 0; i < 26; i++)
            {
                char c = 'a';

                c = (char)(c + i);
                indexToCharTable.Add(c);
            }

            for (int i = 0; i < 26; i++)
            {
                char c = 'A';

                c = (char)(c + i);
                indexToCharTable.Add(c);
            }

            for (int i = 0; i < 10; i++)
            {
                char c = '0';

                c = (char)(c + i);
                indexToCharTable.Add(c);
            }
        }

        public static IDConverter GetInstance()
        {
            INSTANCE ??= new IDConverter();
            return INSTANCE;
        }

        public List<char> GetIndexToCharTable()
        {
            return indexToCharTable;
        }

        public Dictionary<char, long> GetCharToIndexTable()
        {
            return charToIndexTable;
        }
    }
}

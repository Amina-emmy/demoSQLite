using SQLite;

namespace demoSQLite
{
    public class Infos
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string Name { get; set; }
        public string Feeling { get; set; }
    }
}
using SQLite;

namespace Suma6657336
{
    [Table("resultado")]
    public class Resutado
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("numero1")]
        public string? Numero1 { get; set; }
        [Column("numero2")]
        public string? Numero2 { get; set; }
        [Column("suma")]
        public string? Suma {  get; set; }
    } 
}
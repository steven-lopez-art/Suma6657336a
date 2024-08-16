using SQLite;

namespace Suma6657336
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_suma.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Resutado>();
        }

        public async Task<List<Resutado>> GetResutado()
        {
            return await _connection.Table<Resutado>().ToListAsync();
        }

        public async Task<Resutado> GetById(int id)
        {
            return await _connection.Table<Resutado>().Where(x=>x.Id== id).FirstOrDefaultAsync();
        }

        public async Task Create(Resutado resutado)
        {
            await _connection.InsertAsync(resutado);
        }
        public async Task Update(Resutado resutado)
        {
            await _connection.UpdateAsync(resutado);
        }
        public async Task Delete(Resutado resutado)
        {
            await _connection.DeleteAsync(resutado);
        }
    }
}
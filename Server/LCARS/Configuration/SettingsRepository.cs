using System.Diagnostics.CodeAnalysis;

namespace LCARS.Configuration
{
    [ExcludeFromCodeCoverage]
    public class SettingsRepository : BaseTableStorageRepository<SettingsEntity>, IBaseTableStorageRepository<SettingsEntity>
    {
        private const string TableName = "LCARS";

        public SettingsRepository(IConfiguration config) : base(config, TableName)
        {
        }
    }
}

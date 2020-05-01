using Cms.DisplaySettingsService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cms.DisplaySettingsService.Data.Repositories
{
    internal sealed class AccentsRepository : ReadableDisplaySettingRepositoryBase<Accent>
    {
        public AccentsRepository(DbSet<Accent> accentsDb) : base(accentsDb)
        {
        }
    }
}

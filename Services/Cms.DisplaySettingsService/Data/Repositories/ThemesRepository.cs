using Cms.DisplaySettingsService.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cms.DisplaySettingsService.Data.Repositories
{
    internal sealed class ThemesRepository : ReadableDisplaySettingRepositoryBase<Theme>
    {
        public ThemesRepository(DbSet<Theme> themesDb) : base(themesDb)
        {
        }
    }
}

using System.Data.Entity;

namespace Sordid.Core.Model.ModelBuilders
{
    public class TemplateModelBuilder : IModelBuilder
    {
        public void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Character>()
                .HasOptional(c => c.Template)
                .WithMany()
                .HasForeignKey(c => c.TemplateId);
        }
    }
}

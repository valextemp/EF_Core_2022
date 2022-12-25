using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace _010_EF_Core_ConfigDataModel
{
    public class CompanyConfiguration:IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Enterprises").Property(c => c.Name).IsRequired();
            
        }
    }
}

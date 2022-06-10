using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("User"); // nome da tabela
            builder.HasKey(u => u.Id); // chave primaria como Id

            builder.HasIndex(u => u.Email) // criando um index para o email
                .IsUnique(); // email unico (não permite repetição)

            builder.Property(u => u.Name) // propriedades de name
                .IsRequired() // obrigatorio
                .HasMaxLength(60); // tamanho maximo

            builder.Property(u => u.Email) // propriedades de email
                .HasMaxLength(100); // tamanho maximo


        }
    }
}

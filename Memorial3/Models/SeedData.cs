using Memorial3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Memorial3.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Memorial3Context(serviceProvider.GetRequiredService<DbContextOptions<Memorial3Context>>()))
            {
                // Verifica se já existem registros no banco
                if (context.Memorial.Any())
                {
                    return; // O banco já contém dados
                }

                // Adiciona os registros de exemplo
                context.Memorial.AddRange(
                    new Memorial
                    {
                        Name = "João da Silva",
                        Historia = "João foi um homem bondoso que dedicou sua vida à família e à comunidade.",
                        DataNascimento = DateTime.Parse("1940-4-15"),
                        DataFalecimento = DateTime.Parse("2020-7-20"),
                        Formacao = "Engenharia Civil",
                        Religiao = "Católica",
                        Hobbies = "Pesca, Jardinagem",
                        MemorialPictureURL = "https://static.ndmais.com.br/2021/12/willsmith-web-1110x739-1.jpg",
                        Coletivo = "Comunidade local"
                    },

                    new Memorial
                    {
                        Name = "Maria de Souza",
                        Historia = "Maria era uma professora dedicada e amada por todos os seus alunos.",
                        DataNascimento = DateTime.Parse("1955-10-30"),
                        DataFalecimento = DateTime.Parse("2018-3-15"),
                        Formacao = "Pedagogia",
                        Religiao = "Evangélica",
                        Hobbies = "Leitura, Culinária",
                        MemorialPictureURL = "https://static.ndmais.com.br/2021/12/willsmith-web-1110x739-1.jpg",
                        Coletivo = "Escola Estadual"
                    },

                    new Memorial
                    {
                        Name = "Carlos Pereira",
                        Historia = "Carlos era conhecido por seu espírito aventureiro e amor pela natureza.",
                        DataNascimento = DateTime.Parse("1965-6-10"),
                        DataFalecimento = DateTime.Parse("2019-11-25"),
                        Formacao = "Biologia",
                        Religiao = "Agnóstico",
                        Hobbies = "Escalada, Fotografia",
                        MemorialPictureURL = "https://static.ndmais.com.br/2021/12/willsmith-web-1110x739-1.jpg",
                        Coletivo = "Clube de Montanhismo"
                    },

                    new Memorial
                    {
                        Name = "Ana Oliveira",
                        Historia = "Ana dedicou sua vida ao trabalho voluntário e ajuda aos necessitados.",
                        DataNascimento = DateTime.Parse("1970-2-20"),
                        DataFalecimento = DateTime.Parse("2021-5-14"),
                        Formacao = "Serviço Social",
                        Religiao = "Espírita",
                        Hobbies = "Artesanato, Dança",
                        MemorialPictureURL = "https://static.ndmais.com.br/2021/12/willsmith-web-1110x739-1.jpg",
                        Coletivo = "ONG Ajuda ao Próximo"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}

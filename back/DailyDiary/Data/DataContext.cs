using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DailyDiary.Model
{
    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Commentary> Commentaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users").HasKey(x => x.Id);
            modelBuilder.Entity<Post>().ToTable("Posts").HasKey(x => x.Id);
            modelBuilder.Entity<Commentary>().ToTable("Commentaries").HasKey(x => x.Id);
            modelBuilder.Entity<PostLike>().ToTable("PostLikes").HasKey(key => new { key.PostId, key.UserId });

            modelBuilder.Entity<Post>().HasOne(o => o.Owner)
                .WithMany(p => p.UsersPosts)
                .HasForeignKey(k => k.OwnerId);

            modelBuilder.Entity<Commentary>().HasOne(x => x.Post)
                .WithMany(x => x.Commentaries)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<User>().HasMany(x => x.UsersCommentaries)
                .WithOne(x => x.Owner)
                .HasForeignKey(x => x.OwnerId);
            

            modelBuilder.Entity<User>().Property(x => x.Id).HasIdentityOptions(11);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    Type = User.UserType.Public,
                    UserName = "jsmith",
                    Email = "jsmith@gmail.com",
                    Firstname = "James",
                    Lastname = "Smith",
                    BirthDate = new DateTime(1991, 01, 14, 10, 10, 49, 691),
                    PhoneNumber = "56781991",
                    Bio = "My name is James, not John Smith. I hate you.",
                    Adult = true,
                    AvatarLink = "https://i.imgur.com/B6ZumbE.jpeg"

                },
                new User
                {
                    Id = "2",
                    Type = User.UserType.Public,
                    UserName = "hsimmons",
                    Email = "hsimmons@yahoo.com",
                    Firstname = "Harold",
                    Lastname = "Simmons",
                    BirthDate = new DateTime(1993, 02, 14, 10, 10, 49, 691),
                    PhoneNumber = "56781993",
                    Bio = "I am 18 years and 120 months old.",
                    Adult = true,
                    AvatarLink = "https://i.imgur.com/DDKv7i6.jpeg"
                },
                new User
                {
                    Id = "3",
                    Type = User.UserType.Public,
                    UserName = "hbutler",
                    Email = "hbutler@facebook.com",
                    Firstname = "Hunter",
                    Lastname = "Butler",
                    BirthDate = new DateTime(1996, 03, 14, 10, 10, 49, 691),
                    PhoneNumber = "56781996",
                    Bio = "Stupid last name! As a matter of fact, first name as well.",
                    Adult = true,
                    AvatarLink = "https://i.imgur.com/sXZpDex.jpg"
                },
                new User
                {
                    Id = "4",
                    Type = User.UserType.Public,
                    UserName = "cmartinez",
                    Email = "cmartinez@outlook.com",
                    Firstname = "Carlos",
                    Lastname = "Martinez",
                    BirthDate = new DateTime(1999, 04, 14, 10, 10, 49, 691),
                    PhoneNumber = "56781999",
                    Bio = "Viva la fiesta! Aiaiaiaiaiai!",
                    Adult = true,
                    AvatarLink = "https://i.imgur.com/Afxs62A.jpg"
                },
                new User
                {
                    Id = "5",
                    Type = User.UserType.Public,
                    UserName = "devans",
                    Email = "devans@google.com",
                    Firstname = "Douglas",
                    Lastname = "Evans",
                    BirthDate = new DateTime(2002, 05, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782002",
                    Bio = "I work at Google. And you don't. Haha.",
                    Adult = true,
                    AvatarLink = "https://i.imgur.com/ZSwxzDu.jpg"
                },
                new User
                {
                    Id = "6",
                    Type = User.UserType.Public,
                    UserName = "jpeterson",
                    Email = "jpeterson@hotmail.com",
                    Firstname = "Joey",
                    Lastname = "Peterson",
                    BirthDate = new DateTime(2003, 06, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782003",
                    Bio = "I am actually 18 years old quite soon!",
                    Adult = false,
                    AvatarLink = "string"

                },
                new User
                {
                    Id = "7",
                    Type = User.UserType.Public,
                    UserName = "mmurray",
                    Email = "mmurray@gmail.com",
                    Firstname = "Martin",
                    Lastname = "Murray",
                    BirthDate = new DateTime(2004, 07, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782004",
                    Bio = "Bill Murray is my dad.",
                    Adult = false,
                    AvatarLink = "https://i.imgur.com/vszTsGu.jpg"

                },
                new User
                {
                    Id = "8",
                    Type = User.UserType.Public,
                    UserName = "ptucker",
                    Email = "ptucker@yahoo.com",
                    Firstname = "Paul",
                    Lastname = "Tucker",
                    BirthDate = new DateTime(2005, 08, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782005",
                    Bio = "Go TUCK yourself!",
                    Adult = false,
                    AvatarLink = "https://i.imgur.com/rfHHK6j.jpg"
                },
                new User
                {
                    Id = "9",
                    Type = User.UserType.Public,
                    UserName = "rjohnson",
                    Email = "rjohnson@outlook.com",
                    Firstname = "Robert",
                    Lastname = "Johnson",
                    BirthDate = new DateTime(2006, 09, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782006",
                    Bio = "I'm 15, and already famous!",
                    Adult = false,
                    AvatarLink = "https://i.imgur.com/Xn74svQ.jpg"

                },
                new User
                {
                    Id = "10",
                    Type = User.UserType.Private,
                    UserName = "mpower",
                    Email = "mpower@facebook.com",
                    Firstname = "Max",
                    Lastname = "Power",
                    BirthDate = new DateTime(2007, 10, 14, 10, 10, 49, 691),
                    PhoneNumber = "56782007",
                    Bio = "I GOT THE POWAAA!",
                    Adult = false,
                    AvatarLink = "https://i.imgur.com/2djgZPs.jpg"
                });


            modelBuilder.Entity<Post>().Property(x => x.Id).HasIdentityOptions(11);
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = 1,
                    Type = Post.PostType.Public,
                    Title = "Marvin Scott III died in Texas police custody",
                    Text =
                        "Marvin Scott III was arrested in March on a misdemeanor marijuana charge. When he was taken to the county jail, officers restrained him, blasted pepper spray and covered his head with a hood. He became unresponsive and was pronounced dead at a hospital. Though seven of the sheriff’s officers have been fired after initially being put on administrative leave and another resigned while under investigation, the family and protesters say they don’t plan to stop until the officers have been charged with a crime.",
                    ReleaseDate = new DateTime(2021, 04, 13, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey3dZFSXAAU8l2O?format=jpg&name=900x900",
                    OwnerId = "10",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 2,
                    Type = Post.PostType.Public,
                    Title = "Cariol Horne was fired for stopping a chokehold. She's been vindicated.",
                    Text =
                        "Cariol Horne forcibly removed a white officer and traded blows with him after he put a Black man, who was handcuffed, into a chokehold. 15 years later, a judge said her firing was wrong.",
                    ReleaseDate = new DateTime(2021, 04, 12, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey75YoWXIAQXomv?format=jpg&name=900x900",
                    OwnerId = "10",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 3,
                    Type = Post.PostType.Private,
                    Title = "Olivia's Rodrigo's Debut Album Sour Is the Breakup Soundtrack of 2021",
                    Text =
                        "After months of eagerly playing 'Drivers License' on repeat, we finally got a glimpse at the album artwork for Rodrigo's new album, and the vibe is definitely melted strawberry ice cream meets heartbreak.",
                    ReleaseDate = new DateTime(2021, 04, 11, 10, 10, 49, 681),
                    NSFW = true,
                    ImageLink = "https://pbs.twimg.com/media/Ey49CO2UYAUYAxg?format=jpg&name=900x900",
                    OwnerId = "8",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 4,
                    Type = Post.PostType.Public,
                    Title = "Secrecy and Abuse Claims Haunt China's Solar Factories in Xinjiang",
                    Text =
                        "The world's green power surge depends on polysilicon made in China's remote Northwest. No one really knows what is going on inside the facilities. In the Gobi Desert sit two factories that churn out vast quantities of polysilicon, the raw material in billions of solar panels worldwiRe.NSWFmost no one outside China knows what goes on inside these factories, or two others elsewhere in Xinjiang.",
                    ReleaseDate = new DateTime(2021, 04, 10, 10, 10, 49, 6),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/card_img/1381831922272280576/LVFKYGLq?format=jpg&name=small",
                    OwnerId = "7",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 5,
                    Type = Post.PostType.Public,
                    Title = "How could an officer mistake a gun for a Taser?",
                    Text =
                        "Tasers look and feel different from pistols, and most police forces have protocols to prevent a mix,uR. NSWF how could the officer who shot Daunte Wright in Minnesota on Sunday grab her gun instead of her Taser, as the police chief said?",
                    ReleaseDate = new DateTime(2021, 04, 08, 10, 10, 49, 6),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey3wUFUWUAMhegz?format=jpg&name=900x900",
                    OwnerId = "6",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 6,
                    Type = Post.PostType.Public,
                    Title = "The killing of Daunte Wright shows this American sickness doesn’t stop, columnist writes",
                    Text =
                        "There isn’t a single thing Black folks can do to solve white racism, but the onus is placed at our feet anyway, writes L.A. Times columnist LZ Granderson. In his latest column, Granderson looks at Daunte Wright's killing compared to how white suspects are treated by police.",
                    ReleaseDate = new DateTime(2021, 04, 09, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey5dlIWUUAAgVur?format=png&name=900x900",
                    OwnerId = "5",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 7,
                    Type = Post.PostType.Private,
                    Title = "The Soul of Bravo",
                    Text =
                        "A year of national reckonings on race and inequality has tested how real the Housewives should be, by writer Anna Peele for Vulture.",
                    ReleaseDate = new DateTime(2021, 04, 07, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey8MFFYXIAAi0Z8?format=jpg&name=900x900",
                    OwnerId = "4",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 8,
                    Type = Post.PostType.Public,
                    Title = "Mercedes-AMG PETRONAS F1 Team",
                    Text =
                        "The AMG High Performance Battery combines high power that can be called up frequently in succession with low weight to increase the overall performance of the vehicle!",
                    ReleaseDate = new DateTime(2021, 04, 06, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/media/Ey8bUlUXIAQzV_R?format=jpg&name=900x900",
                    OwnerId = "3",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 9,
                    Type = Post.PostType.Private,
                    Title = "Death of DMX",
                    Text =
                        "I’m devastated to wake up to the death of DMX. I grew up listening to him, his words and stories got me through some difficult times as a kid and I’m so grateful for his wisdom and light. Gone too soon but I hope on to a more peaceful place. Rest in peace DMX",
                    ReleaseDate = new DateTime(2021, 04, 05, 10, 10, 49, 681),
                    NSFW = true,
                    ImageLink = "https://pbs.twimg.com/media/Eyjbda4WgAEPKbo?format=jpg&name=900x900",
                    OwnerId = "2",
                    Likes = new List<User>()
                },
                new Post
                {
                    Id = 10,
                    Type = Post.PostType.Public,
                    Title = "United Nations",
                    Text =
                        "The UN works for everyone, everywhere by: preventing conflict, vaccinating children, protecting refugees, feeding the hungry, addressing the climate crisis, empowering women, responding to #COVID19 & so much more.",
                    ReleaseDate = new DateTime(2021, 04, 14, 10, 10, 49, 681),
                    NSFW = false,
                    ImageLink = "https://pbs.twimg.com/profile_banners/14159148/1607000252/1500x500",
                    OwnerId = "1",
                    Likes = new List<User>()
                });

            modelBuilder.Entity<Commentary>().Property(x => x.Id).HasIdentityOptions(4);
            modelBuilder.Entity<Commentary>().HasData(
                new Commentary
                {
                    Id = 1,
                    Text = "Test comment 1",
                    PostId = 1,
                    OwnerId = "1",
                    Likes = new List<User>()
                },
                new Commentary
                {
                    Id = 2,
                    Text = "Comment Test 2",
                    PostId = 1,
                    OwnerId = "2",
                    Likes = new List<User>()
                },
                new Commentary
                {
                    Id = 3,
                    Text = "Test comment 3",
                    PostId = 1,
                    OwnerId = "3",
                    Likes = new List<User>()
                });
            modelBuilder.Entity<PostLike>().HasData(new PostLike() {PostId = 1, UserId = "1"});



            modelBuilder.UseIdentityColumns();
        }
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizVistaApiInfrastructureLayer.Entities;

namespace QuizVistaApiInfrastructureLayer.DbContexts;

public partial class QuizVistaDbContext : DbContext
{
    public QuizVistaDbContext()
    {
    }

    public QuizVistaDbContext(DbContextOptions<QuizVistaDbContext> options)
        : base(options)
    {
    }

    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
    {
        return Set<TEntity>();
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Attempt> Attempts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("answer_PK");

            entity.ToTable("ANSWER");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnswerText)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("answer_text");
            entity.Property(e => e.AttemptId).HasColumnName("attempt_id");
            entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
            entity.Property(e => e.QuestionId).HasColumnName("QUESTION_id");

            entity.HasOne(d => d.Attempt).WithMany(p => p.Answers)
                .HasForeignKey(d => d.AttemptId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ANSWER_ATTEMPT_FK");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("ANSWER_QUESTION_FK");
        });

        modelBuilder.Entity<Attempt>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("attempt_PK");

            entity.ToTable("ATTEMPT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("create_date");
            entity.Property(e => e.EditionDate)
                .HasColumnType("datetime")
                .HasColumnName("edition_date");
            entity.Property(e => e.UsersId).HasColumnName("USERS_id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Attempt)
                .HasForeignKey<Attempt>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ATTEMPT_USERS_FK");

            entity.HasMany(d => d.AnswersNavigation).WithMany(p => p.Attempts)
                .UsingEntity<Dictionary<string, object>>(
                    "SavedAnswer",
                    r => r.HasOne<Answer>().WithMany()
                        .HasForeignKey("AnswerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SAVED_ANSWERS_ANSWER_FK"),
                    l => l.HasOne<Attempt>().WithMany()
                        .HasForeignKey("AttemptId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("SAVED_ANSWERS_ATTEMPT_FK"),
                    j =>
                    {
                        j.HasKey("AttemptId", "AnswerId").HasName("SAVED_ANSWERS_PK");
                        j.ToTable("SAVED_ANSWERS");
                        j.IndexerProperty<int>("AttemptId").HasColumnName("attempt_id");
                        j.IndexerProperty<int>("AnswerId").HasColumnName("answer_id");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_PK");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("question_PK");

            entity.ToTable("QUESTION");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalValue).HasColumnName("additional_value");
            entity.Property(e => e.CmsQuestionsStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_questions_style");
            entity.Property(e => e.CmsTitleStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_title_style");
            entity.Property(e => e.QuizId).HasColumnName("QUIZ_id");
            entity.Property(e => e.SubstractionalValue).HasColumnName("substractional_value");
            entity.Property(e => e.Text)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("text");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasComment("1 - one good question\r\n2 - true/false\r\n3 - multi")
                .HasColumnName("type");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("QUESTION_QUIZ_FK");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("quiz_PK");

            entity.ToTable("QUIZ");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_id");
            entity.Property(e => e.CmsTitleStyle)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cms_title_style");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.EditionDate)
                .HasColumnType("datetime")
                .HasColumnName("edition_date");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.QuizzesNavigation)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QUIZ_USERS_FK");

            entity.HasOne(d => d.Category).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("QUIZ_CATEGORY_FK");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.Name }).HasName("Role_PK");

            entity.ToTable("ROLE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tags_PK");

            entity.ToTable("TAGS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasMany(d => d.Quizzes).WithMany(p => p.Tags)
                .UsingEntity<Dictionary<string, object>>(
                    "TagsQuiz",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Relation_12_quiz_FK"),
                    l => l.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Relation_12_tags_FK"),
                    j =>
                    {
                        j.HasKey("TagsId", "QuizId").HasName("tags_quiz_PK");
                        j.ToTable("TAGS_QUIZ");
                        j.IndexerProperty<int>("TagsId").HasColumnName("tags_id");
                        j.IndexerProperty<int>("QuizId").HasColumnName("quiz_id");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_PK");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.Email, "User_email_UN").IsUnique();

            entity.HasIndex(e => e.UserName, "User_user_name_UN").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("password_hash");
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasMany(d => d.Quizzes).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AccessQuiz",
                    r => r.HasOne<Quiz>().WithMany()
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("access_quiz_quiz_FK"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("access_quiz_User_FK"),
                    j =>
                    {
                        j.HasKey("UserId", "QuizId").HasName("access_quiz_PK");
                        j.ToTable("ACCESS_QUIZ");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("QuizId").HasColumnName("quiz_id");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId", "RoleName")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_role_Role_FK"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("user_role_User_FK"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId", "RoleName").HasName("user_role_PK");
                        j.ToTable("USER_ROLE");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                        j.IndexerProperty<string>("RoleName")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("Role_name");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

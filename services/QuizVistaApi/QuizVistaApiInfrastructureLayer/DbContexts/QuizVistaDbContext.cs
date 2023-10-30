using Microsoft.EntityFrameworkCore;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace QuizVistaApiInfrastructureLayer.DbContexts
{
    public class QuizVistaDbContext : DbContext
    {

        public QuizVistaDbContext()
        {
                
        }
        public QuizVistaDbContext([NotNull] DbContextOptions<QuizVistaDbContext> options)
        : base(options)
        {
        }

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public virtual DbSet<Answer> Answers { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Question> Questions { get; set; }

        public virtual DbSet<Quiz> Quizzes { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.HasKey(e => e.AnswerId).HasName("answer_PK");

                entity.ToTable("answer");

                entity.Property(e => e.AnswerId).HasColumnName("answer_id");
                entity.Property(e => e.AnswerText)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("answer_text");
                entity.Property(e => e.IsCorrect).HasColumnName("is_correct");
                entity.Property(e => e.QuestionQuestionId).HasColumnName("question_question_id");

                entity.HasOne(d => d.QuestionQuestion).WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionQuestionId)
                    .HasConstraintName("answer_question_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("category_PK");

                entity.ToTable("category");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");
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
                entity.HasKey(e => e.QuestionId).HasName("question_PK");

                entity.ToTable("question");

                entity.Property(e => e.QuestionId).HasColumnName("question_id");
                entity.Property(e => e.AdditionalValue).HasColumnName("additional_value");
                entity.Property(e => e.CmsQuestionsStyle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cms_questions_style");
                entity.Property(e => e.CmsTitleStyle)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("cms_title_style");
                entity.Property(e => e.QuizQuizId).HasColumnName("quiz_quiz_id");
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

                entity.HasOne(d => d.QuizQuiz).WithMany(p => p.Questions)
                    .HasForeignKey(d => d.QuizQuizId)
                    .HasConstraintName("question_quiz_FK");
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.QuizId).HasName("quiz_PK");

                entity.ToTable("quiz");

                entity.Property(e => e.QuizId).HasColumnName("quiz_id");
                entity.Property(e => e.Author)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("author");
                entity.Property(e => e.CategoryCategoryId).HasColumnName("category_category_id");
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

                entity.HasOne(d => d.CategoryCategory).WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.CategoryCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("quiz_category_FK");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("tags_PK");

                entity.ToTable("tags");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasMany(d => d.QuizQuizzes).WithMany(p => p.Tags)
                    .UsingEntity<Dictionary<string, object>>(
                        "TagsQuiz",
                        r => r.HasOne<Quiz>().WithMany()
                            .HasForeignKey("QuizQuizId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("tags_quiz_quiz_FK"),
                        l => l.HasOne<Tag>().WithMany()
                            .HasForeignKey("TagsId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("tags_quiz_tags_FK"),
                        j =>
                        {
                            j.HasKey("TagsId", "QuizQuizId").HasName("tags_quiz_PK");
                            j.ToTable("tags_quiz");
                            j.IndexerProperty<int>("TagsId").HasColumnName("tags_id");
                            j.IndexerProperty<int>("QuizQuizId").HasColumnName("quiz_quiz_id");
                        });
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}

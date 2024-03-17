using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EnglishLevelAssessment.Data.Models;

public partial class EnglishLevelAssessmentContext : DbContext
{
    public EnglishLevelAssessmentContext()
    {
    }

    public EnglishLevelAssessmentContext(DbContextOptions<EnglishLevelAssessmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicYear> AcademicYears { get; set; }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<LanguageLevel> LanguageLevels { get; set; }

    public virtual DbSet<MaturaGrade> MaturaGrades { get; set; }

    public virtual DbSet<MaturaLevel> MaturaLevels { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<StudyProgramme> StudyProgrammes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:EnglishLevelAssessment");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.ToTable("AcademicYear");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Year).HasMaxLength(20);
        });

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(500);

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<LanguageLevel>(entity =>
        {
            entity.ToTable("LanguageLevel");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Level).HasMaxLength(10);
        });

        modelBuilder.Entity<MaturaGrade>(entity =>
        {
            entity.ToTable("MaturaGrade");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Grade).HasMaxLength(500);
        });

        modelBuilder.Entity<MaturaLevel>(entity =>
        {
            entity.ToTable("MaturaLevel");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaturaLevel1)
                .HasMaxLength(500)
                .HasColumnName("MaturaLevel");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Text).HasMaxLength(500);

            entity.HasOne(d => d.LanguageLevel).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LanguageLevelId)
                .HasConstraintName("FK_Question_LanguageLevel");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.ToTable("Result");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.AcademicYear).WithMany(p => p.Results)
                .HasForeignKey(d => d.AcademicYearId)
                .HasConstraintName("FK_Result_AcademicYear");

            entity.HasOne(d => d.LanguageLevel).WithMany(p => p.Results)
                .HasForeignKey(d => d.LanguageLevelId)
                .HasConstraintName("FK_Result_LanguageLevel");

            entity.HasOne(d => d.MaturaGrade).WithMany(p => p.Results)
                .HasForeignKey(d => d.MaturaGradeId)
                .HasConstraintName("FK_Result_MaturaGrade");

            entity.HasOne(d => d.MaturaLevel).WithMany(p => p.Results)
                .HasForeignKey(d => d.MaturaLevelId)
                .HasConstraintName("FK_Result_MaturaLevel");

            entity.HasOne(d => d.StudyProgramme).WithMany(p => p.Results)
                .HasForeignKey(d => d.StudyProgrammeId)
                .HasConstraintName("FK_Result_StudyProgramme");
        });

        modelBuilder.Entity<StudyProgramme>(entity =>
        {
            entity.ToTable("StudyProgramme");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Programme).HasMaxLength(500);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

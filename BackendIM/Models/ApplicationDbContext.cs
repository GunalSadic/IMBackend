using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BackendIM.Models;

public partial class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversation> Conversations { get; set; }

    public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ScheduledMessage> ScheduledMessages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VoiceRecording> VoiceRecordings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SQL6028.site4now.net; Database=db_ab033d_instantmessaging; User Id=db_ab033d_instantmessaging_admin; Password=IMPassword1!; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PK__Conversa__C050D87719602153");

            entity.ToTable("Conversation");
        });

        modelBuilder.Entity<ConversationParticipant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("PK__Conversa__7227995EC8C49258");

            entity.ToTable("ConversationParticipant");

            entity.HasOne(d => d.Conversation).WithMany(p => p.ConversationParticipants)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversationParticipants_Conversation");

            entity.HasOne(d => d.User).WithMany(p => p.ConversationParticipants)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConversationParticipants_User");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0FB65C6355");

            entity.Property(e => e.Document1).HasColumnName("Document");
            entity.Property(e => e.DocumentType).HasMaxLength(50);

            entity.HasOne(d => d.Message).WithMany(p => p.Documents)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documents_Message");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Images__7516F70CCC0FC1DD");

            entity.Property(e => e.Image1).HasColumnName("Image");
            entity.Property(e => e.ImageType).HasMaxLength(50);

            entity.HasOne(d => d.Message).WithMany(p => p.Images)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_Message");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Message__C87C0C9CA2F9247B");

            entity.ToTable("Message");

            entity.Property(e => e.EmbeddedResourceType).HasMaxLength(50);
            entity.Property(e => e.SentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Sender");

            entity.HasOne(e=>e.Conversation).WithMany(e=>e.Messages)
            .HasForeignKey(d => d.ConversationId)
             .HasConstraintName("FK_Messages_Conversation");
        });

        modelBuilder.Entity<ScheduledMessage>(entity =>
        {
            entity.HasKey(e => e.ScheduledMessageId).HasName("PK__Schedule__4813278560E38642");

            entity.HasIndex(e => e.MessageId, "UQ_ScheduledMessages_Message").IsUnique();

            entity.Property(e => e.ScheduledDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Message).WithOne(p => p.ScheduledMessage)
                .HasForeignKey<ScheduledMessage>(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ScheduledMessages_Message");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C9469D0BD");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ__User__A9D105347586FE7D").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__Videos__BAE5126AD1EA3E3B");

            entity.Property(e => e.Video1).HasColumnName("Video");
            entity.Property(e => e.VideoType).HasMaxLength(50);

            entity.HasOne(d => d.Message).WithMany(p => p.Videos)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_Message");
        });

        modelBuilder.Entity<VoiceRecording>(entity =>
        {
            entity.HasKey(e => e.VoiceRecordingId).HasName("PK__VoiceRec__BBDF7EF6263BEFEB");

            entity.Property(e => e.AudioType).HasMaxLength(50);

            entity.HasOne(d => d.Message).WithMany(p => p.VoiceRecordings)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VoiceRecordings_Message");
        });

        OnModelCreatingPartial(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

﻿// <auto-generated />
using System;
using BackendIM.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackendIM.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241215100800_FixGroupName")]
    partial class FixGroupName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackendIM.Models.Conversation", b =>
                {
                    b.Property<Guid>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupPicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsGroupChat")
                        .HasColumnType("bit");

                    b.HasKey("ConversationId")
                        .HasName("PK__Conversa__C050D87719602153");

                    b.ToTable("Conversation", (string)null);
                });

            modelBuilder.Entity("BackendIM.Models.ConversationParticipant", b =>
                {
                    b.Property<Guid>("ParticipantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ParticipantId")
                        .HasName("PK__Conversa__7227995EC8C49258");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("ConversationParticipant", (string)null);
                });

            modelBuilder.Entity("BackendIM.Models.Document", b =>
                {
                    b.Property<Guid>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Document1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Document");

                    b.Property<long>("DocumentSize")
                        .HasColumnType("bigint");

                    b.Property<string>("DocumentType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DocumentId")
                        .HasName("PK__Document__1ABEEF0FB65C6355");

                    b.HasIndex("MessageId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("BackendIM.Models.Image", b =>
                {
                    b.Property<Guid>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Image1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Image");

                    b.Property<long>("ImageSize")
                        .HasColumnType("bigint");

                    b.Property<string>("ImageType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ImageId")
                        .HasName("PK__Images__7516F70CCC0FC1DD");

                    b.HasIndex("MessageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BackendIM.Models.Message", b =>
                {
                    b.Property<Guid>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConversationId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmbeddedResourceType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsEdited")
                        .HasColumnType("bit");

                    b.Property<bool>("IsScheduled")
                        .HasColumnType("bit");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("SentTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("MessageId")
                        .HasName("PK__Message__C87C0C9CA2F9247B");

                    b.HasIndex("ConversationId");

                    b.HasIndex("ConversationId1")
                        .IsUnique()
                        .HasFilter("[ConversationId1] IS NOT NULL");

                    b.HasIndex("SenderId");

                    b.HasIndex("UserId");

                    b.ToTable("Message", (string)null);
                });

            modelBuilder.Entity("BackendIM.Models.ScheduledMessage", b =>
                {
                    b.Property<Guid>("ScheduledMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ScheduledDateTime")
                        .HasColumnType("datetime");

                    b.HasKey("ScheduledMessageId")
                        .HasName("PK__Schedule__4813278560E38642");

                    b.HasIndex(new[] { "MessageId" }, "UQ_ScheduledMessages_Message")
                        .IsUnique();

                    b.ToTable("ScheduledMessages");
                });

            modelBuilder.Entity("BackendIM.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex(new[] { "Email" }, "UQ__User__A9D105347586FE7D")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BackendIM.Models.Video", b =>
                {
                    b.Property<Guid>("VideoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Video1")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("Video");

                    b.Property<long>("VideoSize")
                        .HasColumnType("bigint");

                    b.Property<string>("VideoType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("VideoId")
                        .HasName("PK__Videos__BAE5126AD1EA3E3B");

                    b.HasIndex("MessageId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("BackendIM.Models.VoiceRecording", b =>
                {
                    b.Property<Guid>("VoiceRecordingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Audio")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("AudioSize")
                        .HasColumnType("bigint");

                    b.Property<string>("AudioType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("VoiceRecordingId")
                        .HasName("PK__VoiceRec__BBDF7EF6263BEFEB");

                    b.HasIndex("MessageId");

                    b.ToTable("VoiceRecordings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BackendIM.Models.ConversationParticipant", b =>
                {
                    b.HasOne("BackendIM.Models.Conversation", "Conversation")
                        .WithMany("ConversationParticipants")
                        .HasForeignKey("ConversationId")
                        .IsRequired()
                        .HasConstraintName("FK_ConversationParticipants_Conversation");

                    b.HasOne("BackendIM.Models.User", "User")
                        .WithMany("ConversationParticipants")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_ConversationParticipants_User");

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BackendIM.Models.Document", b =>
                {
                    b.HasOne("BackendIM.Models.Message", "Message")
                        .WithMany("Documents")
                        .HasForeignKey("MessageId")
                        .IsRequired()
                        .HasConstraintName("FK_Documents_Message");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("BackendIM.Models.Image", b =>
                {
                    b.HasOne("BackendIM.Models.Message", "Message")
                        .WithMany("Images")
                        .HasForeignKey("MessageId")
                        .IsRequired()
                        .HasConstraintName("FK_Images_Message");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("BackendIM.Models.Message", b =>
                {
                    b.HasOne("BackendIM.Models.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Messages_Conversation");

                    b.HasOne("BackendIM.Models.Conversation", null)
                        .WithOne("LastSentMessage")
                        .HasForeignKey("BackendIM.Models.Message", "ConversationId1");

                    b.HasOne("BackendIM.Models.User", "Sender")
                        .WithMany("MessageSenders")
                        .HasForeignKey("SenderId")
                        .IsRequired()
                        .HasConstraintName("FK_Messages_Sender");

                    b.HasOne("BackendIM.Models.User", null)
                        .WithMany("MessageReceivers")
                        .HasForeignKey("UserId");

                    b.Navigation("Conversation");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("BackendIM.Models.ScheduledMessage", b =>
                {
                    b.HasOne("BackendIM.Models.Message", "Message")
                        .WithOne("ScheduledMessage")
                        .HasForeignKey("BackendIM.Models.ScheduledMessage", "MessageId")
                        .IsRequired()
                        .HasConstraintName("FK_ScheduledMessages_Message");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("BackendIM.Models.Video", b =>
                {
                    b.HasOne("BackendIM.Models.Message", "Message")
                        .WithMany("Videos")
                        .HasForeignKey("MessageId")
                        .IsRequired()
                        .HasConstraintName("FK_Videos_Message");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("BackendIM.Models.VoiceRecording", b =>
                {
                    b.HasOne("BackendIM.Models.Message", "Message")
                        .WithMany("VoiceRecordings")
                        .HasForeignKey("MessageId")
                        .IsRequired()
                        .HasConstraintName("FK_VoiceRecordings_Message");

                    b.Navigation("Message");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BackendIM.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BackendIM.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackendIM.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BackendIM.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackendIM.Models.Conversation", b =>
                {
                    b.Navigation("ConversationParticipants");

                    b.Navigation("LastSentMessage");

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("BackendIM.Models.Message", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("Images");

                    b.Navigation("ScheduledMessage");

                    b.Navigation("Videos");

                    b.Navigation("VoiceRecordings");
                });

            modelBuilder.Entity("BackendIM.Models.User", b =>
                {
                    b.Navigation("ConversationParticipants");

                    b.Navigation("MessageReceivers");

                    b.Navigation("MessageSenders");
                });
#pragma warning restore 612, 618
        }
    }
}

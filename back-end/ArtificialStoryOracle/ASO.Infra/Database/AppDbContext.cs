﻿﻿using ASO.Domain.AI.Entities;
using ASO.Domain.Game.Entities;
using ASO.Domain.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASO.Infra.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Ancestry> Ancestries { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<Skill> Skill { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<Image> Images { get; set; } = null!;
    public DbSet<GeneratedAIContent> GeneratedAIContents { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<PlayerUser> PlayerUsers { get; set; } = null!;
    public DbSet<Friendship> Friendships { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
}